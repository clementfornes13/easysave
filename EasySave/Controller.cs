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
        private SaveFiles[] m_workingFiles;
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
            if (m_workingFiles.Length >= 5)
            {
                Console.WriteLine("Can't Have more than 5 working project delete one if you need to add one.");
            }
            else
            {
                if (m_cli.PathTo1 == null)
                {
                    m_workingFiles[m_workingFiles.Length] = new SaveFiles(m_cli.PathFrom1);
                }
                else
                {
                    m_workingFiles[m_workingFiles.Length] = new SaveFiles(m_cli.PathFrom1, m_cli.PathTo1);
                }
            }
        }

        private void delWorkingFiles()
        {

        }

        private void createJob() //Thomas
        {

        }

        private void showStates()
        {

        }
    }
}