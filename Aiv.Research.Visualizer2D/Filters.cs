using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D.Filters
{
    static class Filters
    {
        static Filters()
        {
            GaussianBlur3x3 = new FilterGaussianBlur3x3();
            GaussianBlur5x5 = new FilterGaussianBlur5x5();
            GaussianBlur7x7 = new FilterGaussianBlur7x7();
            CenterOfMass    = new FilterCenter();
        }

        public static FilterGaussianBlur3x3 GaussianBlur3x3 { get; private set; }
        public static FilterGaussianBlur5x5 GaussianBlur5x5 { get; private set; }
        public static FilterGaussianBlur7x7 GaussianBlur7x7 { get; private set; }
        public static FilterCenter          CenterOfMass    { get; private set; }

    }
}
