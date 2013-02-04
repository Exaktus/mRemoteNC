using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

namespace mRemoteNC.Connection
{
    internal class TeamViewer : Base
    {
        public IntPtr Handle;
        public IntPtr IcHandel;
        public Process TVProcess;

        public override void Focus()
        {
            try
            {
                Native.ForceForegroundWindow(Handle);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strPuttyFocusFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        private void CatchTV()
        {
            while (Handle == IntPtr.Zero)
            {
                Thread.Sleep(100);
                Handle = Native.GetPtrToProcWithWindowWithTextInCap("(" + this.InterfaceControl.Info.Hostname.Insert(3, " ").Insert(7, " ") + ")");  //cache the window handle
            }
            TVProcess = Native.PtrToProc(Handle);
            TVProcess.EnableRaisingEvents = true;
            TVProcess.Exited += (sender, args) => Disconnect();
            //Windows API call to change the parent of the target window.
            //It returns the hWnd of the window's parent prior to this call.
            Native.SetParent(Handle, IcHandel);
            Resize();
        }

        public override bool Connect()
        {
            try
            {
                IcHandel = InterfaceControl.Handle;
                var pDocked = new Process();
                pDocked.StartInfo.FileName = Settings.Default.TeamViewerPath;
                pDocked.StartInfo.Arguments = "-i " + this.InterfaceControl.Info.Hostname + " --Password " + this.InterfaceControl.Info.Password;
                pDocked.Start();
                ThreadPool.QueueUserWorkItem(state => CatchTV());
                base.Connect();
                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strPuttyConnectionFailed + Constants.vbNewLine +
                                                    ex.Message);
                return false;
            }
        }

        public override void Resize()
        {
            try
            {
                //ToDo: Rewrite code
                var ax = Convert.ToInt32(-SystemInformation.FrameBorderSize.Width);
                var ay = Convert.ToInt32(-(SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height));
                int b = Convert.ToInt32(InterfaceControl.Width + (SystemInformation.FrameBorderSize.Width * 2));
                int a = Convert.ToInt32(InterfaceControl.Height + SystemInformation.CaptionHeight +
                                        (SystemInformation.FrameBorderSize.Height * 2));

                var r = new Rectangle();
                var rect = new Native.RECT();
                var c = Native.GetWindowRect(Handle, out r);
                int width = 0;
                int height = 0;
                if (Native.GetClientRect(Handle, out rect))
                {
                    width = rect.Right - rect.Left;
                    height = rect.Bottom - rect.Top;
                }
                Double W = width;
                Double H = height;
                Double w1 = b;
                Double h1 = a;
                double S = w1 / W > h1 / H ? h1 / H : w1 / W;
                Debug.WriteLine(Convert.ToInt32(W * S) + "  " + Convert.ToInt32(H * S) );
                Native.MoveWindow(Handle, ax, ay, Convert.ToInt32(W * S - S), Convert.ToInt32(H * S - S)+1, true);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strPuttyResizeFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        public override void Disconnect()
        {
            Close();
            base.Disconnect();
        }

        public override void Close()
        {
            try
            {
                if (TVProcess != null && TVProcess.HasExited == false)
                {
                    TVProcess.EnableRaisingEvents = false;
                    TVProcess.Kill();
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
                if (TVProcess != null) TVProcess.Dispose();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strPuttyDisposeFailed + Constants.vbNewLine +
                                                    ex.Message, true);
            }

            base.Close();
        }
    }
}