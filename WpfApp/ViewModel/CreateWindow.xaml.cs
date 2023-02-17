using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using EasySaveModel;

namespace WpfApp
{
    public partial class CreateWindow : Window
    {
        private const string CsvFilePath = "jobsproperties.csv";
        private static List<SaveFiles> jobsProps = new List<SaveFiles>();
        public static List<SaveFiles> JobsProps { get => jobsProps; set => jobsProps = value; }
        public static string CsvFilePath1 => CsvFilePath;
        private SaveFiles _savefiles;

        public CreateWindow()
        {
            InitializeComponent();
        }
        public void ChooseFromButtonClick(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog sourceDialog = new FolderBrowserDialog())
            {
                sourceDialog.Description = "Source";
                if (sourceDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TextBlockSource.Text = sourceDialog.SelectedPath;
                    GridFromTo.ColumnPathFrom1 = sourceDialog.SelectedPath;
                }
            }
        }
        public void ChooseToButtonClick(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog destinationDialog = new FolderBrowserDialog())
            {
                destinationDialog.Description = "Destination";
                if (destinationDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TextBlockDestination.Text = destinationDialog.SelectedPath;
                    GridFromTo.ColumnPathTo1 = destinationDialog.SelectedPath;
                }
            }
        }
        public void BackMenuButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow1.Show();
            Close();
        }
        public void CreateButtonClick(object sender, RoutedEventArgs e)
        {
           /* string typesave = "";
            if (DifferentialCheckBox.IsChecked == false && SequentialCheckBox.IsChecked == false)
            {
                System.Windows.MessageBox.Show("Erreur, aucun type de sauvegarde n'est choisi");
                return;
            }
            if (DifferentialCheckBox.IsChecked == true && SequentialCheckBox.IsChecked == true)
            {
                System.Windows.MessageBox.Show("Erreur, aucun type de sauvegarde n'est choisi");
                return;
            }
            if (DifferentialCheckBox.IsChecked == true)
            {
                typesave = "D";
            }
            else
            {
                typesave = "S";
            }*/
            SaveFiles sf = new SaveFiles(TextBlockSource.Text, TextBlockDestination.Text, NameTextBox.Text, CryptoSoftCheckBox.IsChecked == true);
            JobsProps.Add(sf);
            SaveJobsPropsToCsv();
        }
        private void GotFocusName(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.IsFocused)
            {
                NameTextBox.Text = "";
            }
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        public void SaveJobsPropsToCsv()
        {
            using (var writer = new StreamWriter(CsvFilePath))
            {
                writer.WriteLine("Nom,Source,Destination,Cryptosoft");
                foreach (var sf in JobsProps)
                {
                    writer.WriteLine(_savefiles.Nom + ","
                        + _savefiles.PathFrom + ","
                        + _savefiles.PathTo + ","
                        + _savefiles.Cryptosoft + ",");
                }
            }
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
}