using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot_Whole
{
    public partial class Mandelbrot : Form
    {

        public static double[] center = { 0,0 };

        int maxIterations = 200;
        double zoom = 1;

        int WidthPixel;
        int HeightPixel;
    
        int[] XRange = new int[] { 0, 0 };
        int[] YRange = new int[] { 0, 0 };

        public Mandelbrot()
        {
            InitializeComponent();
        
            
            //TODO -TCP IP    TcpConnector.ConnectToTCP();
            
        }   
        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            WidthPixel = pictureBox1.Width;
            HeightPixel = pictureBox1.Height;

            ComputeMandelbrot();
        }
        private void ComputeMandelbrot()
        {
           string message = CreateMandelbrotRequestObject();





            /* 
             *  TcpConnector.Send(message);
             * MandelbrotJSON mandelbrotJSON = ConverterJSON.ReadFromString(TcpConnector.Recieve());
            */
            // TODO - tcp/ip connection

            MandelbrotJSON mandelbrotJSON = ConverterJSON.ReadJSON("../../json/recieved.json");

            Bitmap bm = new Bitmap(WidthPixel, HeightPixel);


            for (int xPixel = 0; xPixel < WidthPixel; xPixel++)
            {

                for (int yPixel = 0; yPixel < HeightPixel; yPixel++)
                {
                    int i = mandelbrotJSON.MandelbrotMatrix[xPixel][yPixel];

                    bm.SetPixel(xPixel, yPixel, i < maxIterations ? Color.FromArgb(20, 20, i % 255) : Color.Black);
                }
            }

            pictureBox1.Image = bm;
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

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            XRange[1] = e.X+1;
            YRange[1] = e.Y+1;
      
            FixTables();
            AdjustAspectRatio();
            ZoomIn();

            ComputeMandelbrot();
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
    