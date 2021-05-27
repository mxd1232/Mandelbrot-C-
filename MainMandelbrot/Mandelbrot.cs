using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot_Whole
{
    public partial class Mandelbrot : Form
    {
        int ZoomIteration = 0;

        public static double[] centerPrev  = { 0,0 };
        public static double[] center = { 0,0 };

        int maxIterations = 200;
        double zoom = 1;
        double zoomPrev;

        int WidthPixel;
        int HeightPixel;

        int[] XRangeIncremental = new int[] { 0, 0 };
        int[] YRangeIncremental = new int[] { 0, 0 };

        int[] XRange = new int[] { 0, 0 };
        int[] YRange = new int[] { 0, 0 };

        public Mandelbrot()
        {
            InitializeComponent();



            //TODO -TCP IP    
            TcpConnector.ConnectToTCP("127.0.0.1",2222);
            TcpConnector.ConnectToTCP("127.0.0.1", 2223);

        }
        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            WidthPixel = pictureBox1.Width;
            HeightPixel = pictureBox1.Height;


            XRangeIncremental[0] = 0;
            YRangeIncremental[0] = 0;
            XRangeIncremental[1] = WidthPixel;
            YRangeIncremental[1] = HeightPixel;


            DrawMandelbrot(ZoomIteration, 0);

            ZoomIteration++;
        }
        private void DrawMandelbrot(int i, int scktID)
        {
           string message = CreateMandelbrotRequestObject();
        
           TcpConnector.Send(scktID);
           Bitmap bitmap = TcpConnector.Recieve(scktID);

            bitmap.Save("Saves/save[" +i +"].png",ImageFormat.Png);


            pictureBox1.Image = bitmap;

            
        }
        private void ZoomIncrementaly(int XRange0, int YRange0, int XRange1,  int YRange1 )
        {

            Complex start, end;

            start = ConstantIncremental(0, XRange0, YRange0);
            end = ConstantIncremental(0, XRange1, YRange1);


            center[0] = (start.A + end.A) / 2;
            center[1] = (start.B + end.B) / 2;

            double zoomTemp = WidthPixel / (double) (XRange1 - XRange0);
            zoom *= zoomTemp;

        }
        private string CreateMandelbrotRequestObject()
        {
            MandelbrotJSON mandelbrotJSON = new MandelbrotJSON()
            {
                Center = center,
                Zoom = zoom,
                HeightPixel = HeightPixel,
                WidthPixel = WidthPixel,
                MaxIterations = maxIterations
            };

            return ConverterJSON.CreateJSON(mandelbrotJSON);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
     

            XRange[0] = e.X;
            YRange[0] = e.Y;
        }

        private void ZoomLoop()
        {
            int numberOfZooms = 10;

            int XIncrement0 = (XRange[0] - XRangeIncremental[0]) / numberOfZooms;
            int YIncrement0 = (YRange[0] - YRangeIncremental[0]) / numberOfZooms;

            int XIncrement1 = (XRangeIncremental[1] - XRange[1]) / numberOfZooms;
            int YIncrement1 = (YRangeIncremental[1] - YRange[1]) / numberOfZooms;

            int x0;
            int y0;

            int x1;
            int y1;
      

            Array.Copy(center, centerPrev, center.Length);
            zoomPrev = zoom;
           



            for (int i = 1; i <= numberOfZooms; i++)
            {
                x0 = XRangeIncremental[0] + XIncrement0 * i;
                y0 = YRangeIncremental[0] + YIncrement0 * i;
                x1 = XRangeIncremental[1] - XIncrement1 * i;
                y1 = YRangeIncremental[1] - YIncrement1 * i;


                ZoomIncrementaly(x0, y0, x1, y1);
                AdjustAspectRatioIncremental(ref x0, ref x1, ref y0, ref y1);

                DrawMandelbrot(ZoomIteration,i%(TcpConnector.sockets.Count));
                //DrawMandelbrot(ZoomIteration,0);
               
                
                ZoomIteration++;
               
                if(i<numberOfZooms)
                {
                    Array.Copy(centerPrev, center, center.Length);

                    zoom = zoomPrev;
                }
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {


            XRange[1] = e.X+1;
            YRange[1] = e.Y+1;
      
            FixTables();
            AdjustAspectRatio();


            ZoomLoop();
            

          //  ZoomIn();
          //  DrawMandelbrot();


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
        public Complex ConstantIncremental(double increment, int x, int y)
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
        private void ZoomIn()
        {
           // startingX += XRange[0] / WidthPixel / zoom;
          // startingY -= YRange[0] / HeightPixel/ zoom;

            Complex start, end;

            start = Constant(0, XRange[0], YRange[0]);
            end = Constant(0, XRange[1], YRange[1]);


            center[0] = (start.A+end.A) / 2;
            center[1] = (start.B+ end.B) / 2;

            var zoomTemp = WidthPixel / (XRange[1] - XRange[0]);
            zoom *= zoomTemp;


        }
        
        private void AdjustAspectRatio()
        {
            //TODO - just a click is 0/0?
            var ratio = Math.Abs(XRange[1] - XRange[0]) / Math.Abs(YRange[1] - YRange[0]);
            var sratio = WidthPixel / HeightPixel;
            if (sratio > ratio)
            {
                var xf = sratio / ratio;
                XRange[0] *= xf;
                XRange[1] *= xf;              
                zoom *= xf;
            }
            else
            {
                var yf = ratio / sratio;
                YRange[0] *= yf;
                YRange[1] *= yf;
                zoom *= yf;
            }
            
        }
        private void AdjustAspectRatioIncremental(ref int x0,ref int x1,ref int y0, ref int y1)
        {
            //TODO - just a click is 0/0?
            var ratio = Math.Abs(x1 - x0) / Math.Abs(y1 - y0);
            if(ratio==0)
            {
                ratio = 1;
            }
            var sratio = WidthPixel / HeightPixel;
            if (sratio > ratio)
            {
                var xf = sratio / ratio;
                x0 *= xf;
                x1 *= xf;
                zoom *= xf;
            }
            else
            {
                var yf = ratio / sratio;
                y0 *= yf;
                y1 *= yf;
                zoom *= yf;
            }

        }
        //making first field always smaller
        private void FixTables()
        {
            if (XRange[0] > XRange[1])
            {
                int temp = XRange[1];
                XRange[1] = XRange[0];
                XRange[0] = temp;

            }
            if (YRange[0] > YRange[1])
            {
                int temp = YRange[1];
                YRange[1] = YRange[0];
                YRange[0] = temp;

            }     
            
           int diference = (XRange[1] - XRange[0]) - (YRange[1] - YRange[0]);

            if ((XRange[1]-XRange[0])<(YRange[1]-YRange[0]))
            {
                XRange[1] += Math.Abs(diference);
            }
            else
            {
                YRange[1] += Math.Abs(diference);
            }
          

        }
        public double Diameter()
        {           
            return Math.Min(HeightPixel, WidthPixel);
        }

    }
}
    