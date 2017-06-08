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
    public class Stocazzo 
    {
        private const string FileName = "Settings.xml"; //asdasd
        public string TrainingServiceAddress { get; set; }
        public int TrainingServicePort { get; set; }

        public static Stocazzo Load()
        {
           
            //as asd as ss
            try
            {
                Stocazzo hRes;

                if (!File.Exists(FileName))
                {                    
                    using (Stream hFs = File.OpenWrite(FileName))
                    {
                        XmlSerializer hSerializer = new XmlSerializer(typeof(Stocazzo));
                        hSerializer.Serialize(hFs, new Stocazzo());
                    }
                }

                using (Stream hFs = File.OpenRead(FileName))
                {
                    XmlSerializer hSerializer = new XmlSerializer(typeof(Stocazzo));
                    hRes = hSerializer.Deserialize(hFs) as Stocazzo;
                }

                return hRes;
            }
            catch (Exception)
            {
                return new Stocazzo();
            }
        }

        public static void Save(Stocazzo hSettings)
        {
            try
            {
                if (!File.Exists(FileName))
                    File.Delete(FileName);

                using (Stream hFs = File.OpenWrite(FileName))
                {
                    XmlSerializer hSerializer = new XmlSerializer(typeof(Stocazzo));
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
