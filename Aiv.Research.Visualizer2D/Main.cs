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
using System.Collections.Generic;
using Aiv.Research.Visualizer2D.Filters;
using System.Reflection;

namespace Aiv.Research.Visualizer2D
{
    //I have implemented a hand written digit recognizer using MNIST dataset alone.It gives accuracy comparable to that of the latest OCR software.
    //I have used the full MNIST (60000 + 10000 examples) augmented with additional two versions of MNIST, altered using elastic deformations explained here : Distorting the MNIST Image Data Set
    //I have used a deep convolution neural network with two convolution-subsampling layers and an additional two hidden layer MLP.As this is a deep architecture, the more data you use for training, the better.
    //-The real challenge lies not in building the classifier, but preprocessing of data. You should make sure that the images you prepare for classification should be as close to that of MNIST, because MNIST the most cleanest dataset in terms of Image quality. You should crop your image well, add padding and remove noises, though CNN can deal with noise to some extent.
    public partial class Main : Form
    {        
        private SampleEditor    m_hPenDrawer;   
        private FormNNDrawer    m_hNeuralDisplay;
        private Settings        m_hSettings;
        private double[]        m_hLastIdeal;

        public Main()
        {
            InitializeComponent();

            m_hSettings = Settings.Load();
            m_hPanel.Visible = false;
            m_hPenDrawer    = new SampleEditor(m_hPanel);        

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
                    string sFilename = $"Sample{m_hSamples.Items.Count}.bmp";

                    PropertyGridForm hIdealInput = new PropertyGridForm(m_hLastIdeal);
                                        
                    if (hIdealInput.ShowDialog() == DialogResult.OK)
                    {
                        double[] hIdeal = m_hLastIdeal.Clone() as double[];
                        hBmp.Save(sFilename, ImageFormat.Bmp);
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
                m_hLastIdeal         = new double[m_hPenDrawer.Network.OutputSize];
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
            if (!m_hWorker.IsBusy)
            {
                m_hProgressBar.Minimum = 0;
                m_hProgressBar.Maximum = m_hPenDrawer.Network.Iterations;
                m_hProgressBar.Step    = 1;
                m_hWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Training Already In Progress", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
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

            hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone()     as IActivationFunction, true, hConfig.InputSize));

            if (hConfig.HL0Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL0Size));

            if (hConfig.HL1Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL1Size));

            if (hConfig.HL2Size > 0)
                hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone() as IActivationFunction, true, hConfig.HL2Size));

            hNetwork.AddLayer(new BasicLayer(hConfig.Activation.Clone()     as IActivationFunction, true, hConfig.OutputSize));

            hNetwork.Structure.FinalizeStructure();
            hNetwork.Reset();

            double[][] hInput = hConfig.Samples.Select(s => s.Values).ToArray();
            double[][] hIdeal = hConfig.Samples.Select(s => s.Ideal).ToArray();

            INeuralDataSet hTrainingSet = new BasicNeuralDataSet(hInput, hIdeal);
            ITrain hTraining = new ResilientPropagation(hNetwork, hTrainingSet);

            

            for (int i = 0; i < hConfig.Iterations; i++)
            {
                hTraining.Iteration(1);
                m_hWorker.ReportProgress( (i / hConfig.Iterations) * 100 );
            }


            e.Result = hNetwork;

            m_hNeuralDisplay = new FormNNDrawer(hNetwork, 20, 800, 600);            
        }

        private void OnProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            m_hProgressBar.Value += 1;
        }

        private void OnRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            m_hProgressBar.Value = 0;

            BasicNetwork hNetwork = e.Result as BasicNetwork;

            using (SaveFileDialog hDialog = new SaveFileDialog())
            {
                if (hDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream hFs = File.OpenWrite(hDialog.FileName))
                    {
                        Encog.Persist.EncogDirectoryPersistence.SaveObject(hFs, hNetwork);
                    }
                }
            }

            m_hNeuralDisplay.Show();            
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save(m_hSettings);
        }

        private void OnOptionsClick(object sender, EventArgs e) => new PropertyGridForm(m_hSettings).ShowDialog();
        private UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        private void OnDataLoad(object sender, EventArgs e)
        {
            string[] hFiles = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("ubyte")).ToArray();

            //Get Embedded Resources Names
            foreach (var item in hFiles)
            {
                if (!File.Exists(item))
                {
                    using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(item))
                    {                        
                        using (FileStream hFs = File.OpenWrite(item))
                        {
                            s.CopyTo(hFs);
                        }
                    }
                }
            }


            //Setup environment
            CreateNetworkForm hCreateDialog = new CreateNetworkForm(784, 10);

            m_hSamples.Items.Clear();

            if (hCreateDialog.ShowDialog() == DialogResult.OK)
            {
                m_hPenDrawer.Network = hCreateDialog.Config;
                m_hPanel.Visible = true;
                m_hLastIdeal = new double[m_hPenDrawer.Network.OutputSize];
            }


            byte[] hData = File.ReadAllBytes(hFiles.Where(x => x.Contains("train-images")).First());
            uint iMagicNumber = ReverseBytes(BitConverter.ToUInt32(hData, 0x0));
            uint iNumberOfImages = ReverseBytes(BitConverter.ToUInt32(hData, 0x4));
            uint iNumberOfRows = ReverseBytes(BitConverter.ToUInt32(hData, 0x8));
            uint iNumberOfColumns = ReverseBytes(BitConverter.ToUInt32(hData, 0xC));
            uint iIndex = 0x10;

            //Image Labels
            byte[] hLabels = File.ReadAllBytes(hFiles.Where(x => x.Contains("train-labels")).First());
            int iOffset = 0x8;

            List<double> hValues = new List<double>((int)(iNumberOfRows * iNumberOfColumns));

            for (int i = 0; i < iNumberOfImages; i++)
            {
                Sample hSample = new Sample();
                hValues.Clear();

                for (int x = 0; x < iNumberOfRows; x++)
                {
                    for (int y = 0; y < iNumberOfColumns; y++)
                    {
                        hValues.Add(hData[iIndex]);
                        iIndex++;
                    }
                }

                byte bLabel = hLabels[i + iOffset];
                hSample.Name = $"{i} - {bLabel}";
                hSample.Ideal = new double[10];
                hSample.Ideal[bLabel] = 1.0;
                hSample.Values = hValues.ToArray();


                m_hSamples.Items.Add(hSample);
                m_hPenDrawer.Network.Samples.Add(hSample);
            }
        }



        private void OnNormalizeCenterOfMass(object sender, EventArgs e)
        {

        }

        private void OnNormalizeValues(object sender, EventArgs e)
        {

        }

        private void OnGaussianBlur3x3(object sender, EventArgs e)
        {
            m_hSamples.Items.Clear();
            Filters.Filters.Apply(Filters.Filters.GaussianBlur3x3, m_hPenDrawer.Network.Samples, m_hPenDrawer.Rows, m_hPenDrawer.Columns);
            foreach (Sample item in m_hPenDrawer.Network.Samples)
            {
                m_hSamples.Items.Add(item);
            }
        }

        private void OnGaussianBlur5x5(object sender, EventArgs e)
        {
            m_hSamples.Items.Clear();
            Filters.Filters.Apply(Filters.Filters.GaussianBlur5x5, m_hPenDrawer.Network.Samples, m_hPenDrawer.Rows, m_hPenDrawer.Columns);
            foreach (Sample item in m_hPenDrawer.Network.Samples)
            {
                m_hSamples.Items.Add(item);
            }
        }

        private void OnGaussianBlur7x7(object sender, EventArgs e)
        {
            m_hSamples.Items.Clear();
            Filters.Filters.Apply(Filters.Filters.GaussianBlur7x7, m_hPenDrawer.Network.Samples, m_hPenDrawer.Rows, m_hPenDrawer.Columns);
            foreach (Sample item in m_hPenDrawer.Network.Samples)
            {
                m_hSamples.Items.Add(item);
            }
        }
    }
}
