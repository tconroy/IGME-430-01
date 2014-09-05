using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerTCP server = new ServerTCP();
            server.startup();
        }
    }
}
