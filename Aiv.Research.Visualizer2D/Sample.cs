using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
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

        public Sample(string sName, double[,] hValues, double[] hIdeal)
        {
            Name = sName;
            List<double> hTmp = new List<double>();

            for (int i = 0; i < hValues.GetLength(0); i++)
            {
                for (int k = 0; k < hValues.GetLength(1); k++)
                {
                    hTmp.Add(hValues[i, k]);
                }
            }

            Ideal = hIdeal;
            Values = hTmp.ToArray();
        }

        public override string ToString() => Name;
    }
}
