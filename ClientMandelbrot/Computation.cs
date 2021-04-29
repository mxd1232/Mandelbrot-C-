using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMandelbrot
{
    class Computation
    {


        public static double[] center = { 0, 0 };

        int maxIterations = 200;
        double zoom = 1;

        int WidthPixel = 700;
        int HeightPixel = 700;


        public List<List<int>> MandelbrotIterations = new List<List<int>>();

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
        public void ComputeMandelbrot()
        {

            double increment = 4 / zoom / Diameter();


            for (int xPixel = 0; xPixel < WidthPixel; xPixel++)
            {
                List<int> PixelList = new List<int>();

                for (int yPixel = 0; yPixel < HeightPixel; yPixel++)
                {

                    Complex c = constant(increment, xPixel, yPixel);
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
                    PixelList.Add(i);
                }
                MandelbrotIterations.Add(PixelList);
            }
        }

        public MandelbrotJSON CreateMandelbrotObject()
        {
            MandelbrotJSON mandelbrotJSON = new MandelbrotJSON()
            {
                Center = center,
                Zoom = zoom,
                HeightPixel = HeightPixel,
                WidthPixel = WidthPixel,
                MaxIterations = maxIterations,
                MandelbrotMatrix = ListToMatrix(MandelbrotIterations)
            };
            return mandelbrotJSON;
        }
        public int[][] ListToMatrix(List<List<int>> mandelbrot)
        {
            int[][] arrays = mandelbrot.Select(a => a.ToArray()).ToArray();

            return arrays;
        }

        public Complex constant(double increment, int x, int y)
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
