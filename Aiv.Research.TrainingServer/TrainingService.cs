using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Research.Shared;
using System.ServiceModel;
using System.Collections.Concurrent;
using System.Threading;
using Encog.Engine.Network.Activation;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Neural.NeuralData;

namespace Aiv.Research.TrainingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    class TrainingService : ITrainingService
    {
        private Task                                                m_hDispatcherTask;
        private BlockingCollection<TrainingSet>           m_hNetworksToTrain;
        private ConcurrentDictionary<TrainingSet, int>    m_hTrainingInProgress;
        private CancellationTokenSource                             m_hDispatcherTakeToken;
        private int                                                 m_iMaxParallelTrainings;

        public TrainingService(int iMaxParallelTrainings)
        {
            m_iMaxParallelTrainings = iMaxParallelTrainings;
            m_hNetworksToTrain      = new BlockingCollection<TrainingSet>(iMaxParallelTrainings);
            m_hTrainingInProgress   = new ConcurrentDictionary<TrainingSet, int>();
            m_hDispatcherTakeToken  = new CancellationTokenSource();
            m_hDispatcherTask       = Task.Factory.StartNew(DispatcherRoutine, null, TaskCreationOptions.LongRunning);

            AppDomain.CurrentDomain.ProcessExit += (o, i) => m_hDispatcherTakeToken.Cancel();
        }

        [ConsoleUIMethod]
        public void DeleteTraining(int iConfigId)
        {
            throw new NotImplementedException();
        }

        [ConsoleUIMethod]
        public byte[] DownloadTrainingData(int iConfigId)
        {
            throw new NotImplementedException();
        }

        [ConsoleUIMethod]
        public IEnumerable<NetworkCreationConfig> EnumerateTrainingsCompleted()
        {
            throw new NotImplementedException();
        }

        [ConsoleUIMethod]
        public IEnumerable<NetworkCreationConfig> EnumerateTrainingsInProgress()
        {
            throw new NotImplementedException();
        }

      
        [ConsoleUIMethod]
        public void StartTraining(NetworkCreationConfig hNetwork, int iIterations, string sConfig)
        {
            TrainingSet hNewTraining = new TrainingSet(hNetwork, iIterations, sConfig);
            m_hNetworksToTrain.Add(hNewTraining);
        }

        [ConsoleUIMethod]
        public void TerminateTraining(int iConfigId)
        {
            throw new NotImplementedException();
        }


        private void DispatcherRoutine(object hParam)
        {
            while(true)
            {
                TrainingSet hJob = m_hNetworksToTrain.Take(m_hDispatcherTakeToken.Token); //dispatcher thread wait here for a job to do

                Task hTraining = Task.Factory.StartNew(NetworkTrainingRoutine, hJob, TaskCreationOptions.LongRunning);     
            }
        }


        private void NetworkTrainingRoutine(object hParam)
        {
            TrainingSet hWorkItem = hParam as TrainingSet;
            if (hWorkItem == null)
                return;
            m_hTrainingInProgress.TryAdd(hWorkItem, 0);
            hWorkItem.StartTraing();
            while (hWorkItem.IsTraining)
            {
                //Aspetto che finisca di fa il training       
            }
            int iRes;
            m_hTrainingInProgress.TryRemove(hWorkItem, out iRes);
            //TODO Send to Classifier
        }
    }

    public class TrainingSet
    {
        public NetworkCreationConfig NetworkConfing { get; private set; }
        public int Iterations { get; private set; }
        public string Config { get; private set; }
        public bool IsTraining { get; private set; }
        private BasicNetwork m_hNetwork;

        public TrainingSet(NetworkCreationConfig hConfig, int iIterations, string sConfig)
        {
            NetworkConfing = hConfig;
            Iterations = iIterations;
            Config = sConfig;
        }
        public void StartTraing()
        {
            IsTraining = true;
            m_hNetwork = new BasicNetwork();
            if(NetworkConfing.InputSize > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfing.Activation, true, NetworkConfing.InputSize));
            if(NetworkConfing.HL0Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfing.Activation, true, NetworkConfing.HL0Size));
            if(NetworkConfing.HL1Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfing.Activation, true, NetworkConfing.HL1Size));
            if(NetworkConfing.HL2Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfing.Activation, true, NetworkConfing.HL2Size));
            if(NetworkConfing.OutputSize > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfing.Activation, true, NetworkConfing.OutputSize));

            m_hNetwork.Structure.FinalizeStructure();
            m_hNetwork.Reset();

            double[][] input = new double[NetworkConfing.Samples.Count][];
            double[][] ideal = new double[NetworkConfing.Samples.Count][];
            for (int i = 0; i < NetworkConfing.Samples.Count; i++)
            {
                input[i] = NetworkConfing.Samples[i].Values;
                ideal[i] = NetworkConfing.Samples[i].Ideal;
            }
            
            INeuralDataSet hNeuralDataSet = new BasicNeuralDataSet(input, ideal);
            ITrain hTraining = new ResilientPropagation(m_hNetwork, hNeuralDataSet);
            hTraining.Iteration(Iterations);

            IsTraining = false;
        }

        public double[] TestNetwork(double[] input)
        {
            double[] output = new double[NetworkConfing.OutputSize];
            m_hNetwork.Compute(input, output);
            return output;
        }
    }
}
