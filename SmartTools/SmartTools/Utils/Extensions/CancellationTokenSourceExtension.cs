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
        public static CancellationTokenSource Reset(this CancellationTokenSource cancelToken)
        {
            cancelToken.Dispose();
            return new CancellationTokenSource();
        }
    }
}
