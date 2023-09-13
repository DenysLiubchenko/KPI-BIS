using BIS.Ciphers;
using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BIS.Layouts
{
    internal class CaesarLayout : Layout
    {
        public CaesarLayout(String name, MainWindow mainWindow)
        {
            Name = name;
            MainWindow = mainWindow;
        }
        public override void Show()
        {
            MainWindow.CipherImage.Source = new BitmapImage(new Uri("C:\\Laba\\Project\\OOP\\BIS_Lab2\\BIS_Lab2\\Images\\caesar-g837eaff83_640.png"));
            MainWindow.CaesarGrid.Visibility = System.Windows.Visibility.Visible;
        }
        public override void Decrypt() 
        {
            MainWindow.OutputTextBox.Text = new CaesarCipher().Encrypt(MainWindow.InputTextBox.Text, int.Parse(MainWindow.CaesarKeyTextBox.Text));
        }
        

        public override void Encrypt() 
        {
            MainWindow.OutputTextBox.Text = new CaesarCipher().Decrypt(MainWindow.InputTextBox.Text, int.Parse(MainWindow.CaesarKeyTextBox.Text));
        }
    }
}
