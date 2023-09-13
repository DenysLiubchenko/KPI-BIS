using BIS.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace BIS.Knapsack
{
    internal class PrivateKey
    {
        public long[] B { get; }
        public long M { get; }
        public long T { get; }
        public long T1 { get; }

        public PrivateKey(long[] B, long M, long T)
        {
            this.B = B;
            this.M = M;
            this.T = T;
            this.T1 = GenerateT1(M, T);
        }
        private long GenerateT1(long M, long T)
        {
            GenerateT1Helper(M, T, out long x, out long y);
            if (y < 0) return M + y;
            return y;
        }
        private void GenerateT1Helper(long m, long t, out long x, out long y)
        {
            if (t == 0)
            {
                x = 1;
                y = 0;
                return;
            }
            GenerateT1Helper(t, m % t, out long x1, out long y1);
            x = y1;
            y = x1 - (m / t) * y1;
        }
    }
    internal class PublicKey
    {
        public long[] Key { get; set; }
        public PublicKey(long[] Key)
        {
            this.Key = Key;
        }
        public PublicKey(PrivateKey key) 
        {
            Key = GeneratePublicKey(key);
        }
        public long[] GeneratePublicKey(PrivateKey key)
        {
            long[] publicKey = new long[key.B.Length];
            for (long i = 0; i < key.B.Length; i++)
            {
                publicKey[i] = key.B[i] * key.T % key.M;
            }
            return publicKey;
        }
    }
    internal class KnapsackCipher : IAsymmetricCipher<PublicKey, PrivateKey>
    {
        public string Decrypt(string text, PrivateKey key)
        {
            long[] splitedText = text.Split(' ').Select(e => long.Parse(e)).ToArray();
            string binary_text = "", buffer;
            long sum;
            for (long i = 0; i < splitedText.Length; i++)
            {
                sum = splitedText[i] * key.T1 % key.M;
                buffer = "";
                for (long j = key.B.Length - 1; j >= 0; j--)
                {
                    if (sum - key.B[j] < 0)
                    {
                        buffer += "0";
                    }
                    else
                    {
                        sum -= key.B[j];
                        buffer += "1";
                    }
                }
                char[] bufferCharArray = buffer.ToCharArray();
                Array.Reverse(bufferCharArray);
                binary_text += new string(bufferCharArray);
            }
            string[] splitedBinary_text = Regex.Matches(binary_text, ".{1,16}").Select(m => m.Value).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            return string.Join("", splitedBinary_text.Select(e => (char)Convert.ToInt64(e, 2)));
        }

        public string Encrypt(string text, PublicKey key)
        {
            string binary_text = string.Join("", text.Select(s => Convert.ToString(s, 2).PadLeft(16, '0')));
            long[] encryptedText = new long[(long)Math.Ceiling((double)binary_text.Length / key.Key.Length)];

            for (int i = 0; i < binary_text.Length; i++)
            {
                if (binary_text[i] == '1') encryptedText[i / key.Key.Length] += key.Key[i % key.Key.Length];
            }
            return string.Join(" ", encryptedText);
        }
    }
}
