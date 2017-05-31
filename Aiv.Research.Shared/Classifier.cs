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
        static DirectoryInfo dirInfo; // cartella
        static List<DirectoryInfo> listDirInfo = new List<DirectoryInfo>();
        static FileInfo supportFile;
        static DirectoryInfo deleteDir;
        private static List<FileInfo> TrainingFiles = new List<FileInfo>();
        private static List<int> listCount = new List<int>();
        private static int counter=0;
        private static string nameFile = "Research";


       

        //Stabilisce dove verranno create le cartelle con i dati
        //Se non ci sta la crea
        public static void SetDataPath(string sPath)
        {
            dirInfo = new DirectoryInfo(sPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create(); // funziona con un path copletamente sballato?
                deleteDir = dirInfo;
            }

           
            //3( se scegliamo un path esistente,( non deve crearlo) caruca tutti i file info nella lista
            //trainingfiles= directory.getfiles(a.fullname) select( f=> bew fileinfo(f)).tolist();
        }

        //salvare i file in qualche formato
        //file info= 
        // qui usate lo gzip streeam per impacchettare tutti i dati
        // name= nome del file
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


            //using (StreamWriter writer = new StreamWriter(dirInfo.FullName + counter + "_" + hTrainedNetwork.Name))
            //{
            //    writer.WriteLine(hTrainedNetwork);
            //    writer.Flush();
            //    writer.Close();
            //}

            using (FileStream hFs = File.OpenWrite("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/FolderFS"))
            {
                XmlSerializer hSerializer = new XmlSerializer(typeof(NetworkCreationConfig));
                hSerializer.Serialize(hFs, hTrainedNetwork);

            }

            //Su disco ora c'e' l'xml con la config

            using (FileStream hFs = File.OpenWrite("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/FolderFS"))
            {
                hFs.Write(hFile, 0, hFile.Length);
                hFs.Flush();

            }


            //abbiamo i 2 file su disco

            //zip
            Zip("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/FolderFS/");

            supportFile.CopyTo("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/" + supportFile.Name);
            listDirInfo.ForEach(x => { x.Delete(true); });

            //TrainingFiles.Select(x => x.CopyTo("C:/Users/marke/Desktop/Aiv.Research/Aiv.Research.TrainingServer/bin/Debug/"));





            //TrainingFiles.Add(...)
        }

        private static void Zip(string destinationPath)
        {
            string path = dirInfo.FullName;
            if (destinationPath.Contains(path))
            {
                throw new Exception($"Non posso leggere e scrivere nello stesso percorso contemporaneamente. source = {path}, dest = {destinationPath}");
            }

            DirectoryInfo dest = new DirectoryInfo(destinationPath);

            if (!dest.Exists)
            {
                dest.Create();
            }
            
            ZipFile.CreateFromDirectory(dirInfo.FullName, dest.FullName + counter + "_" + nameFile + ".zip", CompressionLevel.Optimal, false);

            TrainingFiles.Add(dest.GetFiles().First());
            supportFile = dest.GetFiles().First();


            listDirInfo.Add(dest);
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

        private static void DeleteFolder(string folderName)
        {

        }


        public static byte[] Get(int iId)
        {


            return null;

        }

        public static byte[] Get(string name)
        {


            return null;

        }
    }
}
