using BIS.Interface;
using BIS.Knapsack;
using BIS.RSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BIS.Layouts
{
    internal class RSALayout : Layout
    {
        public RSALayout(String name, MainWindow mainWindow)
        {
            Name = name;
            MainWindow = mainWindow;
        }
        public override void Decrypt()
        {
            MainWindow.OutputTextBox.Text = new RSACipher().Decrypt(MainWindow.InputTextBox.Text, new RSA.PrivateKey(MainWindow.RSAPrivateKeyTextBox.Text));
        }

        public override void Encrypt()
        {
            MainWindow.OutputTextBox.Text = new RSACipher().Encrypt(MainWindow.InputTextBox.Text, new RSA.PublicKey(MainWindow.RSAPublicKeyTextBox.Text));
        }
        public override void Show()
        {
            MainWindow.CipherImage.Source = new BitmapImage(new Uri("C:\\Laba\\Project\\OOP\\BIS_Lab2\\BIS_Lab2\\Images\\RSA.png"));
            MainWindow.RSAGrid.Visibility = System.Windows.Visibility.Visible;
        }

        internal void GenerateRSAKey_Click(object sender)
        {
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
            MainWindow.RSAPrivateKeyTextBox.Text = cryptoServiceProvider.ToXmlString(true);
            MainWindow.RSAPublicKeyTextBox.Text = cryptoServiceProvider.ToXmlString(false);
        }
    }
}
