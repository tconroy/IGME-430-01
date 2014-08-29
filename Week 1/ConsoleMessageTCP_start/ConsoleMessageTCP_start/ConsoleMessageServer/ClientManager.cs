using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ConsoleMessageServer
{
    public delegate void Left(ClientManager item);
    public delegate void Joined(string username);
    public delegate void Message(string userName, string message);

    public class ClientManager
    {
  
    }
}
