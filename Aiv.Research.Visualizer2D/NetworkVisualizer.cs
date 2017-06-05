using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using Aiv.Fast3D;
using OpenTK;
using Encog.Neural.Networks;

namespace Aiv.Research.Visualizer2D
{
    class NetworkVisualizer : Window
    {
        private BasicNetwork m_hBasicNetwork;
        private PerspectiveCamera m_hCamera;
        private Mesh3 m_hDebugMesh;
        private float threshold;

        public NetworkVisualizer(int width, int height, string title, BasicNetwork basicNetwork, float threshold) : base(width, height, title)
        {
            m_hBasicNetwork = basicNetwork;
            this.threshold = threshold;
            m_hCamera = new PerspectiveCamera(new Vector3(), new Vector3(), 60f, 0.1f, 1000f);
            m_hDebugMesh = new Cube();
            m_hDebugMesh.Position3 += Vector3.UnitZ * 2f;
        }

        public void Draw()
        {
            m_hDebugMesh.DrawColor(new Vector4(0f, 0f, 1f, 1f));
        }
    }
}
