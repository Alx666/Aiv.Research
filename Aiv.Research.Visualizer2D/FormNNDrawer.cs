using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aiv.Research.Visualizer2D
{
    public partial class FormNNDrawer : Form
    {
        private Pen      m_hPen;

        private const int NeuronGfxSize = 160;
        private const int VertPadding   = 30;
        private const int HorizPadding  = 30;

        private List<NeuronGfx> m_hNeurons;

        public FormNNDrawer(BasicNetwork hNetwork)
        {
            InitializeComponent();

            m_hPen = new Pen(Color.Green, 3);            
            
            ILayer hLongest = (from hL in hNetwork.Structure.Layers.ToList() orderby hL.NeuronCount descending select hL).First();

            Size vPanelSize             = new Size();
            vPanelSize.Width            = (hLongest.NeuronCount * (NeuronGfxSize * 2)) + HorizPadding * 2;
            vPanelSize.Height           = (hNetwork.Structure.Layers.Count * NeuronGfxSize) + VertPadding * (hNetwork.Structure.Layers.Count + 1);
            m_hPanel.Paint             += OnPanelPaint;
            m_hPanel.Size               = vPanelSize;
            m_hPanel.Parent.Width       = m_hPanel.Width + 40;
            m_hPanel.Parent.Height      = m_hPanel.Height + 60;

            m_hNeurons = new List<NeuronGfx>();
            

            for (int i = 0; i < hNetwork.Structure.Layers.Count; i++)
            {
                ILayer hCurrentLayer = hNetwork.Structure.Layers[i];

                int iPosY = VertPadding + (NeuronGfxSize * i + i * VertPadding ) ;

                for (int k = 0; k < hCurrentLayer.NeuronCount; k++)
                {
                    int iPosX = HorizPadding + (NeuronGfxSize * k + k * HorizPadding);

                    m_hNeurons.Add(new NeuronGfx(new Point(iPosX, iPosY), new Size(NeuronGfxSize, NeuronGfxSize)));
                }
            }

            hNetwork.Structure.FinalizeStructure();
            hNetwork.Reset();
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            m_hNeurons.ForEach(x => x.Draw(m_hPen, e.Graphics));
        }

        class NeuronGfx
        {
            private Rectangle m_vPosition;

            public NeuronGfx(Point vFrom, Size vSize)
            {
                m_vPosition = new Rectangle(vFrom, vSize);
            }

            public void Draw(Pen hPen, Graphics hGfx)
            {
                hGfx.DrawEllipse(hPen, m_vPosition);
            }
        }
    }
}
