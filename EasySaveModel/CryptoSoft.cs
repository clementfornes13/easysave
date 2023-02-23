using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EasySaveModel
{
    public class CryptoSoft
    {
        private string _cryptoSoftPath;

        public string CryptoSoftPath => _cryptoSoftPath;

        public void StartProcess(List<string> extensionCrypt, SaveFiles files, string cryptosoft)
        {
            _cryptoSoftPath = cryptosoft;
            // On test chaque Fichier
            foreach (FileInfo file in files.Files)
            {
                //On test chaque Type paramétré
                foreach (string strExtensionCrypt in extensionCrypt)
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
                    foreach (string strExtensionCrypt in extensionCrypt)
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
