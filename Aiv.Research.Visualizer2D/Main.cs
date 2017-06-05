using Aiv.Research.Shared;
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
using Aiv.Research.TrainingServer;
using System.Collections.Generic;
using System.Threading;

namespace Aiv.Research.Visualizer2D
{
    //I have implemented a hand written digit recognizer using MNIST dataset alone.It gives accuracy comparable to that of the latest OCR software.
    //I have used the full MNIST (60000 + 10000 examples) augmented with additional two versions of MNIST, altered using elastic deformations explained here : Distorting the MNIST Image Data Set
    //I have used a deep convolution neural network with two convolution-subsampling layers and an additional two hidden layer MLP.As this is a deep architecture, the more data you use for training, the better.
    //-The real challenge lies not in building the classifier, but preprocessing of data. You should make sure that the images you prepare for classification should be as close to that of MNIST, because MNIST the most cleanest dataset in terms of Image quality. You should crop your image well, add padding and remove noises, though CNN can deal with noise to some extent.
    public partial class Main : Form
    {        
        private PenDrawer           m_hPenDrawer;   
        private FormNNDrawer        m_hNeuralDisplay;        
        private List<Thread>        m_hThreads;         //TODO: rimuovere dalla lista quando il thread di disegno termina

        public Main()
        {
            InitializeComponent();

            m_hPanel.Visible = false;
            m_hPenDrawer    = new PenDrawer(m_hPanel);
            m_hThreads = new List<Thread>();

            #region XOR Network

            BasicNetwork Network;
            Network = new BasicNetwork();
            Network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 2));
            Network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 8));
            Network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 8));
            Network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 8));
            Network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 1));
            Network.Structure.FinalizeStructure();
            Network.Reset();

            double[][] XorInput = new double[4][];
            XorInput[0] = new double[2] { 0.0, 0.0 };
            XorInput[1] = new double[2] { 0.1, 0.0 };
            XorInput[2] = new double[2] { 0.0, 0.1 };
            XorInput[3] = new double[2] { 0.1, 0.1 };


            double[][] XorIdeal = new double[4][]; 
            XorIdeal[0] = new double[1] { 0.0 };
            XorIdeal[1] = new double[1] { 1.0 };
            XorIdeal[2] = new double[1] { 1.0 };
            XorIdeal[3] = new double[1] { 0.0 };
            INeuralDataSet hTrainingSet = new BasicNeuralDataSet(XorInput, XorIdeal);
            ITrain hTraining = new ResilientPropagation(Network, hTrainingSet);

            hTraining.Iteration(5000);


            Thread hNewThread = new Thread(VisualizerThread);
            m_hThreads.Add(hNewThread);
            hNewThread.Start(Network);


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
            if (e.KeyCode == Keys.C && m_hNeuralDisplay != null)
            {
                double[] hInput;
                m_hPenDrawer.Clear(out hInput);
                m_hNeuralDisplay.Compute(hInput);
            }

            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.Control && m_hPanel.Visible)
            {
                double[] hSamples;

                using (Bitmap hBmp = m_hPenDrawer.Clear(out hSamples))
                {
                    using (Bitmap hDownscaled = hBmp.ResizeImage(320, 240))
                    {
                        string sFilename = $"Sample{m_hSamples.Items.Count}.bmp";
                        IdealInputForm hIdealInput = new IdealInputForm(m_hPenDrawer.Network.OutputSize);

                        if (hIdealInput.ShowDialog() == DialogResult.OK)
                        {                            
                            double[] hIdeal = hIdealInput.Ideal;
                            hDownscaled.Save(sFilename, ImageFormat.Bmp);
                            Sample hSample = new Sample(sFilename, hSamples, hIdeal);
                            m_hPenDrawer.Network.Samples.Add(hSample);
                            m_hSamples.Items.Add(hSample);
                        }
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
            if (m_hSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));

                if (File.Exists(m_hSaveFileDialog.FileName))
                    File.Delete(m_hSaveFileDialog.FileName);

                using (Stream hStream = File.OpenWrite(m_hSaveFileDialog.FileName))
                {
                    hSerializer.Serialize(hStream, m_hPenDrawer.Network);
                }
            }
        }

        private void MenuItemLoad(object sender, EventArgs e)
        {
            if (m_hOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = m_hOpenFileDialog.FileName;

                using (Stream hFs = File.OpenRead(sFileName))
                {
                    XmlSerializer hSerializer   = new XmlSerializer(typeof(NetworkCreationConfig));
                    m_hPenDrawer.Network        = hSerializer.Deserialize(hFs) as NetworkCreationConfig;
                    m_hPanel.Visible            = true;

                    m_hSamples.Items.Clear();

                    for (int i = 0; i < m_hPenDrawer.Network.Samples.Count; i++)
                    {
                        m_hSamples.Items.Add(m_hPenDrawer.Network.Samples[i]);
                    }
                }
            }
        }

        private void MenuItemCreate(object sender, EventArgs e)
        {
            CreateNetworkForm hCreateDialog = new CreateNetworkForm();
            m_hSamples.Items.Clear();

            if (hCreateDialog.ShowDialog() == DialogResult.OK)
            {
                m_hPenDrawer.Network = hCreateDialog.Config;
                m_hPanel.Visible     = true;
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
            if (m_hWorker.IsBusy)
            {
                MessageBox.Show("No", "Stocazzo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_hWorker.RunWorkerAsync();
        }

        private void OnSamplesSelectedIndexChanged(object sender, EventArgs e)
        {
            Sample hSelected = m_hSamples.SelectedItem as Sample;

            if (hSelected != null)
                m_hPenDrawer.OnSampleSelected(hSelected);
        }

        private void OnDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            NetworkCreationConfig hConfig = m_hPenDrawer.Network;
            BasicNetwork hNetwork = new BasicNetwork();

            hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.InputSize));

            if (hConfig.HL0Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL0Size));

            if (hConfig.HL1Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL1Size));

            if (hConfig.HL2Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL2Size));

            hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.OutputSize));

            hNetwork.Structure.FinalizeStructure();
            hNetwork.Reset();

            double[][] hInput = hConfig.Samples.Select(s => s.Values).ToArray();
            double[][] hIdeal = hConfig.Samples.Select(s => s.Ideal).ToArray();

            INeuralDataSet hTrainingSet = new BasicNeuralDataSet(hInput, hIdeal);
            ITrain hTraining = new ResilientPropagation(hNetwork, hTrainingSet);


            for (int i = 0; i < 50000; i++)
            {
                hTraining.Iteration(1);

                if (i % 500 == 0)
                    m_hWorker.ReportProgress(i / 500);
            }

            m_hNeuralDisplay = new FormNNDrawer(hNetwork, 20, 800, 600);            
        }

        private void OnProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            m_hProgressBar.Value = e.ProgressPercentage;
        }

        private void OnRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            m_hProgressBar.Value = 0;
            m_hNeuralDisplay.Show();            
        }

        private void OnRemoteBackPropagation(object sender, EventArgs e)
        {
            TrainingClient hClient = new TrainingClient();
        }

        private void VisualizerThread(object hParam)
        {
            BasicNetwork hNetwork = hParam as BasicNetwork;

            NetworkVisualizer hVisualizer = new NetworkVisualizer(800, 600, "Xor", hNetwork, 0.0001f);

            //Create Main Loop
            while (hVisualizer.IsOpened)
            {
                hVisualizer.Draw();                
                hVisualizer.Update();
            }
        }
    }
}
