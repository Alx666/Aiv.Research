using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Engine.Network.Activation;
using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Aiv.Research.Shared
{
    [Serializable]
    [DataContract]    
    public class NetworkCreationConfig
    {
        public NetworkCreationConfig()
        {
            Samples = new List<Sample>();
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int InputSize { get; set; }
        [DataMember]
        public int OutputSize { get; set; }
        [DataMember]
        public int HL0Size { get; set; }
        [DataMember]
        public int HL1Size { get; set; }
        [DataMember]
        public int HL2Size { get; set; }

       
        public IActivationFunction Activation { get; set; }

        [DataMember]
        public bool Visualize { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int NeuronSize { get; set; }

        [DataMember]
        public List<Sample> Samples { get; set; }


        public override string ToString() => $"[{Id}] {Name}";
    }
}
