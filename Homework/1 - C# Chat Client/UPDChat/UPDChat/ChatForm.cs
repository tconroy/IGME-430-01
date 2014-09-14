using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPDChat
{
    public partial class ChatForm : Form
    {

        private Client client;
        private Server server;

        Thread demoThread;
        delegate void SetTextCallback(string text);

        public ChatForm()
        {
            InitializeComponent();
            this.demoThread = new Thread(new ThreadStart(this.threadProcSafe));
            this.demoThread.Start();
        }

        public void threadProcSafe() { 
            // todo
        }


        // ---------------------
        // init screen
        // ---------------------
        private void button1_Click(object sender, EventArgs e)
        {

            if( clientRadio.Checked ){
                // switch to client settings tab
                tabContainer.SelectTab("clientSettingsTab");
            }
            else if( serverRadio.Checked ){
                // switch to server tab
                tabContainer.SelectTab("serverTab");
                
            }
            else {
                // error
                MessageBox.Show("Please select what you are trying to launch.");
            }
        }


        // ------------------
        // Client Screens
        // ------------------
        
        // settings screen
        private void connectBtn_Click(object sender, EventArgs e)
        {
            // grab user settings
            string username = usernameTxt.Text;
            string ipAddress = clientIpTxt.Text;
            string pw = clientPassTxt.Text;

            // start the client. pass in a reference to this form
            client = new Client(this);

            // pass in our settings
            client.startup(username, ipAddress, pw);
            tabContainer.SelectTab("clientChatTab");
        }
        // end settings screen


        // send on button click
        private void sendBtn_Click(object sender, EventArgs e)
        {
            this.sendMessage(client.username, UPDChat.Client.MessageType.Message, clientInputTxt.Text);
        }
        // send on enter
        private void clientInputTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.sendMessage(client.username, UPDChat.Client.MessageType.Message, clientInputTxt.Text);
            }
        }


        // fires off message to server, clears out local textbox
        private void sendMessage(string username, Client.MessageType messageType, string messageBody) {
            // breakout if message is empty.
            if( String.IsNullOrWhiteSpace( messageBody ) ){
                return;
            }
            client.sendUDPData(username, messageType, messageBody);
            clientInputTxt.Clear();
        }

        public void insertText(string message)
        {
            this.SetText(message);
        }

        public void SetText(string text)
        { 
            if (this.chatRecievedBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(this.SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.chatRecievedBox.AppendText((string)text + Environment.NewLine);
            }

            try
            {
                this.chatRecievedBox.ScrollToCaret();
            }
            catch (Exception e)
            {
                // could not scroll
                Console.WriteLine("Exception trying to scroll caret");
                Console.WriteLine(e.Data);
            }
        }



        // ----------------------
        // server screen
        // ----------------------
        private void serverBtn_Click(object sender, EventArgs e)
        {
            // grab server settings
            string serverName = serverNameTxt.Text;
            string serverPass = serverPassTxt.Text;

            // start the server
            server = new Server();
            
            // pass in settings
            if( server.startup(serverName, serverPass) ){
              // provide feedback in view to user
                serverBtn.Enabled     = false;
                serverNameTxt.Enabled = false;
                serverPassTxt.Enabled = false;
            }
        }



        // fires when form is closed
        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if( this.server != null ){
               //todo: this.server.shutDown();
            }

            if( this.client != null ){
                this.client.shutDown();
            }
        }

    }
}
