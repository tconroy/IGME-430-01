﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMessageClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientWS client = new ClientWS();
            client.startup();
        }
    }
}
