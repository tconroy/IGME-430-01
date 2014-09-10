using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//import our network packages
using System.Net;
using System.Net.Sockets;

namespace ConsoleMessageServer
{
    //enum to hold message types. These will be used to distinguish different types of messages.
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }


    class ServerTCP
    {
        //Our server's TCP socket
        Socket tcpSocket;
        //list to hold all of our clients. These will be of a custom clientmanger class we will build
        List<ClientManager> tcpClients = new List<ClientManager>();

        //the last client that connected. We will only use this for the example. 
        //YOUR chat server will not have this
        ClientManager lastClient;

        //startup method
        public void startup()
        {
            //new TCP task to run asynchronously in the background
            Task.Factory.StartNew(() =>
            {
                //listen to any address on port 8080
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8012);
                //set the socket type to a stream of protocol tcp
                tcpSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                //bind to the endpoint to the socket
                tcpSocket.Bind(localEndPoint);
                //start listening and allowing queuing of up to 10 connections. If the system is overwhelming and the queue goes over 10, the later ones will be dropped
                tcpSocket.Listen(10);

                //begin accepting connections and call onAccept when a new client connects. 
                tcpSocket.BeginAccept(new AsyncCallback(OnAccept), null);

            });

            Console.WriteLine("Hit ctrl-c twice to exit");

            //only here to keep the console open
            Console.ReadKey();
        }

        //Called when a new client starts a tcp connection to the server
        void OnAccept(IAsyncResult connection)
        {
            //finish accepting and grab socket from connection
            Socket clientSocket = tcpSocket.EndAccept(connection);
            //pass socket to our clientmanager class. This will handle all of the reads/writes for this stream
            ClientManager client = new ClientManager(clientSocket);

            //add a reference to our last connected client for a response
            //YOU will not have this in your chat server because you will respond to everyone
            lastClient = client;

            //connect the clientmanager's onleft event to the onleft method in this server
            client.OnLeft += OnLeft;
            //connect the clientmanager's onmessage event to the onmessage method in this server
            client.OnMessage += OnMessage;
            //connect the clientmanager's onjoined event to the onjoined method in this server
            client.OnJoined += OnJoined;

            //add the client to our TCP clients list
            tcpClients.Add(client);

            //begin accepting more connections
            tcpSocket.BeginAccept(new AsyncCallback(OnAccept), null);
        }

        //method will be called every time a clientmanager onleft event fires
        //will be called when clients send a left message
        void OnLeft(ClientManager item)
        {
            //remove the client from our TCP clients list
            tcpClients.Remove(item);
            //disconnect the client and discard the socket. False means that the socket cannot be used again. 
            //True means you can keep using the socket methods even though it's disconnected
            item.socket.Disconnect(false);
        }

        //method will be called every time a clientmanager onjoined event fires
        //will be called when clients send a joined message
        void OnJoined(string username)
        {
            sendChatMessage(username, " has joined chat");
        }

        void OnMessage(string username, string message)
        {
            sendChatMessage(username, message);
        }

        //method to send chat messages to clients
        void sendChatMessage(string username, string message)
        {
            //byte array to hold our parsing information - how long usernames are, how long messages are, etc
            byte[] packetBuffer = new byte[4];
            //byte array formed from the ascii byte version of our username
            byte[] usernameBuffer = System.Text.Encoding.ASCII.GetBytes(username);
            //byte array formed from the ascii byte version of our message
            byte[] messageBuffer = System.Text.Encoding.ASCII.GetBytes(message);

            //The first 4 bytes will be our parsing information. This is how we will know how long each section of the data is when it arrives at the server
            //The server will read these to figure out how many bytes are in each part of the data
            // [0] will be the message type
            // [1] will be the remainder of the username length % 256.
            // [2] will be how many types the username goes into 256. 
            // [3] will be the length of the message
            packetBuffer[0] = (byte)MessageType.Message;
            packetBuffer[1] = (byte)(usernameBuffer.Length % 256);
            packetBuffer[2] = (byte)(usernameBuffer.Length / 256);
            packetBuffer[3] = (byte)(messageBuffer.Length);

            //concat the 3 byte arrays into one (make sure they are in the right order)
            byte[] tcpMessage = packetBuffer.Concat(usernameBuffer).Concat(messageBuffer).ToArray();

            //send our byte array
            lastClient.socket.Send(tcpMessage);

        }

    }
}
