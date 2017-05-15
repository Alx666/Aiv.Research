using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encog;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;

namespace Aiv.Research.Visualizer2D
{
    public partial class Main : Form
    {
        private IDrawer m_hDrawer;

        private PenDrawer   m_hPenDrawer;
        private NullDrawer  m_hNullDrawer;


        private BasicNetwork m_hNetwork;
        private FormNNDrawer m_hNeuralDisplay;

        

        public Main()
        {
            InitializeComponent();

            m_hPenDrawer = new PenDrawer(Color.Green, 1f, m_hPanel);
            m_hNullDrawer = new NullDrawer();
            m_hDrawer = m_hNullDrawer;

            m_hNetwork = new BasicNetwork();
            m_hNetwork.AddLayer(new BasicLayer(3));
            m_hNetwork.AddLayer(new BasicLayer(5));
            m_hNetwork.AddLayer(new BasicLayer(2));
            m_hNetwork.AddLayer(new BasicLayer(1));



            m_hNeuralDisplay = new FormNNDrawer(m_hNetwork);

            m_hNeuralDisplay.Show();
        }

        #region Panel Event Handlers

        private void OnPanelMouseEnter(object sender, EventArgs e) => m_hDrawer = m_hPenDrawer;
        private void OnPanelMouseLeave(object sender, EventArgs e)
        {
            m_hDrawer.End();
            m_hDrawer = m_hNullDrawer;
        }

        private void OnPanelMouseDown(object sender, MouseEventArgs e)
        {
            m_hDrawer.Begin(e.X, e.Y);
        }

        private void OnPanelMouseMove(object sender, MouseEventArgs e)
        {
            m_hDrawer.Update(e.X, e.Y);
        }

        private void OnPanelMouseUp(object sender, MouseEventArgs e)
        {
            m_hDrawer.End();
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.Control)
            {                
                using (Bitmap hBmp = m_hDrawer.Clear())
                {
                    using (Bitmap hDownscaled = hBmp.ResizeImage(320, 240))
                    {
                        string sFilename = $"Sample{m_hSamples.Items.Count}.bmp";
                        hDownscaled.Save(sFilename, ImageFormat.Bmp);

                        m_hSamples.Items.Add(new Sample(sFilename));
                    }
                }
            }
        }

        #endregion


    }
}
