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
        public float[] Values { get; set; }
        public float[] Ideal { get; set; }

        public Sample()
        {

        }

        public Sample(float[] hValues, float[] hIdeal)
        {
            Ideal  = hIdeal;
            Values = hValues;
            Name = Values.Select(x => x.ToString()).Aggregate((x, y) => x + " " + y);
            Name += " => " + Ideal.Select(x => x.ToString()).Aggregate((x, y) => x + " " + y);
        }

        public Sample(int iInputCount, int iIdealCount)
        {
            Ideal  = new float[iInputCount];
            Values = new float[iIdealCount];
        }

        public override string ToString() => Name;
    }
}
