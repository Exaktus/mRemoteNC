using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using My;
using mRemoteNC.App;

namespace mRemoteNC.Connection
{
    class RAdmin: Base
    {
        public IntPtr Handle;
        public IntPtr IcHandle;
        public Process RAProcess;

        public enum Colors
        {
            bpp24=24,
            bpp16=16,
            bpp8=8, 
            bpp4=1,
            bpp2=1, 
            bpp=1
        }

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


        public override void Resize(object sender, EventArgs eventArgs)
        {
            try
            {
                Native.MoveWindow(Handle, Convert.ToInt32(-SystemInformation.FrameBorderSize.Width),
                                  Convert.ToInt32(-(SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height)),
                                  Convert.ToInt32(InterfaceControl.Width + (SystemInformation.FrameBorderSize.Width * 2)),
                                  Convert.ToInt32(InterfaceControl.Height + SystemInformation.CaptionHeight + (SystemInformation.FrameBorderSize.Height * 2)), true);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    Language.strPuttyResizeFailed + Constants.vbNewLine + ex.Message,
                                                    true);
            }
        }

        public void CatchRadmin()
        {
            var p = IntPtr.Zero;
            while (p == IntPtr.Zero)
            {
                p = Native.GetPtrToProcWithWindowWithTextInCap(": " + InterfaceControl.Info.Hostname);
                if (p == IntPtr.Zero)
                {
                    Thread.Sleep(100);
                    continue;
                }
                Native.ForceForegroundWindow(Handle);
                SendKeys.SendWait(InterfaceControl.Info.Username + "{Tab}" + InterfaceControl.Info.Password + "{ENTER}");
            }
            p = IntPtr.Zero;
            while (p == IntPtr.Zero)
            {
                p = Native.GetPtrToProcWithWindowWithTextInCap(InterfaceControl.Info.Hostname + " - ");
                if (p == IntPtr.Zero)
                {
                    Thread.Sleep(10);
                }
            }

            Handle = p;
            Native.SetParent(Handle, IcHandle);
            Thread.Sleep(50);
            Native.ForceForegroundWindow(Handle);
            SendKeys.SendWait("{F12}{F12}");
            Event_Connected(this);
            this.Resize(null, null); // bad fix for small size of new connection before any resizing of main window
        }

        public override void Disconnect()
        {
            this.Close();
            base.Disconnect();
        }

        public override bool Connect()
        {
            try
            {
                Event_Connecting(this);
                IcHandle = InterfaceControl.Handle;
                RAProcess = new Process
                    {
                        StartInfo =
                            {
                                FileName = Settings.Default.RAdminPath,
                                Arguments =
                                    "/connect:" + InterfaceControl.Info.Hostname + ":" + InterfaceControl.Info.Port
                            },
                        EnableRaisingEvents = true
                    };
                RAProcess.Exited += (sender, args) => Disconnect();
                RAProcess.Start();
                
                ThreadPool.QueueUserWorkItem(state => CatchRadmin());
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


        public override void Close()
        {
            try
            {
                if (RAProcess!=null)
                {
                    RAProcess.EnableRaisingEvents=false;
                    if (RAProcess.HasExited == false)
                    {
                        RAProcess.Kill();
                    }
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
                if (RAProcess != null) RAProcess.Dispose();
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
