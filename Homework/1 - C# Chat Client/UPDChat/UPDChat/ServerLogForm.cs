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
        Thread demoThread;

        delegate void SetTextCallback(string text);

         public ServerLogForm(ref Server activeServer)
        {
            InitializeComponent();
            server = activeServer;
            this.demoThread = new Thread(new ThreadStart(this.ThreadProcSafe));
            this.demoThread.Start();
        }


        private void ServerLogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.Write("closed");
            // stop listening on the server, re-enable the buttons and inputs
            Application.Exit();           
        }


        public void SetText(string text)
        {
            
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true.

            SetTextCallback d = new SetTextCallback(SetText);
            if (this.ServerLogTxt.InvokeRequired)
            {
                //SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.ServerLogTxt.AppendText((string)text + Environment.NewLine);
            }
        }

        private void ThreadProcSafe()
        {
            this.SetText("");
        }

    }
}
