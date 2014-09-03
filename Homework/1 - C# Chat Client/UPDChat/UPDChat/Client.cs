using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UPDChat
{
    class Client
    {
        public enum MessageType
        {
            Joined,
            Left,
            Message
        }

        UdpClient udpClient;
        public string username = "";
        public string ip;
        private string password;

        private ChatForm chatForm;

        // recieving
        Socket udpSocket;
        Socket recieveSocket;
        List<EndPoint> udpClients = new List<EndPoint>();
        private IPEndPoint serverIPEndpoint;
        private EndPoint serverEndpoint;
        byte[] recBuffer = new byte[512];
        
        private const String DEFAULT_USERNAME = "Anonymous";

        
        public Client( ChatForm formRef ){
            this.chatForm = formRef;
        }



        public void startup(String un, String ipAddress, String pw)
        {
            // store user settings
            this.setUsername(un);
            this.setIPAddress(ipAddress);
            this.setPassword(pw);

            try
            {
                IPEndPoint serverIPEndpoint = new IPEndPoint(IPAddress.Parse(ip), 8011);
                udpClient = new UdpClient();
                udpClient.Connect(serverIPEndpoint);

                sendUDPData(username, MessageType.Joined, "");
                
                // start listening for response from the server
                recieveSocket = new Socket(SocketType.Dgram, ProtocolType.Udp); // creates a socket, tells it that we're using a UDP protocol
                recieveSocket.Bind(serverEndpoint);
                //recieveSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref serverEndpoint, new AsyncCallback(MessageReceivedCallback), this);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                Console.WriteLine( e.Message);
                Console.WriteLine(e.StackTrace);
                //System.Environment.Exit(-1);
            }

            // begin listening for server
            // starting a sepearte process.
            Task.Factory.StartNew(() =>
            {
                // listen for packets from server IP address, and open / bind a UDP socket.
                EndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8011);
                udpSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                udpSocket.Bind(localEndPoint);

                // pass in our recieve buffer, where to begin (0, the beginning), where to end (512 bytes, so 2 packets), 
                // ignore socket flags, where it's coming from (local endpoint), the callback method, and a reference to this task
                udpSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref localEndPoint, new AsyncCallback(MessageReceivedCallback), this);
            });

        }





        
        public void insertText( string message ) {
            this.chatForm.insertTextToLog(message);
        }



        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {
            if (udpClient != null && username != "")
            {
                sendUDPData(username, MessageType.Left, "");
            }
        }





        // async callback. called every time the task recieves a message, the async result passed in.
        void MessageReceivedCallback(IAsyncResult result)
        {
            // instantiate vars for username, message, and remote(client) endpoint.
            string username = "";
            string message = "";
            EndPoint remoteEndPoint = new IPEndPoint(0, 0);

            // try to end recieving, and store the client end point if we don't already have it
            try
            {
                int readBytes = udpSocket.EndReceiveFrom(result, ref remoteEndPoint);
                if (udpClients.Contains(remoteEndPoint) == false)
                {
                    udpClients.Add(remoteEndPoint);
                }
            }
            catch (Exception e)
            {
                // todo: hanle callback fail exception
                Console.WriteLine("Exception in Client::MessageRecievedCallback()");
            }

            // tells it to go back up and keep listening.
            udpSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref remoteEndPoint, new AsyncCallback(MessageReceivedCallback), this);

            // [1] = the remainder, [2]*256 = the contents
            short usernameLength = (short)(recBuffer[1] + (recBuffer[2] * 256));

            // translate the bytes to a string. start at the 4th byte
            username = Encoding.ASCII.GetString(recBuffer, 4, usernameLength);

            // sent "joined server" packet
            if (recBuffer[0] == (byte)MessageType.Joined)
            {
                string msg = (string)username + " has joined the server";
                this.insertText(msg);
            }
            // sent "left server" packet 
            else if (recBuffer[0] == (byte)MessageType.Left)
            {
                string msg = (string)username + " has left the server.";
                this.insertText(msg);
            }
            // sent standard "chat message" packet.
            else if (recBuffer[0] == (byte)MessageType.Message)
            {
                // start right after the username packet ends (4+usernamelength)
                message = Encoding.ASCII.GetString(recBuffer, 4 + usernameLength, recBuffer[3]);
                message = username + ": " + message;
                this.insertText(message);
            }

            // clear out the recieved buffer.
            Array.Clear(recBuffer, 0, recBuffer.Length);
        }





        public void sendUDPData(string username, MessageType type, string message)
        {   
            byte[] packetBuffer = new byte[4];

            byte[] usernameBuffer = System.Text.Encoding.ASCII.GetBytes(username);

            byte[] messageBuffer = System.Text.Encoding.ASCII.GetBytes(message);

            packetBuffer[0] = (byte)type;
            packetBuffer[1] = (byte)(usernameBuffer.Length % 256); // we break up into chunks of 256 characters so we know the size of each packet.
            packetBuffer[2] = (byte)(usernameBuffer.Length / 256);
            packetBuffer[3] = (byte)(messageBuffer.Length);

            byte[] udpMessage = packetBuffer.Concat(usernameBuffer).Concat(messageBuffer).ToArray();

            udpClient.Client.Send(udpMessage);
        }




        // username setter
        public void setUsername( String aUsername ) {
            if (!String.IsNullOrWhiteSpace(aUsername))
            {
                this.username = aUsername;
            }
            else {
                this.username = DEFAULT_USERNAME;
            }
        }

        public void setIPAddress(String anIP) {
            
            if( !String.IsNullOrWhiteSpace(anIP) ){
                this.ip = anIP;
            }else {
                this.ip = null;
            }

        }
        public void setPassword(String aPass) {

            if (!String.IsNullOrWhiteSpace(aPass))
            {
                this.password = aPass;
            }
            else {
                this.password = null;
            }

        }

    }
}
