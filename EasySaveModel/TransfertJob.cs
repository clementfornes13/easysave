using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EasySaveModel
{
    public class TransfertJob
    {
        private static SaveFiles _files;
        private static bool _actualStates = false; //Active/Waiting
        private static double _elapsedTransfertTime;
        private static uint _nbFiles, _nbFilesMoved;
        private static Mutex countMutex = new Mutex();

        private Thread _mainThread = null;

        public TransfertJob(SaveFiles files)
        {
            _files = files;
            _nbFiles = (uint)files.Files.Count;
            foreach (DirectoryInfo dir in _files.SubDirs)
            {
                _nbFiles += (uint)dir.GetFiles().Length;
            }
        }

        ~TransfertJob()
        {
            if (_mainThread != null && _mainThread.IsAlive)
            {
                _mainThread.Join();
            }
        }

        public void ThreadBackUp()
        {
            if (_mainThread != null)
            {
                throw new Exception($"Back up Thread of {_files.Name} already alive");
            }
            else
            {
                _mainThread = new Thread(BackUp);
                Debug.WriteLine("Launch backup");
                _mainThread.Start();
            }
        }

        public void ThreadBackUpDiff()
        {
            if (_mainThread != null)
            {
                throw new Exception($"Back up Thread of {_files.Name} already alive");
            }
            else
            {
                _mainThread = new Thread(BackUpDiff);
                Debug.WriteLine("Launch backupdiff");
                _mainThread.Start();
            }
        }

        //Make a fill copy
        public static void BackUp()
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

                countMutex.WaitOne();
                _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                _nbFilesMoved++;
                Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles}");
                countMutex.ReleaseMutex();
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
                foreach (FileInfo subfile in subFiles)
                {
                    string targetFile = Path.Combine(targetdir, subfile.Name);
                    Console.WriteLine($"File {subfile.Name} written");
                    try
                    {
                        if (!File.Exists(targetFile))
                        {
                            subfile.CopyTo(targetFile);
                        }
                    }
                    catch (Exception e) { Console.Error.Write(e.ToString()); }

                    countMutex.WaitOne();
                    _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                    _nbFilesMoved++;
                    Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles}");
                    countMutex.ReleaseMutex();
                }
            }
            stopwatch.Stop();
            _actualStates = false;
            Loggin();
        }

        //Make a fill copy
        public static void BackUpDiff()
        {
            //Make state file
            //Start a chrono ofr mesuring time elaspsed
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timed for the log file


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
                    else
                    {
                        var lastwrite = File.GetLastAccessTimeUtc(targetFile);
                        if (lastwrite != file.LastAccessTimeUtc)
                        {
                            file.CopyTo(targetFile, true);
                        }
                    }
                }
                catch (Exception e) { Debug.WriteLine(e.ToString()); }

                countMutex.WaitOne();
                _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                _nbFilesMoved++;
                Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles}");
                countMutex.ReleaseMutex();
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
                        else
                        {
                            var lastwrite = File.GetLastAccessTimeUtc(targetFile);
                            if (lastwrite != file.LastAccessTimeUtc)
                            {
                                file.CopyTo(targetFile, true);
                            }
                        }
                    }
                    catch (Exception e) { Debug.WriteLine(e.ToString()); }

                    countMutex.WaitOne();
                    _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                    _nbFilesMoved++;
                    Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles}");
                    countMutex.ReleaseMutex();
                }
            }
            stopwatch.Stop();
            _actualStates = false;
            Loggin();
        }

        public static void Loggin()
        {
            string nameLog = Path.GetFileName(_files.PathFrom);
            string totalSizeFileStr = _files.TotalSizeFile.ToString();
            string elapsedTransfertTimeStr = _elapsedTransfertTime.ToString();

            LogsFile JSONmyLogs = LogsFile.GetInstance(true);
            LogsFile XMLmyLogs = LogsFile.GetInstance(false);
            Mutex JSONMutex = LogsFile.GetMutex(true);
            Mutex XMLMutex = LogsFile.GetMutex(false);

            JSONMutex.WaitOne();
            JSONmyLogs.WriteLog(nameLog, _files.PathFrom, _files.PathTo, totalSizeFileStr, elapsedTransfertTimeStr);
            JSONMutex.ReleaseMutex();

            XMLMutex.WaitOne();
            XMLmyLogs.WriteLog(nameLog, _files.PathFrom, _files.PathTo, totalSizeFileStr, elapsedTransfertTimeStr);
            XMLMutex.ReleaseMutex();
        }

        public uint CalcProgress()
        {
            return (_nbFilesMoved / _nbFiles) * 100;
        }

        public bool ActualStates { get => _actualStates; set => _actualStates = value; }
        public double ElapsedTransfertTime { get => _elapsedTransfertTime; }
        internal SaveFiles workingFile { get => _files; }
        public uint NbFiles { get => _nbFiles; }
        public uint NbFilesMoved { get => _nbFilesMoved; set => _nbFilesMoved = value; }
    }
}
