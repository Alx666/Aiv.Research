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
        static void Main(string[] args) => new ConsoleUI(new TrainingService(4), "TrainingServer").RunAndWait();        
    }
}