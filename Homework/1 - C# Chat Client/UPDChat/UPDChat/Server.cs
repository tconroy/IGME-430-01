using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UPDChat
{
    public class Server
    {
        // --------------------------------
        // Properties
        // --------------------------------
        public enum MessageType
        {
            Joined,
            Left,
            Message,
            Command
        }

        // obj
        public ServerLogForm serverLog;
        Socket udpSocket;

        // string
        private const String DEFAULT_NAME = "the Server";
        private String name;
        private String password = null;
        
        // containers
        List<EndPoint> udpClients = new List<EndPoint>();
        List<String> udpClientNames = new List<String>();
        byte[] recBuffer = new byte[512];

        
        // --------------------------------
        // init
        // --------------------------------
        public bool startup(String serverName = null, String ServerPass = null)
        {
            // store values
            this.setServerName(serverName);
            this.setServerPassword(ServerPass);

            // starting a sepearte process.
            Task.Factory.StartNew(() =>
            {
                    // listen for packets from any IP address, and open / bind a UDP socket.
                    EndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 13337); 
                    udpSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                    udpSocket.Bind(localEndPoint);

                    // pass in our recieve buffer, where to begin (0, the beginning), where to end (512 bytes, so 2 packets), 
                    // ignore socket flags, where it's coming from (local endpoint), the callback method, and a reference to this task
                    udpSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref localEndPoint, new AsyncCallback(MessageReceivedCallback), this);                
            });


            // make an instance of the server log. this is where we'll store messages for viewing server-side.
            Server serv = this;
            serverLog = new ServerLogForm(ref serv);
            serverLog.Show();
            serverLog.SetText( (string)this.name + " started." );
            
            return true;
        }


        // async callback. called every time the task recieves a message, the async result passed in.
        void MessageReceivedCallback(IAsyncResult result)
        {
            // instantiate vars for username, message, and remote(client) endpoint.
            string username = "";
            string message = "";
            bool go        = true;
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
                Console.WriteLine("Exception in Server::MessageRecievedCallback()");
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
                string msg = (string)username + " has joined " + this.name + ".";
                serverLog.SetText(msg);
                if( ! udpClientNames.Contains(username) ){
                    udpClientNames.Add(username);
                }

            }
            // sent "left server" packet 
            else if (recBuffer[0] == (byte)MessageType.Left)
            {
                string msg = (string)username + " has left the server.";
                serverLog.SetText( msg);
                if( udpClientNames.Contains(username) ){
                    udpClientNames.Remove(username);
                }
                if (udpClients.Contains(remoteEndPoint) == true)
                {
                    udpClients.Remove(remoteEndPoint);
                }
            }
            // sent standard "chat message" packet.
            else if (recBuffer[0] == (byte)MessageType.Message)
            {
                // start right after the username packet ends (4+usernamelength)
                message = Encoding.ASCII.GetString(recBuffer, 4 + usernameLength, recBuffer[3]);
                string msg = username + ": " + message;
                serverLog.SetText(msg);

                if( message[0] == '/'  ){
                    string cmd = message.TrimStart('/');
                    this.serverCommand(ref remoteEndPoint, cmd.Trim(), username, MessageType.Command);
                    // dont want to send it to all clients so breakout
                    go = false; 
                }

            }

            // only update if we have data in recBuffer
            // update the clients, then clear out the recieved buffer.
            if( go )
            {
                foreach( EndPoint client in udpClients ){
                    udpSocket.SendTo(recBuffer, client);
                }            
            }
            Array.Clear(recBuffer, 0, recBuffer.Length);
        }


        void serverCommand(ref EndPoint client, string cmd, string username, MessageType type)
        {
            // user typed /list, so send list of active users.
            if( cmd == "list" )
            {
                string message = "Currently chatting with: " + String.Join(", ", udpClientNames);
                serverLog.SetText( message );

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

                udpSocket.SendTo(udpMessage, client);
            }
        }

        // ----------
        // SETTERS 
        // ----------

        void setServerName( String aName ) {
            if (!String.IsNullOrWhiteSpace(aName))
            {
                this.name = aName;
            }
            else {
                this.name = DEFAULT_NAME;
            }
        }
        void setServerPassword( String aPass ) {
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