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

        int quality = 100;
        double zoom = 1;

        double[] XRange = new double[] { 0, 0 };
        double[] YRange = new double[] { 0, 0 };

        double startingX=0;
        double startingY=0;


        public Mandelbrot()
        {
            InitializeComponent();
        }   
        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            DrawMandelbrot();
        }
        private void DrawMandelbrot()
        {

            double xMove = XRange[0];
            double yMove = YRange[0];

            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);










    
            pictureBox1.Image = bm;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            XRange[0] = e.X;
            YRange[0] = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            XRange[1] = e.X;
            YRange[1] = e.Y;
            AdjustAspectRatio();
        }

        
        private void AdjustAspectRatio()
        {
            FixTables();
            //TODO - just a click is 0/0?
            var ratio = Math.Abs(XRange[1] - XRange[0]) / Math.Abs(YRange[1] - YRange[0]);
            var sratio = pictureBox1.Width / pictureBox1.Height;
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
            

            DrawMandelbrot();
        }
        //making first field always smaller
        private void FixTables()
        {
            if (XRange[0] > XRange[1])
            {
                double temp = XRange[1];
                XRange[1] = XRange[0];
                XRange[0] = temp;

            }
            if (YRange[0] > YRange[1])
            {
                double temp = YRange[1];
                YRange[1] = YRange[0];
                YRange[0] = temp;

            }
        }
        private void SetWindow()
        { 
            if(startingX==0)
            {
                startingX = XRange[0]-pictureBox1.Width/2;
            }
            else
            {

            }
            if (startingY == 0)
            {
                startingY = YRange[0] - pictureBox1.Height / 2;
            }
            else
            {

            }


        }

    }
}


/*      for (int x = 0; x < pictureBox1.Width; x++)
          {
              for (int y = 0; y < pictureBox1.Height; y++)
              {
                  double a = (double)(xMove + x - (pictureBox1.Width / 2)) / (double)(pictureBox1.Width/4 ) / zoom;
                  double b = (double)(yMove + y - (pictureBox1.Height / 2)) / (double)(pictureBox1.Height/4) / zoom;

                  Complex c = new Complex { A = a, B = b };
                  Complex z = new Complex { A = 0, B = 0 };

                  int i = 0;
                  do
                  {
                      i++;
                      z.Square();
                      z.Add(c);
                      if (z.Magnitude() > 2.0) break;
                  }
                  while (i < quality);
                  bm.SetPixel(x, y, i < quality ? Color.FromArgb(20, 20, i) : Color.Black);
              }
          }*/