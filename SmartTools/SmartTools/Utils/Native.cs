using System;
using System.Diagnostics;

namespace SmartTools.Utils
{
    public class Native
    {
        private static Process _CURRENTPROCESS = Process.GetCurrentProcess();

        // 释放掉未使用的页面
        // 使任务管理器中的数字看起来稍微好看点
        // 然而并没有什么卵用，对于编程来说完全是胡说八道
        // 但就用户体验来说
        // 是能够得到一个很好的体验，这是一个出发点
        public static void ReleaseMemory()
        {
#if !_CONSOLE
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();

            if (UIntPtr.Size == 4)
            {
                Win32API.SetProcessWorkingSetSize(_CURRENTPROCESS.Handle,
                                                 (UIntPtr)0xFFFFFFFF,
                                                 (UIntPtr)0xFFFFFFFF);
            }
            else if (UIntPtr.Size == 8)
            {
                Win32API.SetProcessWorkingSetSize(_CURRENTPROCESS.Handle,
                                                 (UIntPtr)0xFFFFFFFFFFFFFFFF,
                                                 (UIntPtr)0xFFFFFFFFFFFFFFFF);
            }
#endif
        }

    }
}
