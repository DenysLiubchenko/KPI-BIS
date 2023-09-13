using BIS.Ciphers;
using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BIS.Layouts
{
    internal class XORLayout : Layout
    {
        public XORLayout(String name, MainWindow mainWindow)
        {
            Name = name;
            MainWindow = mainWindow;
        }
        public override void Decrypt()
        {
            MainWindow.OutputTextBox.Text = new XORCipher().Decrypt(MainWindow.InputTextBox.Text, int.Parse(MainWindow.XORKeyTextBox.Text));
        }

        public override void Encrypt()
        {
            MainWindow.OutputTextBox.Text = new XORCipher().Encrypt(MainWindow.InputTextBox.Text, int.Parse(MainWindow.XORKeyTextBox.Text));
        }

        public override void Show()
        {
            MainWindow.CipherImage.Source = new BitmapImage(new Uri("C:\\Laba\\Project\\OOP\\BIS_Lab2\\BIS_Lab2\\Images\\XOR.png"));
            MainWindow.XORGrid.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
