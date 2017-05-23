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
            m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 2));
            m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 6));
            m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 1));
            m_hNetwork.Structure.FinalizeStructure();
            m_hNetwork.Reset();

            double[][] XorInput = new double[4][];
            XorInput[0] = new double[2] { 0.0, 0.0 };
            XorInput[1] = new double[2] { 1.0, 0.0 };
            XorInput[2] = new double[2] { 0.0, 1.0 };
            XorInput[3] = new double[2] { 1.0, 1.0 };

            double[][] XorIdeal = new double[4][];
            XorIdeal[0] = new double[1] { 0.0 };
            XorIdeal[1] = new double[1] { 1.0 };
            XorIdeal[2] = new double[1] { 1.0 };
            XorIdeal[3] = new double[1] { 0.0 };


            INeuralDataSet hTrainingSet = new BasicNeuralDataSet(XorInput, XorIdeal);


            ITrain hTraining = new ResilientPropagation(m_hNetwork, hTrainingSet);

            hTraining.Iteration(5000);

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
