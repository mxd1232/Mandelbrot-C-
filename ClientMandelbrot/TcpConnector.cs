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

        private static DateTime t2;
        private static DateTime t3;


        public static void Send(System.Drawing.Bitmap bitmap)
        {
            //time
            t3 = DateTime.Now;
            TimeSpan CommunicationTime = t3.Subtract(t2);          
            byte[] timeBytes = BitConverter.GetBytes(CommunicationTime.TotalMilliseconds);

            //bitmap
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bitmapBytes = ms.GetBuffer();
            bitmap.Dispose();
            ms.Close();


            //add time

            byte[] messageBytes = CombineByteArrays(timeBytes, bitmapBytes);

            try
            {
            socket.Send(messageBytes);
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Serwer left");
            }               
        }
        private static byte[] CombineByteArrays(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        public static string Recieve()
        {       
                Byte[] recievedBytes = new Byte[1000];
                try
                {
                    int ret = socket.Receive(recievedBytes, recievedBytes.Length, 0);
                    t2 = DateTime.Now;
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

            Console.WriteLine("Enter ip: ");
            string serverIp = Console.ReadLine();
           // string serverIp = "127.0.0.1";

            Console.WriteLine("Enter port: ");

            int port = Convert.ToInt32(Console.ReadLine());
         //   int port = 2222;

            while (true)
            {

                try
                {
                    tcpLsn = new TcpListener(IPAddress.Parse(serverIp), port);
                    
                    tcpLsn.Start();
                    Console.WriteLine("Waiting on IP: {0}, port: {1}", serverIp, port);
                    socket = tcpLsn.AcceptSocket();
                    break;
                }
                catch(Exception e)
                {
                    //Console.WriteLine(e.Message);

                    port++;
                    Console.WriteLine("Port taken. Trying port: "+ port);
                }


            }
           
        }
        public static void ConnectToTCP()
        {

            Serve();
            Console.WriteLine("Connected");


        }

        private static string CleanUpMessage(string message)
        {
            return message.Replace("\0", string.Empty);
        }
    }
}