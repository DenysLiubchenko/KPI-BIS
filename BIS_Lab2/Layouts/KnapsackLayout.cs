using BIS.Ciphers;
using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using BIS.Knapsack;
using Microsoft.VisualBasic;
using BIS.util;

namespace BIS.Layouts
{
    internal class KnapsackLayout : Layout
    {
        public KnapsackLayout(String name, MainWindow mainWindow)
        {
            Name = name;
            MainWindow = mainWindow;
        }
        public override void Decrypt()
        {
            MainWindow.OutputTextBox.Text = new KnapsackCipher().Decrypt(MainWindow.InputTextBox.Text, 
                GeneratePrivateKey(MainWindow.KnapsackBKeyTextBox.Text, MainWindow.KnapsackMKeyTextBox.Text, MainWindow.KnapsackTKeyTextBox.Text));
        }

        public override void Encrypt()
        {
            
            long[] publicKey = Array.ConvertAll(MainWindow.KnapsackPublicKeyTextBox.Text.Split(" "), s => long.Parse(s));
            if (publicKey.Length == 0)
            {
                MainWindow.OutputTextBox.Text = new KnapsackCipher().Encrypt(MainWindow.InputTextBox.Text,
                    GeneratePublicKey(GeneratePrivateKey(MainWindow.KnapsackBKeyTextBox.Text, MainWindow.KnapsackMKeyTextBox.Text, MainWindow.KnapsackTKeyTextBox.Text)));
            }
            else
            {
                MainWindow.OutputTextBox.Text = new KnapsackCipher().Encrypt(MainWindow.InputTextBox.Text, new PublicKey(publicKey));
            }
        }
        private Knapsack.PrivateKey GeneratePrivateKey (string B, string M, string T)
        {
            long[] long_B = Array.ConvertAll(B.Split(" "), s => long.Parse(s));
            long long_M = long.Parse(M), long_T = long.Parse(T);
            if (B.Contains('-') || long_M < 0 || long_T < 0) throw new InvalidKeyException("All values are needed to be positive");
            if (long_B.Sum() >= long_M) throw new InvalidKeyException("Sum of B must be less than M");
            if (!IsSuperIncreasingSequence(long_B)) throw new InvalidKeyException("Sequence B is not super increasing");
            if (ValidateT(long_M, long_T) == -1) throw new InvalidKeyException("T must not contain common denominator with M or equal 0");
            return new Knapsack.PrivateKey(long_B, long_M, long_T);
        }

        private Knapsack.PublicKey GeneratePublicKey (PrivateKey key)
        {
            return new Knapsack.PublicKey(key);
        }

        private bool IsSuperIncreasingSequence(long[] sequence)
        {
            if (sequence.Length < 2) return true;
            long sum = sequence[0];
            for (int i = 1; i < sequence.Length; i++)
            {
                if (sequence[i] <= sum) return false;
                sum += sequence[i];
            }
            return true;
        }

        private long ValidateT (long m, long t)
        {
            if (m == 1) return 1;
            else if (t == 0) return -1;
            return ValidateT(t, m%t);
        }

        public override void Show()
        {
            MainWindow.CipherImage.Source = new BitmapImage(new Uri("C:\\Laba\\Project\\OOP\\BIS_Lab2\\BIS_Lab2\\Images\\Knapsack.jpg"));
            MainWindow.KnapsackGrid.Visibility = System.Windows.Visibility.Visible;
        }
        public void GeneratePublicKey_Click(object sender)
        {
            PublicKey key = GeneratePublicKey(GeneratePrivateKey(MainWindow.KnapsackBKeyTextBox.Text, MainWindow.KnapsackMKeyTextBox.Text, MainWindow.KnapsackTKeyTextBox.Text));
            MainWindow.KnapsackPublicKeyTextBox.Text = string.Join(" ", Array.ConvertAll(key.Key, s => s.ToString()));
        }
    }
}
