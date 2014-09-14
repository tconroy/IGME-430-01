using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//import net package
using System.Net.Sockets;

namespace ConsoleMessageServer
{
    //delegate functions to use for our events
    //the methods that will be called are in the server class
    public delegate void Left(ClientManager item, string username);
    public delegate void Joined(string username);
    public delegate void Message(string userName, string message);
    public delegate void Command(ClientManager client, string username, string message);

    //class to hold and manage each TCP stream
    //each stream will listen independently so we will have an object for each
    public class ClientManager
    {
        //mapping events to our delegates above
        //this means when these events fire, they will call the delegate
        //the delegates are mapped to server methods in the server class
        public event Left OnLeft;
        public event Message OnMessage;
        public event Joined OnJoined;
        public event Command onCommand;

        //This TCP connection's socket and stream along with accessors and mutators
        public Socket socket { get; set; }
        public NetworkStream networkStream { get; set; }

        public string username;

        //constructor accepts a socket of the connected client
        public ClientManager(Socket clientSocket)
        {
            //set the socket for this object and grab the stream
            //this object will manage all of the stream operations for this client
            this.socket = clientSocket;
            this.networkStream = new NetworkStream(this.socket);

            //start a new task to listen for messages from the TCP connection for this client
            //Because this is a new task, the constructor will return and the server will continue running
            //this prevents code from getting blocked while this TCP connection is listening
            Task.Factory.StartNew(() =>
            {
                //while this socket is connected
                //"Connected" is a built in property of a socket telling it if the stream is still open
                while (socket.Connected)
                {
                    //create a new buffer to read. We don't know the size, so 1024 should be enough for now
                    byte[] buffer = new byte[1024];

                    //readBytes will be a variable to hold how many bytes we read on each read
                    int readBytes = 0;
                    
                    //read in one byte from the stream starting at position [0] in the stream and write it to the buffer
                    //this will fill the buffer with one byte
                    /*try
                    {
                        
                    }
                    catch (Exception e)
                    { 
                        // todo
                    }*/

                    readBytes = this.networkStream.Read(buffer, 0, 1);

                    //variables to hold our message data
                    short usernameLength;
                    short messageLength;
                    username = "";
                    string message  = "";

                    //check if we actually read any bytes. If not the stream was empty
                    if (readBytes != 0)
                    {
                        //read 2 more positions from the stream starting at stream position [1] and add it to the buffer
                        readBytes = this.networkStream.Read(buffer, 1, 2);
                        //now that we just read the username length bytes into the buffer
                        //read the bytes from the buffer and get the username length
                        usernameLength = (short)(buffer[1] + (buffer[2] * 256));

                        //read 1 position from the stream starting at stream position [3] and add it to the buffer
                        readBytes = this.networkStream.Read(buffer, 3, 1);
                        //read the new byte from the buffer to get the messageLength
                        messageLength = (short)(buffer[3]);

                        //read the username from the stream to the buffer so we can get to it
                        readBytes = this.networkStream.Read(buffer, 4, usernameLength);
                        //read the username from the buffer
                        username = Encoding.ASCII.GetString(buffer, 4, usernameLength);

                        /**This try catch is here because these functions will call the delegates which will call the send method
                           If the send method tries to send to a client that is no longer connected
                           an exception will be thrown and cause this code to break thus stopping connection to this client **/
                        try
                        {
                            //if the message type was joined call the onJoined event which will call the delegate
                            if (buffer[0] == (byte)MessageType.Joined)
                            {
                                OnJoined(username);
                            }
                            //if the message type was left call the onLeft event which will call the delegate
                            else if (buffer[0] == (byte)MessageType.Left)
                            {
                                OnLeft(this, username);
                            }
                            // if the user issued a command "/" delegate it here
                            else if (buffer[0] == (byte)MessageType.Command)
                            {
                                onCommand(this, username, message);
                            }
                            //if the message type was message call the onMessage event which will call the delegate
                            else if (buffer[0] == (byte)MessageType.Message)
                            {
                                // read the message from the stream to the buffer
                                readBytes = this.networkStream.Read(buffer, 4 + usernameLength, messageLength);
                                //read the message from the buffer to the stream
                                message = Encoding.ASCII.GetString(buffer, 4 + usernameLength, messageLength);

                                // call the onMessage event to send a message in the server
                                OnMessage(username, message);
                            }
                        }
                        //catch any socket errors that occur and keep running
                        catch (Exception e)
                        {
                        }
                    }

                }

                //if the loop breaks out because this socket gets disconnected
                //call the onLeft event
                OnLeft(this, this.username);

            });
        }
    }
}
