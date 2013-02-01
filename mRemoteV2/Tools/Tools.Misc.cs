using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;

//using mRemoteNC.Runtime;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.App.Info;
using My;
using Settings = My.Settings;

namespace mRemoteNC.Tools
{
    public class Misc
    {
        private struct SHFILEINFO
        {
            public IntPtr hIcon; // : icon
            public int iIcon; // : icondex
            public int dwAttributes; // : SFGAO_ flags
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi,
                                                   int cbFileInfo, int uFlags);

        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        //Private Const SHGFI_LARGEICON = &H0    ' Large icon

        public static Icon GetIconFromFile(string FileName)
        {
            try
            {
                if (File.Exists(FileName) == false)
                {
                    return null;
                }

                IntPtr hImgSmall; //The handle to the system image list.
                //Dim hImgLarge As IntPtr  'The handle to the system image list.
                SHFILEINFO shinfo;
                shinfo = new SHFILEINFO();

                shinfo.szDisplayName = new string('\0', 260);
                shinfo.szTypeName = new string('\0', 80);

                //Use this to get the small icon.
                hImgSmall = SHGetFileInfo(FileName, 0, ref shinfo, Marshal.SizeOf(shinfo),
                                          System.Convert.ToInt32(SHGFI_ICON | SHGFI_SMALLICON));

                //Use this to get the large icon.
                //hImgLarge = SHGetFileInfo(fName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);

                //The icon is returned in the hIcon member of the
                //shinfo struct.
                System.Drawing.Icon myIcon;
                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);

                return myIcon;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("GetIconFromFile failed (Tools.Misc)" + Constants.vbNewLine +
                                                     ex.Message), true);
                return null;
            }
        }

        public delegate void SQLUpdateCheckFinishedEventHandler(bool UpdateAvailable);

        private static SQLUpdateCheckFinishedEventHandler SQLUpdateCheckFinishedEvent;

        public static event SQLUpdateCheckFinishedEventHandler SQLUpdateCheckFinished
        {
            add
            {
                SQLUpdateCheckFinishedEvent =
                    (SQLUpdateCheckFinishedEventHandler)System.Delegate.Combine(SQLUpdateCheckFinishedEvent, value);
            }
            remove
            {
                SQLUpdateCheckFinishedEvent =
                    (SQLUpdateCheckFinishedEventHandler)System.Delegate.Remove(SQLUpdateCheckFinishedEvent, value);
            }
        }

        public static void IsSQLUpdateAvailableBG()
        {
            System.Threading.Thread t = new System.Threading.Thread(() => IsSQLUpdateAvailable());
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
        }

        public static bool IsSQLUpdateAvailable()
        {
            try
            {
                SqlConnection sqlCon;
                SqlCommand sqlQuery;
                SqlDataReader sqlRd;

                DateTime LastUpdateInDB;

                if (Settings.Default.SQLUser != "")
                {
                    sqlCon =
                        new SqlConnection(
                            (string)
                            ("Data Source=" + Settings.Default.SQLHost + ";Initial Catalog=" +
                             Settings.Default.SQLDatabaseName + ";User Id=" + Settings.Default.SQLUser + ";Password=" +
                             Security.Crypt.Decrypt((string)Settings.Default.SQLPass, (string)General.EncryptionKey)));
                }
                else
                {
                    sqlCon =
                        new SqlConnection("Data Source=" + Settings.Default.SQLHost + ";Initial Catalog=" +
                                          Settings.Default.SQLDatabaseName + ";Integrated Security=True");
                }

                sqlCon.Open();

                sqlQuery = new SqlCommand("SELECT * FROM tblUpdate", sqlCon);
                sqlRd = sqlQuery.ExecuteReader(CommandBehavior.CloseConnection);

                sqlRd.Read();

                if (sqlRd.HasRows)
                {
                    LastUpdateInDB = Convert.ToDateTime(sqlRd["LastUpdate"]);

                    if (LastUpdateInDB > Runtime.LastSqlUpdate)
                    {
                        if (SQLUpdateCheckFinishedEvent != null)
                            SQLUpdateCheckFinishedEvent(true);
                        return true;
                    }
                }

                if (SQLUpdateCheckFinishedEvent != null)
                    SQLUpdateCheckFinishedEvent(false);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("IsSQLUpdateAvailable failed (Tools.Misc)" + Constants.vbNewLine +
                                                     ex.Message), true);
            }

            return false;
        }

        public static string PasswordDialog(bool Verify = true)
        {
            frmPassword nPwFrm = new frmPassword();

            nPwFrm.Verify = Verify;

            if (nPwFrm.ShowDialog() == DialogResult.OK)
            {
                return nPwFrm.Password;
            }
            else
            {
                return "";
            }
        }

        private static ArrayList rndNums = new ArrayList();

        public static string CreateConstantID()
        {
            string cID;
            Random rnd = new Random(DateTime.Now.Second);
            int iRnd = 0;
            bool NewFound = false;

            while (!NewFound)
            {
                iRnd = rnd.Next(10000, 99999);

                if (rndNums.Contains(iRnd) == false)
                {
                    rndNums.Add(iRnd);
                    NewFound = true;
                }
            }

            cID =
                System.Convert.ToString(DateTime.Now.Year + LeadingZero(DateTime.Now.Month.ToString()) +
                                        LeadingZero(DateTime.Now.Day.ToString()) +
                                        LeadingZero(DateTime.Now.Hour.ToString()) +
                                        LeadingZero(DateTime.Now.Minute.ToString()) +
                                        LeadingZero(DateTime.Now.Second.ToString()) +
                                        LeadingZero(System.Convert.ToString(DateTime.Now.Millisecond + iRnd.ToString())));

            return cID;
        }

        public static string LeadingZero(string Number)
        {
            if (Convert.ToInt32(Number) < 10)
            {
                return "0" + Number;
            }
            else
            {
                return Number;
            }
        }

        public static string DBDate(DateTime Dt)
        {
            string strDate;

            strDate =
                System.Convert.ToString(Dt.Year + LeadingZero(Dt.Month.ToString()) + LeadingZero(Dt.Day.ToString()) +
                                        " " + LeadingZero(Dt.Hour.ToString()) + ":" + LeadingZero(Dt.Minute.ToString()) +
                                        ":" + LeadingZero(Dt.Second.ToString()));

            return strDate;
        }

        public static string PrepareForDB(string Text)
        {
            Text = Strings.Replace(Text, "\'True\'", "1", 1, -1, CompareMethod.Text);
            Text = Strings.Replace(Text, "\'False\'", "0", 1, -1, CompareMethod.Text);

            return Text;
        }

        public static string PrepareValueForDB(string Text)
        {
            Text = Strings.Replace(Text, "\'", "\'\'", 1, -1, CompareMethod.Text);

            return Text;
        }

        public static object StringToEnum(Type t, string value)
        {
            foreach (FieldInfo fI in t.GetFields())
            {
                if (fI.Name == value)
                {
                    return fI.GetValue(Constants.vbNull);
                }
            }

            Exception ex = new Exception(string.Format("Can\'t convert {0} to {1}", value, t.ToString()));
            Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                (string)("StringToEnum failed" + Constants.vbNewLine + ex.Message),
                                                true);
            throw (ex);
        }

        public static Image TakeScreenshot(UI.Window.Connection sender)
        {
            try
            {
                int LeftStart =
                    System.Convert.ToInt32(
                        sender.TabController.SelectedTab.PointToScreen(new Point(sender.TabController.SelectedTab.Left))
                            .X); //Me.Left + Splitter.SplitterDistance + 11
                int TopStart =
                    System.Convert.ToInt32(
                        sender.TabController.SelectedTab.PointToScreen(new Point(sender.TabController.SelectedTab.Top)).
                            Y); //Me.Top + Splitter.Top + TabController.Top + TabController.SelectedTab.Top * 2 - 3
                int LeftWidth = System.Convert.ToInt32(sender.TabController.SelectedTab.Width);
                //Me.Width - (Splitter.SplitterDistance + 16)
                int TopHeight = System.Convert.ToInt32(sender.TabController.SelectedTab.Height);
                //Me.Height - (Splitter.Top + TabController.Top + TabController.SelectedTab.Top * 2 + 2)

                Size currentFormSize = new Size(LeftWidth, TopHeight);
                Bitmap ScreenToBitmap = new Bitmap(LeftWidth, TopHeight);
                System.Drawing.Graphics gGraphics = System.Drawing.Graphics.FromImage(ScreenToBitmap);

                gGraphics.CopyFromScreen(new Point(LeftStart, TopStart), new Point(0, 0), currentFormSize);

                return ScreenToBitmap;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("Taking Screenshot failed" + Constants.vbNewLine + ex.Message),
                                                    true);
            }

            return null;
        }

        public class EnumTypeConverter : EnumConverter
        {
            private System.Type _enumType;

            public EnumTypeConverter(System.Type type)
                : base(type)
            {
                _enumType = type;
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destType)
            {
                return destType == typeof(string);
            }

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
                                             object value, System.Type destType)
            {
                FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, value));
                DescriptionAttribute dna =
                    (DescriptionAttribute)(Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)));

                if (dna != null)
                {
                    return dna.Description;
                }
                else
                {
                    return value.ToString();
                }
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type srcType)
            {
                return srcType == typeof(string);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
                                               object value)
            {
                foreach (FieldInfo fi in _enumType.GetFields())
                {
                    DescriptionAttribute dna =
                        (DescriptionAttribute)(Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)));

                    if ((dna != null) && (((string)value) == dna.Description))
                    {
                        return Enum.Parse(_enumType, fi.Name);
                    }
                }

                return Enum.Parse(_enumType, (string)value);
            }
        }

        public class YesNoTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return true;
                }

                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
                                               object value)
            {
                if (value.GetType() == typeof(string))
                {
                    if (value.ToString().ToLower() == Language.strYes.ToLower())
                    {
                        return true;
                    }

                    if (value.ToString().ToLower() == Language.strNo.ToLower())
                    {
                        return false;
                    }

                    throw (new Exception("Values must be \"Yes\" or \"No\""));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
                                             object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return ((System.Convert.ToBoolean(value)) ? Language.strYes : Language.strNo);
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                bool[] bools = new bool[] { true, false };

                System.ComponentModel.TypeConverter.StandardValuesCollection svc =
                    new System.ComponentModel.TypeConverter.StandardValuesCollection(bools);

                return svc;
            }
        }

        public class Fullscreen
        {
            private static FormWindowState winState;
            private static FormBorderStyle brdStyle;
            private static bool topMost;
            private static Rectangle bounds;

            public static Form targetForm = frmMain.defaultInstance;
            public static bool FullscreenActive = false;

            public static void EnterFullscreen()
            {
                try
                {
                    if (!FullscreenActive)
                    {
                        FullscreenActive = true;
                        Save();
                        targetForm.WindowState = FormWindowState.Maximized;
                        targetForm.FormBorderStyle = FormBorderStyle.None;
                        SetWinFullScreen(targetForm.Handle);
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Entering Fullscreen failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            public static void Save()
            {
                winState = targetForm.WindowState;
                brdStyle = targetForm.FormBorderStyle;
                bounds = targetForm.Bounds;
            }

            public static void ExitFullscreen()
            {
                try
                {
                    targetForm.WindowState = winState;
                    targetForm.FormBorderStyle = brdStyle;
                    targetForm.Bounds = bounds;
                    FullscreenActive = false;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Exiting Fullscreen failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
            public static extern int GetSystemMetrics(int which);

            [DllImport("user32.dll")]
            public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width,
                                                   int height, UInt32 flags);

            private static IntPtr HWND_TOP = IntPtr.Zero;
            private const int SWP_SHOWWINDOW = 64;
            // 0Ч0040

            public static void SetWinFullScreen(IntPtr hwnd)
            {
                try
                {
                    Screen curScreen = Screen.FromHandle(targetForm.Handle);
                    SetWindowPos(hwnd, HWND_TOP, curScreen.Bounds.Left, curScreen.Bounds.Top, curScreen.Bounds.Right,
                                 curScreen.Bounds.Bottom, SWP_SHOWWINDOW);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("SetWindowPos failed" + Constants.vbNewLine + ex.Message), true);
                }
            }
        }

        //
        //* Arguments class: application arguments interpreter
        //*
        //* Authors:		R. LOPES
        //* Contributors:	R. LOPES
        //* Created:		25 October 2002
        //* Modified:		28 October 2002
        //*
        //* Version:		1.0
        //
        public class CMDArguments
        {
            private StringDictionary Parameters;

            // Retrieve a parameter value if it exists
            public string this[string Param]
            {
                get { return (Parameters[Param]); }
            }

            public CMDArguments(string[] Args)
            {
                Parameters = new StringDictionary();
                Regex Spliter = new Regex("^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex Remover = new Regex("^[\'\"]?(.*?)[\'\"]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                string Parameter = null;
                string[] Parts;

                // Valid parameters forms:
                // {-,/,--}param{ ,=,:}((",')value(",'))
                // Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'

                try
                {
                    foreach (string Txt in Args)
                    {
                        // Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
                        Parts = Spliter.Split(Txt, 3);
                        switch (Parts.Length)
                        {
                            case 1:
                                // Found a value (for the last parameter found (space separator))
                                if (Parameter != null)
                                {
                                    if (!Parameters.ContainsKey(Parameter))
                                    {
                                        Parts[0] = Remover.Replace(Parts[0], "$1");
                                        Parameters.Add(Parameter, Parts[0]);
                                    }
                                    Parameter = null;
                                }
                                // else Error: no parameter waiting for a value (skipped)
                                break;
                            case 2:
                                // Found just a parameter
                                // The last parameter is still waiting. With no value, set it to true.
                                if (Parameter != null)
                                {
                                    if (!Parameters.ContainsKey(Parameter))
                                    {
                                        Parameters.Add(Parameter, "true");
                                    }
                                }
                                Parameter = Parts[1];
                                break;
                            case 3:
                                // Parameter with enclosed value
                                // The last parameter is still waiting. With no value, set it to true.
                                if (Parameter != null)
                                {
                                    if (!Parameters.ContainsKey(Parameter))
                                    {
                                        Parameters.Add(Parameter, "true");
                                    }
                                }
                                Parameter = Parts[1];
                                // Remove possible enclosing characters (",')
                                if (!Parameters.ContainsKey(Parameter))
                                {
                                    Parts[2] = Remover.Replace(Parts[2], "$1");
                                    Parameters.Add(Parameter, Parts[2]);
                                }
                                Parameter = null;
                                break;
                        }
                    }
                    // In case a parameter is still waiting
                    if (Parameter != null)
                    {
                        if (!Parameters.ContainsKey(Parameter))
                        {
                            Parameters.Add(Parameter, "true");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Creating new Args failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }
        }
    }
}