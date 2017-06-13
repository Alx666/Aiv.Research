using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D.Filters
{
    public class FilterGaussianBlur5x5 : Filter
    {
        public FilterGaussianBlur5x5()
        {
            Matrix = new double[5, 5]
            {
                {0.003, 0.013, 0.022, 0.013, 0.003 },
                {0.013, 0.059, 0.097, 0.059, 0.013 },
                {0.022, 0.097, 0.159, 0.097, 0.022 },
                {0.013, 0.059, 0.097, 0.059, 0.013 },
                {0.003, 0.013, 0.022, 0.013, 0.003 }
            };
        }
    }
}
