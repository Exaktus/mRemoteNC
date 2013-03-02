using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using My;

namespace mRemoteNC
{
    namespace My
    {
        // The following events are available for MyApplication:
        //
        // Startup: Raised when the application starts, before the startup form is created.
        // Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
        // UnhandledException: Raised if the application encounters an unhandled exception.
        // StartupNextInstance: Raised when launching a single-instance application and the application is already active.
        // NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
        partial class MyApplication
        {
            public static System.Threading.Mutex mutex;

            public static void MyApplication_Startup()
            {
                if (Settings.Default.SingleInstance)
                {
                    string mutexID = "mRemoteNC_SingleInstanceMutex";

                    mutex = new System.Threading.Mutex(false, mutexID);

                    if (!mutex.WaitOne(0, false))
                    {
                        try
                        {
                            SwitchToCurrentInstance();
                        }
                        catch (Exception)
                        {
                        }

                        ProjectData.EndApp();
                    }

                    GC.KeepAlive(mutex);
                }
            }

            static private IntPtr GetCurrentInstanceWindowHandle()
            {
                IntPtr hWnd = IntPtr.Zero;
                Process curProc = Process.GetCurrentProcess();

                foreach (Process proc in Process.GetProcessesByName(curProc.ProcessName))
                {
                    if (proc.Id != curProc.Id && proc.MainModule.FileName == curProc.MainModule.FileName &&
                        proc.MainWindowHandle != IntPtr.Zero)
                    {
                        hWnd = proc.MainWindowHandle;
                        break;
                    }
                }

                return hWnd;
            }

            static private void SwitchToCurrentInstance()
            {
                IntPtr hWnd = GetCurrentInstanceWindowHandle();

                if (hWnd != IntPtr.Zero)
                {
                    //Restore window if minimized. Do not restore if already in
                    //normal or maximised window state, since we don't want to
                    //change the current state of the window.
                    if (mRemoteNC.Native.IsIconic(hWnd) != 0)
                    {
                        mRemoteNC.Native.ShowWindow(hWnd, mRemoteNC.Native.SW_RESTORE);
                    }

                    mRemoteNC.Native.SetForegroundWindow(hWnd);
                }
            }

            public static void MyApplication_Shutdown()
            {
                if (mutex != null)
                {
                    mutex.Close();
                }
            }
        }
    }
}