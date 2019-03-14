using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace serv
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            //sett local address
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("wait for client...");
            listener.Start();
            
            //get client
            TcpClient client = listener.AcceptTcpClient();

            //set network stream
            NetworkStream nwStream;
            byte[] buffer = null;
            
            string SendText;
            Console.WriteLine("Connected");
            while (true)
            {
                
                nwStream = client.GetStream();
                buffer = new byte[client.ReceiveBufferSize];

                //read byte data
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //convert string
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received : " + dataReceived);

                Console.WriteLine("Sending back : ");
                SendText = Console.ReadLine();

                if (SendText == "end")
                {

                    byte[] bufferToSend = Encoding.ASCII.GetBytes(SendText);

                    //send to the client
                    nwStream.Write(bufferToSend, 0, bufferToSend.Length);
                    
                    //nwStream.Close();
                    client.Close();
                    listener.Stop();
                    break;
                }
                else
                {
                    byte[] bufferToSend = Encoding.ASCII.GetBytes(SendText);

                    //send to the client
                    nwStream.Write(bufferToSend, 0, bufferToSend.Length);
                }
                
                
              
                
            }
            
        }
    }
}