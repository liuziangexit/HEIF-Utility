using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HEIF_Utility
{
    class ListViewWithoutScrollBar : System.Windows.Forms.ListView
    {
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 0x83: // WM_NCCALCSIZE
                    int style = (int)GetWindowLong(this.Handle, GWL_STYLE);
                    if ((style & WS_HSCROLL) == WS_HSCROLL)
                        SetWindowLong(this.Handle, GWL_STYLE, style & ~WS_HSCROLL);
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        const int GWL_STYLE = -16;
        const int WS_HSCROLL = 0x00100000;

        public static int GetWindowLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return (int)GetWindowLong32(hWnd, nIndex);
            else
                return (int)(long)GetWindowLongPtr64(hWnd, nIndex);
        }

        public static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong)
        {
            if (IntPtr.Size == 4)
                return (int)SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            else
                return (int)(long)SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, int dwNewLong);
    }
}
