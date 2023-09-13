using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Interface
{
    internal interface ICipher<K>
    {
        public String Encrypt(String text, K key);
        public String Decrypt(String text, K key);
        
    }
}
