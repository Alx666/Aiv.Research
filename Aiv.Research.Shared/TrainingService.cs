using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Research.Shared;
using System.ServiceModel;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        public const string TRAINING_SERVICE_NAME = "Training";
        private Task                                        m_hDispatcherTask;
        private BlockingCollection<TrainingSet>             m_hNetworksToTrain;
        private BlockingCollection<TrainingSet>             m_hCompletedTrainings;
        private ConcurrentDictionary<TrainingSet, int>      m_hTrainingInProgress;
        private CancellationTokenSource                     m_hDispatcherTakeToken;
        private int                                         m_iMaxParallelTrainings;
        private ServiceHost                                 m_hService;

        public TrainingService(int iMaxParallelTrainings)
        {
            m_iMaxParallelTrainings = iMaxParallelTrainings;
            m_hNetworksToTrain      = new BlockingCollection<TrainingSet>(iMaxParallelTrainings);
            m_hCompletedTrainings   = new BlockingCollection<TrainingSet>();
            m_hTrainingInProgress   = new ConcurrentDictionary<TrainingSet, int>();
            m_hDispatcherTakeToken  = new CancellationTokenSource();
            m_hDispatcherTask       = Task.Factory.StartNew(DispatcherRoutine, null, TaskCreationOptions.LongRunning);

            AppDomain.CurrentDomain.ProcessExit += (o, i) => m_hDispatcherTakeToken.Cancel();
        }

        [ConsoleUIMethod]
        public string StartService(int iPort)
        {
            m_hService = new ServiceHost(this, new Uri($"net.tcp://localhost:{iPort}/{TRAINING_SERVICE_NAME}/"));
            NetTcpBinding hBinding = new NetTcpBinding(SecurityMode.None, true);
            hBinding.ReceiveTimeout = TimeSpan.MaxValue;
            hBinding.SendTimeout = TimeSpan.MaxValue;
            m_hService.AddServiceEndpoint(typeof(ITrainingService), hBinding, string.Empty);
            m_hService.Open();
            Classifier.SetDataPath(Environment.CurrentDirectory);
            List<NetworkCreationConfig> hConfigs = Classifier.Enumerate().ToList();
            for (int i = 0; i < hConfigs.Count; i++)
            {
                m_hCompletedTrainings.Add(new TrainingSet(hConfigs[i]));
            }
            return "Service Started";
        }

        [ConsoleUIMethod]
        public void StopService()
        {
            m_hService.Close();
        }

        [ConsoleUIMethod]
        public bool DeleteTraining(int iConfigId)
        {
            TerminateTraining(iConfigId);
            int iRes;
            return m_hTrainingInProgress.TryRemove(m_hTrainingInProgress.Keys.FirstOrDefault(x => x.NetworkConfing.Id == iConfigId), out iRes);
        }

        //public IEnumerable<NetworkCreationConfig> EnumerateTrainingsCompleted(string sExceptedConfigs)
        //{
        //    sExceptedConfigs = sExceptedConfigs.ToLower();
        //    IEnumerable<TrainingSet> hResult = new List<TrainingSet>();
        //    throw new NotImplementedException("YOU ARE NOT PREPARED");
        //}

        [ConsoleUIMethod]
        public byte[] DownloadTrainingData(int iConfigId)
        {
            return Classifier.Get(iConfigId);
        }

        [ConsoleUIMethod]
        public IEnumerable<NetworkCreationConfig> EnumerateTrainingsCompleted()
        {
            List<NetworkCreationConfig> hNetworks = new List<NetworkCreationConfig>();
            m_hCompletedTrainings.ToList().ForEach(x => hNetworks.Add(x.NetworkConfing));
            return hNetworks;
        }

        [ConsoleUIMethod]
        public IEnumerable<NetworkCreationConfig> EnumerateTrainingsInProgress()
        {
            List<NetworkCreationConfig> hNetworks = new List<NetworkCreationConfig>();
            m_hTrainingInProgress.ToList().ForEach(x => hNetworks.Add(x.Key.NetworkConfing));
            return hNetworks;
        }
        
        public void StartTraining(NetworkCreationConfig hNetwork)
        {
            hNetwork.Id = Classifier.GetId();
            TrainingSet hNewTraining = new TrainingSet(hNetwork);
            m_hNetworksToTrain.Add(hNewTraining);
        }

        [ConsoleUIMethod]
        public void TerminateTraining(int iConfigId)
        {
            m_hTrainingInProgress.Keys.FirstOrDefault(x => x.NetworkConfing.Id == iConfigId).Token.ThrowIfCancellationRequested();
        }


        private void DispatcherRoutine(object hParam)
        {
            while(true)
            {
                TrainingSet hJob = m_hNetworksToTrain.Take(m_hDispatcherTakeToken.Token); //dispatcher thread wait here for a job to do
                while (m_hTrainingInProgress.Count >= m_iMaxParallelTrainings)
                {
                    //Aspetto che finiscano i vari training
                }
                Task hTraining = Task.Factory.StartNew(NetworkTrainingRoutine, hJob, hJob.Token);
            }
        }


        private void NetworkTrainingRoutine(object hParam)
        {
            TrainingSet hWorkItem = hParam as TrainingSet;
            Console.WriteLine("New Training Started with Name:" + hWorkItem.NetworkConfing.Name);
            if (hWorkItem == null)
                return;
            m_hTrainingInProgress.TryAdd(hWorkItem, hWorkItem.NetworkConfing.Id);
            hWorkItem.StartTraing();
            while (hWorkItem.IsTraining)
            {
                //Aspetto che finisca di fa il training       
            }
            int iRes;
            m_hTrainingInProgress.TryRemove(hWorkItem, out iRes);
            m_hCompletedTrainings.Add(hWorkItem);
            Classifier.SetDataPath(Environment.CurrentDirectory);
            hWorkItem.NetworkConfing.Name = Classifier.GetId().ToString() + "_" + hWorkItem.NetworkConfing.Name;
            Classifier.Store(hWorkItem.NetworkConfing, SerializeToStream(hWorkItem.NetworkConfing).ToArray());
            Console.WriteLine("Training Completed");
        }

        private MemoryStream SerializeToStream(object hObj)
        {
            MemoryStream hStream = new MemoryStream();
            IFormatter hFormatter = new BinaryFormatter();
            hFormatter.Serialize(hStream, hObj);
            return hStream;
        }

        private object DeserializeFromStream(MemoryStream hStream)
        {
            IFormatter hFormatter = new BinaryFormatter();
            hStream.Seek(0, SeekOrigin.Begin);
            return hFormatter.Deserialize(hStream);
        }
    }

    public class TrainingSet
    {
        public NetworkCreationConfig NetworkConfing { get; private set; }
        public int Iterations { get; private set; }
        public bool IsTraining { get; private set; }
        public CancellationToken Token { get; private set; }
        private BasicNetwork m_hNetwork;

        public TrainingSet(NetworkCreationConfig hConfig)
        {
            Token = new CancellationToken();
            NetworkConfing = hConfig;
            Iterations = hConfig.Iterations;
        }
        public void StartTraing()
        {
            IsTraining = true;
            m_hNetwork = NetworkConfing.GetNewNetwork();

            double[][] input = new double[NetworkConfing.Samples.Count][];
            double[][] ideal = new double[NetworkConfing.Samples.Count][];
            for (int i = 0; i < NetworkConfing.Samples.Count; i++)
            {
                input[i] = Array.ConvertAll(NetworkConfing.Samples[i].Values, x => (double)x);
                ideal[i] = Array.ConvertAll(NetworkConfing.Samples[i].Ideal, x => (double) x);
            }
            INeuralDataSet hNeuralDataSet = new BasicNeuralDataSet(input, ideal);
            ITrain hTraining = new ResilientPropagation(m_hNetwork, hNeuralDataSet);
            hTraining.Iteration(Iterations);
            
            IsTraining = false;
        }

        [Obsolete]
        public double[] TestNetwork(double[] input)
        {
            double[] output = new double[NetworkConfing.OutputSize];
            m_hNetwork.Compute(input, output);
            return output;
        }
    }
}
