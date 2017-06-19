using Aiv.Research.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Shared
{
    [ServiceContract]
    public interface ITrainingService
    {
        
        //Ad ogni chiamata viene creato un Thrad (meglio se usate i Task long running, max 4 attivi)
        //Se sono più di 4 vengono messi in una BlockingCollection<T>
        //Lavora i dati (mette eventuali oggetti in una lista apposita List<T> lokkata
        //Terminato il task deve classficicare i dati su filesystem (classe statica Classifier che si occupa di salvare i dati su disco)
        [OperationContract]
        void StartTraining(NetworkCreationConfig hNetwork);

        //ritorna lo stato attuale della lista contenente i task in corso
        [OperationContract]
        IEnumerable<TrainingSet> EnumerateTrainingsInProgress();

        //Chiede al claffier di fornire tutti i dati che ha scritto (li legge sempre da disco)
        [OperationContract]
        IEnumerable<TrainingSet> EnumerateTrainingsCompleted();

        [OperationContract]
        IEnumerable<TrainingSet> EnumerateTrainingsCompleted(string sExceptedConfigs);

        //Ritorna solamente i dati della rete richiesta
        [OperationContract]
        byte[] DownloadTrainingData(int iConfigId);

        //fa capo ad un cancellation token che viene controllato ad ogni iterazione del training
        [OperationContract]
        void TerminateTraining(int iConfigId);

        //Cancella da disco il train con id specificato
        [OperationContract]
        bool DeleteTraining(int iConfigId);

        [OperationContract]
        byte[] Download(int iConfigId);
    }
}
