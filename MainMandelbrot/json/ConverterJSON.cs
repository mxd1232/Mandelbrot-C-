using Mandelbrot_Whole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mandelbrot_Whole
{
    class ConverterJSON
    {
        public static string CreatedFilePath { get; set; } = "../../json/created.json";
        public static string CreateJSON(MandelbrotJSON mandelbrotJSON)
        {
            string directory = Directory.GetCurrentDirectory();

            string jsonString = JsonSerializer.Serialize(mandelbrotJSON);
            File.WriteAllText(CreatedFilePath, jsonString);

            return jsonString;
        }

        public static string WriteFromObject(MandelbrotJSON mandelbrotJSON)
        {
            return JsonSerializer.Serialize(mandelbrotJSON);
        }
    }
}
