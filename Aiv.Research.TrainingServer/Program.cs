using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Aiv.Research.Shared;

namespace Aiv.Research.TrainingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] b = new byte[byte.MaxValue];
            Shared.NetworkCreationConfig config = new Shared.NetworkCreationConfig();
            Classifier.SetDataPath("NeuralData");
            Classifier.Store(config, b);
            Classifier.Get(0);
            //Classifier.Get("0_");

            //TrainingService service = new TrainingService(4);
            //ConsoleUI console = new ConsoleUI(service, "TrainingServer");
            //console.RunAndWait();
        }

    }
}