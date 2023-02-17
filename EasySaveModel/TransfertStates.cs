using System;
using System.Diagnostics;
using System.IO;

namespace EasySaveModel
{
    public class TransfertStatesItems
    {
        private SaveFiles _files;
        private bool _actualStates = false; //Active/Waiting
        private double _elapsedTransfertTime;
        private int _nbFiles, _nbFilesMoved;

        public TransfertStatesItems(SaveFiles files)
        {
            _files = files;
            _nbFiles = files.Files.Count;
        }

        //Threading backup
        //new mutex
        //if complete
        //if sequentiel
        //New the

        
        //Make a fill copy
        public void BackUp()
        {
            //Make state file
            //Start a chrono ofr mesuring time elaspsed
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timed for the log file

            //Create main directory
            if (!Directory.Exists(_files.PathTo))
            {
                Directory.CreateDirectory(_files.PathTo);
            }

            //Move Files
            _actualStates = true;
            foreach (FileInfo file in _files.Files)
            {
                string targetFile = Path.Combine(_files.PathTo, file.Name);

                try
                {
                    if (!File.Exists(targetFile))
                    {
                        file.CopyTo(targetFile);
                    }
                }
                catch (Exception e) { Console.Error.Write(e.ToString()); }

                _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                _nbFilesMoved++;

            }

            //Manage sub dir for copy
            foreach (DirectoryInfo dir in _files.SubDirs)
            {
                string targetdir = Path.Combine(_files.PathTo, dir.Name);
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

                    _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                    _nbFilesMoved++;

                }
            }
            stopwatch.Stop();
            _actualStates = false;
            //prendre Mutex
            Loggin();
            //Rendre mutex
        }

        public void Loggin()
        {
            string m_nameLog = Path.GetFileName(_files.PathFrom);
            string totalSizeFileLog = _files.TotalSizeFile.ToString();
            string m_elapsedTransfertTimeLog = _elapsedTransfertTime.ToString();
            
            LogsFile JSONmyLogs = LogsFile.GetInstance(true);
            LogsFile XMLmyLogs = LogsFile.GetInstance(true);

            JSONmyLogs.WriteLog(m_nameLog, _files.PathFrom, _files.PathTo, totalSizeFileLog, m_elapsedTransfertTimeLog);
            XMLmyLogs.WriteLog(m_nameLog, _files.PathFrom, _files.PathTo, totalSizeFileLog, m_elapsedTransfertTimeLog);
        }

        public bool ActualStates { get => _actualStates; set => _actualStates = value; }
        public double ElapsedTransfertTime { get => _elapsedTransfertTime; }
        internal SaveFiles workingFile { get => _files; }
        public int NbFiles { get => _nbFiles; set => _nbFiles = value; }
        public int NbFilesMoved { get => _nbFilesMoved; set => _nbFilesMoved = value; }
    }
}
