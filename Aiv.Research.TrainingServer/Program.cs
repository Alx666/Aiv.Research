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
        static void Main(string[] args) //=> new ConsoleUI(new TrainingService(4, 28999), "TrainingServer").RunAndWait();
        {
            byte[] b = new byte[byte.MaxValue];
            Shared.NetworkCreationConfig config = new Shared.NetworkCreationConfig();

            Random hRand = new Random();
            for (int i = 0; i < 5; i++)
            {
                b = Enumerable.Range(0, 500).Select(x => (byte)hRand.Next()).ToArray();
                config.Name = i.ToString();
                Classifier.SetDataPath(".\\NeuralData\\Gianni"); 
                //Classifier.Store(config, b);
                //Classifier.Get(i);
                //Classifier.Get(i);
            }

            //Classifier.Get("0_");

            //TrainingService service = new TrainingService(4);
            //ConsoleUI console = new ConsoleUI(service, "TrainingServer");
            //console.RunAndWait();
        }
    }
        
}