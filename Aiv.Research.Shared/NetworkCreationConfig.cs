using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Engine.Network.Activation;
using System;

namespace Aiv.Research.Shared
{
    [Serializable]
    public class NetworkCreationConfig
    {
        public NetworkCreationConfig()
        {
            Samples = new List<Sample>();
        }

        public int InputSize { get; set; }
        public int OutputSize { get; set; }
        public int HL0Size { get; set; }
        public int HL1Size { get; set; }
        public int HL2Size { get; set; }
        public IActivationFunction Activation { get; set; }
        public bool Visualize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NeuronSize { get; set; }

        public List<Sample> Samples { get; set; }
    }
}
