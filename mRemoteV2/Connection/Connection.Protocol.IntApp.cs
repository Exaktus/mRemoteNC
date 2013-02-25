using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;
using mRemoteNC.Tools;

namespace mRemoteNC
{
    public class IntApp : Base
    {
        #region Private Properties

        private ProcessStartInfo IntAppProcessStartInfo = new ProcessStartInfo();
        private string Arguments;
        private Tools.ExternalTool ExtApp;
        private IntPtr ParenrHandle;

        #endregion Private Properties

        #region Public Properties

        private IntPtr _IntAppHandle;

        public IntPtr IntAppHandle
        {
            get { return this._IntAppHandle; }
            set { this._IntAppHandle = value; }
        }

        private Process _IntAppProcess;

        public Process IntAppProcess
        {
            get { return this._IntAppProcess; }
            set { this._IntAppProcess = value; }
        }

        private string _IntAppPath;

        public string IntAppPath
        {
            get { return _IntAppPath; }
            set { _IntAppPath = value; }
        }

        public bool Focused
        {
            get
            {
                if (Native.GetForegroundWindow() == IntAppHandle)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public IntApp()
        {
        }

        public void Refresh()
        {
            try
            {
                Resize();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                    Language.strIntAppConnectionFailed + Constants.vbNewLine +
                                    ex.Message);
            }
        }

        public override bool SetProps()
        {
            ExtApp = Runtime.GetExtAppByName(InterfaceControl.Info.ExtApp);
            if (InterfaceControl.Info != null)
            {
                ExtApp.ConnectionInfo = InterfaceControl.Info;
            }

            _IntAppPath = (string)(ExtApp.ParseText(ExtApp.FileName));
            Arguments = (string)(ExtApp.ParseText(ExtApp.Arguments));

            return base.SetProps();
        }

        public override bool Connect()
        {
            try
            {
                if (ExtApp.TryIntegrate == false)
                {
                    ExtApp.Start(this.InterfaceControl.Info);
                    this.Close();
                    return false;
                }

                IntAppProcessStartInfo.FileName = _IntAppPath;
                IntAppProcessStartInfo.Arguments = Arguments!=null?ExternalTool.EscapeArguments(Arguments):"";
                IntAppProcess=new Process();
                IntAppProcess.StartInfo = IntAppProcessStartInfo;
                IntAppProcess.EnableRaisingEvents = true;
                IntAppProcess.Exited += ProcessExited;
                IntAppProcess.Start();
                ParenrHandle = InterfaceControl.Parent.Handle;
                ThreadPool.QueueUserWorkItem(state => CatchIntApp());
                
                base.Connect();
                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIntAppConnectionFailed + Constants.vbNewLine +
                                                    ex.Message);
                return false;
            }
        }

        private void CatchIntApp()
        {
            try
            {
                IntAppProcess.WaitForInputIdle();
                int TryCount = 0;
                while (
                    !(IntAppProcess.MainWindowHandle != IntPtr.Zero &&
                      IntAppProcess.MainWindowTitle != "Default IME"))
                {
                    if (TryCount >= Settings.Default.MaxPuttyWaitTime * 2)
                    {
                        break;
                    }

                    IntAppProcess.Refresh();

                    Thread.Sleep(500);

                    TryCount++;
                }

                IntAppHandle = IntAppProcess.MainWindowHandle;

                Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg, Language.strIntAppStuff,
                                                    true);

                Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                    string.Format(Language.strIntAppHandle, IntAppHandle.ToString()),
                                                    true);
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                    string.Format(Language.strIntAppTitle,
                                                                  IntAppProcess.MainWindowTitle), true);
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                    string.Format(Language.strIntAppParentHandle,
                                                                  ParenrHandle.ToString()),
                                                    true);

                Native.SetParent(IntAppHandle, ParenrHandle);
                Native.SetWindowLong(IntAppHandle, 0, Native.WS_VISIBLE);
                Native.ShowWindow(IntAppHandle, Convert.ToInt32(Native.SW_SHOWMAXIMIZED));

                Resize();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                    Language.strIntAppConnectionFailed + Constants.vbNewLine +
                                    ex.Message);
            }
        }

        public override void Focus()
        {
            try
            {
                Native.SetForegroundWindow(IntAppHandle);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIntAppFocusFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        public override void Resize()
        {
            try
            {
                Native.MoveWindow(IntAppHandle, System.Convert.ToInt32(-SystemInformation.FrameBorderSize.Width),
                                  System.Convert.ToInt32(
                                      -(SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height)),
                                  System.Convert.ToInt32(this.InterfaceControl.Width +
                                                         (SystemInformation.FrameBorderSize.Width * 2)),
                                  System.Convert.ToInt32(this.InterfaceControl.Height +
                                                         SystemInformation.CaptionHeight +
                                                         (SystemInformation.FrameBorderSize.Height * 2)), true);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIntAppResizeFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }
        }

        public override void Close()
        {
            try
            {
                if (IntAppProcess.HasExited == false)
                {
                    IntAppProcess.Kill();
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIntAppKillFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }

            try
            {
                if (IntAppProcess.HasExited == false)
                {
                    IntAppProcess.Dispose();
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strIntAppDisposeFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }

            base.Close();
        }

        #endregion Public Methods

        #region Public Shared Methods

        #endregion Public Shared Methods

        #region Enums

        public enum Defaults
        {
            Port = 0
        }

        #endregion Enums
    }
}