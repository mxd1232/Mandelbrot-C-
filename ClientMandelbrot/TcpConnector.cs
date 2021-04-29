using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerMandelbrot
{
    class TcpConnector
    {

        private static Socket s;

  

        public static void Send(string message)
        {        
                Byte[] byteData = Encoding.ASCII.GetBytes(message.ToCharArray());
                try
                {
                    s.Send(byteData, byteData.Length, 0);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Console.WriteLine("Serwer left");
                }
               
               

            
        }
        public static string Recieve()
        {
            
                Byte[] recievedBytes = new Byte[2000000];
                try
                {
                    int ret = s.Receive(recievedBytes, recievedBytes.Length, 0);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Console.WriteLine("Serwer left");
                }
                string message = null;
                message = System.Text.Encoding.ASCII.GetString(recievedBytes);
                if (message.Length > 0)
                {
                    message = CleanUpMessage(message);
                    if (String.Equals(message, ""))
                    {
                        Console.WriteLine("Serwer left");
                    }
                }
            return message;
        }

        private static void Connect()
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostadd = IPAddress.Parse("127.0.0.1");
            int port = 2222;
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            s.Connect(EPhost);
            Console.WriteLine(" client ");

        }
         public static void ConnectToTCP()
        {
            Connect();

        }
        private static string CleanUpMessage(string message)
        {
            return message.Replace("\0", string.Empty);


        }
    }
}