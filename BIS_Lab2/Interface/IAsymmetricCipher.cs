using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Interface
{
    internal interface IAsymmetricCipher<EKey, DKey>
    {
        public String Encrypt(String text, EKey key);
        public String Decrypt(String text, DKey key);

    }
}
