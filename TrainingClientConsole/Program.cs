using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Research.Shared;
using System.Windows.Forms;

namespace TrainingClientConsole
{

    class Program
    {
        [STAThread]
        static void Main(string[] args) => new ConsoleUI(new TrainingClient(), "Training Client").RunAndWait();
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Main());
        //}
    }
}
