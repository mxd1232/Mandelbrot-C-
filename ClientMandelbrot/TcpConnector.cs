using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace ServerMandelbrot
{
    class TcpConnector
    {

        private static Socket socket;

        public static void Send(System.Drawing.Bitmap bitmap)
        {

            MemoryStream ms = new MemoryStream();
            // Save to memory using the Jpeg format
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] bmpBytes = ms.GetBuffer();
            bitmap.Dispose();
            ms.Close();


            try
                {
                socket.Send(bmpBytes);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Console.WriteLine("Serwer left");
                }               
        }
        public static string Recieve()
        {
           

                Byte[] recievedBytes = new Byte[1000];
                try
                {
                    int ret = socket.Receive(recievedBytes, recievedBytes.Length, 0);
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
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostadd = IPAddress.Parse("127.0.0.1");
            int port = 2222;
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            socket.Connect(EPhost);
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