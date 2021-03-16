using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot_Whole
{
    public partial class Mandelbrot : Form
    {

        public static double[] center = { 0,0 };

        int maxIterations = 1000;
        double zoom = 1;

        int WidthPixel;
        int HeightPixel;
    
        int[] XRange = new int[] { 0, 0 };
        int[] YRange = new int[] { 0, 0 };

        public Mandelbrot()
        {
            InitializeComponent();
        }   
        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            WidthPixel = pictureBox1.Width;
            HeightPixel = pictureBox1.Height;

            DrawMandelbrot();
        }
        private void DrawMandelbrot()
        {          
            Bitmap bm = new Bitmap(WidthPixel, HeightPixel);

            double increment = 4 / zoom / Diameter();


            for (int xPixel= 0; xPixel < WidthPixel; xPixel++ )
            {           

                for (int yPixel = 0; yPixel < HeightPixel; yPixel++)
                {
                    
                    Complex c = constant(increment,xPixel,yPixel);
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

                    bm.SetPixel(xPixel, yPixel, i < maxIterations ? Color.FromArgb(20, 20, i%255) : Color.Black);
                }
            }
            pictureBox1.Image = bm;
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

            DrawMandelbrot();
        }

        public Complex constant(double increment, int x, int y)
        {
            if (increment == 0)
            {
                increment = 4 / zoom / Diameter();
            }


            Complex c = new Complex{
                A= (-increment*WidthPixel/2  +  center[0] + increment*x),
                B= ( increment*HeightPixel/2 +  center[1] - increment*y) 
            };
            return c;
        }
        private void ZoomIn()
        {
           // startingX += XRange[0] / WidthPixel / zoom;
          // startingY -= YRange[0] / HeightPixel/ zoom;

            Complex start, end;

            start = constant(0, XRange[0], YRange[0]);
            end = constant(0, XRange[1], YRange[1]);


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
    