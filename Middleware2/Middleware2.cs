using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
namespace Middleware2
{
    public static class ISynchronizeInvokeExtensions
    {
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }
    }
    public partial class Middleware2 : Form
    {

        
        public Middleware2()
        {
            InitializeComponent();
        }

        const int myPort = 8083;
        int mescount = 1;
        int recNum = 0;
        List<Tuple<string, int>> recBuffer = new List<Tuple<string, int>>();
        private async void ReceiveMulticast()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Determine the IP address of localhost
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    break;
                }
            }

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, myPort);

            // Create a TCP/IP socket for receiving message from the Network.
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start(10);

            try
            {
                string data = null;

                // Start listening for connections.
                while (true)
                {
                    Debug.WriteLine("Waiting for a connection...");

                    // Program is suspended while waiting for an incoming connection.
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();

                    //Console.WriteLine("connectted");
                    data = null;

                    // Receive one message from the network
                    while (true)
                    {
                        bytes = new byte[1024];
                        NetworkStream readStream = tcpClient.GetStream();
                        int bytesRec = await readStream.ReadAsync(bytes, 0, 1024);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        //ReceivedBox.Items.Add(data);
                        // All messages ends with "<EOM>"
                        // Check whether a complete message has been received
                        if (data.IndexOf("<seqNum>") > -1)
                        {
                            string[] full_msg = data.Split(',');
                            string msg = full_msg[0];
                            msg = msg.Substring(0, msg.Length - 1);
                            string flag = full_msg[1];
                            int number = Int32.Parse(flag.Substring(0, flag.IndexOf("<")));
                            Tuple<string, int> store_msg = new Tuple<string, int>(msg, number);
                            recBuffer.Add(store_msg);
                            break;
                        }
                        else if (data.IndexOf("<EOM>") > -1)
                        {
                            ReceivedBox.Items.Add(data);
                            //seqNum += 1;
                            //sendSequence(data);
                            //confirm_seq();
                            //Debug.WriteLine(number.ToString());
                            //Debug.WriteLine("The Tuple: " + store_msg);
                            break;
                        }
                    }
                    Debug.WriteLine("msg received:    {0}", data);
                }

            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.ToString());
            }
        }

        // This method first sets up a task for receiving messages from the Network.
        // Then, it sends a multicast message to the Netwrok.
        public void sendMessage()
        {
            // Sets up a task for receiving messages from the Network.
            // Send a multicast message to the Network
            try
            {
                // Find the IP address of localhost
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8081);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);
                    string timestamp = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"); ;
                    string full_message = "Msg #" + mescount.ToString() + " from Middleware 2 : " + timestamp + " <EOM>\n";
                    // Generate and encode the multicast message into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(full_message);
                    
                    // Send the data to the network.
                    int bytesSent = sendSocket.Send(msg);
                    sendTOLeader(full_message);
                    SendBox.Items.Add(full_message);
                    mescount += 1;
                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();

                    //Debug.WriteLine("Press ENTER to terminate ...");
                    //Debug.ReadLine();
                }
                catch (ArgumentNullException ane)
                {
                    Debug.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Debug.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception excp)
                {
                    Debug.WriteLine("Unexpected exception : {0}", excp.ToString());
                }

            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }

        private void sendTOLeader(string data)
        {
            try
            {
                // Find the IP address of localhost
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8082);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);
                    //string random_message = get_random_message();
                    string timestamp = DateTime.Now.ToString();
                    string full_message =  data + "<ASK>" ;
                    //string msg_flag = "MW1" + mescount.ToString();
                    // Generate and encode the multicast message into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(full_message);

                    // Send the data to the network.
                    int bytesSent = sendSocket.Send(msg);
                    //SendBox.Items.Add(full_message);
                    //mescount += 1;
                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();

                    //Debug.WriteLine("Press ENTER to terminate ...");
                    //Debug.ReadLine();
                }
                catch (ArgumentNullException ane)
                {
                    Debug.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Debug.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception excp)
                {
                    Debug.WriteLine("Unexpected exception : {0}", excp.ToString());
                }

            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }
        private void confirm_seq()
        {
            //Debug.WriteLine("Process running!");
            //Thread.Sleep(500);
            //Debug.WriteLine("recBuffer.Count: " + recBuffer.Count.ToString());
            if (recBuffer.Count < 0)
            {
                return;
            }

            for (int i = 0; i < recBuffer.Count; i++)
            {

                Tuple<string, int> msg_pair = recBuffer[i];
                string msg = msg_pair.Item1;
                int msgNum = msg_pair.Item2;
                //Debug.WriteLine("msg: " + msg);
                //Debug.WriteLine("msgNum: "+ msgNum);
                if ((recNum + 1) == msgNum)
                {
                    recNum += 1;
                    this.InvokeEx(f => f.ReadyBox.Items.Add(msg));
                    recBuffer.RemoveAt(i);

                }

                if (recBuffer.Count < 0)
                {
                    break;
                }
            }
        }
        private string get_random_message()
        {
            string mystring = "";
            int position;

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            

            var random = new Random();
            int looptime = random.Next(3, 7);

            for(int i =0; i < looptime; i++)
            {
                position = random.Next(1, alphabet.Length);
                mystring += alphabet[position];
            }

            string msg_end = random.Next(0,1000).ToString();
            int end_len = msg_end.Length;
            if (end_len < 3)
            {
                msg_end = new string('0', 3 - end_len) + msg_end;
            }
            mystring += msg_end;
            return mystring;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReceiveMulticast();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);

            var timer = new System.Threading.Timer((ev) =>
            {
                confirm_seq();
            }, null, startTimeSpan, periodTimeSpan);
        }

        private void sendButton__Click(object sender, EventArgs e)
        {
            sendMessage();
        }
    }
}
