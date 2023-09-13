using BIS;
using BIS.Interface;
using BIS.Layouts;
using BIS.util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileWorker FileTools;
        private Dictionary<String, Layout> Layouts;
        private Layout CurrentLayout;
        public MainWindow()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();
            FileTools = new FileWorker();
            Layouts = new Dictionary<String, Layout>
            {
                {"Caesar", new CaesarLayout("Caesar", this)},
                {"Trithemius", new TrithemiusLayout("Trithemius", this)},
                {"XOR", new XORLayout("XOR", this)},
                {"Book", new BookLayout("Book", this) },
                {"DES", new DESLayout("DES", this) },
                {"Knapsack", new KnapsackLayout("Knapsack", this) },
                {"RSA", new RSALayout("RSA", this) }
            };
            CurrentLayout = Layouts.GetValueOrDefault("Caesar");
            
            CurrentLayout.Show();
            
            foreach (KeyValuePair<string, Layout> e in Layouts)
            {
                MenuItem item = new MenuItem();
                item.Header = e.Key;
                item.Click += ChangeCipher;
                MethodMenuItem.Items.Add(item);
            }
        }

        private void ChangeCipher (object sender, RoutedEventArgs e)
        {
            foreach (Grid g in KeyGrids.Children)
            {
                g.Visibility = Visibility.Collapsed;
            }
            MenuItem menuItem = sender as MenuItem;
            CurrentLayout = Layouts.GetValueOrDefault(menuItem.Header.ToString());
            CurrentLayout.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(FileTools.AboutMe(), "Info about me", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileTools.CreateFile(SaveFileDialog(), OutputTextBox.Text);
            }
            catch (FileNotFoundException) { MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InputTextBox.Text = FileTools.OpenFile(OpenFileDialog());
            }
            catch (FileNotFoundException) { MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileTools.SaveFile(SaveFileDialog(), OutputTextBox.Text);
            }
            catch (FileNotFoundException) { MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ExportFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileTools.ExportFile(SaveFileDialog(), OutputTextBox.Text);
            }
            catch (FileNotFoundException) { MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentLayout.Encrypt();
            }
            catch (InvalidKeyException error) { MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("Your key has a wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentLayout.Decrypt();
            }
            catch (InvalidKeyException error) { MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("Your key has a wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private string SaveFileDialog()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }
            throw new FileNotFoundException();
        }
        private string OpenFileDialog ()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }
            throw new FileNotFoundException();
        }
        private void DropImport(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                TextBox input = FindName("InputTextBox") as TextBox;
                input.Text = File.ReadAllText(files[0]);
            }
        }
        private void TextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
        private void GeneratePublicKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KnapsackLayout kl = (KnapsackLayout)CurrentLayout;
                kl.GeneratePublicKey_Click(sender);
            }
            catch (InvalidKeyException error) { MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("Your key has a wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void GenerateRSAKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RSALayout kl = (RSALayout)CurrentLayout;
                kl.GenerateRSAKey_Click(sender);
            }
            catch (InvalidKeyException error) { MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("Your key has a wrong format", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
