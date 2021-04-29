using System;

namespace ClientMandelbrot
{
    class Complex
    {
        public double A; //real
        public double B; //imaginary

        public void Square()
        {
            double tempA = (A * A - B * B);
            B = 2 * A * B;
            A = tempA;
        }

        public double Magnitude()
        {
            return Math.Sqrt((A * A) + (B * B));
        }
        public void Add(Complex c)
        {
            A += c.A;
            B += c.B;
        }
    }
}
