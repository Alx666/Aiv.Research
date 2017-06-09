
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



        private static string trainingName;
        private static DirectoryInfo dInfo = new DirectoryInfo(Directory.GetCurrentDirectory());


        public static void SetDataPath(string sPath)
        {
            lock (m_hSyncRoot)
            {
                m_hDataPath = new DirectoryInfo(sPath);

                if (!m_hDataPath.Exists)
                    m_hDataPath.Create();

                //prendo tutti gli xml dalla cartella di lavoro
                XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));

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
                string sFilename = $"{hConfig.Id}{hConfig.Name}.zip"; //323RetePilotaTest.zip

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




        //public static void Store(NetworkCreationConfig hTrainedNetwork, byte[] hFile)
        //{
        //    lock (SyncRoot)
        //    {

        //        DirectoryInfo Fs = new DirectoryInfo(".\\FolderFs\\");

        //        if (!Fs.Exists)
        //        {
        //            Fs.Create();
        //        }

        //        trainingName = hTrainedNetwork.Name;
        //        using (FileStream hFs = File.OpenWrite(Fs.FullName + "/" + m_iCounter + "_" + m_hDataPath.Name + "_" + hTrainedNetwork.Name + ".xml"))
        //        {
        //            XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));
        //            hSerializer.Serialize(hFs, hTrainedNetwork);
        //        }

        //        using (FileStream hFs = File.OpenWrite(Fs.FullName + "/" + m_iCounter + "_" + m_hDataPath.Name + "_" + hTrainedNetwork.Name + ".dat"))
        //        {
        //            hFs.Write(hFile, 0, hFile.Length);
        //            hFs.Flush();
        //        }

        //        m_hTrainingFiles = Fs.GetFiles().ToList();

        //        Zip(Fs);



        //    }
        //}

        //private static void Zip(DirectoryInfo dFs)
        //{
        //    lock (SyncRoot)
        //    {
        //        string path = m_hDataPath.FullName;

        //        ZipFile.CreateFromDirectory(dFs.FullName, m_iCounter + "_" + m_hDataPath.Name + "_" + nameFile + ".zip", CompressionLevel.Optimal, false);

        //        dFs.Delete(true);
        //    }
        //}



        //private static void Delete(string filename)
        //{
        //    //lock (SyncRoot)
        //    //{
        //    //    try
        //    //    {
        //    //        FileInfo hFile = m_hDataPath.GetFiles().Where(f => f.Name == filename).First();
        //    //        hFile.Delete();
        //    //    }
        //    //    catch (Exception)
        //    //    {
        //    //        throw new FileNotFoundException();
        //    //    }
        //    //}
        //}

    }
}

