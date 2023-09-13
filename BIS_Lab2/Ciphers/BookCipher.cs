using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BIS.Ciphers
{
    internal class BookCipher : ICipher<String>
    {
        public string Decrypt(string text, string key)
        {
            text = text.ToLower();
            key = key.ToLower();
            string result = "";
            string[] splitedKey = SplitStringWithAllSybols(key, '\n'),  splitedText = text.Split(',');

            for (int i = 0; i < splitedText.Length; i++)
            {
                result += splitedKey[int.Parse(splitedText[i].Split('/')[0])][int.Parse(splitedText[i].Split('/')[1])];
            }
            return result;
        }

        public string Encrypt(string text, string key)
        {
            text = text.ToLower();
            key = key.ToLower();
            Dictionary<char, List<string>> chars = UsageOfChars(key);
            string result = "";
            Random random = new Random();

            for (int i = 0; i < text.Length; i++)
            {
                result += chars[text[i]][random.Next(chars[text[i]].Count)] + ",";
            }

            return result.Substring(0, result.Length-1);

        }

        private Dictionary<char, List<string>> UsageOfChars(string text)
        {
            string[] splitedText = SplitStringWithAllSybols(text, '\n');

            Dictionary<char, List<string>> charsUsageDictionary = new Dictionary<Char, List<String>>();

            for (int i = 0; i < splitedText.Length; i++)
            {
                for (int j = 0; j < splitedText[i].Length; j++)
                {
                    List<string> list = charsUsageDictionary.GetValueOrDefault(splitedText[i][j], new List<string>());
                    list.Add ( i + "/" + j );
                    if (list.Count == 1)
                        charsUsageDictionary.Add(splitedText[i][j], list);
                    else
                        charsUsageDictionary[splitedText[i][j]] = list;
                }
            }

            return charsUsageDictionary;
        }

        private string[] SplitStringWithAllSybols(string text, char symbol)
        {
            string[] splitedText = text.Split(symbol);
            for (int i = 0; i < splitedText.Length; i++)
            {
                splitedText[i] += symbol;
            }
            return splitedText;
        }
    }
}
