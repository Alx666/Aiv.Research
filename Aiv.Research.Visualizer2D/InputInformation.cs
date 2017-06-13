using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
{
    public class InputInformation
    {
        public InputInformation()
        {

        }

        public InputInformation(RectangleF area)
        {
            Area = area;
            InUse = true;
        }

        public InputInformation(RectangleF area, double dValue)
        {
            Area = area;
            Value = dValue;
            InUse = true;
        }

        public InputInformation(RectangleF area, bool bInUse)
        {
            Area = area;
            InUse = bInUse;
        }

        public double Value { get; set; }
        public RectangleF Area { get; set; }
        public bool InUse { get; set; }

        public Color Color
        {
            get
            {
                return Color.FromArgb(0, (int)(255 * Value), 0);
            }
        }

        public override string ToString()
        {
            return $"{Area} {Value} {InUse}";
        }
    }
}
