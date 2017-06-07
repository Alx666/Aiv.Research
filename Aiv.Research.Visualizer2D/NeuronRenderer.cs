using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Aiv.Research.Visualizer2D
{
    public class NeuronRenderer : Mesh
    {
        public NeuronRenderer() : base(null, 3)
        {
           
        }


        protected override void ApplyMatrix()
        {
            base.ApplyMatrix();
        }
        public void Draw()
        {
            this.Draw((m) =>
           {

           });
        }
    }
}
