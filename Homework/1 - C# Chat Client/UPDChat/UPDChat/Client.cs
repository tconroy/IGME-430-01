using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace UPDChat
{
    class Client
    {
        public enum MessageType
        {
            Joined,
            Left,
            Message,
            Command
        }

        // obj
        UdpClient udpClient;
        ChatForm chatForm;
        Timer timer;

        // string
        public string username;
        const string DEFAULT_USERNAME = "Anonymous";
        public string ip;
        private string password;
        
        // arr
        byte[] recBuffer = new byte[512];

        
        // -------------------------------
        // Constructor
        // -------------------------------
        public Client( ChatForm formRef ){
            this.chatForm = formRef;
            this.timer = new Timer(5000);
        }

        // -------------------------------
        // Init
        // -------------------------------
        public void startup(String un, String ipAddress, String pw)
        {
            // store user settings
            this.setUsername(un);
            this.setIPAddress(ipAddress);
            this.setPassword(pw);

            // try to connect to the server and send a connection packet. 
            // if Exception thrown, could not connect to the server.
            try
            {
                IPEndPoint serverIPEndpoint = new IPEndPoint(IPAddress.Parse(ip), 13337);
                udpClient = new UdpClient();
                udpClient.Connect(serverIPEndpoint);
                sendUDPData(username, MessageType.Joined, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                Console.WriteLine( e.Message);
                Console.WriteLine(e.StackTrace);
                //System.Environment.Exit(-1);
            }

            // Start process to listen for recieved data from server.
            Task.Factory.StartNew(() =>
            {
                getUDPData();
            });

        }

        
        public void insertText( string message ) {
            this.chatForm.insertText(message);
        }


        // fired when the form is closed
        public void shutDown() {
            sendUDPData(username, MessageType.Left, "");
        }



        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {
            sendUDPData(username, MessageType.Left, "left the server");
        }



        // -------------------------------------------
        //          UDP FUNCTIONS
        // -------------------------------------------

        // transmit UDP packets over the wire to server
        public void sendUDPData(string username, MessageType type, string message)
        {            
            // set up byte arrays where we'll store message parts
            byte[] packetBuffer   = new byte[4];
            byte[] usernameBuffer = System.Text.Encoding.ASCII.GetBytes(username);
            byte[] messageBuffer  = System.Text.Encoding.ASCII.GetBytes(message);

            // break up into 256bit chunks
            packetBuffer[0] = (byte)type;
            packetBuffer[1] = (byte)(usernameBuffer.Length % 256);
            packetBuffer[2] = (byte)(usernameBuffer.Length / 256);
            packetBuffer[3] = (byte)(messageBuffer.Length);

            // concat the byte array together and send it on its way
            byte[] udpMessage = packetBuffer.Concat(usernameBuffer).Concat(messageBuffer).ToArray();
            udpClient.Client.Send(udpMessage);
        }

        // recieve UDP packets over the wire from server
        public void getUDPData()
        {
            IPEndPoint endPoint = new IPEndPoint(0, 0);
            try
            {
                //pull username from buffer. convert the bytes into a string.
                byte[] buffer        = udpClient.Receive(ref endPoint);
                short usernameLength = (short)(buffer[1] + (buffer[2] * 256));
                string username      = Encoding.ASCII.GetString(buffer, 4, usernameLength);
                string message       = "";

                if (buffer[0] == (byte)MessageType.Joined)
                {
                    message = (string)username + " has joined the server";
                }

                else if (buffer[0] == (byte)MessageType.Left)
                {
                    message = (string)username + " has left the server.";
                }

                else if (buffer[0] == (byte)MessageType.Message)
                {
                    // start right after the username packet ends (4+usernamelength)
                    message = Encoding.ASCII.GetString(buffer, 4 + usernameLength, buffer[3]);
                    message = username + ": " + message;
                }

                else if ( buffer[0] == (byte)MessageType.Command )
                {
                    message = Encoding.ASCII.GetString(buffer, 4 + usernameLength, buffer[3]);
                }

                // send to window.
                insertText(message);

                // clear out the recieved buffer.
                Array.Clear(buffer, 0, buffer.Length);

                // repeat call
                getUDPData();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in getUDPData()");
                Console.WriteLine(e.Data);
            }
        }




        // -------------------------------------------
        //                  SETTERS
        // -------------------------------------------
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
