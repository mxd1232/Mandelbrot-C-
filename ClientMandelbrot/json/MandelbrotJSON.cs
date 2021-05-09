namespace ClientMandelbrot
{
    class MandelbrotJSON
    {
        public double[] Center { get; set; }

        public int MaxIterations { get; set; }
        public double Zoom { get; set; }

        public int WidthPixel { get; set; }
        public int HeightPixel { get; set; }

    }
}
