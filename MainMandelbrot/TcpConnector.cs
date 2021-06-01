using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public static List<Socket> sockets = new List<Socket>();


        public static Bitmap Recieve(int scktID)
        {
            Bitmap bmpReturn = null;

            Byte[] recievedBytes = new Byte[1000000];
            try
            {
                int ret = sockets[scktID].Receive(recievedBytes, recievedBytes.Length, 0);


                MemoryStream memoryStream = new MemoryStream(recievedBytes);
                memoryStream.Position = 0;
              //  bmpReturn = new Bitmap(memoryStream);
                bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);


                memoryStream.Close();
                memoryStream = null;

            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Serwer left");
            }



            return bmpReturn;

          
        }

        public static void Send(int scktID)
        {
            try
            {
                sockets[scktID].SendFile(ConverterJSON.CreatedFilePath);
              
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Serwer left");
            }

        }

        private static bool Connect(string ipAddres, int port)
        {
            //"127.0.0.1" - ip

            Socket sckt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostadd = IPAddress.Parse(ipAddres);
           // int port = 2222;
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);

            try 
            {
                sckt.Connect(EPhost);

            }
            catch (SocketException e)
            {
                return false;
            }

           
            if (sckt.Connected)
            {
                Console.WriteLine(" Poprawne polączenie ");
                sockets.Add(sckt);
                return true;
            }
            else
            {
                return false;
            }
            

        }
        public static bool ConnectToTCP(string ipAddres, int port)
        {
            if (Connect(ipAddres, port))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

      

        private static string CleanUpMessage(string message)
        {
            return message.Replace("\0", string.Empty);

        }
    }
}
