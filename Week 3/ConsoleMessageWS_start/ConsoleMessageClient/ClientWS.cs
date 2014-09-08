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
//these need to be installed via nuget
using Newtonsoft.Json;
using SuperWebSocket;

namespace ConsoleMessageClient
{
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }

    public class ChatMessage
    {
        public MessageType type { get; set; }
        public string username { get; set; }
        public string data { get; set; }
    }

    class ClientWS
    {
        private ClientWebSocket wsClient;
        string username = "";
        CancellationToken ct = new CancellationToken();

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

                wsClient = new ClientWebSocket();
                wsClient.ConnectAsync( new Uri("ws://" + ip + ":8082"), ct ); // pass in web socket URI, websocket port, and the cancellation token

                sendWSData( username, MessageType.Joined, "" );


            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to server");
                System.Environment.Exit(-1);
            }

            //
            Task.Factory.StartNew(() =>
            {
                // if the socket is open
                if( wsClient.State == WebSocketState.Open ){
                    // get data from the server
                    getWSData();
                }    
            });




            Console.Write("\nEnter Message and hit enter to send\n");

            while (true)
            {
                string text = Console.ReadLine();

                if (text != "" && text != null)
                {
                    sendWSData(username, MessageType.Message, text);
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

        /**
         * recieve data to the server websocket
         * */
        async void getWSData()
        {
            byte[] buffer = new byte[1024];
            ArraySegment<byte> bufferSegment = new ArraySegment<byte>(buffer);
            // "await" allows it to run async
            WebSocketReceiveResult result = await wsClient.ReceiveAsync(bufferSegment, ct);

            ChatMessage message = JsonConvert.DeserializeObject<ChatMessage>(Encoding.UTF8.GetString(bufferSegment.Array, 0, result.Count));

            Console.WriteLine(message.username + ": " + message.data);

            await Task.Factory.StartNew(() =>
            {
                getWSData();
            });
        }

        /**
         * transmits data to the server websocket
         * */
        async void sendWSData(string username, MessageType type, string message)
        {
            ChatMessage msg = new ChatMessage() { username = username, type = type, data = message }; // pass in a collection as the variables for an object
            string json = JsonConvert.SerializeObject(msg);

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(json);
            ArraySegment<byte> bufferSegment = new ArraySegment<byte>(buffer);
            await wsClient.SendAsync( bufferSegment, WebSocketMessageType.Binary, true, ct );
        }

    }
}
