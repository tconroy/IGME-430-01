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

    public class ChatMessage
    {
        public MessageType type { get; set; }
        public string username { get; set; }
        public string data { get; set; }
    }


    class ServerWS
    {

        WebSocketServer webSocket;
        WebSocketSession client; 

        public void startup()
        {


            Console.WriteLine("Hit ctrl-c twice to exit");

            //only here to keep the console open
            Console.ReadKey();
        }

        void webSocket_NewSessionConnected(WebSocketSession session)
        {

        }

        void webSocket_NewDataReceived(WebSocketSession session, byte[] value)
        {

        }

        void webSocket_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
        }

        public void SendChatMessage(string username, string data)
        {

        }

    }
}
