using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//need to import these
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using Newtonsoft.Json;
using SuperWebSocket;

namespace ConsoleMessageServer
{
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }

    //This will be a class we will use for messages
    //Objects of this class will be sent as message
    public class ChatMessage
    {
        public MessageType type { get; set; }
        public string username { get; set; }
        public string data { get; set; }
    }


    class ServerWS
    {
        //Server and client objects
        WebSocketServer webSocket;
        //WebSocketSession client; 
        List<WebSocketSession> wsClients = new List<WebSocketSession>();

        public void startup()
        {
            //Create a new task to start the websocket server
            Task.Factory.StartNew(() =>
            {
                //setup new server on port 8082 (an arbitrary port)
                webSocket = new WebSocketServer();
                webSocket.Setup(8082);
                //map all of the websocket events to local methods
                //these will handle new sessions, message receiving and ending of sessions
                webSocket.NewSessionConnected += webSocket_NewSessionConnected;
                webSocket.NewDataReceived += webSocket_NewDataReceived;
                webSocket.SessionClosed += webSocket_SessionClosed;

                //start the websocket server
                webSocket.Start();
            });

            Console.WriteLine("Hit ctrl-c twice to exit");

            //only here to keep the console open
            Console.ReadKey();
        }

        //upon new connections
        void webSocket_NewSessionConnected(WebSocketSession session)
        {
            //this will set our client to the last person to connect
            //in the chat server homework, you will need to keep many sessions, not just one
            //client = session;
            wsClients.Add(session);
        }

        //upon reciving a message
        void webSocket_NewDataReceived(WebSocketSession session, byte[] value)
        {
            //get the json string from the byte[] value
            string buffer = System.Text.Encoding.ASCII.GetString(value);

            //Get the array from the buffersegment and deserialize the result into a ChatMessage starting at position 0 in the array and going as long as the result
            ChatMessage chatMessage = JsonConvert.DeserializeObject<ChatMessage>(Encoding.ASCII.GetString(value, 0, value.Length));

            //check the message type from our message object and send a message
            if (chatMessage.type == MessageType.Joined)
            {
                SendChatMessage(chatMessage.username, " has joined the server");
            }
            if (chatMessage.type == MessageType.Left)
            {
                SendChatMessage(chatMessage.username, " has left the server");
            }
            if (chatMessage.type == MessageType.Message)
            {
                SendChatMessage(chatMessage.username, chatMessage.data);
            } 
            
        }

        //upon session closing
        void webSocket_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            //in this example we are not going to use this, but it is one of the built in events
        }

        //sending a chat message
        public void SendChatMessage(string username, string data)
        {
            //start a new task so that code calling this does not wait for completion before returning
            Task.Factory.StartNew(() =>
            {
                //Create a new chat message object setting the object's properties
                ChatMessage msg = new ChatMessage() { username = username, type = MessageType.Message, data = data };

                //serialize the object into a json string
                string json = JsonConvert.SerializeObject(msg);

                //convert the json string to a byte[] like we did with TCP and UDP
                byte[] wsbuffer = System.Text.Encoding.ASCII.GetBytes(json);

                //conver the byte[] into array segments to send as websocket messages 
                ArraySegment<byte> bufferSegment = new ArraySegment<byte>(wsbuffer);

                //send out the array segements to the client
                //in your homework, you will need to send a message to every connected client
                //client.Send(bufferSegment);
                foreach( WebSocketSession client in wsClients )
                {
                    client.Send(bufferSegment);
                }

            });
        }

    }
}
