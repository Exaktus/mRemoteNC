using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Microsoft.VisualBasic;
using Tools;
using mRemoteNC.App;

//using mRemoteNC.Runtime;

namespace mRemoteNC.Tools
{
    public class ExternalTool
    {
        #region Properties

        private string _DisplayName;

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        private bool _WaitForExit;

        public bool WaitForExit
        {
            get { return _WaitForExit; }
            set { _WaitForExit = value; }
        }

        private string _Arguments;

        public string Arguments
        {
            get { return _Arguments; }
            set { _Arguments = value; }
        }

        private bool _TryIntegrate;

        public bool TryIntegrate
        {
            get { return _TryIntegrate; }
            set { _TryIntegrate = value; }
        }

        private Connection.Info _ConnectionInfo;

        public Connection.Info ConnectionInfo
        {
            get { return _ConnectionInfo; }
            set { _ConnectionInfo = value; }
        }

        public Icon Icon
        {
            get
            {
                if (File.Exists(this._FileName))
                {
                    return Tools.Misc.GetIconFromFile(this._FileName);
                }
                else
                {
                    return null;
                }
            }
        }

        public Image Image
        {
            get
            {
                Icon iC = this.Icon;
                if (iC != null)
                {
                    return iC.ToBitmap();
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion Properties

        public ExternalTool()
            : this("")
        {
        }

        public ExternalTool(string DisplayName)
            : this(DisplayName, "", "")
        {
        }

        public ExternalTool(string DisplayName, string Filename, string Arguments)
        {
            _DisplayName = DisplayName;
            _FileName = Filename;
            _Arguments = Arguments;
        }

        // Start external app
        public Process Start(Connection.Info ConnectionInfo = null)
        {
            try
            {
                if (_FileName == "")
                {
                    throw (new Exception("No Filename specified!"));
                }

                if (_TryIntegrate)
                {
                    StartIntApp(ConnectionInfo);
                    return null;
                }

                _ConnectionInfo = ConnectionInfo;

                Process p = new Process();
                ProcessStartInfo pI = new ProcessStartInfo();

                pI.UseShellExecute = false;
                pI.FileName = ParseText(_FileName);
                pI.Arguments = CommandLineArguments.EscapeBackslashes(_Arguments);

                p.StartInfo = pI;

                p.Start();

                if (_WaitForExit)
                {
                    p.WaitForExit();
                }

                return p;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("Couldn\'t start external application." + Constants.vbNewLine +
                                                     ex.Message));
                return null;
            }
        }

        // Start external app integrated
        public void StartIntApp(Connection.Info ConnectionInfo = null)
        {
            try
            {
                _ConnectionInfo = ConnectionInfo;

                Connection.Info nCI = new Connection.Info();

                nCI.Protocol = mRemoteNC.Protocols.IntApp;
                nCI.ExtApp = this.DisplayName;
                nCI.Name = this.DisplayName;
                nCI.Panel = "Int. Apps";
                if (_ConnectionInfo!=null)
                {
                    nCI.Hostname = _ConnectionInfo.Hostname;
                    nCI.Port = _ConnectionInfo.Port;
                    nCI.Username = _ConnectionInfo.Username;
                    nCI.Password = _ConnectionInfo.Password;
                    nCI.Domain = _ConnectionInfo.Domain;
                    nCI.Description = _ConnectionInfo.Description;
                    nCI.MacAddress = _ConnectionInfo.MacAddress;
                    nCI.UserField = _ConnectionInfo.UserField;
                    nCI.Description = _ConnectionInfo.Description;
                    nCI.PreExtApp = _ConnectionInfo.PreExtApp;
                    nCI.PostExtApp = _ConnectionInfo.PostExtApp;
                }

                Runtime.OpenConnection(nCI);
            }
            catch (Exception)
            {
            }
        }

        public string ParseText(string Text)
        {
            string pText = Text;

            try
            {
                if (_ConnectionInfo != null)
                {
                    pText = Strings.Replace(pText, "%Name%", (string)_ConnectionInfo.Name, 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%HostName%", (string)_ConnectionInfo.Hostname, 1, -1,
                                            CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Port%", _ConnectionInfo.Port.ToString(), 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%UserName%", (string)_ConnectionInfo.Username, 1, -1,
                                            CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Password%", (string)_ConnectionInfo.Password, 1, -1,
                                            CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Domain%", (string)_ConnectionInfo.Domain, 1, -1,
                                            CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Description%", (string)_ConnectionInfo.Description, 1, -1,
                                            CompareMethod.Text);
                    pText = Strings.Replace(pText, "%MacAddress%", (string)_ConnectionInfo.MacAddress, 1, -1,
                                            CompareMethod.Text);
                    pText = Strings.Replace(pText, "%UserField%", (string)_ConnectionInfo.UserField, 1, -1,
                                            CompareMethod.Text);
                }
                else
                {
                    pText = Strings.Replace(pText, "%Name%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%HostName%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Port%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%UserName%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Password%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Domain%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%Description%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%MacAddress%", "", 1, -1, CompareMethod.Text);
                    pText = Strings.Replace(pText, "%UserField%", "", 1, -1, CompareMethod.Text);
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                    (string)
                                                    ("ParseText failed (Tools.ExternalApp)" + Constants.vbNewLine +
                                                     ex.Message), true);
            }

            return pText;
        }
    }

    public class ExternalAppsTypeConverter : StringConverter
    {
        public static string[] ExternalApps = new string[] { };

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(
            System.ComponentModel.ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(ExternalApps);
        }

        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}