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
        private SaveFiles _savefiles;
        public VisualModel()
        {

        }
       
        public void delWorkingFiles()
        {

        }

        public void createJob()
        {

        }
    }
}
