using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace EasySaveModel
{
    public class CryptoSoft
    {
        private readonly string _cryptoSoftPath = @"C:\Users\jorda\source\repos\eystone\prosoft\CryptoSoft\bin\Debug\CryptoSoft.exe";
        public void StartProcess(List<string> extensionCrypt, SaveFiles files)
        {
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
