using System;
using Newtonsoft.Json;
using System.IO;
using System.Resources;

namespace EasySave
{

    class LogsFile
    {
        ResourceManager rm = new ResourceManager("EasySave.Resources.Langue", typeof(EasySave).Assembly);
        // Variable definitions
        private string logsFilePath = System.Environment.CurrentDirectory + @"\Logs\";

        private string logsFileName = "Log {0}.xml";
        private StreamWriter writingStream;

        // Singleton
        private static LogsFile xmlInstance, jsonInstance;
        private static bool m_jsonOrXml;
        private LogsFile() { }
        public static LogsFile GetInstance(bool jsonOrXml)
        {
            m_jsonOrXml = jsonOrXml;
            if (jsonOrXml)
            {
                if (jsonInstance == null)
                {
                    jsonInstance = new LogsFile();
                }
                return jsonInstance;
            }
            else
            {
                if (xmlInstance == null)
                {
                    xmlInstance = new LogsFile();
                }
                return xmlInstance;
            }
            //if (states logs)
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
        public bool WriteLogJson(string name, string pathFrom, string pathTo, string sizeFile, string transferTime)
        {
            try
            {
                CreateDirectory();

                // Define the path to take 
                string formattedDate = DateTime.Now.ToString("dd-MM-yyyy");
                string filePath = Path.Combine(logsFilePath, string.Format("Log {0}.json", formattedDate));

                // Create a log object with the provided information
                var log = new
                {
                    Name = name,
                    SourcePath = pathFrom,
                    DestinationPath = pathTo,
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

                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return false;
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
                    // Write the header in the xml file
                    writingStream.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                }
            }

        }
        public bool WriteLogXml(string name, string pathFrom, string pathTo, string sizeFile, string transferTime)
        {
            try
            {
                CreateFile();

                // Define the path to take 
                using (writingStream = new StreamWriter(Path.Combine(logsFilePath, string.Format(logsFileName, DateTime.Now.ToString("dd-MM-yyyy"))), true))
                {
                    // Write the various information to the file
                    writingStream.WriteLine(
                        "{0}{1}{2}{3}{4}{5}{6}{7}",
                        "< LogsFile > \n",
                        "\t< Time >" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.f") + "< \\Time >\n",
                        "\t< Name >" + name + "< \\Name >\n",
                        "\t< PathFrom >" + pathFrom + "< \\PathFrom >\n",
                        "\t< PathTo >" + pathTo + "< \\PathTo >\n",
                        "\t< SizeFile >" + sizeFile + "< \\SizeFile >\n",
                        "\t< TransfertTime >" + transferTime + "< \\TransfertTime >\n",
                        "< \\LogsFile > \n"
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
