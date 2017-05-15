using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
{
    interface IDrawer
    {
        void Begin(int iX, int iY);

        void Update(int iX, int iY);

        void End();

        Bitmap Clear();
    }
}
