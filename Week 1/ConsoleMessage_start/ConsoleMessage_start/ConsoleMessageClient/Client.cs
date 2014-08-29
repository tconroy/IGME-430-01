using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleMessageClient
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
        string username = ""; 

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
                Console.Write("Enter IP of chat server: ");
                ip = Console.ReadLine();

                IPEndPoint udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8011);
                udpClient = new UdpClient();
                udpClient.Connect(udpEndPoint);

                sendUDPData(username, MessageType.Joined, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                System.Environment.Exit(-1);
            }

            Console.Write("\nEnter Message and hit enter to send\n");

            while (true)
            {
                string text = Console.ReadLine();

                if (text != "")
                {
                    sendUDPData(username, MessageType.Message, text);
                }
            }
        }

        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {
            if (udpClient != null && username != "")
            {
                sendUDPData(username, MessageType.Left, "");
            }
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

    }
}
