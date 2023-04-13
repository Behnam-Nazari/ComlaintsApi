using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework
{
    public enum ApiResultStatusCode
    {
        Success = 0,
        ServerError = 1,
        BadResquesst = 2,
        NotFound = 3,
        ListEmpty = 4
    }
}
