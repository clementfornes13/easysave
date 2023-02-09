using System;
using System.Diagnostics;
using System.Resources;
using System.IO;

namespace EasySave
{
    class TransfertStatesItems
    {
        ResourceManager rm = new ResourceManager("EasySave.Resources.Langue", typeof(EasySave).Assembly);
        private SaveFiles m_files;
        private bool m_actualStates = false; //Active/Waiting
        private double m_elapsedTransfertTime;
        private int m_nbFiles, m_nbFilesMoved;

        public TransfertStatesItems(SaveFiles files)
        {
            m_files = files;
            m_nbFiles = files.Files.Count;
        }

        public void BackUp()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timed for the log file

            if (!System.IO.Directory.Exists(m_files.PathTo))
            {
                System.IO.Directory.CreateDirectory(m_files.PathTo);
            }

            m_actualStates = true;
            foreach (FileInfo file in m_files.Files)
            {
                string targetFile = System.IO.Path.Combine(m_files.PathTo, file.Name);

                try
                {
                    file.CopyTo(targetFile);
                }
                catch (Exception e) { Console.Error.Write(e.ToString()); }

                m_elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                m_nbFilesMoved++;
            }
            stopwatch.Stop();
            m_actualStates = false;
            Loggin();
        }

        public void Loggin()
        {
            string m_nameLog = System.IO.Path.GetFileName(m_files.PathFrom);
            string totalSizeFileLog = m_files.TotalSizeFile.ToString();
            string m_elapsedTransfertTimeLog = m_elapsedTransfertTime.ToString();

            LogsFile myLog = new LogsFile();
            //Log Json
            myLog.WriteLogJson(m_nameLog, m_files.PathFrom, m_files.PathTo, totalSizeFileLog, m_elapsedTransfertTimeLog);
            // Log CSV
            myLog.WriteLog(m_nameLog, m_files.PathFrom, m_files.PathTo, totalSizeFileLog, m_elapsedTransfertTimeLog);
        }

        public bool ActualStates { get => m_actualStates; set => m_actualStates = value; }
        public double ElapsedTransfertTime { get => m_elapsedTransfertTime; }
        internal SaveFiles File { get => m_files; }
        public int NbFiles { get => m_nbFiles; set => m_nbFiles = value; }
        public int NbFilesMoved { get => m_nbFilesMoved; set => m_nbFilesMoved = value; }
    }
}
