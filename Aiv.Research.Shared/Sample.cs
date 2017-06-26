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

        public Sample(string sName, float[] hValues, float[] hIdeal)
        {
            Name   = sName;
            Ideal  = hIdeal;
            Values = hValues;
        }

        public Sample(int iInputCount, int iIdealCount)
        {
            Ideal  = new float[iInputCount];
            Values = new float[iIdealCount];
        }

        public override string ToString()
        {
            string sRes = Values.Aggregate(string.Empty, (current, t) => current + $", {t}");
            return Ideal.Aggregate(sRes, (current, t) => current + $", {t}");
        }
    }
}
