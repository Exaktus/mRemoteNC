using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Tools;
using mRemoteNC.App;
using My;
using mRemoteNC.Messages;

namespace mRemoteNC
{
    namespace Connection
    {
        public class PuttyBase : Base
        {
            #region Constants

            private const int IDM_RECONF = 0x50; // PuTTY Settings Menu ID

            #endregion Constants

            #region Private Properties

            private bool _isPuttyNg;

            #endregion Private Properties

            #region Public Properties

            private Putty_Protocol _PuttyProtocol;

            public Putty_Protocol PuttyProtocol
            {
                get { return this._PuttyProtocol; }
                set { this._PuttyProtocol = value; }
            }

            private Putty_SSHVersion _PuttySSHVersion;

            public Putty_SSHVersion PuttySSHVersion
            {
                get { return this._PuttySSHVersion; }
                set { this._PuttySSHVersion = value; }
            }

            public IntPtr PuttyHandle { get; set; }

            private Process _PuttyProcess;

            public Process PuttyProcess
            {
                get { return this._PuttyProcess; }
                set { this._PuttyProcess = value; }
            }

            private static string _PuttyPath;

            public static string PuttyPath
            {
                get { return _PuttyPath; }
                set { _PuttyPath = value; }
            }

            //Private borderWidth As Integer = frmMain.Size.Width - frmMain.ClientSize.Width
            //Private borderHeight As Integer = frmMain.Size.Height - frmMain.ClientSize.Height
            private static Size _BorderSize;

            public static Size BorderSize
            {
                get { return _BorderSize; }
                set { _BorderSize = value; }
            }

            public bool Focused
            {
                get
                {
                    return Native.GetForegroundWindow() == PuttyHandle;
                }
            }

            #endregion Public Properties

            #region Private Events & Handlers

            private void ProcessExited(object sender, System.EventArgs e)
            {
                base.Event_Closed(this);
            }

            #endregion Private Events & Handlers

            #region Public Methods

            public PuttyBase()
            {
            }

            // Due to the way PuTTY handles command line arguments, backslashes followed by a quotation mark will be removed.
            // Since all the strings we send to PuTTY are surrounded by quotation marks, we need to escape any trailing
            // backslashes by adding another. See split_into_argv() in WINDOWS\WINUTILS.C of the PuTTY source for more info.
            private static string PuttyEscapeArgument(string argument)
            {
                if (argument.EndsWith("\\"))
                    argument = argument + "\\";
                return argument;
            }


            public override bool Connect()
            {
                try
                {
                    _isPuttyNg = IsFilePuttyNg(PuttyPath);

                    PuttyProcess = new Process();
                    var _with1 = PuttyProcess.StartInfo;
                    _with1.UseShellExecute = false;
                    _with1.FileName = _PuttyPath;

                    CommandLineArguments arguments = new CommandLineArguments();
                    arguments.EscapeForShell = false;

                    arguments.Add("-load", InterfaceControl.Info.PuttySession);
                    arguments.Add("-" + _PuttyProtocol.ToString());

                    if (_PuttyProtocol == Putty_Protocol.ssh)
                    {
                        string username = "";
                        string password = "";

                        if (!string.IsNullOrEmpty(InterfaceControl.Info.Username))
                        {
                            username = InterfaceControl.Info.Username;
                        }
                        else
                        {
                            if (Settings.Default.EmptyCredentials == "windows")
                            {
                                username = Environment.UserName;
                            }
                            else if (Settings.Default.EmptyCredentials == "custom")
                            {
                                username = Settings.Default.DefaultUsername;
                            }
                        }

                        if (!string.IsNullOrEmpty(InterfaceControl.Info.Password))
                        {
                            password = InterfaceControl.Info.Password;
                        }
                        else
                        {
                            if (Settings.Default.EmptyCredentials == "custom")
                            {
                                password = Security.Crypt.Decrypt(Settings.Default.DefaultPassword, AppInfo.General.EncryptionKey);
                            }
                        }

                        arguments.Add("-" + (int)_PuttySSHVersion);
                        if (!string.IsNullOrEmpty(username))
                        {
                            arguments.Add("-l", username);
                        }
                        if (!string.IsNullOrEmpty(password))
                        {
                            arguments.Add("-pw", password);
                        }
                    }

                    arguments.Add("-P", InterfaceControl.Info.Port.ToString());
                    arguments.Add(InterfaceControl.Info.Hostname);

                    if (_isPuttyNg)
                    {
                        arguments.Add("-hwndparent", InterfaceControl.Handle.ToString());
                    }

                    _with1.Arguments = arguments.ToString();

                   
                    //REMOVE IN RELEASE!
#if DEBUG
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                        "PuTTY Arguments: " + PuttyProcess.StartInfo.Arguments, true);
                    Debug.WriteLine("PuTTY Arguments: " + PuttyProcess.StartInfo.Arguments);
#endif

                    PuttyProcess.EnableRaisingEvents = true;
                    PuttyProcess.Exited += ProcessExited;

                    PuttyProcess.Start();
                    PuttyProcess.WaitForInputIdle(Settings.Default.MaxPuttyWaitTime * 1000);

                    int startTicks = Environment.TickCount;
                    while (PuttyHandle.ToInt32() == 0 & Environment.TickCount < startTicks + (Settings.Default.MaxPuttyWaitTime * 1000))
                    {
                        if (_isPuttyNg)
                        {
                            PuttyHandle = Native.FindWindowEx(InterfaceControl.Handle, IntPtr.Zero, Constants.vbNullString, Constants.vbNullString);
                        }
                        else
                        {
                            PuttyProcess.Refresh();
                            PuttyHandle = PuttyProcess.MainWindowHandle;
                        }
                        if (PuttyHandle.ToInt32() == 0)
                            Thread.Sleep(50);
                    }

                    if (!_isPuttyNg)
                    {
                        Native.SetParent(PuttyHandle, InterfaceControl.Handle);
                    }

                    Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, Language.strPuttyStuff, true);

                    Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, string.Format(Language.strPuttyHandle, PuttyHandle.ToString()), true);
                    Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, string.Format(Language.strPuttyTitle, PuttyProcess.MainWindowTitle), true);
                    Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, string.Format(Language.strPuttyParentHandle, InterfaceControl.Parent.Handle.ToString()), true);

                    Resize(this, new EventArgs());

                    base.Connect();
                    return true;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg, Language.strPuttyConnectionFailed + Constants.vbNewLine + ex.Message);
                    return false;
                }
            }

            public override void Focus()
            {
                try
                {
                    //Native.ForceForegroundWindow(PuttyHandle);
                    Native.SetForegroundWindow(PuttyHandle);
                    Debug.WriteLine("Native.ForceForegroundWindow");
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strPuttyFocusFailed + Constants.vbNewLine + ex.Message,
                                                        true);
                }
            }

            public override void Resize(object sender, EventArgs e)
            {
                try
                {
                    if ((InterfaceControl.Size == Size.Empty))
                    {
                        return;
                    }
                    Native.MoveWindow(PuttyHandle, Convert.ToInt32(-SystemInformation.FrameBorderSize.Width),
                                      Convert.ToInt32(
                                          -(SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height)),
                                      Convert.ToInt32(InterfaceControl.Width +
                                                             (SystemInformation.FrameBorderSize.Width * 2)),
                                      Convert.ToInt32(InterfaceControl.Height + SystemInformation.CaptionHeight +
                                                             (SystemInformation.FrameBorderSize.Height * 2)), true);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strPuttyResizeFailed + Constants.vbNewLine + ex.Message,
                                                        true);
                }
            }

            public override void Close()
            {
                try
                {
                    if (PuttyProcess.HasExited == false)
                    {
                        PuttyProcess.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strPuttyKillFailed + Constants.vbNewLine + ex.Message,
                                                        true);
                }

                try
                {
                    PuttyProcess.Dispose();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strPuttyDisposeFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }

                base.Close();
            }

            public void ShowSystemMenu()
            {
                try
                {
                    //ToDo
                    Point vPoint;
                    Rectangle vRect;
                    //Native.SendMessage(PuttyHandle,Native.WM_INITMENU,)
                    Native.GetCursorPos(out vPoint);
                    Native.SendMessage(PuttyHandle, Native.WM_SYSCOMMAND, Native.TrackPopupMenu(Native.GetSystemMenu(PuttyHandle, false),Native.TPM_RETURNCMD | Native.TPM_LEFTBUTTON, vPoint.X, vPoint.Y,0, PuttyHandle, out vRect), 0);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                    Language.strPuttyShowSettingsDialogFailed + Constants.vbNewLine +
                                    ex.Message, true);
                }
            }

            public void ShowSettingsDialog()
            {
                try
                {
                    Native.PostMessage(this.PuttyHandle, Native.WM_SYSCOMMAND, System.Convert.ToInt32(IDM_RECONF), 0);
                    Native.SetForegroundWindow(this.PuttyHandle);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strPuttyShowSettingsDialogFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }
            }

            #endregion Public Methods

            #region Public Shared Methods

            public static string[] GetSessions()
            {
                try
                {
                    RegistryKey regKey;
                    regKey = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\Sessions");

                    string[] arrKeys;
                    arrKeys = regKey.GetSubKeyNames();
                    Array.Resize(ref arrKeys, arrKeys.Length + 1);
                    arrKeys[arrKeys.Length - 1] = "Default Settings";

                    for (int i = 0; i <= arrKeys.Length - 1; i++)
                    {
                        arrKeys[i] = System.Web.HttpUtility.UrlDecode(arrKeys[i]);
                    }

                    return arrKeys;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                        Language.strPuttyGetSessionsFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                    return null;
                }
            }

            public static bool IsFilePuttyNg(string file)
            {
                bool isPuttyNg;
                try
                {
                    // PuTTYNG enhancements are not yet compatible with Windows 8
                    if (Environment.OSVersion.Version.CompareTo(new Version(6, 2)) >= 0)
                    {
                        return false;
                    }

                    isPuttyNg =
                        System.Convert.ToBoolean(FileVersionInfo.GetVersionInfo(file).InternalName.Contains("PuTTYNG"));
                }
                catch
                {
                    isPuttyNg = false;
                }
                
                return isPuttyNg;
            }

            public static void StartPutty()
            {
                try
                {
                    Process p;
                    ProcessStartInfo pSI = new ProcessStartInfo();
                    pSI.FileName = PuttyPath;

                    p = Process.Start(pSI);
                    p.WaitForExit();

                    PuttySession.PuttySessions = GetSessions();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strPuttyStartFailed + Constants.vbNewLine + ex.Message,
                                                        true);
                }
            }

            #endregion Public Shared Methods

            #region Enums

            public enum Putty_Protocol
            {
                ssh = 0,
                telnet = 1,
                rlogin = 2,
                raw = 3,
                serial = 4
            }

            public enum Putty_SSHVersion
            {
                ssh1 = 1,
                ssh2 = 2
            }

            #endregion Enums
        }
    }
}