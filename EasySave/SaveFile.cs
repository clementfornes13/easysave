using System;
using System.IO;

namespace EasySave
{

    class SaveFiles
    {
        private string[] m_names; //file.co
        private string m_pathFrom, m_pathTo; //C:/dir/dir/dir
        private long totalSizeFile = 0;

        public SaveFiles(string pathFrom)
        {
            m_pathFrom = pathFrom;
            //Initialise un repertoire pour la backup si non-spécifié par l'utilisateur.
            //Utilise l'ID du thread comme clef unique du job
            m_pathTo = System.Environment.CurrentDirectory + @"\backUps\" + System.Threading.Thread.CurrentThread.ManagedThreadId;

            init();
            calcSizeFiles();
        }
        public SaveFiles(string pathFrom, string pathTo)
        {
            m_pathFrom = pathFrom;
            m_pathTo = pathTo;

            init();
            calcSizeFiles();
        }

        private void init()
        {
            //Need to make a feature for subdirectory
            m_names = System.IO.Directory.GetFiles(m_pathFrom);
            if (m_names.Length == 0)
            {
                throw new DirectoryNotFoundException("ERROR 404 : Directory Not Found ! " + m_pathFrom);
            }
            
        }
        ~SaveFiles()
        {
            //File.close();
        }

        public void calcSizeFiles()
        {
            FileInfo fileData;
            foreach (string name in m_names)
            {
                fileData = new FileInfo(System.IO.Path.Combine(m_pathFrom, name));
                totalSizeFile += fileData.Length;
            }
            Logging();
        }
        public void Logging()
        {
            
            string m_nameLog = System.IO.Path.GetFileName(m_pathFrom);
            string totalSizeFileLog = totalSizeFile.ToString();
            string transferTime = "0";

            LogsFile myLog = new LogsFile();
            myLog.WriteLogJson(m_nameLog, m_pathFrom, m_pathTo, totalSizeFileLog,  transferTime);
        }

        public string[] Names { get => m_names; }
        public string PathFrom { get => m_pathFrom; set => m_pathFrom = value; }
        public string PathTo { get => m_pathTo; set => m_pathTo = value; }
        public long TotalSizeFile { get => totalSizeFile;}
    }


}
