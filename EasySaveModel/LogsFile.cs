using System;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace EasySaveModel
{
    public class LogsFile
    {
        // Variable definitions
        private string _logsFilePath = System.Environment.CurrentDirectory + @"\Logs\";

        private string _logsFileName = "Log {0}.xml";
        private StreamWriter _writingStream;

        // Singleton
        private static LogsFile _xmlInstance, _jsonInstance;
        private static Mutex _xmlMutex, _jsonMutex;
        private readonly bool _jsonOrXml;

        private LogsFile(bool jsonOrXML)
        {
            _jsonOrXml = jsonOrXML;
        }
        public static LogsFile GetInstance(bool jsonOrXml)
        {
            if (jsonOrXml)
            {
                if (_jsonInstance == null)
                {
                    _jsonInstance = new LogsFile(jsonOrXml);
                }
                return _jsonInstance;
            }
            else
            {
                if (_xmlInstance == null)
                {
                    _xmlInstance = new LogsFile(jsonOrXml);
                }
                return _xmlInstance;
            }
        }

        public static Mutex GetMutex(bool jsonOrXml)
        {
            if (jsonOrXml)
            {
                if (_jsonMutex == null)
                {
                    _jsonMutex = new Mutex();
                }
                return _jsonMutex;
            }
            else
            {
                if (_xmlMutex == null)
                {
                    _xmlMutex = new Mutex();
                }
                return _xmlMutex;
            }
        }

        // Create a Logs folder
        private void CreateDirectory()
        {
            // Check if the folder already exists
            if (!Directory.Exists(_logsFilePath))
            {
                Directory.CreateDirectory(_logsFilePath);
            }
        }

        public void WriteLog(string name, string fileSource, string fileTarget, string sizeFile, string transferTime, string cryptTime)
        {
            if (_jsonOrXml)
            {
                WriteLogJson(name, fileSource, fileTarget, sizeFile, transferTime, cryptTime);
            }
            else
            {
                WriteLogXml(name, fileSource, fileTarget, sizeFile, transferTime, cryptTime);
            }
        }

        private void WriteLogJson(string name, string fileSource, string fileTarget, string sizeFile, string transferTime, string cryptTime)
        {
            try
            {
                CreateDirectory();

                // Define the path to take 
                string formattedDate = DateTime.Now.ToString("dd-MM-yyyy");
                string filePath = Path.Combine(_logsFilePath, string.Format("Log {0}.json", formattedDate));

                // Create a log object with the provided information
                var log = new
                {
                    Name = name,
                    SourcePath = fileSource,
                    FileTarget = fileTarget,
                    SizeFile = sizeFile,
                    TransfertTime = transferTime,
                    CryptTime = cryptTime,
                    Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.f")
                };

                // Serialize the log object to JSON format
                string logJson = JsonConvert.SerializeObject(log, Formatting.Indented);

                // Write the JSON string to the file
                if (File.Exists(filePath))
                {
                    File.AppendAllText(filePath, ",");
                    File.AppendAllText(filePath, "\n");
                }
                File.AppendAllText(filePath, logJson);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private void CreateFile()
        {
            CreateDirectory();

            // Define the path to take and the name of the log file
            string formattedDate = DateTime.Now.ToString("dd-MM-yyyy");
            string filePath = Path.Combine(_logsFilePath, string.Format(_logsFileName, formattedDate));

            // Check if the file already exists
            if (!File.Exists(filePath))
            {
                using (_writingStream = new StreamWriter(filePath, false))
                {
                    // Write the header in the xml file
                    _writingStream.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                }
            }

        }
        private void WriteLogXml(string name, string fileSource, string fileTarget, string sizeFile, string transferTime, string cryptTime )

        {
            try
            {
                CreateFile();

                // Define the path to take 
                using (_writingStream = new StreamWriter(Path.Combine(_logsFilePath, string.Format(_logsFileName, DateTime.Now.ToString("dd-MM-yyyy"))), true))
                {
                    // Write the various information to the file
                    _writingStream.WriteLine(
                        "{0}{1}{2}{3}{4}{5}{6}{7}",
                        "< LogsFile > \n",
                        "\t< Name >" + name + "< \\Name >\n",
                        "\t< FileSource >" + fileSource + "< \\FileSource >\n",
                        "\t< FileTarget >" + fileTarget + "< \\FileTarget >\n",
                        "\t< SizeFile >" + sizeFile + "< \\SizeFile >\n",
                        "\t< TransfertTime >" + transferTime + "< \\TransfertTime >\n",
                        "\t< CryptTime >" + cryptTime + "< \\CryptTime >\n",
                        "\t< Time >" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.f") + "< \\Time >\n",
                        "< \\LogsFile > \n"
                    );
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
