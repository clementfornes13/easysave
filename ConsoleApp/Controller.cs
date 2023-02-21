using System;
using System.Collections.Generic;
using System.Resources;
using EasySaveModel;

namespace ConsoleApp
{
    class Controller
    {
        private ConsoleCLI _cli;
        private List<SaveFiles> _workingFiles = new List<SaveFiles>();
        private static List<TransfertJob> _transferts = new List<TransfertJob>();
        ResourceManager rm = new ResourceManager("ConsoleApp.Resources.Langue", typeof(ConsoleApp).Assembly);
        public Controller()
        {
            while (true)
            {
                createUI(); //Summon UI to get choice
            }
        }

        private void createUI()
        {
            if (_cli == null)
            {
                _cli = new ConsoleCLI();

                if (_cli.ChoixLangue())
                {
                    switch (_cli.ChoixSave())
                    {
                        case 1:
                            addWorkingFiles();
                            break;
                        case 2:
                            delWorkingFiles();
                            break;
                        case 3:
                            createJob();
                            break;
                        default:
                            System.Environment.Exit(0);
                            break;
                    }
                }
            }
            else
            {
                switch (_cli.ChoixSave())
                {
                    case 1:
                        addWorkingFiles();
                        break;
                    case 2:
                        delWorkingFiles();
                        break;
                    case 3:
                        createJob();
                        break;
                    default:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }

        private void addWorkingFiles()
        {
            _workingFiles.Add(new SaveFiles(_cli.PathFrom1, _cli.PathTo1));
        }

        private void delWorkingFiles()
        {
            foreach (SaveFiles file in _workingFiles)
            {
                if (file.PathFrom == _cli.DeletePath1)
                {
                    _workingFiles.Remove(file);
                }
            }
        }

        private void createJob()
        {
            foreach (SaveFiles file in _workingFiles)
            {
                Console.WriteLine(file.ToString());
            }

            foreach (SaveFiles file in _workingFiles)
            {
                if (file.PathFrom == _cli.ExecutePath1)
                {
                    _transferts.Add(new TransfertJob(file));
                    Console.WriteLine(rm.GetString("BeginBackup"));
                    _transferts[_transferts.Count - 1].ThreadBackUp();
                    Console.Write(rm.GetString("BackupTime") + $"{_transferts[_transferts.Count - 1].ElapsedTransfertTime}" + "s");
                }
            }
        }
    }
}