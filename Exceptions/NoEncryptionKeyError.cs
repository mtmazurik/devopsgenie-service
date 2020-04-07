using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevopsGenie.Reponook.Exceptions
{

    public class NoEncryptionKeyError : Exception
    {
        public NoEncryptionKeyError()
        {
        }
        public NoEncryptionKeyError(string message)
            : base(message)
        {
        }
    }
}
