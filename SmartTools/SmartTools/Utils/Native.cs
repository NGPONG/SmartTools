﻿using System;
using System.Diagnostics;

namespace SmartTools.Utils
{
    public class Native
    {
        #region Internal
        private static Process _CURRENTPROCESS = Process.GetCurrentProcess(); 
        #endregion

        #region Message
        // WINDOWS
        public const int WM_HSCROLL                 = 0x114;
        public const int WM_VSCROLL                 = 0x115;
        public const int WM_MOUSEWHEEL              = 0x020A;
        public const int WM_KEYDOWN                 = 0x0100;
        public const int WM_PAINT                   = 0x000F;

        // LISTVIEW
        public const int LVM_FIRST                  = 0x1000;
        public const int LVM_GETCOLUMNORDERARRAY    = Native.LVM_FIRST + 59;
        #endregion

        // 释放掉未使用的页面
        // 使任务管理器中的数字看起来稍微好看点
        // 然而并没有什么卵用
        // 对于编程来说完全是胡说八道
        // 但就用户体验来说
        // 是能够得到一个很好的体验
        // 这是一个出发点
        public static void ReleaseMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();

            if (UIntPtr.Size == 4)
            {
                Win32.SetProcessWorkingSetSize(_CURRENTPROCESS.Handle,
                                              (UIntPtr)0xFFFFFFFF,
                                              (UIntPtr)0xFFFFFFFF);
            }
            else if (UIntPtr.Size == 8)
            {
                Win32.SetProcessWorkingSetSize(_CURRENTPROCESS.Handle,
                                              (UIntPtr)0xFFFFFFFFFFFFFFFF,
                                              (UIntPtr)0xFFFFFFFFFFFFFFFF);
            }
        }

        /// <summary>
        /// 获取堆栈调用信息
        /// https://www.cnblogs.com/huangtailang/p/4550177.html
        /// </summary>
        /// <returns></returns>
        public static string GetStackTraceModelName()
        {
            try
            {
                StackTrace st = new StackTrace();
                StackFrame[] sfs = st.GetFrames();
                string _filterdName = "ResponseWrite,ResponseWriteError,";
                string _fullName = string.Empty, _methodName = string.Empty;

                for (int i = 1; i < sfs.Length; ++i)
                {
                    if (StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset())
                        break;
                    _methodName = sfs[i].GetMethod().Name;
                    //sfs[i].GetFileLineNumber();
                    if (_filterdName.Contains(_methodName))
                        continue;

                    _fullName = _methodName + "()->" + _fullName;
                }
                return _fullName.TrimEnd('-', '>');
            }
            catch // can't track
            {
                throw;
            }
        }
    }
}
