using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EasySaveModel
{
    public class CryptoSoft
    {
        private string _cryptoSoftPath;

        public string CryptoSoftPath => _cryptoSoftPath;

        public List<string> _extensionList = new List<string>();
        public CryptoSoft(string cryptoSoftPath, List<string> extensionList)
        {
            _cryptoSoftPath = cryptoSoftPath;
            _extensionList= extensionList;
        }

        public void StartProcess(SaveFiles files)
        {
            // On test chaque Fichier
            foreach (FileInfo file in files.Files)
            {
                //On test chaque Type paramétré
                foreach (string strExtensionCrypt in _extensionList)
                {
                    if (file.Extension == strExtensionCrypt) // If the extension matches, encrypt the file
                    {
                        Process processCryptoSoft = new Process(); // Create a new process for the encryption software
                        processCryptoSoft.StartInfo.FileName = _cryptoSoftPath; // Set the process's filename to the encryption software path
                        processCryptoSoft.StartInfo.Arguments = file.FullName; // Set the process's arguments to the full file path
                        processCryptoSoft.Start(); // Start the encryption process
                    }
                }
            }

            // Loop through all subdirectories in the save files object
            foreach (DirectoryInfo dir in files.SubDirs)
            {
                FileInfo[] subFiles = dir.GetFiles(); // Get all files in the subdirectory

                // Loop through all files in the subdirectory
                foreach (FileInfo file in subFiles)
                {
                    //On test chaque Type paramétré 
                    foreach (string strExtensionCrypt in _extensionList)
                    {
                        if (file.Extension == strExtensionCrypt) // If the extension matches, encrypt the file
                        {
                            Process processCryptoSoft = new Process(); // Create a new process for the encryption software
                            processCryptoSoft.StartInfo.FileName = _cryptoSoftPath; // Set the process's filename to the encryption software path
                            processCryptoSoft.StartInfo.Arguments = file.FullName; // Set the process's arguments to the full file path
                            processCryptoSoft.Start(); // Start the encryption process
                        }
                    }
                }
            }
        }
    }
}