using System;

namespace mRemoteNC
{
    namespace Tools
    {
        public class SystemMenu
        {
            public enum Flags
            {
                MF_STRING = mRemoteNC.Native.MF_STRING,
                MF_SEPARATOR = mRemoteNC.Native.MF_SEPARATOR,
                MF_BYCOMMAND = mRemoteNC.Native.MF_BYCOMMAND,
                MF_BYPOSITION = mRemoteNC.Native.MF_BYPOSITION,
                MF_POPUP = mRemoteNC.Native.MF_POPUP,

                WM_SYSCOMMAND = mRemoteNC.Native.WM_SYSCOMMAND
            }

            public IntPtr SystemMenuHandle;
            public IntPtr FormHandle;

            public SystemMenu(IntPtr Handle)
            {
                FormHandle = Handle;
                SystemMenuHandle = mRemoteNC.Native.GetSystemMenu(FormHandle, false);
            }

            public void Reset()
            {
                SystemMenuHandle = mRemoteNC.Native.GetSystemMenu(FormHandle, true);
            }

            public void AppendMenuItem(IntPtr ParentMenu, Flags Flags, int ID, string Text)
            {
                mRemoteNC.Native.AppendMenu(ParentMenu, (int)Flags, (IntPtr)ID, Text);
            }

            public IntPtr CreatePopupMenuItem()
            {
                return mRemoteNC.Native.CreatePopupMenu();
            }

            public bool InsertMenuItem(IntPtr SysMenu, int Position, Flags Flags, IntPtr SubMenu, string Text)
            {
                return mRemoteNC.Native.InsertMenu(SysMenu, Position, (int)Flags, SubMenu, Text);
            }

            /*public IntPtr SetBitmap(IntPtr Menu, int Position, Flags Flags, Bitmap Bitmap)
			{
                return (IntPtr)((long)-((long)Native.SetMenuItemBitmaps(Menu, Position, (int)Flags, Bitmap.GetHbitmap(), Bitmap.GetHbitmap()) > false));
			}*/
        }
    }
}