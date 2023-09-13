using BIS.Ciphers;
using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BIS.Layouts
{
    internal class DESLayout : Layout
    {
        public DESLayout(String name, MainWindow mainWindow)
        {
            Name = name;
            MainWindow = mainWindow;
        }
        public override void Decrypt()
        {
            try
            {
                MainWindow.OutputTextBox.Text = new DESCipher().Decrypt(MainWindow.InputTextBox.Text, MainWindow.DESKeyTextBox.Text);
            } catch (ArgumentException) { MessageBox.Show("Key length must equal 64 bites", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public override void Encrypt()
        {
            try
            {
                MainWindow.OutputTextBox.Text = new DESCipher().Encrypt(MainWindow.InputTextBox.Text, MainWindow.DESKeyTextBox.Text);
            } catch(ArgumentException) { MessageBox.Show("Key length must equal 64 bites", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public override void Show()
        {
            MainWindow.CipherImage.Source = new BitmapImage(new Uri("C:\\Laba\\Project\\OOP\\BIS_Lab2\\BIS_Lab2\\Images\\DES.png"));
            MainWindow.DESGrid.Visibility = Visibility.Visible;
        }
    }
}
