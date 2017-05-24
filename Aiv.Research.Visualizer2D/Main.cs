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
using System.Xml.Serialization;
using System.IO;

namespace Aiv.Research.Visualizer2D
{
    public partial class Main : Form
    {
        private IDrawer m_hDrawer;

        private PenDrawer   m_hPenDrawer;
        private Pen         m_hPenGrid;
        
        private BasicNetwork m_hNetwork;
        private FormNNDrawer m_hNeuralDisplay;
        private Rectangle[,] m_hGrid;
        private NetworkCreationConfig m_hConfig;



        public Main()
        {
            InitializeComponent();

            m_hPanel.Visible = false;

            m_hPenDrawer    = new PenDrawer(Color.Green, 1f, m_hPanel);            
            m_hPenGrid      = new Pen(Color.DarkRed, 1f);            
            m_hDrawer       = m_hPenDrawer;

            

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
            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.Control && m_hPanel.Visible)
            {
                double[,] hSamples;
                using (Bitmap hBmp = m_hDrawer.Clear(out hSamples))
                {
                    using (Bitmap hDownscaled = hBmp.ResizeImage(320, 240))
                    {
                        string sFilename = $"Sample{m_hSamples.Items.Count}.bmp";

                        IdealInputForm hIdealInput = new IdealInputForm(m_hNetwork.GetLayerNeuronCount(m_hNetwork.LayerCount - 1));
                        hIdealInput.ShowDialog();
                        double[] hIdeal = hIdealInput.Ideal;

                        hDownscaled.Save(sFilename, ImageFormat.Bmp);

                        Sample hSample = new Sample(sFilename, hSamples, hIdeal);
                        m_hConfig.Samples.Add(hSample);
                        m_hSamples.Items.Add(hSample);
                    }
                }

                m_hPanel.Invalidate();
            }
        }

        #endregion


        private Rectangle[,] BuildGrid(int iSize)
        {
            iSize = (int)Math.Sqrt(iSize);

            Rectangle[,] hGrid = new Rectangle[iSize, iSize];

            int iXSize = m_hPanel.Width / iSize;
            int iYSize = m_hPanel.Height / iSize;

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




        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            if (m_hSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer hSerializer = new XmlSerializer(typeof(List<Sample>));

                using (Stream hStream = File.OpenWrite(m_hSaveFileDialog.FileName))
                {
                    hSerializer.Serialize(hStream, m_hConfig);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_hOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = m_hOpenFileDialog.FileName;

                using (Stream hFs = File.OpenRead(sFileName))
                {
                    XmlSerializer hSerializer = new XmlSerializer(typeof(List<Sample>));
                    m_hConfig = hSerializer.Deserialize(hFs) as NetworkCreationConfig;

                    m_hNetwork = this.BuildNetwork(m_hConfig);

                    m_hPanel.Visible = true;
                    m_hGrid = this.BuildGrid(m_hNetwork.GetLayerNeuronCount(0));
                    m_hPenDrawer.QuantizedSpace = m_hGrid;

                    if (m_hConfig.Visualize)
                    {
                        m_hNeuralDisplay = new FormNNDrawer(m_hNetwork, m_hConfig.NeuronSize, m_hConfig.Width, m_hConfig.Height);
                        m_hNeuralDisplay.Show();
                    }

                    m_hPanel.Invalidate();

                }
            }
        }

        private BasicNetwork BuildNetwork(NetworkCreationConfig hConfig)
        {
            BasicNetwork hNetwork = new BasicNetwork();
            hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.InputSize));

            if (m_hConfig.HL0Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL0Size));

            if (m_hConfig.HL1Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL1Size));

            if (m_hConfig.HL2Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL2Size));

            hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.OutputSize));

            hNetwork.Structure.FinalizeStructure();
            hNetwork.Reset();

            return hNetwork;
        }

        private void feedForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNetworkForm hCreateDialog = new CreateNetworkForm();
            m_hSamples.Items.Clear();

            if (hCreateDialog.ShowDialog() == DialogResult.OK)
            {
                m_hConfig = hCreateDialog.Config;

                m_hNetwork = this.BuildNetwork(m_hConfig);

                m_hPanel.Visible            = true;
                m_hGrid                     = this.BuildGrid(m_hNetwork.GetLayerNeuronCount(0));
                m_hPenDrawer.QuantizedSpace = m_hGrid;

                if (m_hConfig.Visualize)
                {
                    m_hNeuralDisplay = new FormNNDrawer(m_hNetwork, m_hConfig.NeuronSize, m_hConfig.Width, m_hConfig.Height);
                    m_hNeuralDisplay.Show();
                }

                m_hPanel.Invalidate();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_hSamples.Items.Clear();
            m_hGrid = null;
            m_hPanel.Visible = false;

            if(m_hNeuralDisplay != null)
                m_hNeuralDisplay.Close();
        }
    }
}
