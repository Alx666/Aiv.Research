﻿using Aiv.Research.Shared;
using Encog.Engine.Network.Activation;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Neural.NeuralData;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Linq;

namespace Aiv.Research.Visualizer2D
{
    public partial class Main : Form
    {        
        private PenDrawer       m_hPenDrawer;   
        private FormNNDrawer    m_hNeuralDisplay;

        public Main()
        {
            InitializeComponent();

            m_hPanel.Visible = false;
            m_hPenDrawer    = new PenDrawer(m_hPanel);            

            #region XOR Network

            //m_hNetwork = new BasicNetwork();
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 2));
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 8));
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 8));
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 8));
            //m_hNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 1));
            //m_hNetwork.Structure.FinalizeStructure();
            //m_hNetwork.Reset();

            //double[][] XorInput = new double[4][];
            //XorInput[0] = new double[2] { 0.0, 0.0 };
            //XorInput[1] = new double[2] { 0.1, 0.0 };
            //XorInput[2] = new double[2] { 0.0, 0.1 };
            //XorInput[3] = new double[2] { 0.1, 0.1 };


            //double[][] XorIdeal = new double[4][];
            //XorIdeal[0] = new double[1] { 0.0 };
            //XorIdeal[1] = new double[1] { 1.0 };
            //XorIdeal[2] = new double[1] { 1.0 };
            //XorIdeal[3] = new double[1] { 0.0 };
            //INeuralDataSet hTrainingSet = new BasicNeuralDataSet(XorInput, XorIdeal);
            //ITrain hTraining = new ResilientPropagation(m_hNetwork, hTrainingSet);

            //hTraining.Iteration(5000);

            //double[] hInput = new double[] { 456.0, 12.0 };
            //double[] hOutput = new double[1];
            //m_hNetwork.Compute(hInput, hOutput);

            //m_hNeuralDisplay = new FormNNDrawer(m_hNetwork, 16, 800, 600);
            //m_hNeuralDisplay.Show();

            #endregion

        }

        #region Panel Event Handlers
        
        private void OnPanelMouseLeave(object sender, EventArgs e)
        {
            m_hPenDrawer.End();
        }

        private void OnPanelMouseDown(object sender, MouseEventArgs e)
        {
            m_hPenDrawer.Begin(e.X, e.Y);
        }

        private void OnPanelMouseMove(object sender, MouseEventArgs e)
        {
            m_hPenDrawer.Update(e.X, e.Y);
        }

        private void OnPanelMouseUp(object sender, MouseEventArgs e)
        {
            m_hPenDrawer.End();
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.Control && m_hPanel.Visible)
            {
                double[] hSamples;

                using (Bitmap hBmp = m_hPenDrawer.Clear(out hSamples))
                {
                    using (Bitmap hDownscaled = hBmp.ResizeImage(320, 240))
                    {
                        string sFilename = $"Sample{m_hSamples.Items.Count}.bmp";

                        IdealInputForm hIdealInput = new IdealInputForm(m_hPenDrawer.Network.InputSize);
                        hIdealInput.ShowDialog();
                        double[] hIdeal = hIdealInput.Ideal;

                        hDownscaled.Save(sFilename, ImageFormat.Bmp);

                        Sample hSample = new Sample(sFilename, hSamples, hIdeal);
                        m_hPenDrawer.Network.Samples.Add(hSample);
                        m_hSamples.Items.Add(hSample);
                    }
                }

                m_hPanel.Invalidate();
            }
        }

        #endregion





        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            m_hPenDrawer.OnPaint(sender, e);
        }

        private void MenuItemSave(object sender, EventArgs e)
        {            
            //if (m_hSaveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));

            //    using (Stream hStream = File.OpenWrite(m_hSaveFileDialog.FileName))
            //    {
            //        hSerializer.Serialize(hStream, m_hPenDrawer.Network);
            //    }
            //}
        }

        private void MenuItemLoad(object sender, EventArgs e)
        {
            //if (m_hOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string sFileName = m_hOpenFileDialog.FileName;

            //    using (Stream hFs = File.OpenRead(sFileName))
            //    {
            //        XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));
            //        m_hPenDrawer.Network = hSerializer.Deserialize(hFs) as NetworkCreationConfig;
                    
            //        m_hPanel.Visible = true;
                    
            //        if (m_hPenDrawer.Network.Visualize)
            //        {
            //            m_hNeuralDisplay = new FormNNDrawer(m_hNetwork, m_hConfig.NeuronSize, m_hConfig.Width, m_hConfig.Height);
            //            m_hNeuralDisplay.Show();
            //        }

            //        m_hPanel.Invalidate();

            //    }
            //}
        }

        private void MenuItemCreate(object sender, EventArgs e)
        {
            CreateNetworkForm hCreateDialog = new CreateNetworkForm();
            m_hSamples.Items.Clear();

            if (hCreateDialog.ShowDialog() == DialogResult.OK)
            {
                m_hPenDrawer.Network = hCreateDialog.Config;

                m_hPanel.Visible            = true;

                //if (m_hConfig.Visualize)
                //{
                //    m_hNeuralDisplay = new FormNNDrawer(m_hNetwork, m_hConfig.NeuronSize, m_hConfig.Width, m_hConfig.Height);
                //    m_hNeuralDisplay.Show();
                //}
            }
        }

        private void MenuItemClose(object sender, EventArgs e)
        {
            m_hSamples.Items.Clear();
            m_hPanel.Visible = false;

            if(m_hNeuralDisplay != null)
                m_hNeuralDisplay.Close();
        }

        private void MenuItemBackpropagationTrain(object sender, EventArgs e)
        {
            //WCF stuff

        }
    }
}
