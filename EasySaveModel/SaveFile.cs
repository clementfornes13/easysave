using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace EasySaveModel
{
    public class SaveFiles
    {
        private string _nom; 
        private bool _cryptosoft;
        private string _pathFrom, _pathTo; //C:/dir/dir/dir

        private readonly List<FileInfo> _files = new List<FileInfo>();
        private List<DirectoryInfo> _subDirs = new List<DirectoryInfo>();
        private long _totalSizeFile = 0;

        public SaveFiles(string pathFrom, string pathTo, string nom, bool cryptosoft)
        {
            _nom = nom;
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
        private void init()
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
        ~SaveFiles()
        {
            //File.close();
        }
        public void calcSizeFiles()
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
        public void Logging()
        {
            string m_nameLog = Path.GetFileName(_pathFrom);
            string totalSizeFileLog = _totalSizeFile.ToString();
            string transferTime = "0";

            LogsFile JSONmyLogs = LogsFile.GetInstance(true);
            LogsFile XMLmyLogs = LogsFile.GetInstance(true);

            JSONmyLogs.WriteLog(m_nameLog, _pathFrom, _pathTo, totalSizeFileLog, transferTime);
            XMLmyLogs.WriteLog(m_nameLog, _pathFrom, _pathTo, totalSizeFileLog, transferTime);
        }

        public List<FileInfo> Files { get => _files; }
        public List<DirectoryInfo> SubDirs { get => _subDirs; }
        public string PathFrom { get => _pathFrom; set => _pathFrom = value; }
        public string PathTo { get => _pathTo; set => _pathTo = value; }
        public long TotalSizeFile { get => _totalSizeFile; }
        public string Nom { get => _nom; set => _nom = value; }
        public bool Cryptosoft { get => _cryptosoft; set => _cryptosoft = value; }
    }
}
