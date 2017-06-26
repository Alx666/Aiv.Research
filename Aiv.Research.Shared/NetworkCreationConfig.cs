using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Engine.Network.Activation;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;

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
        [DataMember]
        public int Iterations { get; set; }

        [DataMember]
        private Guid m_vActivationTypeGuid;

        [DataMember]
        public Guid ActivationTypeGuid {
            get {
                return m_vActivationTypeGuid;
            }
            set {
                m_vActivationTypeGuid = value;
                //TODO Fixare la generazione dell'Activator
                //Activation              = Activator.CreateInstance((from t in Assembly.Load("encog-core-cs").GetTypes() where t.GUID == m_vActivationTypeGuid select t).FirstOrDefault()) as IActivationFunction;
                Activation = new ActivationElliott();
            }
        }


        [XmlIgnore]
        private IActivationFunction m_hActivationFunc;

        [XmlIgnore]
        public IActivationFunction Activation {
            get {
                return m_hActivationFunc;
            }

            set {
                m_hActivationFunc = value;
                if (Activation == null)
                    m_vActivationTypeGuid = new Guid();
                else
                    m_vActivationTypeGuid = value.GetType().GUID;
            }
        }

        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public List<Sample> Samples { get; set; }

        public BasicNetwork GetNewNetwork()
        {
            BasicNetwork m_hNetwork = new BasicNetwork();
            if (this.InputSize > 0)
                m_hNetwork.AddLayer(new BasicLayer(this.Activation, true, this.InputSize));
            if (this.HL0Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(this.Activation, true, this.HL0Size));
            if (this.HL1Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(this.Activation, true, this.HL1Size));
            if (this.HL2Size > 0)
                m_hNetwork.AddLayer(new BasicLayer(this.Activation, true, this.HL2Size));
            if (this.OutputSize > 0)
                m_hNetwork.AddLayer(new BasicLayer(this.Activation, true, this.OutputSize));

            m_hNetwork.Structure.FinalizeStructure();
            m_hNetwork.Reset();

            return m_hNetwork;
        }

        public static byte[] Compress(NetworkCreationConfig hConfig)
        {
            using (MemoryStream serializationOutPut = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(serializationOutPut, hConfig);
                using (MemoryStream compressedOutPut = new MemoryStream())
                {
                    using (GZipStream gZipStream = new GZipStream(compressedOutPut, CompressionMode.Compress))
                    {
                        gZipStream.Write(serializationOutPut.GetBuffer(), 0, (int)serializationOutPut.Length);
                        byte[] hFullRes = compressedOutPut.GetBuffer();
                        byte[] hRes = new byte[compressedOutPut.Position];
                        Buffer.BlockCopy(hFullRes, 0, hRes, 0, hRes.Length);
                        return hRes;
                    }
                }
            }
        }

        public static NetworkCreationConfig Decompress(byte[] compressedArray)
        {
            byte[] byteArray = new byte[4096];
            using (MemoryStream hStream = new MemoryStream(byteArray))
            {
                using (GZipStream hGZipStream = new GZipStream(hStream, CompressionMode.Decompress))
                {
                    byteArray = new byte[byteArray.Length];
                    int rByte = hGZipStream.Read(byteArray, 0, byteArray.Length);

                    using (MemoryStream hOutPutStream = new MemoryStream(byteArray))
                    {
                        BinaryFormatter hFormatter = new BinaryFormatter();
                        return (NetworkCreationConfig) hFormatter.Deserialize(hOutPutStream);
                    }
                }
            }
        }

        public override string ToString() => $"[{Id}] {Name}";
    }
}
