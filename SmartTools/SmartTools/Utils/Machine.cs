using System;
using System.Diagnostics;

namespace SmartTools.Utils
{
    public class Machine
    {
        static Process _CURRENTPROCESS = Process.GetCurrentProcess();

        public static void ReleaseMemory()
        {
#if !_CONSOLE
            // release any unused pages
            // making the numbers look good in task manager
            // this is totally nonsense in programming
            // but good for those users who care
            // making them happier with their everyday life
            // which is part of user experience
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
