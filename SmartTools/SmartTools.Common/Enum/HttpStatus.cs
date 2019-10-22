using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Common.Enum
{
    public enum HttpStatus
    {
        OK = 200,
        Accepted = 202,
        Found = 302,
        NotFound = 404,
        Error = 500,
        CustomError = 512
    }
}
