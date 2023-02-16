using System.IO;
using System.Windows;
using System.Windows.Input;
using EasySaveModel;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private VisualModel model;
        internal VisualModel Model { get => model; set => model = value; }

        public MainWindow()
        {
            InitializeComponent();
            JobsGrid.ItemsSource = CreateWindow.JobsProps;
            LoadJobsPropsFromCsv();
        }
        private void CreateWindowButtonClick(object sender, RoutedEventArgs e) //Bouton creer
        {
            CreateWindow1.Show();
            Close();
        }
        private void LaunchMainButtonClick(object sender, RoutedEventArgs e)
        {
            /*foreach (Jobs item in JobsGrid.ItemsSource)
            {
                if (((System.Windows.Controls.CheckBox)CheckboxColumn.GetCellContent(item)).IsChecked == true)
                {
                    GridFromTo.ColumnPathTo1 = ((System.Windows.Controls.TextBlock)PathToColumn.GetCellContent(item)).Text;
                    GridFromTo.ColumnPathFrom1 = ((System.Windows.Controls.TextBlock)PathFromColumn.GetCellContent(item)).Text;
                    ctl.addWorkingFiles();
                }
            }
            model.createJob();*/
        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow1.Show();
            Close();
        }
        private void PauseButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void StopButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteMainButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void EnglishButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void FrenchButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void LoadJobsPropsFromCsv()
        {
            if (!File.Exists(CreateWindow.CsvFilePath1))
            {
                return;
            }
            CreateWindow.JobsProps.Clear();
            StreamReader reader = new StreamReader(CreateWindow.CsvFilePath1);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string[] props = reader.ReadLine().Split(',');
                Jobs job = new Jobs();
                job.Nom = props[0];
                job.Source = props[1];
                job.Destination = props[2];
                job.Cryptosoft = bool.Parse(props[3]);
                job.Progressbar = double.Parse(props[4]);
                job.Checkbox = bool.Parse(props[5]);
                CreateWindow.JobsProps.Add(job);
            }
            JobsGrid.ItemsSource = CreateWindow.JobsProps;
            reader.Close();
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
    public class Jobs
    {
        public string Nom { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public bool Cryptosoft { get; set; }
        public double Progressbar { get; set; }
        public bool Checkbox { get; set; }
    }
    public static class GridFromTo
    {
        private static string ColumnPathFrom;
        private static string ColumnPathTo;

        public static string ColumnPathFrom1 { get => ColumnPathFrom; set => ColumnPathFrom = value; }
        public static string ColumnPathTo1 { get => ColumnPathTo; set => ColumnPathTo = value; }
    }
}
