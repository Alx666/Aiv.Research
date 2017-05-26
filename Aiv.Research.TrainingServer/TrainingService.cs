using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Research.Shared;
using System.ServiceModel;
using System.Collections.Concurrent;
using System.Threading;

namespace Aiv.Research.TrainingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    class TrainingService : ITrainingService
    {
        private Task                                                m_hDispatcherTask;
        private BlockingCollection<NetworkCreationConfig>           m_hNetworksToTrain;
        private ConcurrentDictionary<NetworkCreationConfig, int>    m_hTrainingInProgress;
        private CancellationTokenSource                             m_hDispatcherTakeToken;
        private int                                                 m_iMaxParallelTrainings;

        public TrainingService(int iMaxParallelTrainings)
        {
            m_iMaxParallelTrainings = iMaxParallelTrainings;
            m_hNetworksToTrain      = new BlockingCollection<NetworkCreationConfig>(iMaxParallelTrainings);
            m_hTrainingInProgress   = new ConcurrentDictionary<NetworkCreationConfig, int>();
            m_hDispatcherTakeToken  = new CancellationTokenSource();
            m_hDispatcherTask       = Task.Factory.StartNew(DispatcherRoutine, null, TaskCreationOptions.LongRunning);
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
            if (m_hNetworksToTrain.Count >= m_iMaxParallelTrainings)
            {
                //non possiamo mettere altre cose a lavoro (eccezione, fault channel, o cosa???)

            }

            m_hNetworksToTrain.Add(hNetwork); //salvare anche iterations ed eventuali altri dati
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
                NetworkCreationConfig hJob = m_hNetworksToTrain.Take(m_hDispatcherTakeToken.Token); //dispatcher thread wait here for a job to do

                Task hTraining = Task.Factory.StartNew(NetworkTrainingRoutine, hJob, TaskCreationOptions.LongRunning);     
            }
        }


        private void NetworkTrainingRoutine(object hParam)
        {
            NetworkCreationConfig hWorkItem = hParam as NetworkCreationConfig;

            m_hTrainingInProgress.TryAdd(hWorkItem, 0);

            //Looooong Training here

            m_hTrainingInProgress.TryRemove(hWorkItem, out int iValue);

            //Send to Classifier
        }
    }
}
