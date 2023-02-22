using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Documents;
using EasySaveModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace WpfApp
{
    public partial class SettingsWindow : Window
    {
        private const string EncryptExtension = "extensionencrypt.csv";
        private string _BusinessAppName;
        private List<string> extensionsList = new List<string>();
        private string CryptoSoftPath;
        public string BusinessAppName { get => _BusinessAppName; set => _BusinessAppName = value; }

        public SettingsWindow()
        {
            InitializeComponent();
            LoadExtensionsFromCsv();
        }
        public void AddExtensionButtonClick(object sender, RoutedEventArgs e)
        {
            string extension = ExtensionBox.Text;
            if (string.IsNullOrWhiteSpace(extension))
            {
                ExtensionLabelError.Content = "Erreur, vide";
                ExtensionLabelSuccess.Content = null;
                return;
            }
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }
            foreach (Extensions item in ExtensionsGrid.Items)
            {
                if (item.Extension == extension)
                {
                    ExtensionLabelError.Content = "Extensions déjà ajoutée";
                    ExtensionLabelSuccess.Content = null;
                    return;
                }
            }
            ExtensionsGrid.Items.Add(new Extensions { Extension = extension });
            ExtensionLabelSuccess.Content = "Extension ajoutée avec succès";
            ExtensionLabelError.Content = null;
            extensionsList.Add(extension);
            Debug.WriteLine(extensionsList);
            SaveExtensionsToCsv();
        }
        public void DeleteExtensionButtonClick(object sender, RoutedEventArgs e )
        {
            Extensions selectedExtension = ExtensionsGrid.SelectedItem as Extensions;
            if (selectedExtension != null )
            {
                ExtensionsGrid.Items.Remove(selectedExtension);
                SaveExtensionsToCsv();
                extensionsList.Remove(selectedExtension.Extension);
            }
        }
        private void GotFocusExtension(object sender, RoutedEventArgs e)
        {
            ExtensionBox.Text = "";
        }
        private void SaveExtensionsToCsv()
        {
            using (StreamWriter writer = new StreamWriter(EncryptExtension))
            {
                foreach (Extensions item in ExtensionsGrid.Items) 
                {
                    
                    writer.WriteLine(item.Extension);
                }
            }
        }
        private void LoadExtensionsFromCsv()
        {
            if (File.Exists(EncryptExtension))
            {
                using (StreamReader reader = new StreamReader(EncryptExtension))
                {
                    string ligne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        ExtensionsGrid.Items.Add(new Extensions { Extension = ligne });
                    }
                }
            }
        }
        public void SaveMaxTransfertButtonClick(object sender, RoutedEventArgs e)
        {

        }
        public void SaveBusinessAppButtonClick(object sender, RoutedEventArgs e)
        {
            BusinessAppName = SaveBusinessApp.Text;
        }
        public void SaveCryptoSoftClick(object sender, RoutedEventArgs e)
        {
            CryptoSoftPath=CryptoSoftTextbox.Text;
        }
        public void BackMenuButtonClickSettings(object sender, RoutedEventArgs e)
        {
            MainWindow1.Show();
            Close();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.CloseApp(this);
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            App.ResizeApp(this);
        }
        private void Window_MouseDownClick(object sender, MouseButtonEventArgs e)
        {
            App.Window_MouseDown(this, e);
        }
        private void ChooseCryptosoftPathClick(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog cryptosoftdialog = new System.Windows.Forms.OpenFileDialog())
            {
                cryptosoftdialog.Filter = "Executables (*.exe)|*.exe";
                if (cryptosoftdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CryptoSoftTextbox.Text = cryptosoftdialog.FileName;
                }
            }
        }
    }

    public class Extensions
    {
        public string Extension { get; set; }
    }
}
