using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;


namespace client
{
    class Program
    {

        
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        
        static void Main(string[] args)
        {
            Dota2 dotadata = new Dota2();
            //a.ID = 112344;
            //Console.WriteLine(dotadata.ID);
            Console.ReadLine();


            //---data to send to the server---
            string textToSend;

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream;
            string a;
            byte[] bytesToSend = null;
            Console.WriteLine("Connected");
            while (true)
            {

                try
                {
                    nwStream = client.GetStream();

                    //set text to send
                    Console.WriteLine("Text To Send : ");
                    textToSend = Console.ReadLine();
                    Console.WriteLine("Sending : " + textToSend);
                    bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

                    //send the text
                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                    //read text
                    byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                    int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

                    Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                    a = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                    //Console.ReadLine();
                    if (a == "end")
                    {
                        client.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    break;

                }


            }
        }

        private static void encryption(string data)
        {

           
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
            

        }
        private static void decryption(string data)
        {

            byte[] toEncrypt = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;


        }
   
    }
    public class Dota2
    {
        private int IDPlayer = 100;
        public int positionX, positionY;
        public int Rotation,kill,death;
        public bool Skill1, Skill2;
        //public int Target;

        public int ID
        {
            get
            {
                return IDPlayer;
            }
         
        }
    }
}
