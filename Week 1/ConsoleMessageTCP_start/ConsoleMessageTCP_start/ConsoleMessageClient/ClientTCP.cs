using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace ConsoleMessageClient
{
    public enum MessageType
    {
        Joined,
        Left,
        Message
    }

    class ClientTCP
    {
        private TcpClient tcpClient;
        string username = "";
        NetworkStream tcpStream;

        public void startup()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(shutdown);

            string ip;

            while (username == "")
            {
                Console.Write("\nEnter username: ");

                username = Console.ReadLine();
            }
        }

        /**
         * Only fires on ctrl-C events
         * */
        public void shutdown(object sender, ConsoleCancelEventArgs args)
        {

        }

        void getTCPData()
        {

        }

    }
}
