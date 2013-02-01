using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace mRemoteNC
{
    public class Native
    {
        private Native()
        {
            // Fix Warning 292 CA1053 : Microsoft.Design : Because type 'Native' contains only 'static' ('Shared' in Visual Basic) members, add a default private constructor to prevent the compiler from adding a default public constructor.
        }

        public static void ForceForegroundWindow(IntPtr hWnd)
        {
            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);

            uint appThread = GetCurrentThreadId();

            const uint SW_SHOW = 5;

            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);

                BringWindowToTop(hWnd);

                ShowWindow(hWnd, (int)SW_SHOW);

                AttachThreadInput(foreThread, appThread, false);
            }

            else
            {
                BringWindowToTop(hWnd);

                ShowWindow(hWnd, (int)SW_SHOW);
            }
        }

        #region Functions

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        //When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool AppendMenu(IntPtr hMenu, int uFlags, IntPtr uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePopupMenu();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName,
                                                 string windowTitle);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, IntPtr uIDNewItem,
                                             string lpNewItem);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetMenuItemBitmaps(IntPtr hMenu, int uPosition, int uFlags, IntPtr hBitmapUnchecked,
                                                     IntPtr hBitmapChecked);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("User32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool TrackPopupMenuEx(IntPtr hmenu, int fuFlags, int x, int y, IntPtr hwnd, IntPtr tpmParams);
        
        [DllImport("User32.dll")]
        public static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, out Rectangle prcRect);

        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        #endregion Functions

        #region Structures

        [Flags()]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
            /// contents of the client area are saved and copied back into the client area after the window is sized or
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
            /// window uncovered as a result of the window being moved. When this flag is set, the application must
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        #endregion Structures

        #region Helpers

        public static int MAKELONG(int wLow, int wHigh)
        {
            return wLow | wHigh << 16;
        }

        public static int MAKELPARAM(int wLow, int wHigh)
        {
            return MAKELONG(wLow, wHigh);
        }

        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }

        public static int LOWORD(IntPtr value)
        {
            return LOWORD(value.ToInt32());
        }

        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        public static int HIWORD(IntPtr value)
        {
            return HIWORD(value.ToInt32());
        }

        #endregion Helpers

        #region Constants

        // GetWindowLong
        public const int GWL_STYLE = (-16);

        // TrackPopupMenu 
        public const uint TPM_LEFTBUTTON = 0;
        public const uint TPM_RIGHTBUTTON = 2;
        public const uint TPM_LEFTALIGN = 0;
        public const uint TPM_CENTERALIGN = 4;
        public const uint TPM_RIGHTALIGN = 8;
        public const uint TPM_TOPALIGN = 0;
        public const uint TPM_VCENTERALIGN = 0x10;
        public const uint TPM_BOTTOMALIGN = 0x20;
        public const uint TPM_RETURNCMD = 0x100;
        public const uint WM_INITMENU = 0x0116;

        // AppendMenu / ModifyMenu / DeleteMenu / RemoveMenu
        public const int MF_BYCOMMAND = 0x0;
        public const int MF_BYPOSITION = 0x400;
        public const int MF_STRING = 0x0;
        public const int MF_POPUP = 0x10;
        public const int MF_SEPARATOR = 0x800;

        // WM_LBUTTONDOWN / WM_LBUTTONUP
        public const int MK_LBUTTON = 0x1;

        // ShowWindow
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_RESTORE = 9;

        // SetWindowPos / WM_WINDOWPOSCHANGING / WM_WINDOWPOSCHANGED
        public const int SWP_NOSIZE = 0x1;
        public const int SWP_NOMOVE = 0x2;
        public const int SWP_NOZORDER = 0x4;
        public const int SWP_NOREDRAW = 0x8;
        public const int SWP_NOACTIVATE = 0x10;
        public const int SWP_DRAWFRAME = 0x20;
        public const int SWP_FRAMECHANGED = 0x20;
        public const int SWP_SHOWWINDOW = 0x40;
        public const int SWP_HIDEWINDOW = 0x80;
        public const int SWP_NOCOPYBITS = 0x100;
        public const int SWP_NOOWNERZORDER = 0x200;
        public const int SWP_NOSENDCHANGING = 0x400;
        public const int SWP_NOCLIENTSIZE = 0x800;
        public const int SWP_NOCLIENTMOVE = 0x1000;
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        public const int SWP_STATECHANGED = 0x8000;

        // WM_ACTIVATE
        public const int WA_INACTIVE = 0x0;
        public const int WA_ACTIVE = 0x1;
        public const int WA_CLICKACTIVE = 0x2;

        // Window Messages
        public const int WM_CREATE = 0x1;
        public const int WM_DESTROY = 0x2;
        public const int WM_ACTIVATE = 0x6;
        public const int WM_GETTEXT = 0xD;
        public const int WM_CLOSE = 0x10;
        public const int WM_ACTIVATEAPP = 0x1C;
        public const int WM_MOUSEACTIVATE = 0x21;
        public const int WM_WINDOWPOSCHANGED = 0x47;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_XBUTTONDOWN = 0x20B;
        public const int WM_XBUTTONUP = 0x20C;
        public const int WM_PARENTNOTIFY = 0x210;
        public const int WM_ENTERSIZEMOVE = 0x231;
        public const int WM_EXITSIZEMOVE = 0x232;
        public const int WM_DRAWCLIPBOARD = 0x308;
        public const int WM_CHANGECBCHAIN = 0x30D;

        // Window Styles
        public const int WS_MAXIMIZE = 0x1000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_EX_MDICHILD = 0x40;

        // Virtual Key Codes
        public const int VK_CONTROL = 0x11;
        public const int VK_C = 0x67;

        #endregion Constants

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // This static method is required because legacy OSes do not support
        // SetWindowLongPtr
        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        public static IntPtr GetPtrToProcWithWindowWithTextInCap(string input)
        {
            try
            {
                IntPtr res = IntPtr.Zero;
                EnumWindowsProc delEnumfunc = (hWnd, param) =>
                {
                    try
                    {
                        string strTitle = GetWindowText(hWnd);
                        if (!String.IsNullOrEmpty(strTitle) && strTitle.Contains(input))
                        {
                            res = hWnd;
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        return false;
                    }
                };
                bool bSuccessful = EnumWindows(delEnumfunc, IntPtr.Zero); //for current desktop
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return IntPtr.Zero;
            }
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
    ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int _GetWindowText(IntPtr hWnd,
                                                 StringBuilder lpWindowText, int nMaxCount);

        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder strbTitle = new StringBuilder(255);
            try
            {
                int nLength = _GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                strbTitle.Length = nLength;
            }
            catch (Exception)
            {
            }
            return strbTitle.ToString();
        }
    }
}