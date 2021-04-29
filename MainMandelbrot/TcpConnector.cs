using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mandelbrot_Whole
{
    class TcpConnector
    {
        private static TcpListener tcpLsn;
        private static Socket sckt;


        public static string Recieve()
        {

                Byte[] recievedBytes = new Byte[2000000];
                try
                {
                    int ret = sckt.Receive(recievedBytes, recievedBytes.Length, 0);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Console.WriteLine("Client left");

                }
                string tmp = null;
                tmp = System.Text.Encoding.ASCII.GetString(recievedBytes);
                if (tmp.Length > 0)
                {
                    tmp = CleanUpMessage(tmp);                
                }
            return tmp;
        }


        public static void Send(string message)
        {      
                Byte[] byteData = Encoding.ASCII.GetBytes(message.ToCharArray());
                try
                {
                    sckt.Send(byteData, byteData.Length, 0);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Client left");

                }
                Console.WriteLine(message);
          
        }
        public static void End()
        {
            sckt.Close();
            tcpLsn.Stop();
        }
        public static void Serve()
        {
            tcpLsn = new TcpListener(IPAddress.Parse("127.0.0.1"), 2222);
            tcpLsn.Start();
            sckt = tcpLsn.AcceptSocket();
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
