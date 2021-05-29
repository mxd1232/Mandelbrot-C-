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

        double Increment;
  
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

            CountIncrement();

            SetAllPixelsColor(bitmap);
            
            return bitmap;
        }    
        public void SetAllPixelsColor(Bitmap bitmap)
        {
            for (int xPixel = 0; xPixel < WidthPixel; xPixel++)
            {
                for (int yPixel = 0; yPixel < HeightPixel; yPixel++)
                {
                    SetPixelColor(bitmap, xPixel, yPixel);
                }
            }
        }


        public void SetPixelColor(Bitmap bitmap, int xPixel,int yPixel)
        {
            int i = CountIterations(xPixel, yPixel);
            bitmap.SetPixel(xPixel, yPixel, i < maxIterations ? Color.FromArgb(20, 20, i % 255) : Color.Black);
        }
        public int CountIterations(int xPixel,int yPixel)
        {
            Complex c = Constant(xPixel, yPixel);
            Complex z = new Complex { A = 0, B = 0 };


            int i = 0;
            do
            {
                i++;
                z.Square();
                z.Add(c);
                if (Math.Pow(z.Magnitude(), 2) >4.0) break;
            }
            while (i < maxIterations);

            return i;
        }
  

        public Complex Constant(int x, int y)
        {
            if (Increment == 0)
            {
                 CountIncrement();
            }


            Complex c = new Complex
            {
                A = (-Increment * WidthPixel / 2 + center[0] + Increment * x),
                B = (Increment * HeightPixel / 2 + center[1] - Increment * y)
            };
            return c;
        }

        //making first field always smaller
        public void CountIncrement()
        {
            Increment = 4 / zoom / Diameter();
        }

        public double Diameter()
        {
            return Math.Min(HeightPixel, WidthPixel);
        }

    }
}
