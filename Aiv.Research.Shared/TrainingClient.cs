using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using Encog.Engine.Network.Activation;
using System.Xml;
using System.Xml.Serialization;

namespace Aiv.Research.Shared
{
    public class TrainingClient
    {
        public const string TRAINING_SERVICE_NAME = "Training";
        private ChannelFactory<ITrainingService> trainingServiceFactory;
        private NetTcpBinding tcpBinding;
        private ITrainingService serviceInstance;
        public TrainingClient()
        {
            tcpBinding = new NetTcpBinding(SecurityMode.None, true);
            trainingServiceFactory = new ChannelFactory<ITrainingService>(tcpBinding);
        }

        [ConsoleUIMethod]
        public void Connect(string address, int port)
        {
            serviceInstance = trainingServiceFactory.CreateChannel(new EndpointAddress($"net.tcp://{address}:{port}/{TRAINING_SERVICE_NAME}"));
        }

        [ConsoleUIMethod]
        public string PrintCurrentPath()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).ToString();
        }

        [ConsoleUIMethod]
        public void Disconnect()
        {
            // ... do something...
        }

        [ConsoleUIMethod]
        public void StartTrainingVerbose(string networkConfigPath)
        {
            if (!networkConfigPath.EndsWith(".xml"))
                throw new Exception($"Input error: { networkConfigPath } is not an xml.");

            using (FileStream configFileStream = File.OpenRead(networkConfigPath))
            {
                if (configFileStream == null)
                    throw new Exception($"Path: { networkConfigPath } means nothing to me.");

                XmlSerializer serializer = new XmlSerializer(typeof(NetworkCreationConfig));
                NetworkCreationConfig config = (NetworkCreationConfig)serializer.Deserialize(configFileStream);
                if (config == null)
                    throw new Exception($"The deserialization output is not a valid XML.");

                serviceInstance.StartTraining(config);
            }
        }
    }
}
