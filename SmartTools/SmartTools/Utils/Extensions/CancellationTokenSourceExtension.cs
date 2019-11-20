using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartTools.Utils.Extensions
{
    public static class CancellationTokenSourceExtension
    {
        public static void Reset(this CancellationTokenSource cancelToken)
        {
            cancelToken.Dispose();
            cancelToken = new CancellationTokenSource();
        }
    }
}
