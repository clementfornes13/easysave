using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                var notifyIcon = new NotifyIcon();
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.BalloonTipTitle = "EasySave";
                notifyIcon.BalloonTipText = "EasySave est déjà lancé sur votre ordinateur";
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(0);
                System.Windows.Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }
        public static void CloseApp(Window window)
        {
            window.Close();
        }
        public static void ResizeApp(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
        public static void Window_MouseDown(Window window, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }
    }
    public static class Global
    {
        private static string pathFrom;
        private static string pathTo;

        public static string PathFrom { get => pathFrom; set => pathFrom = value; }
        public static string PathTo { get => pathTo; set => pathTo = value; }
    }
    public class MainWindow1
    {
        public static MainWindow mw = new MainWindow();
        public static void Show()
        {
            mw = new MainWindow();
            mw.Show();
        }
    }
    public class SettingsWindow1
    {
        public static SettingsWindow mw = new SettingsWindow();
        public static void Show()
        {
            mw = new SettingsWindow();
            mw.Show();
        }
    }
    public class CreateWindow1
    {
        public static CreateWindow cw = new CreateWindow();
        public static void Show()
        {
            cw = new CreateWindow();
            cw.Show();
        }
    }
}
