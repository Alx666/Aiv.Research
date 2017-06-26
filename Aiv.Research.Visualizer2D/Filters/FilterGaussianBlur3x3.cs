using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D.Filters
{
    public class FilterGaussianBlur3x3 : Filter
    {
        public FilterGaussianBlur3x3()
        {
            Matrix = new double[3, 3] 
            {
                { 0, 0.2, 0 },
                { 0.2, 0.2, 0.2 },
                { 0, 0.2, 0 }
            };
        }
    }
}
