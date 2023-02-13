using System;
using Newtonsoft.Json;
using System.IO;
using System.Resources;

namespace EasySave
{

    class LogsFile
    {
        ResourceManager _rm = new ResourceManager("EasySave.Resources.Langue", typeof(EasySave).Assembly);
        // Variable definitions
        private string _logsFilePath = System.Environment.CurrentDirectory + @"\Logs\";

        private string _logsFileName = "Log {0}.xml";
        private StreamWriter _writingStream;

        // Singleton
        private static LogsFile xmlInstance, jsonInstance;
        private readonly bool _jsonOrXml;
        private LogsFile(bool jsonOrXML)
        {
            _jsonOrXml = jsonOrXML;
        }
        public static LogsFile GetInstance(bool jsonOrXml)
        {
            if (jsonOrXml)
            {
                if (jsonInstance == null)
                {
                    jsonInstance = new LogsFile(jsonOrXml);
                }
                return jsonInstance;
            }
            else
            {
                if (xmlInstance == null)
                {
                    xmlInstance = new LogsFile(jsonOrXml);
                }
                return xmlInstance;
            }
            //if (states logs)
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

        public void WriteLog(string name, string fileSource, string fileTarget, string sizeFile, string transferTime)
        {
            if (_jsonOrXml)
            {
                WriteLogJson(name, fileSource, fileTarget, sizeFile, transferTime);
            }
            else
            {
                WriteLogXml(name, fileSource, fileTarget, sizeFile, transferTime);
            }
        }

        private void WriteLogJson(string name, string fileSource, string fileTarget, string sizeFile, string transferTime)
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
                    TransferTime = transferTime,
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
        private void WriteLogXml(string name, string fileSource, string fileTarget, string sizeFile, string transferTime)

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
