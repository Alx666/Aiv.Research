using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
{
    class Sample
    {
        public int Number { get; set; }

        public Sample()
        {

        }

        public override string ToString() => $"Sample: {Number}";
    }
}
