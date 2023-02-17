using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;

namespace WpfApp
{
    public partial class CreateWindow : Window
    {
        private const string CsvFilePath = "jobsproperties.csv";
        private static List<Jobs> jobsProps = new List<Jobs>();
        public static List<Jobs> JobsProps { get => jobsProps; set => jobsProps = value; }
        public static string CsvFilePath1 => CsvFilePath;

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
                    Global.PathFrom = sourceDialog.SelectedPath;
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
                    Global.PathTo = destinationDialog.SelectedPath;
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
            string typesave = "";
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
                typesave = "Différentiel";
            }
            else
            {
                typesave = "Séquentiel";
            }
            Jobs job = new Jobs
            {
                Nom = NameTextBox.Text,
                Source = TextBlockSource.Text,
                Destination = TextBlockDestination.Text,
                Cryptosoft = CryptoSoftCheckBox.IsChecked == true,
                Type = typesave,
                Progressbar = 0,
                Checkbox = false
            };
            JobsProps.Add(job);
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
                writer.WriteLine("Nom,Source,Destination,Cryptosoft,Type,Progressbar,Checkbox");
                foreach (var job in JobsProps)
                {
                    writer.WriteLine(job.Nom + ","
                        + job.Source + ","
                        + job.Destination + ","
                        + job.Cryptosoft + ","
                        + job.Type + ","
                        + job.Progressbar + ","
                        + job.Checkbox);
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