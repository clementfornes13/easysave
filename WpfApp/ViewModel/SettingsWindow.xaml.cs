using System.Windows;
using System.IO;
using System.Windows.Input;

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
            if (File.Exists(CsvFilePath))
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
    }
    public class Extensions
    {
        public string extension { get; set; }
    }
}
