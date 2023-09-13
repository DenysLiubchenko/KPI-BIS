using BIS.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Ciphers
{
    internal class CaesarCipher : ICipher<int>
    {
        public string Encrypt(string text, int key)
        {
            char[] chars = text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] + key);
            }
            return new string(chars);
        }
        public string Decrypt(string text, int key)
        {
            return Encrypt(text, -key);
        }
    }
}
