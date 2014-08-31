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

        private const String DEFAULT_USERNAME = "Anonymous";


        public void startup(String un, String ipAddress, String pw)
        {
            // store user settings
            this.setUsername(un);
            this.setIPAddress(ipAddress);
            this.setPassword(pw);

            try
            {
                ip = ipAddress;

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

            // send message
            // sendUDPData(username, MessageType.Message, text);

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
