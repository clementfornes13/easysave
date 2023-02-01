using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave
{
    class EasySave
    {
        static void Main(string[] args)
        {
        }
    }

    class SaveFile
    {
        private string m_name;
        private string m_pathFrom, m_pathTo;
        private uint sizeFile;
        private uint transfertTime;

        SaveFile(string filename)
        {
            m_name = filename;
        }
        SaveFile(string filename, string pathFrom, string pathTo) 
        {
            m_name = filename;
            m_pathFrom = pathFrom;
            m_pathTo = pathTo;
        }
        ~SaveFile()
        {

        }

        public bool calcTransfertTime()
        {
            //Calc time to transfert from => to
            return true;
        }

        public string Name { get => m_name; }
        public string PathFrom { get => m_pathFrom; set => m_pathFrom = value; }
        public string PathTo { get => m_pathTo; set => m_pathTo = value; }
        public uint SizeFile { get => sizeFile; set => sizeFile = value; }
        public uint TransfertTime { get => transfertTime; }
    }
}
