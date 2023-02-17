using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using EasySaveModel;
using CheckBox = System.Windows.Controls.CheckBox;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private SaveFiles _savefiles;
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
            foreach (SaveFiles item in JobsGrid.ItemsSource)
            {
                if (((CheckBox)CheckboxColumn.GetCellContent(item)).IsChecked == true)
                {
                    GridFromTo.ColumnPathTo1 = ((TextBlock)PathToColumn.GetCellContent(item)).Text;
                    GridFromTo.ColumnPathFrom1 = ((TextBlock)PathFromColumn.GetCellContent(item)).Text;
                    VisualModel model = new VisualModel();
                    model.createJob();
                }
            }
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
        private void LogicielMetier()
        {
            // Faire un fichier settings pour extensions, logiciel metier, max transfert size --> revoir methodes
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            CreateWindow.JobsProps.Clear();
            CreateWindow.JobsProps = (System.Collections.Generic.List<SaveFiles>)JobsGrid.ItemsSource;
            CreateWindow cw = new CreateWindow();
            cw.SaveJobsPropsToCsv();
            JobsGrid.Items.Refresh();
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
                SaveFiles saveFiles = new SaveFiles(props[1], props[2], props[0], bool.Parse(props[3])) ;
                CreateWindow.JobsProps.Add(saveFiles);

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
        public double Progression { get; set; } //A ajouter (transfertstate)
    }
    public static class GridFromTo
    {
        private static string ColumnPathFrom;
        private static string ColumnPathTo;

        public static string ColumnPathFrom1 { get => ColumnPathFrom; set => ColumnPathFrom = value; }
        public static string ColumnPathTo1 { get => ColumnPathTo; set => ColumnPathTo = value; }
    }
}
