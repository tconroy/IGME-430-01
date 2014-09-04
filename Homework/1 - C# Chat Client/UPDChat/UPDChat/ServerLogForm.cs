using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UPDChat
{
    public partial class ServerLogForm : Form
    {

        Server server;

        delegate void SetTextCallback(string text);

         public ServerLogForm(ref Server activeServer)
        {
            InitializeComponent();
            server = activeServer;
        }


        private void ServerLogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.Write("closed");
            Application.Exit();           
        }


        public void SetText(string text)
        {
            if (ServerLogTxt.InvokeRequired)
            {
                ServerLogTxt.Invoke(new MethodInvoker(delegate { ServerLogTxt.AppendText(text + Environment.NewLine); }));
            }
            else
            {
                ServerLogTxt.AppendText((string)text + Environment.NewLine);
            }
        }

        private void ThreadProcSafe()
        {
            this.SetText("");
        }

    }
}
