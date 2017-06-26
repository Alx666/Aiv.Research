using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Aiv.Research.Visualizer2D
{
    [Serializable]
    public class Settings 
    {
        private const string FileName = "Settings.xml"; //asdasd
        public string TrainingServiceAddress { get; set; }
        public int TrainingServicePort { get; set; }

        public static Settings Load()
        {
           
            //as asd as ss
            try
            {
                Settings hRes;

                if (!File.Exists(FileName))
                {                    
                    using (Stream hFs = File.OpenWrite(FileName))
                    {
                        XmlSerializer hSerializer = new XmlSerializer(typeof(Settings));
                        hSerializer.Serialize(hFs, new Settings());
                    }
                }

                using (Stream hFs = File.OpenRead(FileName))
                {
                    XmlSerializer hSerializer = new XmlSerializer(typeof(Settings));
                    hRes = hSerializer.Deserialize(hFs) as Settings;
                }

                return hRes;
            }
            catch (Exception)
            {
                return new Settings();
            }
        }

        public static void Save(Settings hSettings)
        {
            try
            {
                if (!File.Exists(FileName))
                    File.Delete(FileName);

                using (Stream hFs = File.OpenWrite(FileName))
                {
                    XmlSerializer hSerializer = new XmlSerializer(typeof(Settings));
                    hSerializer.Serialize(hFs, hSettings);
                }
            }
            catch (Exception)
            {
                //if problem dont save settings
            }
        }
    }
}
