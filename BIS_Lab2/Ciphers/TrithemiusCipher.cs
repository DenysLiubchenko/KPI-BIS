using BIS.Interface;

namespace BIS.Trithemius
{
    internal class Key
    {
        public string KeyValue { get; }
        public string Type { get; }

        public Key(string key, string type)
        {
            KeyValue = key;
            Type = type;
        }
    }
    internal class TrithemiusCipher : ICipher<Key>
    {
        private long Function(long p, long a, long b, long c)
        {
            return a * p * p + b * p + c;
        }
        private char Slogan (int p, Key key)
        {
            return key.KeyValue[p % key.KeyValue.Length];
        }
        private string Crypt(string text, Key key, bool boolean)
        {
            char[] chars = text.ToCharArray();
            if (key.Type == "2DV" || key.Type == "3DV")
            {
                string[] keys = key.KeyValue.Split(';');
                long a = 0, b, c;
                if (key.Type == "3DV")
                {
                    a = long.Parse(keys[2]);
                }
                b = long.Parse(keys[1]);
                c = long.Parse(keys[0]);
                if (!boolean)
                {
                    a = -a;
                    b = -b;
                    c = -c;
                }
                for (int i = 0; i < chars.Length; i++)
                {
                    chars[i] = (char)(chars[i] + Function(i, a, b, c));
                }
            }
            else if (key.Type == "Slgn")
            {
                if (boolean)
                {
                    for (int i = 0; i < chars.Length; i++)
                    {
                        chars[i] = (char)(chars[i] + Slogan(i, key));
                    }
                }
                else
                {
                    for (int i = 0; i < chars.Length; i++)
                    {
                        chars[i] = (char)(chars[i] - Slogan(i, key));
                    }
                }
            }
            return new string(chars);
        }
        public string Encrypt(string text, Key key)
        {
            return Crypt(text, key, true);
        }
        public string Decrypt(string text, Key key)
        {
            return Crypt(text, key, false);
        }
    }
}
