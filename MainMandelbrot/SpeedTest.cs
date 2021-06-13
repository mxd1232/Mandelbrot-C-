using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot_Whole
{
    public class SpeedTest
    {
        public double FullTime { get; set; }
        public double CommunicationTime{ get; set; }
        public double ComputationTime { get; set; }
        public int ScktID { get; set; }
    }
}
