using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevopsGenie.Reponook.Exceptions
{

    public class APIBodyParseError : Exception
    {
        public APIBodyParseError()
        {
        }
        public APIBodyParseError(string message)
            : base(message)
        {
        }
    }
}
