using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Resources;

namespace EasySaveModel
{
    public class SaveFiles
    {
        private string _name;
        private bool _cryptosoft;
        private string _pathFrom, _pathTo; //C:/dir/dir/dir

        private double _progress = 0;

        private readonly List<FileInfo> _files = new List<FileInfo>();
        private List<DirectoryInfo> _subDirs = new List<DirectoryInfo>();
        private long _totalSizeFile = 0;

        public SaveFiles(string pathFrom, string pathTo, string name, bool cryptosoft)
        {
            _name = name;
            _cryptosoft = cryptosoft;
            _pathFrom = pathFrom;
            if (pathTo == null || pathTo == "" || pathTo == "Destination")
            {
                _pathTo = System.Environment.CurrentDirectory
                    + @"\Backups\"
                    + System.Threading.Thread.CurrentThread.ManagedThreadId;
            }
            else
            {
                _pathTo = pathTo;
            }
            init();
            calcSizeFiles();
        }
        public SaveFiles(string pathFrom, string pathTo)
        {
            _pathFrom = pathFrom;
            if (pathTo == null || pathTo == "" || pathTo == "Destination")
            {
                _pathTo = System.Environment.CurrentDirectory
                    + @"\Backups\"
                    + System.Threading.Thread.CurrentThread.ManagedThreadId;
            }
            else
            {
                _pathTo = pathTo;
            }
            init();
            calcSizeFiles();
        }
        ~SaveFiles()
        {
            //File.close();
        }

        public void init()
        {
            try
            {
                //Need to make a feature for subdirectory
                string[] names = System.IO.Directory.GetFiles(_pathFrom);
                if (names.Length == 0)
                {
                    //throw new DirectoryNotFoundException("DirectoryError" + m_pathFrom);
                }

                foreach (string filename in names)
                {
                    _files.Add(new FileInfo(filename));
                }

                DirectoryInfo dirFrom = new DirectoryInfo(_pathFrom);
                _subDirs = new List<DirectoryInfo>(dirFrom.GetDirectories());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void calcSizeFiles()
        {
            try
            {
                foreach (FileInfo file in _files)
                {
                    _totalSizeFile += file.Length;
                }
                foreach (DirectoryInfo dir in _subDirs)
                {
                    FileInfo[] subFiles = dir.GetFiles();
                    foreach (FileInfo file in subFiles)
                    {
                        _totalSizeFile += file.Length;
                    }
                }
                Logging();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void Logging()
        {
            try
            {
                string m_nameLog = Path.GetFileName(_pathFrom);
                string totalSizeFileLog = _totalSizeFile.ToString();
                string transferTime = "0";
                string cryptTime = "0";

                LogsFile JSONmyLogs = LogsFile.GetInstance(true);
                LogsFile XMLmyLogs = LogsFile.GetInstance(true);

                JSONmyLogs.WriteLog(m_nameLog, _pathFrom, _pathTo, totalSizeFileLog, transferTime, cryptTime);
                XMLmyLogs.WriteLog(m_nameLog, _pathFrom, _pathTo, totalSizeFileLog, transferTime, cryptTime);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public List<FileInfo> Files { get => _files; }
        public List<DirectoryInfo> SubDirs { get => _subDirs; }
        public string PathFrom { get => _pathFrom; set => _pathFrom = value; }
        public string PathTo { get => _pathTo; set => _pathTo = value; }
        public long TotalSizeFile { get => _totalSizeFile; }
        public string Name { get => _name; set => _name = value; }
        public bool Cryptosoft { get => _cryptosoft; set => _cryptosoft = value; }
        public double Progress { get => _progress; set => _progress = value; }
    }
}
