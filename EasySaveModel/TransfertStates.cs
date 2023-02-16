using System;
using System.Diagnostics;
using System.Resources;
using System.IO;

namespace EasySaveModel
{
    public class TransfertStatesItems
    {
        private SaveFiles m_files;
        private bool m_actualStates = false; //Active/Waiting
        private double m_elapsedTransfertTime;
        private int m_nbFiles, m_nbFilesMoved;

        public TransfertStatesItems(SaveFiles files)
        {
            m_files = files;
            m_nbFiles = files.Files.Count;
        }

        //Make a fill copy
        public void BackUp()
        {
            //Make state file
            //Start a chrono ofr mesuring time elaspsed
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timed for the log file

            //Create main directory
            if (!Directory.Exists(m_files.PathTo))
            {
                Directory.CreateDirectory(m_files.PathTo);
            }

            //Move Files
            m_actualStates = true;
            foreach (FileInfo file in m_files.Files)
            {
                string targetFile = Path.Combine(m_files.PathTo, file.Name);

                try
                {
                    if (!File.Exists(targetFile))
                    {
                        file.CopyTo(targetFile);
                    }
                }
                catch (Exception e) { Console.Error.Write(e.ToString()); }

                m_elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                m_nbFilesMoved++;

            }

            //Manage sub dir for copy
            foreach (DirectoryInfo dir in m_files.SubDirs)
            {
                string targetdir = Path.Combine(m_files.PathTo, dir.Name);
                if (!Directory.Exists(targetdir))
                {
                    Directory.CreateDirectory(targetdir);
                }

                FileInfo[] subFiles = dir.GetFiles();
                Console.WriteLine($"Found {subFiles.Length} files in the {dir.Name} subdir");
                foreach (FileInfo file in subFiles)
                {
                    string targetFile = Path.Combine(targetdir, file.Name);
                    Console.WriteLine($"File {file.Name} written");
                    try
                    {
                        if (!File.Exists(targetFile))
                        {
                            file.CopyTo(targetFile);
                        }
                    }
                    catch (Exception e) { Console.Error.Write(e.ToString()); }

                    m_elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                    m_nbFilesMoved++;

                }
            }
            stopwatch.Stop();
            m_actualStates = false;
            Loggin();
        }

        public void Loggin()
        {
            string m_nameLog = Path.GetFileName(m_files.PathFrom);
            string totalSizeFileLog = m_files.TotalSizeFile.ToString();
            string m_elapsedTransfertTimeLog = m_elapsedTransfertTime.ToString();
            
            LogsFile JSONmyLogs = LogsFile.GetInstance(true);
            LogsFile XMLmyLogs = LogsFile.GetInstance(true);

            JSONmyLogs.WriteLog(m_nameLog, m_files.PathFrom, m_files.PathTo, totalSizeFileLog, m_elapsedTransfertTimeLog);
            XMLmyLogs.WriteLog(m_nameLog, m_files.PathFrom, m_files.PathTo, totalSizeFileLog, m_elapsedTransfertTimeLog);
        }

        public bool ActualStates { get => m_actualStates; set => m_actualStates = value; }
        public double ElapsedTransfertTime { get => m_elapsedTransfertTime; }
        internal SaveFiles workingFile { get => m_files; }
        public int NbFiles { get => m_nbFiles; set => m_nbFiles = value; }
        public int NbFilesMoved { get => m_nbFilesMoved; set => m_nbFilesMoved = value; }
    }
}
