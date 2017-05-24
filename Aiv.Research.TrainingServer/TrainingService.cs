using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Research.Shared;
using System.ServiceModel;
using System.Collections.Concurrent;

namespace Aiv.Research.TrainingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, 
                     InstanceContextMode = InstanceContextMode.Single, 
                     AddressFilterMode = AddressFilterMode.Any)]
    class TrainingService : ITrainingService
    {
        private Task m_hTask;
        public TrainingService()
        {
            m_hTask = Task.Factory.StartNew(metodo, cancellationToken, TaskCreationOptions.LongRunning);
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

        BlockingCollection<NetworkCreationConfig> m_hCollection;
        [ConsoleUIMethod]
        public void StartTraining(NetworkCreationConfig hNetwork, int iIterations, string sConfig)
        {
            m_hCollection.Add(hNetwork);
        }

        [ConsoleUIMethod]
        public void TerminateTraining(int iConfigId)
        {
            throw new NotImplementedException();
        }

        private void InputDataManager()
        {
            while(cancellation token)
            {
                m_hCollection.Take(); //sta sempre fermo qui

            }
        }
    }
}
