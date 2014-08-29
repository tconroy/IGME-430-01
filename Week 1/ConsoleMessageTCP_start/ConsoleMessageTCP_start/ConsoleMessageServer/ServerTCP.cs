using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace ConsoleMessageServer
{
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }


    class ServerTCP
    {

        Socket tcpSocket;
        List<ClientManager> tcpClients = new List<ClientManager>();
        byte[] buffer = new byte[1024];

        public void startup()
        {

        }

        void OnAccept(IAsyncResult connection)
        {

        }

        void OnLeft(ClientManager item)
        {

        }

        void OnJoined(string username)
        {

        }

        void OnMessage(string username, string message)
        {

        }

        void sendChatMessage(string username, string message)
        {

        }

    }
}
