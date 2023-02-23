using System;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace EasySaveModel
{
    public class LogsFile
    {
        // Variable definitions

        // The path where log files will be saved
        private string _logsFilePath = System.Environment.CurrentDirectory + @"\Logs\";

        // The format string used to name logfiles "Log 23-02-2023.xml"
        private string _logsFileName = "Log {0}.xml";

        // The StreamWriter used for writing logs to a file
        private StreamWriter _writingStream;

        // Singleton
        private static LogsFile _xmlInstance, _jsonInstance;
        private static Mutex _xmlMutex, _jsonMutex;
        private readonly bool _jsonOrXml;

        // Initializes the _jsonOrXml
        private LogsFile(bool jsonOrXML)
        {
            _jsonOrXml = jsonOrXML;
        }

        // Returns a singleton instance of the LogsFile class
        public static LogsFile GetInstance(bool jsonOrXml)
        {
            // Check whether the instance should use JSON or XML format
            if (jsonOrXml)
            {
                // If using JSON, create a new instance if one doesn't exist
                if (_jsonInstance == null)
                {
                    _jsonInstance = new LogsFile(jsonOrXml);
                }
                return _jsonInstance;
            }
            else
            {
                // If using XML, create a new instance if one doesn't exist
                if (_xmlInstance == null)
                {
                    _xmlInstance = new LogsFile(jsonOrXml);
                }
                return _xmlInstance;
            }
        }

        // Returns a mutex object used for thread synchronization
        public static Mutex GetMutex(bool jsonOrXml)
        {
            // Check whether the mutex should be used for JSON or XML format
            if (jsonOrXml)
            {
                // If using JSON, create a new mutex if one doesn't exist
                if (_jsonMutex == null)
                {
                    _jsonMutex = new Mutex();
                }
                return _jsonMutex;
            }
            else
            {
                // If using XML, create a new mutex if one doesn't exist
                if (_xmlMutex == null)
                {
                    _xmlMutex = new Mutex();
                }
                return _xmlMutex;
            }
        }

        // Creates the Logs directory if it doesn't already exist
        private void CreateDirectory()
        {
            // Check if the folder already exists
            if (!Directory.Exists(_logsFilePath))
            {
                // If not, create it
                Directory.CreateDirectory(_logsFilePath);
            }
        }

        // Writes a log entry to a file using the specified format (JSON or XML)
        public void WriteLog(string name, string fileSource, string fileTarget, string sizeFile, string transferTime, string cryptTime)
        {
            // Check whether to use JSON or XML format
            if (_jsonOrXml)
            {
                // If using JSON, call the WriteLogJson method
                WriteLogJson(name, fileSource, fileTarget, sizeFile, transferTime, cryptTime);
            }
            else
            {
                // If using XML, call the WriteLogXml method
                WriteLogXml(name, fileSource, fileTarget, sizeFile, transferTime, cryptTime);
            }
        }

        // Writes a log entry to a JSON file
        private void WriteLogJson(string name, string fileSource, string fileTarget, string sizeFile, string transferTime, string cryptTime)
        {
            try
            {
                // Create the Logs directory if it doesn't exist
                CreateDirectory();

                // Define the path for the log file using the current date 
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
                    // If the file already exists, append the new log to it
                    File.AppendAllText(filePath, ",");
                    File.AppendAllText(filePath, "\n");
                }
                File.AppendAllText(filePath, logJson);
            }
            catch (Exception e)
            {
                // Print the exception to the console if an error occurs
                Console.Error.WriteLine(e);
            }
        }

        private void CreateFile()
        {
            // Create the Logs directory if it doesn't exist
            CreateDirectory();

            // Define the path to take and the name of the log file
            string formattedDate = DateTime.Now.ToString("dd-MM-yyyy");
            string filePath = Path.Combine(_logsFilePath, string.Format(_logsFileName, formattedDate));

            // Check if the file already exists
            if (!File.Exists(filePath))
            {
                // If the file doesn't exist, create it and write the header
                using (_writingStream = new StreamWriter(filePath, false))
                {
                    _writingStream.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                }
            }

        }
        private void WriteLogXml(string name, string fileSource, string fileTarget, string sizeFile, string transferTime, string cryptTime )
        {
            try
            {
                // Create the log file if it doesn't exist
                CreateFile();

                // Write the log information to the file
                using (_writingStream = new StreamWriter(Path.Combine(_logsFilePath, string.Format(_logsFileName, DateTime.Now.ToString("dd-MM-yyyy"))), true))
                {
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
