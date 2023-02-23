using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EasySaveModel
{
    public class CryptoSoft
    {
        private string _cryptoSoftPath; // Path to the encryption software

        public string CryptoSoftPath => _cryptoSoftPath; // Property to get the encryption software path

        public void StartProcess(List<string> extensionCrypt, SaveFiles files, string cryptosoft) // Starts the encryption process
        {
            _cryptoSoftPath = cryptosoft; // Set the encryption software path

            // Loop through all files in the save files object
            foreach (FileInfo file in files.Files)
            {
                // Check if the file extension is in the list of extensions to encrypt
                foreach (string strExtensionCrypt in extensionCrypt)
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
                    // Check if the file extension is in the list of extensions to encrypt
                    foreach (string strExtensionCrypt in extensionCrypt)
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