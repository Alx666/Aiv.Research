using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D.Utils.Input;
using Aiv.Research.Shared;

namespace Aiv.Research.Tests.Landing
{
    class Program
    {
        static void Main(string[] args) => new ConsoleUI(new Controller(), "Ландер").RunAndWait();                            
    }
}
