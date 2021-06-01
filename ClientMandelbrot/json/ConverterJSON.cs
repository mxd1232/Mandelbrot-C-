using ClientMandelbrot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServerMandelbrot
{
    class ConverterJSON
    {
        public static string RecievedFilePath { get; set; } = "../../../json/recieved.json";
        public static string CreatedFilePath { get; internal set; }

        public static MandelbrotJSON ReadJSON(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                MandelbrotJSON mandelbrotJSON = JsonSerializer.Deserialize<MandelbrotJSON>(jsonString);

                return mandelbrotJSON;
            }
            catch
            {
               

                TcpConnector.End();
                TcpConnector.Serve();

                return null;
            }

            
        }
        public static MandelbrotJSON ReadFromString(string message)
        {
            try
            {

                return JsonSerializer.Deserialize<MandelbrotJSON>(message);

            }
            catch
            {
                TcpConnector.End();
                TcpConnector.Serve();
                return null;
            }

        }
    }
}
