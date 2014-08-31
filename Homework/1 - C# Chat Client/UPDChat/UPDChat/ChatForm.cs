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


        // chat screen
        private void sendBtn_Click(object sender, EventArgs e)
        {
            string text = clientInputTxt.Text;

            // dont send anything if empty.
            if( String.IsNullOrWhiteSpace(text) ) {
                return;
            }

            client.sendUDPData(client.username, UPDChat.Client.MessageType.Message, text);
            
            // put the text into the chatbox, and clear the input box
            string message = client.username + ": " + text + Environment.NewLine;
            chatRecievedBox.AppendText(message);
            clientInputTxt.Clear();

        } // end chat screen

        // show "user is typing" message
        private void clientInputTxt_TextChanged(object sender, EventArgs e)
        {
            Console.Write(client.username + " is typing...");
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
