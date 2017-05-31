using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Engine.Network.Activation;
using System;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

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
        public string Name      { get; set; }
        [DataMember]
        public int Id           { get; set; }
        [DataMember]
        public int InputSize    { get; set; }
        [DataMember]
        public int OutputSize   { get; set; }
        [DataMember]
        public int HL0Size      { get; set; }
        [DataMember]
        public int HL1Size      { get; set; }
        [DataMember]
        public int HL2Size      { get; set; }
        [DataMember]
        public int iIterations { get; set; }

        [DataMember]
        private Guid m_vActivationTypeGuid;

        [DataMember]
        public Guid ActivationTypeGuid
        {
            get
            {
                return m_vActivationTypeGuid;
            }
            set
            {
                m_vActivationTypeGuid   = value;
                Activation              = Activator.CreateInstance((from t in Assembly.Load("encog-core-cs").GetTypes() where t.GUID == m_vActivationTypeGuid select t).FirstOrDefault()) as IActivationFunction;
            }
        }


        [XmlIgnore]
        private IActivationFunction m_hActivationFunc;

        [XmlIgnore]
        public IActivationFunction Activation
        {
            get
            {
                return m_hActivationFunc;
            }

            set
            {
                m_hActivationFunc       = value;
                m_vActivationTypeGuid   = value.GetType().GUID;
            }
        }

        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public List<Sample> Samples { get; set; }

        public override string ToString() => $"[{Id}] {Name}";
    }
}
