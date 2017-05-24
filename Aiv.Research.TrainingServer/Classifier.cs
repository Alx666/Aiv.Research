using Aiv.Research.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.TrainingServer
{
    static class Classifier
    {
        //Stabilisce dove verranno create le cartelle con i dati
        //Se non ci sta la crea
        public static void SetDataPath(string sPath)
        {
        }

        public static void Store(NetworkCreationConfig hTraninedNetwork)
        {
        }

        public static IEnumerable<NetworkCreationConfig> Enumerate()
        {
            return null;
        }

        public static void Delete(NetworkCreationConfig hConfig)
        {
        }

        public static NetworkCreationConfig Get(int iId)
        {
            return null;
        }
    }
}
