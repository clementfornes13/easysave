using System.Windows;
using System.IO;

namespace WpfApp
{
    public partial class SettingsWindow : Window
    {
        private const string CsvFilePath = "extensions.csv";
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
            if (ExtensionBox != null)
            {
                ExtensionBox.Text = null;
            }
        }
        private void SaveExtensionsToCsv()
        {
            using (StreamWriter writer = new StreamWriter(CsvFilePath))
            {
                foreach (Extensions item in ExtensionsGrid.Items) 
                {
                    writer.WriteLine(item.extension);
                }
            }
        }
        private void LoadExtensionsFromCsv()
        {
            try
            {
                using (StreamReader reader = new StreamReader(CsvFilePath))
                {
                    string ligne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        ExtensionsGrid.Items.Add(new Extensions { extension = ligne });
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
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

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            App.FullScreenApp(this);
        }
    }
    public class Extensions
    {
        public string extension { get; set; }
    }
}
