using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//Import websockets libraries
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using Newtonsoft.Json;
using SuperWebSocket;

namespace ConsoleMessageClient
{
    public enum MessageType
    {
        Joined,
        Left,
        Message,
        Command
    }

    //This will be a class we will use for messages
    //Objects of this class will be sent as message
    public class ChatMessage
    {
        public MessageType type { get; set; }
        public string username { get; set; }
        public string data { get; set; }
    }

    class ClientWS
    {
        //client web socket object
        private ClientWebSocket wsClient;
        string username = "";

        //a cancellation token is required by many websocket methods in c#
        //a cancellation token just allows you to cancel pending or current operations
        //in this example, we won't really use it but it is required for some method calls
        CancellationToken ct = new CancellationToken();

        public void startup()
        {
            //Fire the shutdown method with ctrl+c is pressed in the console
            Console.CancelKeyPress += new ConsoleCancelEventHandler(shutdown);

            string ip;

            //get a username from the console user
            while (username == "")
            {
                Console.Write("\nEnter username: ");

                username = Console.ReadLine();
            }

            try
            {
                //get an ip address and connect to the websocket URI
                Console.Write("Enter IP of chat server: ");
                ip = Console.ReadLine();

                //create a new web socket client
                wsClient = new ClientWebSocket();
                //connect to the URI. Remember a URI is like a URL. In fact, a URL is a type of URI
                //A URI is a procotol an ip or domain and a port. 
                //The connectAsync method also takes a cancellation token
                wsClient.ConnectAsync(new Uri("ws://" + ip + ":8082"), ct);

                //send a join message
                sendWSData(username, MessageType.Joined, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                System.Environment.Exit(-1);
            }

            //start a new task to listen for data
            Task.Factory.StartNew(() =>
            {
                //if the websocket is open (meaning it is running), listen for data
                getWSData();
            });


            Console.Write("\nEnter Message and hit enter to send\n");

            //listen for user input and send messages 
            while (true)
            {
                string text = Console.ReadLine();

                if (text != "" && text != null)
                {
                    if (text[0] == '/')
                    {
                        text = text.Substring(1);
                        sendWSData(username, MessageType.Command, text);
                    }
                    else
                    {
                        sendWSData(username, MessageType.Message, text);
                    }

                }
            }
        }

        /**
         * Only fires on ctrl-C events
         * */
        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {
            sendWSData(username, MessageType.Left, "");
        }

        //upon receiving data
        //async means that this method runs asynchronously. The keyword tells c# to expect that
        async void getWSData()
        {
            //create a new buffer to handle a decent amount of data
            byte[] buffer = new byte[1024];

            //Create a new array segment of type bytes to record each of the segements from websockets
            ArraySegment<byte> bufferSegment = new ArraySegment<byte>(buffer); 

            //wait for a message to be received asynchronously. 
            //the Await keyword tells the line to wait for an asynchronous result before continuing. 
            //The receiveAsync method takes a bufferSegment to fill and a cancellation token in case the receive needs to be cancelled
            WebSocketReceiveResult result = await wsClient.ReceiveAsync(bufferSegment,ct);

            //Get the array from the buffersegment and deserialize the result into a ChatMessage starting at position 0 in the array and going as long as the result
            ChatMessage message = JsonConvert.DeserializeObject<ChatMessage>(Encoding.ASCII.GetString(bufferSegment.Array, 0, result.Count));


            Console.WriteLine("\n\n" + message.username + ": " + message.data);

            //start listening for messages again
            Task.Factory.StartNew(() =>
            {
                getWSData();
            });
            
        }

        //send a message
        //async means that this method runs asynchronously. The keyword tells c# to expect that
        async void sendWSData(string username, MessageType type, string message)
        {
            //Create a new chat message object setting the object's properties
            ChatMessage msg = new ChatMessage() { username = username, type = type, data = message };

            //get the json string of the serialized object
            string json = JsonConvert.SerializeObject(msg);

            //get the bytes from the json string
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(json);

            //create a new arraysegment from the buffer
            ArraySegment<byte> bufferSegment = new ArraySegment<byte>(buffer);

            //send the message to the server
            //await tells this to wait until the line completes before moving on
            //the arguments are the arraysegment, the websocket message type, whether or not this is the last message of the transmission and a cancellation token
            await wsClient.SendAsync(bufferSegment, WebSocketMessageType.Binary, true, ct);
        }

    }
}
