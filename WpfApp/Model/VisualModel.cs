using System;
using System.Collections.Generic;
using System.Resources;
using EasySaveModel;

namespace WpfApp
{
    class VisualModel
    {
        private List<SaveFiles> m_workingFiles = new List<SaveFiles>();
        private List<TransfertStatesItems> m_transferts = new List<TransfertStatesItems>();
        ResourceManager rm = new ResourceManager("WpfApp.Resources.Langue", typeof(MainWindow).Assembly);
        public VisualModel()
        {
            while (true)
            {

            }
        }

        public void addWorkingFiles()
        {
            m_workingFiles.Add(new SaveFiles(GridFromTo.ColumnPathFrom1, GridFromTo.ColumnPathTo1));
        }

        public void delWorkingFiles()
        {
            foreach (SaveFiles file in m_workingFiles)
            {
                if (file.PathFrom == Global.PathFrom)
                {
                    m_workingFiles.Remove(file);
                }
            }
        }

        public void createJob()
        {
            foreach (SaveFiles file in m_workingFiles)
            {
                if (file.PathFrom == Global.PathFrom)
                {
                    m_transferts.Add(new TransfertStatesItems(file));
                    m_transferts[m_transferts.Count - 1].BackUp();
                }
            }
        }
    }
}
