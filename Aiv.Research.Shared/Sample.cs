using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Shared
{
    [Serializable]
    public class Sample
    {
        public string Name { get; set; }
        public double[] Values { get; set; }
        public double[] Ideal { get; set; }

        public Sample()
        {

        }

        public Sample(string sName, double[] hValues, double[] hIdeal)
        {
            Name = sName;
            Ideal = hIdeal;
            Values = hValues;
        }

        public override string ToString() => Name;
    }
}
