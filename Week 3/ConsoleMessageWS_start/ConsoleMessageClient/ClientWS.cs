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

        async void getWSData()
        {
            
        }

        async void sendWSData(string username, MessageType type, string message)
        {

        }

    }
}
