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
        public static void CreateJSON(MandelbrotJSON mandelbrotJSON)
        {
            string directory = Directory.GetCurrentDirectory();

            string jsonString = JsonSerializer.Serialize(mandelbrotJSON);
            File.WriteAllText("../../../json/created.json", jsonString);
        }
        public static MandelbrotJSON ReadJSON(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            MandelbrotJSON mandelbrotJSON = JsonSerializer.Deserialize<MandelbrotJSON>(jsonString);

            return mandelbrotJSON;
        }
        public static MandelbrotJSON ReadFromString(string message)
        {
            return JsonSerializer.Deserialize<MandelbrotJSON>(message);
        }
        public static string WriteFromObject(MandelbrotJSON mandelbrotJSON)
        {
            return JsonSerializer.Serialize(mandelbrotJSON);
        }
    }
}
