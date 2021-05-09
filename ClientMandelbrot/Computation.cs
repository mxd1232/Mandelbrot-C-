using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace ClientMandelbrot
{
    class Computation
    {


        public static double[] center = { 0, 0 };

        int maxIterations = 200;
        double zoom = 1;

        int WidthPixel = 700;
        int HeightPixel = 700;
  
        public Computation()
        {

        }
        public Computation(MandelbrotJSON mandelbrotJSON)
        {
            center = mandelbrotJSON.Center;
            maxIterations = mandelbrotJSON.MaxIterations;
            zoom = mandelbrotJSON.Zoom;
            WidthPixel = mandelbrotJSON.WidthPixel;
            HeightPixel = mandelbrotJSON.HeightPixel;
        }
        public Bitmap ComputeMandelbrot()
        {

            Bitmap bitmap = new Bitmap(WidthPixel, HeightPixel);

            double increment = 4 / zoom / Diameter();


            for (int xPixel = 0; xPixel < WidthPixel; xPixel++)
            {
                List<int> PixelList = new List<int>();

                for (int yPixel = 0; yPixel < HeightPixel; yPixel++)
                {

                    Complex c = Constant(increment, xPixel, yPixel);
                    Complex z = new Complex { A = 0, B = 0 };

                    int i = 0;
                    do
                    {
                        i++;
                        z.Square();
                        z.Add(c);
                        if (z.Magnitude() > 2.0) break;
                    }
                    while (i < maxIterations);
                    bitmap.SetPixel(xPixel, yPixel, i < maxIterations ? Color.FromArgb(20, 20, i % 255) : Color.Black);
                  
                }
               
            }
            return bitmap;
        }    


        public Complex Constant(double increment, int x, int y)
        {
            if (increment == 0)
            {
                increment = 4 / zoom / Diameter();
            }


            Complex c = new Complex
            {
                A = (-increment * WidthPixel / 2 + center[0] + increment * x),
                B = (increment * HeightPixel / 2 + center[1] - increment * y)
            };
            return c;
        }

        //making first field always smaller
        public double Diameter()
        {
            return Math.Min(HeightPixel, WidthPixel);
        }

    }
}
