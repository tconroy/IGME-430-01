using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace ConsoleMessageClient
{
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }

    class ClientTCP
    {
        private TcpClient tcpClient;
        string username = "";
        NetworkStream tcpStream;

        public void startup()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(shutdown);

            string ip;

            while (username == "")
            {
                Console.Write("\nEnter username: ");

                username = Console.ReadLine();
            }
            try
            {
                ip = Console.ReadLine();
                IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8012);
                tcpClient = new TcpClient();
                tcpClient.Connect(tcpEndPoint);

                tcpStream = tcpClient.GetStream();

                sendTCPData(username, MessageType.Joined, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server.");
            }

            // start a thread to retrieve the data
            Task.Factory.StartNew(() =>
            {
                getTCPData();
            });

            // to prevent the console from auto-closing
            while (true)
            {
                string text = Console.ReadLine();
                if( text != "" )
                {
                    sendTCPData(username, MessageType.Message, text);
                }
            }
        }

        /**
         * Only fires on ctrl-C events
         * */
        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {

        }

        // retrieve data from the server
        void getTCPData()
        {
            byte[] buffer = new byte[1024];
            int bytes          = 0;
            string message     = "";
            string messageUser = "";

            // store byte 1 into our buffer (the message type). reads from position 0, +1 = 1
            bytes = tcpStream.Read(buffer, 0, 1);

            // something in it
            if( bytes != 0 )
            {
                bytes = tcpStream.Read(buffer, 1, 2); // reads from position 1, + 2 = 3
                short usernameLength = (short)(buffer[1] + (buffer[2] * 256));

                bytes = tcpStream.Read(buffer, 3, 1);
                short messageLength = (short)(buffer[3]);

                bytes = tcpStream.Read(buffer, 4, usernameLength);
                messageUser = Encoding.ASCII.GetString(buffer, 4, usernameLength);

                if( buffer[0] == (byte)MessageType.Message )
                {
                    bytes = tcpStream.Read(buffer, 4 + usernameLength, messageLength);
                    message = Encoding.ASCII.GetString(buffer, 4 + usernameLength, messageLength);
                    Console.WriteLine("\n" + messageUser + ":" + message);
                }
            }

            // repeat. 
            Task.Factory.StartNew(() =>
            {
                getTCPData();
            });
        }

        void sendTCPData( string username, MessageType type, string message )
        {
            byte[] packetBuffer = new Byte[4];
            byte[] usernameBuffer = System.Text.Encoding.ASCII.GetBytes(username);
            byte[] messageBuffer = System.Text.Encoding.ASCII.GetBytes(message);

            packetBuffer[0] = (byte)type;
            packetBuffer[1] = (byte)(usernameBuffer.Length % 256);
            packetBuffer[2] = (byte)(usernameBuffer.Length / 256);
            packetBuffer[3] = (byte)(messageBuffer.Length);

            byte[] tcpMessage = packetBuffer.Concat(usernameBuffer).Concat(messageBuffer).ToArray();

            tcpClient.Client.Send(tcpMessage);
        }

    }
}
