using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace pzy.AO.ClickSaver
{
    public class WinApiWrapper
    {
        public delegate bool EnumWindowsFunc( IntPtr hWnd, ref IntPtr hWndOut );

        [DllImport( "user32.dll", SetLastError = true, CharSet = CharSet.Auto )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool EnumWindows( EnumWindowsFunc lpFunc, ref IntPtr hWnd );

        [DllImport( "user32.dll", SetLastError = true, CharSet = CharSet.Auto )]
        public static extern int GetWindowText( IntPtr hWnd, StringBuilder lpString, int nMaxCount );

        [DllImport( "user32.dll", SetLastError = true, CharSet = CharSet.Auto )]
        public static extern int GetWindowTextLength( IntPtr hWnd );
        
        [DllImport( "user32.dll" )]
        public static extern bool ClientToScreen( IntPtr hWnd, ref Point lpPoint );
    }
}
