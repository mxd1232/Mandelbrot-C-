using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;

namespace ServerMandelbrot
{
    class TcpConnector
    {
        private static TcpListener tcpLsn;
        private static Socket socket;

        public static void Send(System.Drawing.Bitmap bitmap)
        {
            

            MemoryStream ms = new MemoryStream();
            // Save to memory using the Jpeg format
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

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
            Console.WriteLine(message);
            return message;
        }

        public static void End()
        {
            socket.Close();
            tcpLsn.Stop();
        }
        public static void Serve()
        {
            
            int port = 2222;

            while (true)
            {

                try
                {
                    tcpLsn = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                    
                    tcpLsn.Start();
                    socket = tcpLsn.AcceptSocket();

                    return;
                }
                catch(Exception e)
                {
                   // Console.WriteLine(e.Message);
                }
                port++;
            }
           
        }
        public static void ConnectToTCP()
        {
            Console.WriteLine("server");
            Serve();

        }

        private static string CleanUpMessage(string message)
        {
            return message.Replace("\0", string.Empty);
        }
    }
}