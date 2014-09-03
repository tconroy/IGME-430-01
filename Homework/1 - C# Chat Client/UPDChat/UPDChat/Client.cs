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
                IPEndPoint serverIPEndpoint = new IPEndPoint(IPAddress.Any, 8011);
                udpClient = new UdpClient();
                udpClient.Connect(serverIPEndpoint);

                sendUDPData(username, MessageType.Joined, "");
                
                // start listening for response from the server
               // serverEndpoint = (EndPoint)serverIPEndpoint;
                recieveSocket = new Socket(SocketType.Dgram, ProtocolType.Udp); // creates a socket, tells it that we're using a UDP protocol
                //recieveSocket.Bind(serverEndpoint);
                recieveSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref serverEndpoint, new AsyncCallback(this.recieveUDPData), null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                Console.WriteLine( e.Message);
                Console.WriteLine(e.StackTrace);
                System.Environment.Exit(-1);
            }

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

        private void recieveUDPData(IAsyncResult result)
        {
            // create the remote endpoint
            //EndPoint remoteEndPoint = new IPEndPoint(0, 0);
            EndPoint remoteEndPoint = serverIPEndpoint;

            // was udpSocket; now recieveSocket
            try
            {
                int readBytes = recieveSocket.EndReceiveFrom(result, ref serverEndpoint);
            }
            catch (Exception e)
            {
                // todo: hanle callback fail exception
            }

            string username = "";
            string message = "";

            // tells it to go back up and keep listening.
            recieveSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref serverEndpoint, new AsyncCallback(recieveUDPData), this);

            // [1] = the remainder, [2]*256 = the contents
            short usernameLength = (short)(recBuffer[1] + (recBuffer[2] * 256));

            // translate the bytes to a string. start at the 4th byte
            username = Encoding.ASCII.GetString(recBuffer, 4, usernameLength);

            if (recBuffer[0] == (byte)MessageType.Joined)
            {
                this.insertText( (string)username + " has joined the server." );
            }
            else if (recBuffer[0] == (byte)MessageType.Left)
            {
                this.insertText( (string)username + " has left the server." );
            }
            else if (recBuffer[0] == (byte)MessageType.Message)
            {
                // start right after the username packet ends (4+usernamelength)
                message = Encoding.ASCII.GetString(recBuffer, 4 + usernameLength, recBuffer[3]);
                message = username + ": " + message;
                this.insertText( message );
            }

            Array.Clear(recBuffer, 0, recBuffer.Length); // clear out the buffer
            
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
