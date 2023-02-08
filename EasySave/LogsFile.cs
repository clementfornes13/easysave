using System;
using System.IO;

namespace EasySave
{
    // Class for tests
    class EasySave
    {
        static void Main(string[] args)
        {
            LogsFile mylog = new LogsFile();
        }
    }

    class LogsFile
    {
        // Variable definitions
        private string logsFilePath = System.Environment.CurrentDirectory + @"\Logs\";
        private string logsFileName = "Log {0}.csv";
        private StreamWriter writingStream;

        public LogsFile()
        {
            string name = "name 2";
            string pathFrom = "from 2";
            string pathTo = "to 2";
            string sizeFile = "sizefile 2";
            string transferTime = "transfertTime 2";

            WriteLog(name, pathFrom, pathTo, sizeFile, transferTime);
        }

        // Create a Logs folder
        private void CreateDirectory()
        {
            // Check if the folder already exists
            if (!Directory.Exists(logsFilePath))
            {
                Directory.CreateDirectory(logsFilePath);
            }
        }

        private void CreateFile()
        {
            CreateDirectory();

            // Define the path to take and the name of the log file
            string formattedDate = DateTime.Now.ToString("dd-MM-yyyy");
            string filePath = Path.Combine(logsFilePath, string.Format(logsFileName, formattedDate));

            // Check if the file already exists
            if (!File.Exists(filePath))
            {
                using (writingStream = new StreamWriter(filePath, false))
                {
                    // Write the header in the csv file
                    writingStream.WriteLine("Timestamp;Name;Source path;Destination path;File size;Transfer time");
                }
            }
        }

        public bool WriteLog(string name, string pathFrom, string pathTo, string sizeFile, string transferTime)
        {
            try
            {
                CreateFile();

                // Define the path to take 
                using (writingStream = new StreamWriter(Path.Combine(logsFilePath, string.Format(logsFileName, DateTime.Now.ToString("dd-MM-yyyy"))), true))
                {
                    // Write the various information to the file
                    writingStream.WriteLine(
                        "{0};{1};{2};{3};{4};{5}",
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.f"),
                        name,
                        pathFrom,
                        pathTo,
                        sizeFile,
                        transferTime
                    );
                }

                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return false;
            }
        }
    }
}
