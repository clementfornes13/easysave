using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EasySaveModel;

namespace WpfApp
{
    public partial class SettingsWindow : Window
    {
        private const string SettingsFilePath = "settings.csv";
        private List<string> _extensions = new List<string>();
        private AppSettings appSettings;


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
                if (item.extension == extension)
                {
                    ExtensionLabelError.Content = "Extensions déjà ajoutée";
                    ExtensionLabelSuccess.Content = null;
                    return;
                }
            }
            ExtensionsGrid.Items.Add(new Extensions { extension = extension });
            ExtensionLabelSuccess.Content = "Extension ajoutée avec succès";
            ExtensionLabelError.Content = null;
            SaveExtensionsToCsv();
        }
        public void DeleteExtensionButtonClick(object sender, RoutedEventArgs e )
        {
            Extensions selectedExtension = ExtensionsGrid.SelectedItem as Extensions;
            if (selectedExtension != null )
            {
                ExtensionsGrid.Items.Remove(selectedExtension);
                SaveExtensionsToCsv();
            }
        }
        private void GotFocusExtension(object sender, RoutedEventArgs e)
        {
            ExtensionBox.Text = "";
        }
        private void SaveExtensionsToCsv()
        {
            using (StreamWriter writer = new StreamWriter(SettingsFilePath))
            {
                foreach (Extensions item in ExtensionsGrid.Items) 
                {
                    _extensions.Add(item.extension);
                }
                appSettings = new AppSettings();
                appSettings.ExtensionEncrypt = string.Join(",", _extensions);
                writer.WriteLine(appSettings.ExtensionEncrypt);
            }
            appSettings.SaveSettings();
        }

        private void LoadExtensionsFromCsv()
        {
            if (File.Exists(SettingsFilePath))
            {
                using (StreamReader reader = new StreamReader(SettingsFilePath))
                {
                    string ligne;
                    if ((ligne=reader.ReadLine()) != null)
                    {
                        appSettings = new AppSettings();
                        appSettings.ExtensionEncrypt = ligne.Split(',')[0];
                        string[] extensions = ligne.Split(',');
                        foreach (string extension in extensions)
                        {
                            ExtensionsGrid.Items.Add(new Extensions { extension = extension });
                        }
                    }
                }
                ExtensionsGrid.Items.RemoveAt(0);
                appSettings.ExtensionEncrypt = null;
            }
        }
        public void SaveMaxTransfertButtonClick(object sender, RoutedEventArgs e)
        {

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

        public static string SettingsFilePath1 => SettingsFilePath;

        public List<string> Extensions { get => _extensions; set => _extensions = value; }
    }

    public class Extensions
    {
        public string extension { get; set; }
    }
}
