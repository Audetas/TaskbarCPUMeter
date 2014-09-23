using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskbarCPUMeter
{
    public static class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Size GetWindowSize(IntPtr handle)
        {
            Rect rect = new Rect();
            IntPtr error = GetWindowRect(handle, ref rect);

            Size s = new Size();
            s.Width = rect.right - rect.left;
            s.Height = rect.bottom - rect.top;

            return s;
        }
    }
}
