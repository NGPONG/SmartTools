using System;
using System.Runtime.InteropServices;

namespace SmartTools.Utils
{
    public class Win32API
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroyicon
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr handle);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getasynckeystate
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(global::System.Windows.Forms.Keys vKey);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setprocessworkingsetsize
        /// </summary>
        /// <param name="process"></param>
        /// <param name="minimumWorkingSetSize"></param>
        /// <param name="maximumWorkingSetSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessWorkingSetSize(IntPtr process,UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
    }
}
