using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static Process PtrToProc(IntPtr wnd)
        {
            uint processId;
            GetWindowThreadProcessId(wnd, out processId);
            return Process.GetProcessById((int)processId);
        }

        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        public const uint WS_EX_TOPMOST = 0x00000008;

        public override bool Connect()
        {
            try
            {
                var hWndParent = IntPtr.Zero;

                var pDocked = new Process();
                //pDocked.StartInfo.FileName = @"c:\Program Files (x86)\TeamViewer\Version7\teamviewer.exe";
                pDocked.StartInfo.FileName = Settings.Default.TeamViewerPath;
                pDocked.StartInfo.Arguments = "-i " + this.InterfaceControl.Info.Hostname + " --Password " + this.InterfaceControl.Info.Password;
                pDocked.Start();

                while (Handle == IntPtr.Zero)
                {
                    Thread.Sleep(1000);
                    Handle = Native.GetPtrToProcWithWindowWithTextInCap("(" + this.InterfaceControl.Info.Hostname.Insert(3, " ").Insert(7, " ") + ")");  //cache the window handle
                }

                TVProcess = PtrToProc(Handle);

                //Windows API call to change the parent of the target window.
                //It returns the hWnd of the window's parent prior to this call.
                Native.SetParent(Handle, InterfaceControl.Handle);
                //Native.SetWindowLong(Handle, GWL_STYLE, Native.GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOPMOST);
                Resize();
                t.Tick += new EventHandler(t_Tick);
                t.Interval = 1000;
                //t.Start();
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

        private void t_Tick(object sender, EventArgs e)
        {
            Resize();
        }

        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        public override void Resize()
        {
            try
            {

                //ToDo: Rewrite code
                int x = 0, y = 0, b, a;
                var ax = Convert.ToInt32(-SystemInformation.FrameBorderSize.Width);
                var ay = Convert.ToInt32(-(SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height));
                b = Convert.ToInt32(InterfaceControl.Width + (SystemInformation.FrameBorderSize.Width * 2));
                a =
                    Convert.ToInt32(InterfaceControl.Height + SystemInformation.CaptionHeight +
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
                int y1 = 0, x1 = 0;
                Double W = width;
                Double H = height;
                Double w1 = b;
                Double h1 = a;
                double S = w1 / W > h1 / H ? h1 / H : w1 / W;
                Debug.WriteLine(Convert.ToInt32(W * S) + "  " + (Convert.ToInt32(H * S) - 20));
                Native.MoveWindow(Handle, ax, ay, Convert.ToInt32(W * S - 50 * S), Convert.ToInt32(H * S - 50 * S), true);
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
            this.Close();
            base.Disconnect();
        }

        public override void Close()
        {
            try
            {
                if (TVProcess.HasExited == false)
                {
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
                TVProcess.Dispose();
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