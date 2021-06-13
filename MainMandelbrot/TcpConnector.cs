using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class TcpConnector
    {
        public static List<Socket> Sockets = new List<Socket>();
        private static DateTime[] t1 = new DateTime[100];
        private static DateTime[] t4 = new DateTime[100];

        public static List<SpeedTest> SpeedTests = new List<SpeedTest>();
       
        public static Bitmap Recieve(int scktID)
        {
            Bitmap bmpReturn = null;

            Byte[] recievedBytes = new Byte[1000000];
            try
            {
                int ret = Sockets[scktID].Receive(recievedBytes, recievedBytes.Length, 0);
                t4[scktID] = DateTime.Now;
                byte[] timeBytes = recievedBytes.Take(8).ToArray();

                double computiationTimeInSeconds = BitConverter.ToDouble(timeBytes,0);

                TimeSpan fullTime = t4[scktID].Subtract(t1[scktID]);
                double communicationTimeInSeconds = fullTime.TotalSeconds - computiationTimeInSeconds;


                SpeedTests.Add(new SpeedTest()
                {
                    FullTime = fullTime.TotalSeconds,
                    CommunicationTime = communicationTimeInSeconds,
                    ComputationTime = computiationTimeInSeconds,
                    ScktID = scktID
                });

             /*   Debug.WriteLine("full time: " + fullTime + 
                    " Time of computation: " + computiationTimeInMiliseconds + 
                    " Time of communication: " + communicationTimeInMiliseconds +
                    " socketID:" +scktID);
              */

             
                byte[] bitmapBytes = recievedBytes.Skip(8).ToArray();


                MemoryStream memoryStream = new MemoryStream(bitmapBytes);
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
        public static void Send(int scktID,string createdFilePath)
        {
            try
            {
                t1[scktID] = DateTime.Now;
                Sockets[scktID].SendFile(createdFilePath);
              
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
                Sockets.Add(sckt);
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
