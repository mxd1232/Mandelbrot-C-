using ServerMandelbrot;
using System;
using System.Drawing;

namespace ClientMandelbrot
{
    class Program
    {

       
        static void Main(string[] args)
        {

            TcpConnector.ConnectToTCP();
            ConnectionLoop();
          
        }
        static void ConnectionLoop()
        {
            while (true)
            {
                string message = TcpConnector.Recieve();
                TcpConnector.Send(ManageComputationRequest(message));
            }
        }
        static Bitmap ManageComputationRequest(string message)
        {
            MandelbrotJSON mandelbrotJSON = ConverterJSON.ReadFromString(message);
            if(mandelbrotJSON==null)
            {
                TcpConnector.End();
                TcpConnector.Serve();
            }

            Computation computation = new Computation(mandelbrotJSON);
            return computation.ComputeMandelbrot();
        }

    }
}

