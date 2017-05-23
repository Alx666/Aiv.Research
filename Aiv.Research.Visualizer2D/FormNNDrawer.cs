using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aiv.Research.Visualizer2D
{
    public partial class FormNNDrawer : Form
    {        
        private const int NeuronGfxSize = 60;

        private NeuronGfx[][]   m_hNeurons;
        private BasicNetwork    m_hNetwork;

        public FormNNDrawer(BasicNetwork hNetwork)
        {
            InitializeComponent();

            m_hNetwork = hNetwork;


            Size vPanelSize             = new Size();
            vPanelSize.Width            = 800;//(hLongest.NeuronCount * (NeuronGfxSize * 2)) + HorizPadding * 2;
            vPanelSize.Height           = 600;// (hNetwork.Structure.Layers.Count * NeuronGfxSize) + VertPadding * (hNetwork.Structure.Layers.Count + 1);
            m_hPanel.Paint             += OnPanelPaint;
            m_hPanel.Size               = vPanelSize;
            m_hPanel.Parent.Width       = m_hPanel.Width + 40;
            m_hPanel.Parent.Height      = m_hPanel.Height + 60;
            
            m_hNeurons                  = new NeuronGfx[hNetwork.LayerCount][];

            int iColumnDivision = vPanelSize.Height / hNetwork.LayerCount;
            int iVertSpacing    = iColumnDivision / 2;

            for (int i = 0; i < hNetwork.LayerCount; i++)
            {                
                int iPosY           = (iVertSpacing + iColumnDivision * i) - NeuronGfxSize / 2;

                int iRowDivision    = vPanelSize.Width  / hNetwork.GetLayerNeuronCount(i);
                int iHorizSpacing   = iRowDivision      / 2;

                m_hNeurons[i] = new NeuronGfx[hNetwork.GetLayerNeuronCount(i)];


                for (int k = 0; k < hNetwork.GetLayerNeuronCount(i); k++)
                {
                    int iPosX = (iHorizSpacing + iRowDivision * k) - NeuronGfxSize / 2;

                    m_hNeurons[i][k] = new NeuronGfx(new Point(iPosX, iPosY), new Size(NeuronGfxSize, NeuronGfxSize), i);
                }
            }


            for (int i = 0; i < m_hNeurons.Length - 1; i++)
            {
                for (int k = 0; k < m_hNeurons[i].Length; k++)
                {
                    NeuronGfx hCurrent = m_hNeurons[i][k];

                    for (int j = 0; j < m_hNeurons[i + 1].Length; j++)
                    {
                        hCurrent.Add(m_hNeurons[i + 1][j], (float)m_hNetwork.GetWeight(i, k, j));
                    }
                }
            }


            StringBuilder hSb = new StringBuilder();
            double[] hInput0 = new double[] { 0.0, 0.0 };
            double[] hInput1 = new double[] { 1.0, 0.0 };
            double[] hInput2 = new double[] { 0.0, 1.0 };
            double[] hInput3 = new double[] { 1.0, 1.0 };
            double[] hOutput0 = new double[1];
            double[] hOutput1 = new double[1];
            double[] hOutput2 = new double[1];
            double[] hOutput3 = new double[1];

            m_hNetwork.Compute(hInput0, hOutput0);
            m_hNetwork.Compute(hInput1, hOutput1);
            m_hNetwork.Compute(hInput2, hOutput2);
            m_hNetwork.Compute(hInput3, hOutput3);

            hSb.AppendLine($"{hInput0[0]} xor {hInput0[1]} = {hOutput0[0]}");
            hSb.AppendLine($"{hInput1[0]} xor {hInput1[1]} = {hOutput1[0]}");
            hSb.AppendLine($"{hInput2[0]} xor {hInput2[1]} = {hOutput2[0]}");
            hSb.AppendLine($"{hInput3[0]} xor {hInput3[1]} = {hOutput3[0]}");

            File.WriteAllText("output.txt", hSb.ToString());
            


        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < m_hNeurons.Length; i++)
            {
                for (int k = 0; k < m_hNeurons[i].Length; k++)
                {
                    m_hNeurons[i][k].Draw(e.Graphics);
                }
            }
        }

        class NeuronGfx
        {
            private static Pen  m_hPenCircle;
            private static Pen  m_hPenRect;
            private static Pen  m_hPenCenter;
            private static Font m_hFont;

            private Rectangle   m_vPosition;
            private Point       m_vCenter;
            private List<NeuronConnection> m_hNeightbours;
            public int Layer { get; private set; }

            static NeuronGfx()
            {
                m_hPenCircle    = new Pen(Color.Green, 2);
                m_hPenRect      = new Pen(Color.Red, 1);
                m_hPenCenter    = new Pen(Color.Yellow, 1);
                m_hFont         = new Font("Arial", 8);
            }

            public NeuronGfx(Point vFrom, Size vSize, int iLayer)
            {
                m_vPosition     = new Rectangle(vFrom, vSize);
                m_vCenter       = new Point(m_vPosition.X + m_vPosition.Width / 2, m_vPosition.Y + m_vPosition.Height / 2);
                Layer           = iLayer;
                m_hNeightbours  = new List<NeuronConnection>();
            }

            public void Draw(Graphics hGfx)
            {
                //First draw lines
                for (int i = 0; i < m_hNeightbours.Count; i++)
                {
                    //Draw Synapse
                    hGfx.DrawLine(m_hPenRect, m_vCenter, m_hNeightbours[i].Next.m_vCenter);

                    //Draw Synapse Weight
                    Vector2 vPos = (Vector2)m_vCenter + (Vector2)m_hNeightbours[i].Next.m_vCenter;
                    vPos /= 2;
                    
                    SizeF vSize = hGfx.MeasureString(m_hNeightbours[i].Weight.ToString(), m_hFont);
                    Rectangle vDeleteArea = new Rectangle((int)vPos.X, (int)vPos.Y, (int)vSize.Width, (int)vSize.Height);
                    hGfx.FillRectangle(Brushes.Black, vDeleteArea);

                    hGfx.DrawString(m_hNeightbours[i].Weight.ToString(), m_hFont, Brushes.Red, (PointF)vPos);
                    hGfx.DrawRectangle(m_hPenCenter, vDeleteArea);

                    //Draw Bias

                }


                hGfx.FillEllipse(Brushes.Black, m_vPosition);
                hGfx.DrawEllipse(m_hPenCircle, m_vPosition);


                //hGfx.DrawRectangle(m_hPenRect, m_vPosition);
                //hGfx.DrawRectangle(m_hPenCenter, m_vCenter.X - 1, m_vCenter.Y - 1, 2, 2);

            }

            public void Add(NeuronGfx hNext, float fWeight)
            {
                NeuronConnection hConn = new NeuronConnection();
                hConn.Next = hNext;
                hConn.Weight = fWeight;
                m_hNeightbours.Add(hConn);
            }


            internal struct NeuronConnection
            {
                public NeuronGfx Next;
                public float Weight;
            }
        }
    }
}
