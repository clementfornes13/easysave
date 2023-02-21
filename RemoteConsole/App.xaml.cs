using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RemoteConsole;

namespace RemoteConsole
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mw = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
                BackgroundWorker Job = new BackgroundWorker();
                Job.WorkerReportsProgress = true;
                Job.DoWork += mw.Job_work;
                Job.ProgressChanged += mw.Job_ProgressChanged;
                Job.RunWorkerAsync();
                mw.State = true;
                base.OnStartup(e);
                //SocketClient.StartClient();

        }
    }
    public class SocketClient
    {
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];

            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 21);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);
                    MessageBox.Show("Socket connected to {0}");
                        sender.RemoteEndPoint.ToString();

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    MessageBox.Show("Echoed test = {0}", 
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show("ArgumentNullException{0}", ane.ToString());

                }
                catch (SocketException se)
                {
                    MessageBox.Show("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unexpected exception : {0}", e.ToString());

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
