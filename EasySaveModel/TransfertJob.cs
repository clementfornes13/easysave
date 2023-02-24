using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace EasySaveModel
{
    public class TransfertJob
    {
        private SaveFiles _files;
        private bool _actualStates = false; //Active/Waiting
        private double _elapsedTransfertTime = 0, _elapsedCrytoTime = 0;
        private double _nbFiles, _nbFilesMoved;
        private uint _maxSizeFile = 999999999;

        private bool _activecrypto = false;

        private List<string> _prioritizeExts;

        private static Mutex _countMutex = new Mutex();
        private static Mutex _pauseMutex;
        private static Mutex _bigFileMutex = new Mutex();
        private Thread _mainThread = null;

        private string _cryptoSoftPath;

        public List<string> _extensionList = new List<string>();


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

        public void ThreadBackUp(bool diff)
        {
            try
            {
                if (_mainThread != null)
                {
                    throw new Exception($"Back up Thread of {_files.Name} already alive");
                }
                else
                {
                    if (diff)
                    {
                        _mainThread = new Thread(BackUpDiff);
                        Debug.WriteLine("Launch backupdiff");
                        _mainThread.Start();
                    }
                    else
                    {
                        _mainThread = new Thread(BackUp);
                        Debug.WriteLine("Launch backup");
                        _mainThread.Start();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

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

            //Move classic Files
            _actualStates = true;
            foreach (FileInfo file in _files.Files)
            {
                _pauseMutex.WaitOne();
                string targetFile = Path.Combine(_files.PathTo, file.Name);

                try
                {
                    if (!System.IO.File.Exists(targetFile))
                    {
                        if (file.Length >= MaxSizeFile)
                        {
                            TransfertJob.BigFileMutex.WaitOne();
                            file.CopyTo(targetFile);
                            if (_activecrypto)
                            {
                                Stopwatch cryptoWatch = new Stopwatch();
                                cryptoWatch.Start();
                                Encrypt(targetFile, targetFile, _cryptoSoftPath, _extensionList);
                                cryptoWatch.Stop();
                                _elapsedCrytoTime += cryptoWatch.Elapsed.TotalSeconds;
                            }
                            TransfertJob.BigFileMutex.ReleaseMutex();
                        }
                        else
                        {
                            TransfertJob.BigFileMutex.WaitOne();
                            TransfertJob.BigFileMutex.ReleaseMutex();
                            file.CopyTo(targetFile);
                            if (_activecrypto)
                            {
                                Stopwatch cryptoWatch = new Stopwatch();
                                cryptoWatch.Start();
                                Encrypt(targetFile, targetFile, _cryptoSoftPath, _extensionList);
                                cryptoWatch.Stop();
                                _elapsedCrytoTime += cryptoWatch.Elapsed.TotalSeconds;
                            }
                        }
                    }
                }
                catch (Exception e) { Debug.WriteLine(e.ToString()); }

                TransfertJob.CountMutex.WaitOne();
                _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                _nbFilesMoved++;
                _files.Progress = _nbFilesMoved / _nbFiles * 100;
                Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles} of Main - {_files.Progress}");
                TransfertJob.CountMutex.ReleaseMutex();

                TransfertJob.PauseMutex.ReleaseMutex();
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
                    _pauseMutex.WaitOne();
                    string targetFile = Path.Combine(targetdir, subfile.Name);
                    try
                    {
                        if (subfile.Length >= MaxSizeFile)
                        {
                            TransfertJob.BigFileMutex.WaitOne();
                            subfile.CopyTo(targetFile);
                            TransfertJob.BigFileMutex.ReleaseMutex();
                        }
                        else
                        {
                            TransfertJob.BigFileMutex.WaitOne();
                            TransfertJob.BigFileMutex.ReleaseMutex();
                            subfile.CopyTo(targetFile);
                        }
                    }
                    catch (Exception e) { Debug.WriteLine(e.ToString()); }

                    TransfertJob.CountMutex.WaitOne();
                    _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                    _nbFilesMoved++;
                    _files.Progress = _nbFilesMoved / _nbFiles * 100;
                    Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles} of Main - {_files.Progress}");
                    TransfertJob.CountMutex.ReleaseMutex();

                    TransfertJob.PauseMutex.ReleaseMutex();
                }
            }
            stopwatch.Stop();
            _actualStates = false;
            Loggin();
        }

        //Make a fill copy
        public void BackUpDiff()
        {
            //Make state file
            //Start a chrono ofr mesuring time elaspsed
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timed for the log file

            //Move Files
            _actualStates = true;
            foreach (FileInfo file in _files.Files)
            {
                _pauseMutex.WaitOne();
                string targetFile = Path.Combine(_files.PathTo, file.Name);

                try
                {
                    if (!System.IO.File.Exists(targetFile))
                    {
                        file.CopyTo(targetFile);
                        if (_activecrypto)
                        {
                            Stopwatch cryptoWatch = new Stopwatch();
                            cryptoWatch.Start();
                            Encrypt(targetFile, targetFile, _cryptoSoftPath, _extensionList);
                            cryptoWatch.Stop();
                            _elapsedCrytoTime += cryptoWatch.Elapsed.TotalSeconds;
                        }
                    }
                    else
                    {
                        var lastwrite = System.IO.File.GetLastAccessTimeUtc(targetFile);
                        if (lastwrite != file.LastAccessTimeUtc)
                        {
                            file.CopyTo(targetFile, true);
                            if (_activecrypto)
                            {
                                Stopwatch cryptoWatch = new Stopwatch();
                                cryptoWatch.Start();
                                Encrypt(targetFile, targetFile, _cryptoSoftPath, _extensionList);
                                cryptoWatch.Stop();
                                _elapsedCrytoTime += cryptoWatch.Elapsed.TotalSeconds;
                            }
                        }
                    }
                }
                catch (Exception e) { Debug.WriteLine(e.ToString()); }

                TransfertJob.CountMutex.WaitOne();
                _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                _nbFilesMoved++;
                _files.Progress = _nbFilesMoved / _nbFiles * 100;
                Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles} - {_files.Progress}");
                TransfertJob.CountMutex.ReleaseMutex();

                TransfertJob.PauseMutex.ReleaseMutex();
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
                    _pauseMutex.WaitOne();
                    string targetFile = Path.Combine(targetdir, file.Name);
                    Console.WriteLine($"File {file.Name} written");
                    try
                    {
                        if (!System.IO.File.Exists(targetFile))
                        {
                            file.CopyTo(targetFile);
                            if (_activecrypto)
                            {
                                Stopwatch cryptoWatch = new Stopwatch();
                                cryptoWatch.Start();
                                Encrypt(targetFile, targetFile, _cryptoSoftPath, _extensionList);
                                cryptoWatch.Stop();
                                _elapsedCrytoTime += cryptoWatch.Elapsed.TotalSeconds;
                            }
                        }
                        else
                        {
                            var lastwrite = System.IO.File.GetLastAccessTimeUtc(targetFile);
                            if (lastwrite != file.LastAccessTimeUtc)
                            {
                                file.CopyTo(targetFile, true);
                                if (_activecrypto)
                                {
                                    Stopwatch cryptoWatch = new Stopwatch();
                                    cryptoWatch.Start();
                                    Encrypt(targetFile, targetFile, _cryptoSoftPath, _extensionList);
                                    cryptoWatch.Stop();
                                    _elapsedCrytoTime += cryptoWatch.Elapsed.TotalSeconds;
                                }
                            }
                        }
                    }
                    catch (Exception e) { Debug.WriteLine(e.ToString()); }

                    TransfertJob.CountMutex.WaitOne();
                    _elapsedTransfertTime = stopwatch.Elapsed.TotalSeconds;
                    _nbFilesMoved++;
                    _files.Progress = _nbFilesMoved / _nbFiles * 100;
                    Debug.WriteLine($"Moved {_nbFilesMoved}/{_nbFiles} - {_files.Progress}");
                    TransfertJob.CountMutex.ReleaseMutex();

                    TransfertJob.PauseMutex.ReleaseMutex();
                }
            }
            stopwatch.Stop();
            _actualStates = false;
            Loggin();
        }

        public void Loggin()
        {
            try
            {
                string nameLog = Path.GetFileName(_files.PathFrom);
                string totalSizeFileStr = _files.TotalSizeFile.ToString();
                string elapsedTransfertTimeStr = _elapsedTransfertTime.ToString();
                string cryptTime = _elapsedCrytoTime.ToString();

                LogsFile JSONmyLogs = LogsFile.GetInstance(true);
                LogsFile XMLmyLogs = LogsFile.GetInstance(false);
                Mutex JSONMutex = LogsFile.GetMutex(true);
                Mutex XMLMutex = LogsFile.GetMutex(false);

                JSONMutex.WaitOne();
                JSONmyLogs.WriteLog(nameLog, _files.PathFrom, _files.PathTo, totalSizeFileStr, elapsedTransfertTimeStr, cryptTime);
                JSONMutex.ReleaseMutex();

                XMLMutex.WaitOne();
                XMLmyLogs.WriteLog(nameLog, _files.PathFrom, _files.PathTo, totalSizeFileStr, elapsedTransfertTimeStr, cryptTime);
                XMLMutex.ReleaseMutex();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void Encrypt(string sourceDir, string targetDir, string _cryptoSoftPath, List<string> _extension)//This function allows you to encrypt files. 
        {
            try
            {
                _extension = _extensionList;
                foreach (FileInfo file in _files.Files)
                {
                    //On test chaque Type paramétré
                    foreach (string strExtensionCrypt in _extension)
                    {
                        if (file.Extension == strExtensionCrypt) // If the extension matches, encrypt the file
                        {
                            using (Process process = new Process())//Declaration of the process
                            {
                                process.StartInfo.FileName = "CryptoSoft.exe"; //Calls the process that is CryptoSoft
                                process.StartInfo.Arguments = String.Format("\"{0}\"", sourceDir) + " " + String.Format("\"{0}\"", targetDir.Replace(".", "_encrypted.")); //Preparation of variables for the process.
                                Debug.Write(process.StartInfo.Arguments);
                                process.StartInfo.WorkingDirectory = Path.GetDirectoryName("C:\\Users\\smite\\source\\repos\\prosoft\\CryptoSoft\\bin\\Debug\\");
                                process.Start(); //Launching the process
                                process.Close();

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            // Loop through all subdirectories in the save files object
            foreach (DirectoryInfo dir in _files.SubDirs)
            {
                try
                {
                    FileInfo[] subFiles = dir.GetFiles(); // Get all files in the subdirectory

                    // Loop through all files in the subdirectory
                    foreach (FileInfo file in subFiles)
                    {
                        //On test chaque Type paramétré 
                        foreach (string strExtensionCrypt in _extension)
                        {
                            if (file.Extension == strExtensionCrypt) // If the extension matches, encrypt the file
                            {
                                using (Process process = new Process())//Declaration of the process
                                {
                                    process.StartInfo.FileName = "CryptoSoft.exe"; //Calls the process that is CryptoSoft
                                    process.StartInfo.Arguments = String.Format("\"{0}\"", sourceDir) + " " + String.Format("\"{0}\"", targetDir.Replace(".", "_encrypted.")); //Preparation of variables for the process.
                                    Debug.Write(process.StartInfo.Arguments);
                                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName("C:\\Users\\smite\\source\\repos\\prosoft\\CryptoSoft\\bin\\Debug\\");
                                    process.Start(); //Launching the process
                                    process.Close();

                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }


        }
        public bool ActualStates { get => _actualStates; set => _actualStates = value; }
        public double ElapsedTransfertTime { get => _elapsedTransfertTime; }
        internal SaveFiles workingFile { get => _files; }
        public double NbFiles { get => _nbFiles; }
        public double NbFilesMoved { get => _nbFilesMoved; set => _nbFilesMoved = value; }
        public string Name { get => _files.Name; set => _files.Name = value; }

        public bool Activecrypto { get => _activecrypto; set => _activecrypto = value; }


        public Thread MainThread { get => _mainThread; set => _mainThread = value; }
        public uint MaxSizeFile { get => _maxSizeFile; set => _maxSizeFile = value; }
        public static Mutex BigFileMutex { get => _bigFileMutex; set => _bigFileMutex = value; }
        public static Mutex PauseMutex { get => _pauseMutex; set => _pauseMutex = value; }
        public static Mutex CountMutex { get => _countMutex; set => _countMutex = value; }
        public static Mutex PauseMutex1 { get => _pauseMutex; set => _pauseMutex = value; }
        public List<string> PrioritizeExts { get => _prioritizeExts; set => _prioritizeExts = value; }
        public double ElapsedCrytoTime { get => _elapsedCrytoTime; set => _elapsedCrytoTime = value; }
        public string CryptoSoftPath { get => _cryptoSoftPath; set => _cryptoSoftPath = value; }
    }
}
