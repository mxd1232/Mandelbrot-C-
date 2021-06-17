using AnimatedGif;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot_Whole
{
    public partial class Mandelbrot : Form
    {
        static HashSet<int> TakenSocketIds = new HashSet<int>();


        public int FramesPerZoom = 10;
        int ZoomIteration = 0;

        public static double[] centerPrev  = { 0,0 };
        public static double[] center = { 0,0 };

        int maxIterations = 200;
        double zoom = 1;
        double zoomPrev;

        public int WidthPixel;
        public int HeightPixel;

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
            
          //  TcpConnector.ConnectToTCP("127.0.0.1",2222);

        }
        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            WidthPixel = pictureBox1.Width;
            HeightPixel = pictureBox1.Height;


            XRangeIncremental[0] = 0;
            YRangeIncremental[0] = 0;
            XRangeIncremental[1] = WidthPixel;
            YRangeIncremental[1] = HeightPixel;


     
        }
        private void DrawMandelbrot(int i, int scktID)
        {
            if (IsRegularComputation(i))
            {
                ComputeRegular(i, scktID);
            }
            else
            {
                
                ComputeParalel(i, scktID);
            
            }     
        }
        private bool IsRegularComputation(int i)
        {
            return (i % (FramesPerZoom - 1) == 0 || TcpConnector.Sockets.Count == 1);
        }

        private void ComputeRegular(int i, int scktID)
        {
                string createdFilePath = "../../json/created[" + i + "].json";
                string message = CreateMandelbrotRequestObject(createdFilePath);
                TcpConnector.Send(scktID, createdFilePath);

                Bitmap bitmap = TcpConnector.Recieve(scktID);
                bitmap.Save("saves/save[" + i + "].png", ImageFormat.Png);
                pictureBox1.Image = bitmap;       
        }
        private void ComputeParalel(int i,int scktID)
        {
            while (TakenSocketIds.Contains(scktID))
            {

                scktID++;
                if (scktID == TcpConnector.Sockets.Count())
                {
                    scktID = 0;
                }
                Thread.Sleep(10);
            }

            Thread communicationThread = new Thread(delegate ()
            {
                RequestParalel(scktID, i);
            });
            communicationThread.Start();
        }
        private static void RequestParalel(int scktID,int i)
        {


            TakenSocketIds.Add(scktID);

            string createdFilePath = "../../json/created[" + i + "].json";
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
            

            int XIncrement0 = (XRange[0] - XRangeIncremental[0]) / FramesPerZoom;
            int YIncrement0 = (YRange[0] - YRangeIncremental[0]) / FramesPerZoom;

            int XIncrement1 = (XRangeIncremental[1] - XRange[1]) / FramesPerZoom;
            int YIncrement1 = (YRangeIncremental[1] - YRange[1]) / FramesPerZoom;

            int x0;
            int y0;

            int x1;
            int y1;
      

            Array.Copy(center, centerPrev, center.Length);
            zoomPrev = zoom;


            DateTime fullTime1 = DateTime.Now;

            for (int i = 1; i <= FramesPerZoom; i++)
            {
                x0 = XRangeIncremental[0] + XIncrement0 * i;
                y0 = YRangeIncremental[0] + YIncrement0 * i;
                x1 = XRangeIncremental[1] - XIncrement1 * i;
                y1 = YRangeIncremental[1] - YIncrement1 * i;


                ZoomIncrementaly(x0, y0, x1, y1);
                AdjustAspectRatioIncremental(ref x0, ref x1, ref y0, ref y1);

                DrawMandelbrot(ZoomIteration,i%(TcpConnector.Sockets.Count));
                             
                
                //DrawMandelbrot(ZoomIteration,0);
                // Refresh(); z tą funkcja wyswietlane sa kolejne klatki
                
                ZoomIteration++;
               
                if(i<FramesPerZoom)
                {
                    Array.Copy(centerPrev, center, center.Length);

                    zoom = zoomPrev;
                }
                
            }
            rectanglePoint = new Point(0,0);
            rectangleEndpoint = new Point(0, 0);


            DateTime fullTime2 = DateTime.Now;
            TimeSpan timeSpan = fullTime2.Subtract(fullTime1);

            string path = @"saves/fullSpeedOfZoom.txt";
            string zoomInfo = "Frames per zoom: " + FramesPerZoom +
                " max iterations: " +maxIterations+
                " height: " + HeightPixel +
                " width: " + WidthPixel +
                " time in seconds: " + timeSpan.TotalSeconds.ToString() + "\n";

            File.AppendAllText(path,zoomInfo);
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

                    if(TcpConnector.Sockets.Count==1)
                    {
                           DrawMandelbrot(ZoomIteration, 0);
                           ZoomIteration++;
                    }
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

        private void animationButton_Click(object sender, EventArgs e)
        {
            using (var gif = AnimatedGif.AnimatedGif.Create("saves/output.gif", 1, 20))
            {
                for (var i = 0; i < this.ZoomIteration; i++)
                {
                    var img = Image.FromFile("saves/save[" + i + "].png");
                    gif.AddFrame(img, delay: -1, quality: GifQuality.Bit8);
                }
            }
        }

        private void speedTestButton_Click(object sender, EventArgs e)
        {

            if(TcpConnector.SpeedTests!=null)
            {
                WriteCSV<SpeedTest>(TcpConnector.SpeedTests, "saves/speedTest.csv");
            }
        }
        public void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (framesText.Text != null && framesText.Text != "" && Convert.ToInt32(framesText.Text) >1)
                {
                    FramesPerZoom = Convert.ToInt32(framesText.Text);
                }
                if (iterationsText.Text != null && iterationsText.Text != "")
                {
                    maxIterations = Convert.ToInt32(iterationsText.Text);
                }
                if (heightText.Text != null && heightText.Text != "")
                {
                    int height = Convert.ToInt32(heightText.Text);

                    pictureBox1.Height = height;
                    HeightPixel = pictureBox1.Height;
                }
                if (widthText.Text != null && widthText.Text != "")
                {
                    pictureBox1.Width = Convert.ToInt32(widthText.Text);
                    WidthPixel = pictureBox1.Width;
                }
            }
            catch(Exception exe)
            {
                Debug.WriteLine(exe);
            }

        }

    }
}
    