using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace WpfApp
{
    public partial class SettingsWindow : Window
    {
        private const string EncryptExtension = "extensionencrypt.csv";
        private const string EncryptExtensionPrio = "extensionencryptprio.csv";
        private string _BusinessAppName;
        private List<string> extensionsList = new List<string>();
        private List<string> extensionsPrioList = new List<string>();
        private uint _MaxSizeTransfert;
        private string _CryptoSoftPath;
        public string BusinessAppName { get => _BusinessAppName; set => _BusinessAppName = value; }
        public string CryptoSoftPath { get => _CryptoSoftPath; set => _CryptoSoftPath = value; }
        public uint MaxSizeTransfert { get => _MaxSizeTransfert; set => _MaxSizeTransfert = value; }

        public SettingsWindow()
        {
            InitializeComponent();
            LoadExtensionsFromCsv();
            LoadExtensionsPrioFromCsv();
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
            ExtensionLabelSuccess.Content = "Extension ajoutée avec succès";
            ExtensionLabelError.Content = null;
            extensionsList.Add(extension);
            ExtensionsGrid.Items.Add(new Extensions { Extension = extension });
            Debug.WriteLine(extensionsList);
            SaveExtensionsToCsv();
        }
        public void DeleteExtensionButtonClick(object sender, RoutedEventArgs e )
        {
            if (ExtensionsGrid.SelectedItem is Extensions selectedExtension)
            {
                ExtensionsGrid.Items.Remove(selectedExtension);
                SaveExtensionsToCsv();
                extensionsList.Remove(selectedExtension.Extension);
            }
        }
        public void AddExtensionPrioButtonClick( object sender, RoutedEventArgs e )
        {
            string extensionprio = PrioritaryFilesBox.Text;
            if (string.IsNullOrWhiteSpace(extensionprio))
            {
                ExtensionPrioLabelError.Content = "Erreur, vide";
                ExtensionPrioLabelSuccess.Content = null;
                return;
            }
            if (!extensionprio.StartsWith("."))
            {
                extensionprio = "." + extensionprio;
            }
            foreach (ExtensionsPrio item2 in PrioritaryGrid.Items)
            {
                if (item2.ExtensionPrio == extensionprio)
                {
                    ExtensionPrioLabelError.Content = "Extensions déjà ajoutée";
                    ExtensionPrioLabelSuccess.Content = null;
                    return;
                }
            }
            ExtensionPrioLabelSuccess.Content = "Extension ajoutée avec succès";
            ExtensionPrioLabelError.Content = null;
            extensionsPrioList.Add(extensionprio);
            PrioritaryGrid.Items.Add(new ExtensionsPrio { ExtensionPrio = extensionprio });
            Debug.WriteLine(extensionsPrioList);
            SaveExtensionsPrioToCsv();
        }
        public void DeleteExtensionPrioButtonClick(object sender, RoutedEventArgs e)
        {
            if (PrioritaryGrid.SelectedItem is ExtensionsPrio selectedExtension)
            {
                PrioritaryGrid.Items.Remove(selectedExtension);
                SaveExtensionsPrioToCsv();
                extensionsPrioList.Remove(selectedExtension.ExtensionPrio);
            }
        }
        private void GotFocusExtension(object sender, RoutedEventArgs e)
        {
            ExtensionBox.Text = "";
            PrioritaryFilesBox.Text = "";
            MaximumSizeTransfert.Text = "";
            CryptoSoftTextbox.Text = "";
            SaveBusinessApp.Text = "";
        }
        private void SaveExtensionsPrioToCsv()
        {
            using (StreamWriter writer2 = new StreamWriter(EncryptExtensionPrio))
            {
                foreach (ExtensionsPrio item2 in PrioritaryGrid.Items) 
                {
                    
                    writer2.WriteLine(item2.ExtensionPrio);
                }
            }
        }
        private void LoadExtensionsPrioFromCsv()
        {
            if (File.Exists(EncryptExtensionPrio))
            {
                using (StreamReader reader2 = new StreamReader(EncryptExtensionPrio))
                {
                    string ligne2;
                    while ((ligne2 = reader2.ReadLine()) != null)
                    {
                        PrioritaryGrid.Items.Add(new ExtensionsPrio { ExtensionPrio = ligne2 });
                        extensionsPrioList.Add(ligne2);
                    }
                }
            }
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
                        extensionsList.Add(ligne);
                    }
                }
            }
        }
        public void SaveMaxTransfertButtonClick(object sender, RoutedEventArgs e)
        {
            if (MaximumSizeTransfert.Text.GetType() == typeof(uint))
            {
                MaxSizeTransfert = uint.Parse(MaximumSizeTransfert.Text);
            }
            else
            {
                MessageBox.Show("FAUXXXXXXXXXXXXXXXX");
            }
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
    public class ExtensionsPrio
    {
        public string ExtensionPrio { get; set; }
    }
}
