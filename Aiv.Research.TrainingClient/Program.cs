using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Research.Shared;

namespace Aiv.Research.TrainingClient
{
    class Program
    {
        static void Main(string[] args) => new ConsoleUI(new Aiv.Research.Shared.TrainingClient(), "Training Client").RunAndWait();
    }
}
