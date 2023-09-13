using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.util
{
    internal class InvalidKeyException : Exception
    {
        string Message { get; set; }

        public InvalidKeyException (string Message) : base(Message) { }
    }
}
