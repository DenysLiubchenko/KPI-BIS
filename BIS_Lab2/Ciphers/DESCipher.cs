using BIS.Interface;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BIS.Ciphers
{
    internal class DESCipher : ICipher<String>
    {
        private readonly DESCryptoServiceProvider DESCryptoProvider = new DESCryptoServiceProvider();
        public string Encrypt(string text, string key)
        {
            ICryptoTransform encryptor = GetCryptoTransform(key, true);
            byte[] byteText = Encoding.UTF8.GetBytes(text);

            return Convert.ToBase64String(encryptor.TransformFinalBlock(byteText, 0, byteText.Length));
        }

        public string Decrypt(string text, string key)
        {
            ICryptoTransform decryptor = GetCryptoTransform(key, false);
            byte[] byteText = Convert.FromBase64String(text);

            return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(byteText, 0, byteText.Length));
        }

        private ICryptoTransform GetCryptoTransform(string key, bool encryption)
        {
            DESCryptoProvider.Mode = CipherMode.ECB;
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            byte[] byteIv = Encoding.UTF8.GetBytes(key);
            if(encryption) return DESCryptoProvider.CreateEncryptor(byteKey, byteIv);
            else return DESCryptoProvider.CreateDecryptor(byteKey, byteIv);
        }
    }
}
