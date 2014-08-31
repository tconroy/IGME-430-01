using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPDChat
{
    public partial class ChatForm : Form
    {

        private Client client;
        private Server server;

        public ChatForm()
        {
            InitializeComponent();
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

            // start the client
            client = new Client();

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


        // show "user is typing" message
        // TODO
        private void clientInputTxt_TextChanged(object sender, EventArgs e)
        {
            Console.Write(client.username + " is typing...");
        }

        // fires off message to server, clears out local textbox
        private void sendMessage(string username, Client.MessageType messageType, string messageBody) {

            // breakout if message is empty.
            if( String.IsNullOrWhiteSpace( messageBody ) ){
                return;
            }
            
            client.sendUDPData(username, messageType, messageBody);
            clientInputTxt.Clear();


            // put the text into the chatbox, and clear the input box
            string message = client.username + ": " + messageBody + Environment.NewLine;
            chatRecievedBox.AppendText(message);

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
            //server.startup(serverName, serverPass);

            if( server.startup(serverName, serverPass) ){
              // provide feedback in view to user
                serverBtn.Enabled = false;
            }
        }

    }
}
