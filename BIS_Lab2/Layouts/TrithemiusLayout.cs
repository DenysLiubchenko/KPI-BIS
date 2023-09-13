using BIS.Ciphers;
using BIS.Interface;
using BIS.Trithemius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BIS.Layouts
{
    internal class TrithemiusLayout : Layout
    {
        public TrithemiusLayout(String name, MainWindow mainWindow)
        {
            Name = name;
            MainWindow = mainWindow;
            MainWindow.TrithemiusComboBox.SelectionChanged += TrithemiusComboBox_SelectionChanged;
        }

        private void TrithemiusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Grid g in MainWindow.TrithemiusMethodGrids.Children)
            {
                g.Visibility = Visibility.Collapsed;
                if (((ComboBoxItem)MainWindow.TrithemiusComboBox.SelectedItem).Tag.ToString()==g.Tag.ToString())
                {
                    g.Visibility = Visibility.Visible;
                }
            }
        }

        public override void Show()
        {
            MainWindow.CipherImage.Source = new BitmapImage(new Uri("C:\\Laba\\Project\\OOP\\BIS_Lab2\\BIS_Lab2\\Images\\Trithemius.png"));
            MainWindow.TrithemiusGrid.Visibility = System.Windows.Visibility.Visible;
        }

        public override void Decrypt() 
        {
            string type = ((ComboBoxItem)MainWindow.TrithemiusComboBox.SelectedItem).Tag.ToString();
            string key = "";
            if (type == "2DV")
            {
                key = MainWindow.Trithemius2D2TextBox.Text + ";" + MainWindow.Trithemius2D1TextBox.Text;
            }
            else if (type == "3DV")
            {
                key = MainWindow.Trithemius3D3TextBox.Text + ";" + MainWindow.Trithemius3D2TextBox.Text + ";" + MainWindow.Trithemius3D1TextBox.Text;
            }
            else if (type == "Slgn")
            {
                key = MainWindow.TrithemiusSloganKeyTextBox.Text;
            }
            if (key == "") throw new ArgumentNullException();
            MainWindow.OutputTextBox.Text = new TrithemiusCipher().Encrypt(MainWindow.InputTextBox.Text, new Trithemius.Key(key, type));
        }
        public override void Encrypt() 
        {
            string type = ((ComboBoxItem)MainWindow.TrithemiusComboBox.SelectedItem).Tag.ToString();
            string key = "";
            if (type == "2DV")
            {
                key = MainWindow.Trithemius2D2TextBox.Text + ";" + MainWindow.Trithemius2D1TextBox.Text;
            }
            else if (type == "3DV")
            {
                key = MainWindow.Trithemius3D3TextBox.Text + ";" + MainWindow.Trithemius3D2TextBox.Text + ";" + MainWindow.Trithemius3D1TextBox.Text;
            }
            else if (type == "Slgn")
            {
                key = MainWindow.TrithemiusSloganKeyTextBox.Text;
            }
            if (key == "") throw new ArgumentNullException();
            MainWindow.OutputTextBox.Text = new TrithemiusCipher().Decrypt(MainWindow.InputTextBox.Text, new Trithemius.Key(key, type));
        }
    }
}
