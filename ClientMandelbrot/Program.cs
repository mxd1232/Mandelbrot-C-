using ServerMandelbrot;
using System;


namespace ClientMandelbrot
{
    class Program
    {

       
        static void Main(string[] args)
        {
            Computation computation = new Computation();

            computation.ComputeMandelbrot();

               MandelbrotJSON mandelbrotJSON = computation.CreateMandelbrotObject();
               ConverterJSON.CreateJSON(mandelbrotJSON);

               MandelbrotJSON recievedMandelbrotJSON = ConverterJSON.ReadJSON("../../../json/recieved.json");
           
           /*
            TcpConnector.ConnectToTCP();
            string message = TcpConnector.Recieve();
            MandelbrotJSON mandelbrotJSON = ConverterJSON.ReadFromString(message);
            
            Computation computation = new Computation(mandelbrotJSON);
            computation.ComputeMandelbrot();

            mandelbrotJSON = computation.CreateMandelbrotObject();
            
            TcpConnector.Send(ConverterJSON.WriteFromObject(mandelbrotJSON));
            */
        }
       

    }
}

