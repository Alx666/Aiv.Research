
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
        static DirectoryInfo deleteDir;

        private static List<FileInfo> TrainingFiles = new List<FileInfo>();
        private static int counter = 0;
        private static string nameFile = "Research";
        private static string trainingName;


        public static void SetDataPath(string sPath)
        {
            dirInfo = new DirectoryInfo(sPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create(); // funziona con un path copletamente sballato?
                deleteDir = dirInfo;
            }


            try
            {
                Regex regex = new Regex("[0-9]*");
                counter = (from i in TrainingFiles.Select(f => new { Id = int.Parse(regex.Match(f.Name).ToString()), File = f })
                           orderby i.Id descending
                           select i).Select(x => x.Id).First() + 1;
            }
            catch (Exception)
            {
                counter = 0;
            }
        }

        public static void Store(NetworkCreationConfig hTrainedNetwork, byte[] hFile)
        {
            trainingName = hTrainedNetwork.Name;
            using (FileStream hFs = File.OpenWrite(dirInfo.FullName + "/" + counter + "_" + dirInfo.Name +  "_" + hTrainedNetwork.Name + ".xml"))
            {
                XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));
                hSerializer.Serialize(hFs, hTrainedNetwork);
            }



            using (FileStream hFs = File.OpenWrite(dirInfo.FullName + "/" + counter + "_" + dirInfo.Name +  "_" + hTrainedNetwork.Name + ".dat"))
            {
                hFs.Write(hFile, 0, hFile.Length);
                hFs.Flush();
            }

            TrainingFiles = dirInfo.GetFiles().ToList();

            Zip("FolderFS/");
        }

        private static void Zip(string destinationPath)
        {
            string path = dirInfo.FullName;
            if (destinationPath.Contains(path))
            {
                throw new Exception($"Non posso leggere e scrivere nello stesso percorso contemporaneamente. source = {path}, dest = {destinationPath}");
            }
            
            ZipFile.CreateFromDirectory(dirInfo.FullName, counter + "_" + dirInfo.Name +  "_" + nameFile + ".zip", CompressionLevel.Optimal, false);


            deleteDir.Delete(true);
        }

        public static IEnumerable<NetworkCreationConfig> Enumerate()
        {
            IEnumerable<NetworkCreationConfig> netConfig = new NetworkCreationConfig() as IEnumerable<NetworkCreationConfig>;
            var target = (from u in netConfig orderby (u.Name) select u).ToList();
            return target;

            //return trainingfiles;
        }

        private static void Delete(string filename)
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
            FileInfo[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip").Select(f => new FileInfo(f)).ToArray();
            
            Regex regex = new Regex("[0-9]*");
            FileInfo match = (from i in files.Select(f => new { Id = int.Parse(regex.Match(f.Name).ToString()), File = f })
                        where i.Id == iId select i.File).First();

            ZipFile.ExtractToDirectory(match.Name, Directory.GetCurrentDirectory());

            #region conditionCapacity
            FileInfo fileByte = null;

            if (iId < 10)
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() == iId.ToString()).First();
            }

            if (iId < 100 && iId >= 10)
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() + n.Name[1].ToString() == iId.ToString()).First();
            }

            if (iId < 1000 && iId >= 100)
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() + n.Name[1].ToString() + n.Name[2].ToString() == iId.ToString()).First();
            }

            if (iId < 10000 && iId >= 1000)
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() + n.Name[1].ToString() + n.Name[2].ToString() + n.Name[3].ToString() == iId.ToString()).First();
            }

            if (iId < 100000 && iId >= 10000)
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() + n.Name[1].ToString() + n.Name[2].ToString() + n.Name[3].ToString() + n.Name[4].ToString() == iId.ToString()).First();
            }

            if (iId < 1000000 && iId >= 100000)
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name[0].ToString() + n.Name[1].ToString() + n.Name[2].ToString() + n.Name[3].ToString() + n.Name[4].ToString() + n.Name[5].ToString() == iId.ToString()).First();
            }
#endregion

            byte[] dataByte = new byte[16*1024];

            using(FileStream fs = File.OpenRead(fileByte.Name))
            {
                fs.Read(dataByte, 0, (int)fs.Length);
            }

            DirectoryInfo s = new DirectoryInfo(Directory.GetCurrentDirectory());

            s.GetFiles().Where(f => f.Name == counter + "_" + dirInfo.Name +  "_" + trainingName + ".dat").First().Delete();
            s.GetFiles().Where(f => f.Name == counter + "_" + dirInfo.Name + "_" + trainingName + ".xml").First().Delete();

            for (int i = 0; i < dataByte.Length; i++)
            {
                Console.WriteLine(dataByte.GetValue(i));
            }
            Console.ReadLine();
            return dataByte;
        }

        public static byte[] Get(string name)
        {
            FileInfo[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip").Select(f => new FileInfo(f)).ToArray();

            FileInfo match = (from i in files where i.Name == name + "Research.zip" select i).First();

            ZipFile.ExtractToDirectory(match.Name, Directory.GetCurrentDirectory());

            #region conditionCapacity
            FileInfo fileByte = null;
            int x = int.Parse(name[0].ToString());
            if (int.Parse(name[0].ToString()) < 10 && name[1].ToString() == "_")
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name + name[0] + ".dat").First();
            }else

            if (int.Parse(name[0].ToString() + name[1].ToString()) < 100 && name[2].ToString() == "_")
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name + name[0] + name[1] + ".dat").First();
            }else

            if (int.Parse(name[0].ToString() + name[1].ToString() + name[2].ToString()) < 1000 && name[3].ToString() == "_")
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name + name[0] + name[1] + name[2] + ".dat").First();
            }else

            if (int.Parse(name[0].ToString() + name[1].ToString() + name[2].ToString() + name[3].ToString()) < 10000 && name[4].ToString() == "_")
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name + name[0] + name[1] + name[2] + name[3] + ".dat").First();
            }else

            if (int.Parse(name[0].ToString() + name[1].ToString() + name[2].ToString() + name[3].ToString() + name[4].ToString()) < 100000 && name[5].ToString() == "_")
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name + name[0] + name[1] + name[2] + name[3] + name[4] + ".dat").First();
            }else

            if (int.Parse(name[0].ToString() + name[1].ToString() + name[2].ToString() + name[3].ToString() + name[4].ToString() + name[5].ToString()) < 1000000 && name[6].ToString() == "_")
            {
                fileByte = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat").Select(f => new FileInfo(f)).ToArray().Where(n => n.Name == name + name[0] + name[1] + name[2] + name[3] + name[4] + name[5] + ".dat").First();
            }
            #endregion

            byte[] dataByte = new byte[16 * 1024];

            using (FileStream fs = File.OpenRead(fileByte.Name))
            {
                fs.Read(dataByte, 0, (int)fs.Length);
            }

            DirectoryInfo s = new DirectoryInfo(Directory.GetCurrentDirectory());

            s.GetFiles().Where(f => f.Name == counter + "_" + dirInfo.Name + "_" + trainingName + ".dat").First().Delete();
            s.GetFiles().Where(f => f.Name == counter + "_" + dirInfo.Name + "_" + trainingName + ".xml").First().Delete();

            return dataByte;
        }
    }
}

