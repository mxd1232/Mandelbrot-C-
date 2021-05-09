using ServerMandelbrot;
using System;


namespace ClientMandelbrot
{
    class Program
    {

       
        static void Main(string[] args)
        {

            TcpConnector.ConnectToTCP();

            while (true)
            {
                string message = TcpConnector.Recieve();

                MandelbrotJSON mandelbrotJSON = ConverterJSON.ReadFromString(message);

                Computation computation = new Computation(mandelbrotJSON);

                System.Drawing.Bitmap bitmap = computation.ComputeMandelbrot();


                TcpConnector.Send(bitmap);
            }
          
        }
       

    }
}

