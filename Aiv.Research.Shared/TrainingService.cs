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

namespace Aiv.Research.Shared
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class TrainingService : ITrainingService
    {
        private Task                                        m_hDispatcherTask;
        private BlockingCollection<TrainingSet>             m_hNetworksToTrain;
        private ConcurrentDictionary<TrainingSet, int>      m_hTrainingInProgress;
        private CancellationTokenSource                     m_hDispatcherTakeToken;
        private int                                         m_iMaxParallelTrainings;
        private int                                         m_iPort;
        private ServiceHost                                 m_hService;


        public TrainingService(int iMaxParallelTrainings, int iPort)
        {
            m_iMaxParallelTrainings = iMaxParallelTrainings;
            m_hNetworksToTrain      = new BlockingCollection<TrainingSet>(iMaxParallelTrainings);
            m_hTrainingInProgress   = new ConcurrentDictionary<TrainingSet, int>();
            m_hDispatcherTakeToken  = new CancellationTokenSource();
            m_hDispatcherTask       = Task.Factory.StartNew(DispatcherRoutine, null, TaskCreationOptions.LongRunning);
            m_iPort                 = iPort;

            AppDomain.CurrentDomain.ProcessExit += (o, i) => m_hDispatcherTakeToken.Cancel();

            this.Start(m_iPort);
        }

        [ConsoleUIMethod]
        public void Start(int iPort)
        {
            m_hService = new ServiceHost(this, new Uri($"net.tcp://0.0.0.0:{iPort}/Training"));
            NetTcpBinding hBinding = new NetTcpBinding(SecurityMode.None, true);
            hBinding.ReceiveTimeout = TimeSpan.MaxValue;
            hBinding.SendTimeout = TimeSpan.MaxValue;
            m_hService.AddServiceEndpoint(typeof(ITrainingService), hBinding, string.Empty);
        }

        [ConsoleUIMethod]
        void Stop()
        {
            m_hService.Close();
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
        
        public void StartTraining(NetworkCreationConfig hNetwork)
        {
            TrainingSet hNewTraining = new TrainingSet(hNetwork);
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
            hWorkItem.StartTraining();
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
        public NetworkCreationConfig NetworkConfig { get; private set; }
        public bool IsTraining { get; private set; }
        private BasicNetwork m_hNetwork;

        public TrainingSet(NetworkCreationConfig hConfig)
        {
            NetworkConfig = hConfig;
        }
        public void StartTraining()
        {
            IsTraining = true;
            m_hNetwork = new BasicNetwork();
            if(NetworkConfig.InputSize > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfig.Activation, true, NetworkConfig.InputSize));
            if(NetworkConfig.HL0Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfig.Activation, true, NetworkConfig.HL0Size));
            if(NetworkConfig.HL1Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfig.Activation, true, NetworkConfig.HL1Size));
            if(NetworkConfig.HL2Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfig.Activation, true, NetworkConfig.HL2Size));
            if(NetworkConfig.OutputSize > 0)
                m_hNetwork.AddLayer(new BasicLayer(NetworkConfig.Activation, true, NetworkConfig.OutputSize));

            m_hNetwork.Structure.FinalizeStructure();
            m_hNetwork.Reset();

            double[][] input = new double[NetworkConfig.Samples.Count][];
            double[][] ideal = new double[NetworkConfig.Samples.Count][];
            for (int i = 0; i < NetworkConfig.Samples.Count; i++)
            {
                input[i] = NetworkConfig.Samples[i].Values;
                ideal[i] = NetworkConfig.Samples[i].Ideal;
            }
            
            INeuralDataSet hNeuralDataSet = new BasicNeuralDataSet(input, ideal);
            ITrain hTraining = new ResilientPropagation(m_hNetwork, hNeuralDataSet);
            hTraining.Iteration(NetworkConfig.iIterations);

            IsTraining = false;
        }

        public double[] TestNetwork(double[] input)
        {
            double[] output = new double[NetworkConfig.OutputSize];
            m_hNetwork.Compute(input, output);
            return output;
        }
    }
}
