using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Ciphers
{
    internal class XORCipher : ICipher<int>
    {
        private string Crypt(string text, int key)
        {
            Encoding encoding= Encoding.GetEncoding(1251);
            byte[] byteText = encoding.GetBytes(text),
                byteGamaKey = new byte[byteText.Length],
                byteResults = new byte[byteText.Length];
            new Random(key).NextBytes(byteGamaKey);
            for (int i = 0; i < byteResults.Length; i++)
            {
                byteResults[i] = (byte)(byteText[i] ^ byteGamaKey[i]);
            }
            return encoding.GetString(byteResults);
        }
        public string Encrypt(string text, int key)
        {
            return Crypt(text, key);
        }
        public string Decrypt(string text, int key)
        {
            return Crypt(text, key);
        }
    }
}
