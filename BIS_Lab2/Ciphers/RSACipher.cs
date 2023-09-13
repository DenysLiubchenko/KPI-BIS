using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace BIS.RSA
{
    internal class PrivateKey
    {
        public string privateKey { get; }
        public PrivateKey(string privateKey)
        {
            this.privateKey = privateKey;
        }
    }
    internal class PublicKey
    {
        public string publicKey { get; }
        public PublicKey(string publicKey)
        {
            this.publicKey = publicKey;
        }
    }
    internal class RSACipher : IAsymmetricCipher<PublicKey, PrivateKey>
    {
        public string Decrypt(string text, PrivateKey key)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key.privateKey);
            byte[] byteText = Convert.FromBase64String(text);
            int blockLen = rsa.KeySize / 8;
            int numOfBlocks = (int)Math.Ceiling(byteText.Length * 1.0 / blockLen);
            List<byte> byteDecrypted = new List<byte>();

            for (int i = 0; i < numOfBlocks; i++)
            {
                byte[] splitedByteText = new byte[Math.Min(blockLen, byteText.Length - i * blockLen)];
                Array.Copy(byteText, i * blockLen, splitedByteText, 0, Math.Min(blockLen, byteText.Length - i * blockLen));
                byteDecrypted.AddRange(rsa.Decrypt(splitedByteText, false));
            }
            return Encoding.Unicode.GetString(byteDecrypted.ToArray());
        }

        public string Encrypt(string text, PublicKey key)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key.publicKey);
            byte[] byteText = Encoding.Unicode.GetBytes(text);
            int blockLen = ((rsa.KeySize - 384) / 8) + 37;
            int numOfBlocks = (int)Math.Ceiling(byteText.Length*1.0 / blockLen);
            List<byte> byteEncrypted = new List<byte>();

            for (int i = 0; i < numOfBlocks; i++)
            {
                byte[] splitedByteText = new byte[Math.Min(blockLen, byteText.Length - i * blockLen)];
                Array.Copy(byteText, i*blockLen, splitedByteText, 0, Math.Min(blockLen, byteText.Length - i * blockLen));
                byteEncrypted.AddRange(rsa.Encrypt(splitedByteText, false));
            }
            return Convert.ToBase64String(byteEncrypted.ToArray());
        }
    }
}
