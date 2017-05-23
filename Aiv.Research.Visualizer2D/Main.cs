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
using Encog.Neural.NeuralData;
using Encog.Neural.Data.Basic;
using Encog.Engine.Network.Activation;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation.Resilient;

namespace Aiv.Research.Visualizer2D
{
    public partial class Main : Form
    {
        private IDrawer m_hDrawer;

        private PenDrawer   m_hPenDrawer;
        private Pen         m_hPenGrid;

        private NullDrawer  m_hNullDrawer;


        private BasicNetwork m_hNetwork;
        private FormNNDrawer m_hNeuralDisplay;
        private Rectangle[,] m_hGrid;
        private int m_iGrid;
        

        public Main()
        {
            InitializeComponent();            

            m_iGrid = 10;
            m_hGrid = this.BuildGrid(m_iGrid);

            m_hPenDrawer = new PenDrawer(Color.Green, 1f, m_hPanel);
            m_hPenDrawer.QuantizedSpace = m_hGrid;
            m_hPenGrid = new Pen(Color.DarkRed, 1f);            
            m_hDrawer = m_hPenDrawer;

            m_hNetwork = new BasicNetwork();

            #region XOR Network

            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 2));
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 2));
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 1));
            //m_hNetwork.Structure.FinalizeStructure();
            //m_hNetwork.Reset();

            //double[][] XorInput = new double[4][];
            //XorInput[0] = new double[2] { 0.0, 0.0 };
            //XorInput[1] = new double[2] { 1.0, 0.0 };
            //XorInput[2] = new double[2] { 0.0, 1.0 };
            //XorInput[3] = new double[2] { 1.0, 1.0 };

            //double[][] XorIdeal = new double[4][];
            //XorIdeal[0] = new double[1] { 0.0 };
            //XorIdeal[1] = new double[1] { 1.0 };
            //XorIdeal[2] = new double[1] { 1.0 };
            //XorIdeal[3] = new double[1] { 0.0 };
            //INeuralDataSet hTrainingSet = new BasicNeuralDataSet(XorInput, XorIdeal);
            //ITrain hTraining = new ResilientPropagation(m_hNetwork, hTrainingSet);

            //hTraining.Iteration(5000);

            #endregion

            //m_hNeuralDisplay = new FormNNDrawer(m_hNetwork);


            //m_hNeuralDisplay.Show();
        }

        #region Panel Event Handlers

        private void OnPanelMouseEnter(object sender, EventArgs e) => m_hDrawer = m_hPenDrawer;
        private void OnPanelMouseLeave(object sender, EventArgs e)
        {
            m_hDrawer.End();
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
                double[,] hSamples;
                using (Bitmap hBmp = m_hDrawer.Clear(out hSamples))
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


        private Rectangle[,] BuildGrid(int iSize)
        {
            Rectangle[,] hGrid = new Rectangle[iSize, iSize];

            int iXSize = m_hPanel.Width / m_iGrid;
            int iYSize = m_hPanel.Height / m_iGrid;

            for (int i = 0; i < iSize; i++)
            {
                for (int k = 0; k < iSize; k++)
                {
                    hGrid[i,k] = new Rectangle(i * iXSize, k * iYSize, iXSize, iYSize);
                }
            }

            return hGrid;
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < m_hGrid.GetLength(0); i++)
            {
                for (int k = 0; k < m_hGrid.GetLength(1); k++)
                {
                    e.Graphics.DrawRectangle(m_hPenGrid, m_hGrid[i, k]);
                }
            }
        }

        private void OnQuantizeTextChanged(object sender, EventArgs e)
        {
            try
            {
                m_iGrid = int.Parse(m_hToolStripTextQuantize.Text);
                m_hGrid = this.BuildGrid(m_iGrid);
                m_hPenDrawer.QuantizedSpace = m_hGrid;

                m_hPanel.Invalidate();
            }
            catch (Exception)
            {
                //Do nothing
            }
        }
    }
}
