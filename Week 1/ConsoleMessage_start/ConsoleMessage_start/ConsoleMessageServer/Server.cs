using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleMessageServer
{
    class Server
    {
        public enum MessageType
        {
            Joined,
            Left,
            Message
        }

        Socket udpSocket;
        List<EndPoint> udpClients = new List<EndPoint>();
        byte[] recBuffer = new byte[512];

        public void startup()
        {
            // starting a sepearte process.
            Task.Factory.StartNew(() => 
            {
                EndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8011); // listen for any IP address
                udpSocket = new Socket(SocketType.Dgram, ProtocolType.Udp); // creates a socket, tells it that we're using a UDP protocol
                udpSocket.Bind(localEndPoint);

                // pass in our recieve buffer, where to begin (0, the beginning), where to end (512 bytes, so 2 packets), 
                // ignore socket flags, where it's coming from (local endpoint), the callback method, and a reference to this task
                udpSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref localEndPoint, new AsyncCallback(MessageReceivedCallback), this);
            });
 

            // prevent the console from immediately closing.
            Console.ReadKey();
        }


        // async callback. called every time the task recieves a message, the async result passed in.
        void MessageReceivedCallback( IAsyncResult result ) {
            
            // create the remote endpoint, 
            EndPoint remoteEndPoint = new IPEndPoint(0, 0);

            try
            {
                int readBytes = udpSocket.EndReceiveFrom(result, ref remoteEndPoint);
                
                // useful for chat program:: how to detect if something already exists.
                if( udpClients.Contains(remoteEndPoint) == false ){
                    udpClients.Add(remoteEndPoint);
                }
            }
            catch (Exception e)
            { 

            }

            string username = "";
            string message = "";

            // tells it to go back up and keep listening.
            udpSocket.BeginReceiveFrom(recBuffer, 0, 512, SocketFlags.None, ref remoteEndPoint, new AsyncCallback(MessageReceivedCallback), this);

            // [1] = the remainder, [2]*256 = the contents
            short usernameLength = (short)(recBuffer[1] + (recBuffer[2] * 256));

            // translate the bytes to a string. start at the 4th byte
            username = Encoding.ASCII.GetString(recBuffer, 4, usernameLength);

            if( recBuffer[0] == (byte)MessageType.Joined ){
                Console.WriteLine(username + " has joined the server!");
            }
            else if( recBuffer[0] == (byte)MessageType.Left ){
                Console.WriteLine(username + " has left");
            }
            else if( recBuffer[0] == (byte)MessageType.Message ) {
                // start right after the username packet ends (4+usernamelength)
                message = Encoding.ASCII.GetString(recBuffer, 4 + usernameLength, recBuffer[3]);
                Console.WriteLine(message);
            }

            Array.Clear(recBuffer, 0, recBuffer.Length); // clear out the buffer 
        }

    }
}
