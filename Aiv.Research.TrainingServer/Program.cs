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
        //static void Main(string[] args)
        //{
        //    NetworkCreationConfig hoConfig = new NetworkCreationConfig();
        //    byte[] h = NetworkCreationConfig.Compress(hoConfig);

        //    NetworkCreationConfig hNewConfig = NetworkCreationConfig.Decompress(h);
        //    Console.ReadLine();
        //}
        static void Main(string[] args) => new ConsoleUI(new TrainingService(4), "Training Service").RunAndWait();
    }
        
}