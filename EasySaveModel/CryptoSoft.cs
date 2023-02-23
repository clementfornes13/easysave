using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EasySaveModel
{
    public class CryptoSoft
    {
        private string _cryptoSoftPath;
        private List<string> _extensionsList = new List<string>();
        public string CryptoSoftPath => _cryptoSoftPath;

        public List<string> ExtensionsList { get => _extensionsList; set => _extensionsList = value; }

        public CryptoSoft(string cryptoSoftPath, List<string> extensionsList)
        {
            _cryptoSoftPath = cryptoSoftPath;
            _extensionsList = extensionsList;
        }

        public void StartProcess(SaveFiles files)
        {

            // On test chaque Fichier
            foreach (FileInfo file in files.Files)
            {
                //On test chaque Type paramétré
                foreach (string strExtensionCrypt in _extensionsList)
                {
                    if (file.Extension == strExtensionCrypt)
                    {
                        Process processCryptoSoft = new Process();
                        processCryptoSoft.StartInfo.FileName = _cryptoSoftPath;
                        processCryptoSoft.StartInfo.Arguments = file.FullName;
                        processCryptoSoft.Start();
                    }
                }
            }

            //Manage sub dir for copy
            foreach (DirectoryInfo dir in files.SubDirs)
            {
                FileInfo[] subFiles = dir.GetFiles();
                // On test chaque Fichier
                foreach (FileInfo file in subFiles)
                {
                    //On test chaque Type paramétré 
                    foreach (string strExtensionCrypt in _extensionsList)
                    {
                        if (file.Extension == strExtensionCrypt)
                        {
                            Process processCryptoSoft = new Process();
                            processCryptoSoft.StartInfo.FileName = _cryptoSoftPath;
                            processCryptoSoft.StartInfo.Arguments = file.FullName;
                            processCryptoSoft.Start();
                        }
                    }
                }
            }
        }
    }
}
