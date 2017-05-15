using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
{
    class Sample
    {
        public string Name { get; private set; }
        
        public Sample(string sName)
        {
            Name = sName;
        }

        public override string ToString() => Name;
    }
}
