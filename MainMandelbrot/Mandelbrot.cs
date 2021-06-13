﻿using System;
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
        static HashSet<int> TakenSocketIds = new HashSet<int>();


        int NumberOfZooms = 10;
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

        Point rectanglePoint;
        Point rectangleEndpoint;

        bool isMouseDown = false;
        Rectangle rectangle;

        public Mandelbrot()
        {
            InitializeComponent();
            
            TcpConnector.ConnectToTCP("127.0.0.1",2222);

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
            string createdFilePath = "../../json/created.json[" + i + "]";


           string message = CreateMandelbrotRequestObject(createdFilePath);

            if(i%(NumberOfZooms-1)==0 || TcpConnector.sockets.Count==1)
            {
                TcpConnector.Send(scktID, createdFilePath);
                Bitmap bitmap = TcpConnector.Recieve(scktID);

                bitmap.Save("saves/save[" + i + "].png", ImageFormat.Png);


                pictureBox1.Image = bitmap;
            }

            else
            {
                while(TakenSocketIds.Contains(scktID))
                {
                    
                    scktID++;
                    if(scktID==TcpConnector.sockets.Count())
                    {
                        scktID = 0;
                    }
                    Thread.Sleep(10);
                }

                Thread communicationThread = new Thread(delegate ()
                {
                    RequestAsync(scktID, i);
                });
                communicationThread.Start();
            }     

        }
        private static void RequestAsync(int scktID,int i)
        {


            TakenSocketIds.Add(scktID);

            string createdFilePath = "../../json/created.json[" + i + "]";

            TcpConnector.Send(scktID, createdFilePath);
            Bitmap bitmap = TcpConnector.Recieve(scktID);
            bitmap.Save("saves/save[" + i + "].png", ImageFormat.Png);

            TakenSocketIds.Remove(scktID);
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
        private string CreateMandelbrotRequestObject(string createdFilePath)
        {
            MandelbrotJSON mandelbrotJSON = new MandelbrotJSON()
            {
                Center = center,
                Zoom = zoom,
                HeightPixel = HeightPixel,
                WidthPixel = WidthPixel,
                MaxIterations = maxIterations
            };

            return ConverterJSON.CreateJSON(mandelbrotJSON,createdFilePath);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            XRange[0] = e.X;
            YRange[0] = e.Y;
            rectanglePoint = new Point(e.X, e.Y);
            isMouseDown = true;
        }

        private void ZoomLoop()
        {
            

            int XIncrement0 = (XRange[0] - XRangeIncremental[0]) / NumberOfZooms;
            int YIncrement0 = (YRange[0] - YRangeIncremental[0]) / NumberOfZooms;

            int XIncrement1 = (XRangeIncremental[1] - XRange[1]) / NumberOfZooms;
            int YIncrement1 = (YRangeIncremental[1] - YRange[1]) / NumberOfZooms;

            int x0;
            int y0;

            int x1;
            int y1;
      

            Array.Copy(center, centerPrev, center.Length);
            zoomPrev = zoom;
           



            for (int i = 1; i <= NumberOfZooms; i++)
            {
                x0 = XRangeIncremental[0] + XIncrement0 * i;
                y0 = YRangeIncremental[0] + YIncrement0 * i;
                x1 = XRangeIncremental[1] - XIncrement1 * i;
                y1 = YRangeIncremental[1] - YIncrement1 * i;


                ZoomIncrementaly(x0, y0, x1, y1);
                AdjustAspectRatioIncremental(ref x0, ref x1, ref y0, ref y1);

                DrawMandelbrot(ZoomIteration,i%(TcpConnector.sockets.Count));
                
                
                
                //DrawMandelbrot(ZoomIteration,0);


                // Refresh(); z tą funkcja wyswietlane sa kolejne klatki
                
                ZoomIteration++;
               
                if(i<NumberOfZooms)
                {
                    Array.Copy(centerPrev, center, center.Length);

                    zoom = zoomPrev;
                }
                
            }
            rectanglePoint = new Point(0,0);
            rectangleEndpoint = new Point(0, 0);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {

                XRange[1] = e.X + 1;
                YRange[1] = e.Y + 1;
                rectangleEndpoint = new Point(e.X + 1, e.Y + 1);

                isMouseDown = false;

            }
            

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

        private void Mandelbrot_Load(object sender, EventArgs e)
        {

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            //127.0.0.1
            Int32 serverPort = Convert.ToInt32(serverPortText.Text);
            String serverIP = serverIPText.Text;

            if((serverPort != 0) && (serverIP.Length >= 9))
            {
                if(TcpConnector.ConnectToTCP(serverIP, serverPort))
                {
                    statusLabel.Text = "Connected";
                }
                else
                {
                    statusLabel.Text = "Error";
                }
                
            }
            else
            {
                statusLabel.Text = "Wrong data";
            }

        }

        private void sendingButton_Click(object sender, EventArgs e)
        {
            FixTables();
            AdjustAspectRatio();
            ZoomLoop();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                rectangleEndpoint = new Point(e.X, e.Y);

                Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(rectangle != null)
            {
                e.Graphics.DrawRectangle(Pens.White, GetRect());
            }
        }

        private Rectangle GetRect()
        {
            rectangle = new Rectangle();

            rectangle.X = Math.Min(rectanglePoint.X, rectangleEndpoint.X);
            rectangle.Y = Math.Min(rectanglePoint.Y, rectangleEndpoint.Y);

            rectangle.Width = Math.Abs(rectanglePoint.X - rectangleEndpoint.X);
            rectangle.Height = Math.Abs(rectanglePoint.Y - rectangleEndpoint.Y);

            return rectangle;
        }
    }
}
    