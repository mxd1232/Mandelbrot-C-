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

            //while (true)
            //{
            //	//creating object of TcpClient to catch the stream of connected computer
            //	TcpClient client = tcpLsn.AcceptTcpClient();

            //	//getting the networkclient stream object
            //	NetworkStream clientstream = client.GetStream();

            //	//creating streamreader object to read messages from client
            //	StreamReader reader = new StreamReader(clientstream);

            //	// reading file name from client
            //	string sourcefile = reader.ReadLine();

            //	Stream inputstream;
            //	try
            //	{
            //		//opening file in read mode
            //		inputstream = File.OpenRead(sourcefile);
            //	}
            //	catch
            //	{
            //		Console.WriteLine("\n\n  File not found named: {0}", sourcefile);

            //		clientstream.Close();
            //		continue;
            //	}
            //	const int sizebuff = 1024;
            //	try
            //	{
            //		/*creating the bufferedstrem object for reading 1024 size of bytes from the 
            //			file */
            //		BufferedStream bufferedinput = new BufferedStream(inputstream);

            //		/*creating the bufferedstrem object for sending bytes which are read 
            //			from file   */
            //		BufferedStream bufferedoutput = new BufferedStream(clientstream);

            //		/* creating array of bytes size is 1024 */
            //		byte[] buffer = new Byte[sizebuff];
            //		int bytesread;

            //		/* Reading bytes from the file until the end */
            //		while ((bytesread = bufferedinput.Read(buffer, 0, sizebuff)) > 0)
            //		{
            //			/* sending the bytes to the client */
            //			bufferedoutput.Write(buffer, 0, bytesread);
            //		}
            //		Console.WriteLine("\n\n    file copied name:{0}", sourcefile);

            //		/* Closing connections*/
            //		bufferedoutput.Flush();
            //		bufferedinput.Close();
            //		bufferedoutput.Close();
            //	}
            //	catch (Exception)
            //	{
            //		Console.WriteLine("\n\n Connection Couldnot Esablished  because Client forget to create-\n \"ftpService\" folder in his (C) Drive or his/her harddisk is full or Client close its Connection in between process");
            //		reader.Close();
            //		continue;
            //	}

            //	reader.Close();
            //}
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

        private static void Connect(string ipAddres, int port)
        {
            //"127.0.0.1" - ip

            

            Socket sckt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostadd = IPAddress.Parse(ipAddres);
           // int port = 2222;
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            sckt.Connect(EPhost);
            Console.WriteLine(" Poprawne polączenie ");

            sockets.Add(sckt);

        }
        public static void ConnectToTCP(string ipAddres, int port)
        {
            Connect(ipAddres, port);

        }

      

        private static string CleanUpMessage(string message)
        {
            return message.Replace("\0", string.Empty);

        }
    }
}
