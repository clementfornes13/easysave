using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var langCode = WpfApp.Properties.Settings.Default.languageCode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langCode);
            Process thisProc = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                var notifyIcon = new NotifyIcon();
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.BalloonTipTitle = "EasySave";
                notifyIcon.BalloonTipText = "EasySave est déjà lancé sur votre ordinateur";
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(0);
                Current.Shutdown();
                return;
            }
            base.OnStartup(e);
            //SocketListener.StartServer();
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
    public class SocketListener
    {
        public static void StartServer()
        {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 21);
            try
            {
                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);

                System.Windows.MessageBox.Show("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                System.Windows.MessageBox.Show("Text received : {0}", data);
                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }
    }
}
