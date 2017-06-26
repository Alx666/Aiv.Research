
using Aiv.Research.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace Aiv.Research.Shared
{
    public static class Classifier
    {
        private static DirectoryInfo m_hDataPath;
        private static int m_iCounter = 0;
        private static List<NetworkCreationConfig> m_hTrainingFiles = new List<NetworkCreationConfig>();
        private static Regex m_hIdRegex = new Regex("[0-9]*");
        private static object m_hSyncRoot = new object();
        private static XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));


        public static void SetDataPath(string sPath)
        {
            lock (m_hSyncRoot)
            {
                m_hDataPath = new DirectoryInfo(sPath);

                if (!m_hDataPath.Exists)
                    m_hDataPath.Create();

                //prendo tutti gli xml dalla cartella di lavoro
                m_hTrainingFiles = m_hDataPath.GetFiles("*.xml").Select(x =>
                {
                    using (Stream hStream = x.OpenRead())
                    {
                        return hSerializer.Deserialize(hStream) as NetworkCreationConfig;
                    }

                }).ToList();

                //da questi prendo il numero + alto
                try
                {
                    m_iCounter = m_hTrainingFiles.OrderByDescending(x => x.Id).First().Id + 1;
                }
                catch (Exception)
                {
                    m_iCounter = 0;
                }
            }
        }

        public static IEnumerable<NetworkCreationConfig> Enumerate()
        {
            lock (m_hSyncRoot)
            {
                return new List<NetworkCreationConfig>(m_hTrainingFiles); //return a copy
            }
        }

        public static int GetId()
        {
            lock (m_hSyncRoot)
            {
                return m_iCounter++;
            }
        }

        public static byte[] Get(int iId)
        {
            lock (m_hSyncRoot)
            {
                //Prendi la config che ti interessa
                NetworkCreationConfig hConfig = m_hTrainingFiles.Where(x => x.Id == iId).Single();

                //Ricavi il nome del file associato
                string sFilename = $"{m_hDataPath}/{hConfig.Name}.zip"; //323RetePilotaTest.zip

                //Carico Il byte[]
                return File.ReadAllBytes(sFilename);
            }
        }

        public static byte[] Get(string name)
        {
            lock (m_hSyncRoot)
            {
                //Prendi la config che ti interessa
                NetworkCreationConfig hConfig = m_hTrainingFiles.Where(x => x.Name == name).Single();

                //Ricavi il nome del file associato
                string sFilename = $"{hConfig.Id}{hConfig.Name}.zip"; //323RetePilotaTest.zip

                //Carico Il byte[]
                return File.ReadAllBytes(sFilename);
            }
        }

        public static void Store(NetworkCreationConfig hTrainedNetwork, byte[] hFile)
        {
            lock (m_hSyncRoot)
            {

                // Zippo il file .dat
                using (ZipArchive zip = ZipFile.Open(m_hDataPath.FullName + $"/{hTrainedNetwork.Name}.zip", ZipArchiveMode.Create))
                {
                    using (FileStream hFs = File.OpenWrite(m_hDataPath.FullName + $"/{hTrainedNetwork.Name}.dat"))
                    {
                        hFs.Write(hFile, 0, hFile.Length);
                        hFs.Flush();
                    }

                    zip.CreateEntryFromFile(m_hDataPath.FullName + $"/{hTrainedNetwork.Name}.dat", $"{hTrainedNetwork.Name}.dat");
                }

                //Salvo il file xml
                using (FileStream hFs = File.OpenWrite(m_hDataPath.FullName + $"/{hTrainedNetwork.Name}.xml"))
                {
                    XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));
                    hSerializer.Serialize(hFs, hTrainedNetwork);
                }

                m_hDataPath.GetFiles($"{hTrainedNetwork.Name}.dat").Select(x => x).First().Delete();

                // Aggiungo il file .xml alla lista di file che si trovano dentro alla cartella settata
                m_hTrainingFiles.Add(m_hDataPath.GetFiles($"{hTrainedNetwork.Name}.xml").Select(x =>
                {
                    using (Stream hStream = x.OpenRead())
                    {
                        return hSerializer.Deserialize(hStream) as NetworkCreationConfig;
                    }

                }).First());
            }
        }
    }
}

