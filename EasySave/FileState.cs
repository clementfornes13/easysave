using System;
using System.Resources;
using System.IO;
using Newtonsoft.Json;

namespace EasySave
{
    class FileState
    {
        ResourceManager _rm = new ResourceManager("EasySave.Resources.Langue", typeof(EasySave).Assembly);
        // Variable definitions
        private string _stateFilePath = System.Environment.CurrentDirectory + @"\State\";

        // Singleton
        private static FileState obj;
        private FileState() { }

        public static FileState GetInstance()
        {
            if (obj == null)
              obj = new FileState();
            return obj;
        }


        // Create a Logs folder
        private void CreateDirectoryStatut()
        {
            // Check if the folder already exists
            if (!Directory.Exists(_stateFilePath))
            {
                Directory.CreateDirectory(_stateFilePath);
            }
        }

        public void WriteStateJson(string name, string sourceFilePath, string fileFileTarget, string state, string totalFilesToCopy, string totalFilesSize, string nbFilesLeftToDo, string progression)
        {
            try
            {
                CreateDirectoryStatut();

                string filePath = Path.Combine(_stateFilePath, string.Format("State.json"));

                // Create a log object with the provided information
                var statut = new
                {
                    Name = name,
                    SourceFilePath = sourceFilePath,
                    FileFileTarget = fileFileTarget,
                    State = state,
                    TotalFilesToCopy = totalFilesToCopy,
                    TotalFilesSize = totalFilesSize,
                    NbFilesLeftToDo = nbFilesLeftToDo,
                    Progression = progression
                };

                // Serialize the log object to JSON format
                string logJson = JsonConvert.SerializeObject(statut, Formatting.Indented);

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
    }
}
