using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Aiv.Research.TrainingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] b = new byte[byte.MaxValue];
            Shared.NetworkCreationConfig config = new Shared.NetworkCreationConfig();
            Classifier.SetDataPath("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/cicciofolder/");
            Classifier.Store(config, b);
            //Classifier.Zip("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/zippofolder/");
            // Classifier.Delete("zippofolder");
            //TrainingService service = new TrainingService(4);
        }

    }
}
