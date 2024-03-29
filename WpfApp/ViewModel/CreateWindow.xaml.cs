﻿using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using EasySaveModel;

namespace WpfApp
{
    public partial class CreateWindow : Window
    {
        private const string _csvFilePath = "jobsproperties.csv";
        private SaveFiles _savefiles;
        public string _errorLabelCopy;

        public CreateWindow()
        {
            InitializeComponent();
            _errorLabelCopy = ErrorLabel.ContentStringFormat;
        }
        public void ChooseFromButtonClick(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog sourceDialog = new FolderBrowserDialog())
            {
                sourceDialog.Description = "Source";
                if (sourceDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TextBlockSource.Text = sourceDialog.SelectedPath;
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
            if (TextBlockDestination.Text == "" || TextBlockSource.Text =="")
            {
                ErrorLabel.Content = _errorLabelCopy;
            }
            else 
            {
                ErrorLabel.Content = "";
                _savefiles = new SaveFiles(TextBlockSource.Text, TextBlockDestination.Text, NameTextBox.Text, CryptoSoftCheckBox.IsChecked == true);
                MainWindow.JobsProps.Add(_savefiles);
                SaveJobsPropsToCsv();
            }
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
                writer.WriteLine("Source,Destination,Name,Cryptosoft");
                foreach (var _savefiles in MainWindow.JobsProps)
                {
                    writer.WriteLine(_savefiles.PathFrom + ","
                        + _savefiles.PathTo + ","
                        + _savefiles.Name + ","
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

        public static string CsvFilePath => _csvFilePath;
    }
}