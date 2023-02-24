using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace RemoteConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static NetworkStream _networkStream;
        public static TcpClient _socketForServer;
        public static string _clientmessage = "test";
        public static string _ip = "127.0.0.1";

        public Socket _workSocket = null;// Client socket.  
        public const int _BufferSize = 4096;// Size of receive buffer.  
        public byte[] _buffer = new byte[_BufferSize];// Receive buffer.  
        private StringBuilder _sb = new StringBuilder();// Received data string.  

        // The port number for the remote device.  
        private const int _port = 66;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);
        private static ManualResetEvent _receiveDone = new ManualResetEvent(false);

        // The response from the remote device.  
        private static String _response = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_exit(object sender, RoutedEventArgs e)//Function of the button to close the software
        {
            Process.GetCurrentProcess().Kill();
        }

        private void StartClient(object sender, RoutedEventArgs e)//Function to start the function to start the connection with the server.
        {
            try
            {
                StartClient(_ip);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server not found " + ex.ToString());
            }

        }

        public void StartClient(string ip)//Function that allows to start the client with the server and view the backups.
        {
            // Connect to a remote device.  
            try
            {
                // Establish the local endpoint for the socket.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(_ip);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                _connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(client, "getdata");
                _sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(client);
                _receiveDone.WaitOne();

                Save_work.Items.Clear(); //Cleaning the list table
                string request = _response;
                string[] array = request.Split(Environment.NewLine);
                foreach (var obj in array)
                {
                    Save_work.Items.Add(obj); //Displaying backups in the listbox
                }
                Save_work.Items.RemoveAt(array.GetUpperBound(0));

            }
            catch (Exception e)
            {
                // MessageBox.Show(e.ToString());
            }

        }

        private void ConnectCallback(IAsyncResult ar)//Receive the message and call ReceiveCallBack
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                // Signal that the connection has been made.  
                _connectDone.Set();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Receive(Socket client) //Reads and assigns message to response
        {
            try
            {
                // Create the state object.
                _workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(_buffer, 0, _BufferSize, 0, new AsyncCallback(ReceiveCallback), this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)//Function to receive the message
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                Socket client = _workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    _sb.Append(Encoding.ASCII.GetString(_buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(_buffer, 0, _BufferSize, 0, new AsyncCallback(ReceiveCallback), this);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (_sb.Length > 1)
                    {
                        _response = _sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    _receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Send(Socket client, String data)//Function to send a message
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)//Function that allows you to send a message asynchronously. Method linked with the send function
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.  
                _sendDone.Set();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void LoadProgress(object sender, RoutedEventArgs e)//Function to retrieve progress information.
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(_ip);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                _connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(client, "getprogressing" + Save_work.SelectedItem);
                _sendDone.WaitOne();

                Receive(client);
                _receiveDone.WaitOne();
                MessageBox.Show("GET PRGRESSING....");
                progression.Content = "Progressing : " + _response;
            }
            catch
            {

            }
        }
    }
}
