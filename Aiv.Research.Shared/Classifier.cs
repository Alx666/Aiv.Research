
﻿using Aiv.Research.Shared;
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
        static DirectoryInfo dirInfo;
        static List<DirectoryInfo> listDirInfo = new List<DirectoryInfo>();
        static FileInfo supportFile;
        static DirectoryInfo deleteDir;

        private static List<FileInfo> TrainingFiles = new List<FileInfo>();
        private static List<int> listCount = new List<int>();
        private static int counter=0;
        private static string nameFile = "Research";


        public static void SetDataPath(string sPath)
        {
            dirInfo = new DirectoryInfo(sPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create(); // funziona con un path copletamente sballato?
                deleteDir = dirInfo;
            }
        }

        public static void Store(NetworkCreationConfig hTrainedNetwork, byte[] hFile)
        {
            FileInfo[] files = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/", "*.zip").Select(f => new FileInfo(f)).ToArray();
            if ( files.Count() <= 0)
            {
                counter = 0;
            }
            else
            {
                Regex regex = new Regex("[0-9]*");
                counter = (from i in files.Select(f => new { Id = int.Parse(regex.Match(f.Name).ToString()), File = f })
                           orderby i.Id descending
                           select i).Select(x => x.Id).First() + 1;
            }

            using (FileStream hFs = File.OpenWrite(dirInfo.FullName + counter + "_" + hTrainedNetwork.Name))
            {
                XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));
                hSerializer.Serialize(hFs, hTrainedNetwork);

            }

            using (FileStream hFs = File.OpenWrite(dirInfo.FullName + counter + "_" + hTrainedNetwork.Name))
            {
                hFs.Write(hFile, 0, hFile.Length);
                hFs.Flush();

            }

            Zip("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/FolderFS/");

            supportFile.CopyTo("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/" + supportFile.Name);
            listDirInfo.ForEach(x => { x.Delete(true); });
        }

        private static void Zip(string destinationPath)
        {
            string path = dirInfo.FullName;
            if (destinationPath.Contains(path))
            {
                throw new Exception($"Non posso leggere e scrivere nello stesso percorso contemporaneamente. source = {path}, dest = {destinationPath}");
            }



            DirectoryInfo destination = new DirectoryInfo(destinationPath);
            

            if (!destination.Exists)
            {
                destination.Create();
            }
            
            ZipFile.CreateFromDirectory(dirInfo.FullName, destination.FullName + counter + "_" + nameFile + ".zip", CompressionLevel.Optimal, false);

            TrainingFiles.Add(destination.GetFiles().First());
            supportFile = destination.GetFiles().First();


            listDirInfo.Add(destination );
            deleteDir.Delete(true);

        }

        public static IEnumerable<NetworkCreationConfig> Enumerate()
        {
            IEnumerable<NetworkCreationConfig> netConfig = new NetworkCreationConfig() as IEnumerable<NetworkCreationConfig>;
            var target = (from u in netConfig orderby (u.Name) select u).ToList();
            return target;

            //return trainingfiles;
        }

        public static void Delete(string filename)
        {
            try
            {
                FileInfo hFile = dirInfo.GetFiles().Where(f => f.Name == filename).First();
                hFile.Delete();
            }
            catch (Exception)
            {
                throw new FileNotFoundException();
            }
        }

        public static byte[] Get(int iId)
        {
            FileInfo[] files = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/", "*.zip").Select(f => new FileInfo(f)).ToArray();

            Regex regex = new Regex("[0-9]*");
            FileInfo match = (from i in files.Select(f => new { Id = int.Parse(regex.Match(f.Name).ToString()), File = f })
                        where i.Id == iId select i.File).First();

            ZipFile.ExtractToDirectory(match.Name, "C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/");

            //FileInfo fileByte = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == sName).First();
            FileInfo fileByte = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() == iId.ToString()).First();

            byte[] dataByte = new byte[16*1024];

            using(FileStream fs = File.OpenRead(fileByte.Name))
            {
                fs.Read(dataByte, 0, (int)fs.Length);

            }
            return dataByte;
            // bisogna usare using(fs bla bla ), leggere tutti i byte e ritornare il byte[]
        }

        public static byte[] Get(string name)
        {
            FileInfo[] files = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/", "*.zip").Select(f => new FileInfo(f)).ToArray();

            FileInfo match = (from i in files where i.Name == name + "Research.zip" select i).First();

            ZipFile.ExtractToDirectory(match.Name, "C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/");

            //FileInfo fileByte = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == sName).First();
            FileInfo fileByte = Directory.GetFiles("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name).First();

            byte[] dataByte = new byte[16 * 1024];

            using (FileStream fs = File.OpenRead(fileByte.Name))
            {
                fs.Read(dataByte, 0, (int)fs.Length);

            }
            return dataByte;
        }
    }
}

