using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySave

{
    class Controller
    {
        private ConsoleCLI m_cli;
        private List<SaveFiles> m_workingFiles = new List<SaveFiles>();
        private List<TransfertStatesItems> m_transferts = new List<TransfertStatesItems>();
        public Controller()
        {
            while (true)
            {
                createUI(); //Summon UI to get choice
            }
        }

        private void createUI() // clement
        {
            if (m_cli == null)
            {
                m_cli = new ConsoleCLI();

                if (m_cli.ChoixLangue())
                {
                    switch (m_cli.ChoixSave())
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
                switch (m_cli.ChoixSave())
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

        private void addWorkingFiles() //Thomas - Jordan
        {
            if (m_workingFiles.Count >= 5)
            {
                Console.WriteLine("Can't Have more than 5 working project delete one if you need to add one.");
            }
            else
            {
                if (m_cli.PathTo1 == null)
                {
                    m_workingFiles.Add(new SaveFiles(m_cli.PathFrom1));
                }
                else
                {
                    m_workingFiles.Add(new SaveFiles(m_cli.PathFrom1, m_cli.PathTo1));
                }
            }
        }

        private void delWorkingFiles()
        {
            foreach (SaveFiles file in m_workingFiles)
            {
                if (file.PathFrom == m_cli.DeletePath1)
                {
                    m_workingFiles.Remove(file);
                }
            }
        }

        private void createJob()
        {
            foreach (SaveFiles file in m_workingFiles)
            {
                Console.WriteLine(file.ToString());
            }

            foreach (SaveFiles file in m_workingFiles)
            {
                if (file.PathFrom == m_cli.ExecutePath1)
                {
                    m_transferts.Add(new TransfertStatesItems(file));
                    Console.WriteLine("Beginning backUp of files...");
                    m_transferts[m_transferts.Count-1].BackUp();
                    Console.Write($"Done in {0}", m_transferts[m_transferts.Count-1].ElapsedTransfertTime);
                }
            }
        }
    }
}