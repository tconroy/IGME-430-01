using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//import out network packages
using System.Net;
using System.Net.Sockets;

namespace ConsoleMessageClient
{
    //enum to hold message types. These will be used to distinguish different types of messages.
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }

    class ClientTCP
    {
        //tcpclient object for our tcp connection/methods
        private TcpClient tcpClient;

        string username = "";

        //network stream for communicating with the server
        NetworkStream tcpStream;

        //startup method
        public void startup()
        {
            //On a ctrl+c command, this event will fire and shut down
            Console.CancelKeyPress += new ConsoleCancelEventHandler(shutdown);

            //string to store the server's IP
            string ip;

            //grab username from console
            while (username == "")
            {
                Console.Write("\nEnter username: ");

                username = Console.ReadLine();
            }


            try
            {
                //read IP from command line
                Console.Write("Enter IP of chat server: ");
                ip = Console.ReadLine();

                //Create a new IPEndpoint to the server for port 8012. 
                IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8012);

                //create a new tcpclient and connect to our endpoint.
                //Connecting will start the stream or throw an exception
                tcpClient = new TcpClient();
                tcpClient.Connect(tcpEndPoint);

                //get the newly created stream from the connected tcpclient object
                tcpStream = tcpClient.GetStream();

                //send joined message
                sendTCPData(username, MessageType.Joined, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                System.Environment.Exit(-1);
            }

            //start new task to listen for new TCP data
            //the code will move on and not wait for this because it is a task
            Task.Factory.StartNew(() =>
            {
                getTCPData();
            });

            Console.Write("\nEnter Message and hit enter to send\n");

            //keep accepting console input
            while (true)
            {
                //get string from the console
                string text = Console.ReadLine();

                //if they actually typed a message
                if (text != "")
                {
                    //send a message to the server
                    sendTCPData(username, MessageType.Message, text);
                }
            }
        }

        /**
         * Only fires on ctrl-C events
         * */
        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {
            sendTCPData(username, MessageType.Left, "");
        }

        //method to receive data from the server
        void getTCPData()
        {
            //new byte array to hold data. We don't know the size of the data, so 1024 gives us a decent amount of room
            byte[] buffer = new byte[1024];
            //int to hold the number of bytes we read
            int bytes = 0;

            //strings to hold the username and message we received from the server
            string message = "";
            string messageUser = "defaultUserName";

            //read 1 byte from the stream starting at position 0 of the stream and put it into the buffer
            //The return is the number of bytes we read from the stream. This will be 0 if the stream is empty
            //If the stream has data the first byte will be put into our buffer array
            bytes = tcpStream.Read(buffer, 0, 1);

            //if we read any bytes above- meaning the stream was not empty
            if (bytes != 0)
            {
                //read from the stream starting at position 1 and read 2 spots and add them to the buffer.
                //this will read array positions [1] and [2] from the stream.
                //again, the return will be the number of bytes actually read. 
                bytes = tcpStream.Read(buffer, 1, 2);
                //now that we filled the buffer some more, read out the username length
                //we know [1] is the modulus of 256 and [2] is the number of times the username goes into 256
                short usernameLength = (short)(buffer[1] + (buffer[2] * 256));

                //read how long the message is starting at position [3] from the stream and reading 1
                //this will append to the buffer again
                bytes = tcpStream.Read(buffer, 3, 1);
                //grab the message length from the newly filled buffer
                short messageLength = (short)(buffer[3]);

                //read out the username from the stream starting at [4] and going the username length.
                //this will be pulled into the buffer
                bytes = tcpStream.Read(buffer, 4, usernameLength);
                //read the username from the buffer now that we read it into the buffer
                messageUser = Encoding.ASCII.GetString(buffer, 4, usernameLength);

                //if the message type is message
                if (buffer[0] == (byte)MessageType.Message)
                {
                    //read message into buffer
                    bytes = tcpStream.Read(buffer, 4 + usernameLength, messageLength);
                    //get the message from the buffer
                    message = Encoding.ASCII.GetString(buffer, 4 + usernameLength, messageLength);

                    Console.Write("\n" + messageUser + ":" + message + "\n");
                }
            }

            //start a new task to start listening for more data again
            Task.Factory.StartNew(() =>
            {
                getTCPData();
            });

        }

        //method to send tcp data
        void sendTCPData(string username, MessageType type, string message)
        {
            //byte array to hold our parsing information - how long usernames are, how long messages are, etc
            byte[] packetBuffer = new byte[4];
            //byte array formed from the ascii byte version of our username
            byte[] usernameBuffer = System.Text.Encoding.ASCII.GetBytes(username);
            //byte array formed from the ascii byte version of our message
            byte[] messageBuffer = System.Text.Encoding.ASCII.GetBytes(message);

            //The first 4 bytes will be our parsing information. This is how we will know how long each section of the data is when it arrives at the server
            //The server will read these to figure out how many bytes are in each part of the data
            packetBuffer[0] = (byte)type;
            packetBuffer[1] = (byte)(usernameBuffer.Length % 256);
            packetBuffer[2] = (byte)(usernameBuffer.Length / 256);
            packetBuffer[3] = (byte)(messageBuffer.Length);

            //concat the 3 byte arrays into one (make sure they are in the right order)
            byte[] tcpMessage = packetBuffer.Concat(usernameBuffer).Concat(messageBuffer).ToArray();

            //send to our client. 
            //in a chat server, you would send to every client
            tcpClient.Client.Send(tcpMessage);
        }

    }
}
