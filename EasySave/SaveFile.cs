using System;
using System.IO;

namespace EasySave
{
    /*class EasySave
    {
        static void Main(string[] args)
        {
            SaveFile File1 = new SaveFile("1.txt","C:\\Users\\Eystone\\source\\repos", "C:\\Users\\Eystone\\source");
            Console.WriteLine($"Transfert {0} from {1} to {2} ...", File1.Name, File1.PathFrom, File1.PathTo);
            TransfertStatesItems transfert = new TransfertStatesItems(File1);
            transfert.MoveFromTo();
            Console.Write("Done");

            while (Console.ReadLine() != "q") { }
        }
    }*/

    class SaveFile
    {
        private string m_name; //file.co
        private string m_pathFrom, m_pathTo; //C:/dir/dir/dir
        private uint sizeFile;
        private uint transfertTime;

        public SaveFile(string filename)
        {
            m_name = filename;
            if (init() != 1) {
                //Destroy Object
                Console.WriteLine("1");
            }
            Console.WriteLine("0");
        }
        public SaveFile(string filename, string pathFrom, string pathTo) 
        {
            m_name = filename;
            m_pathFrom = pathFrom;
            m_pathTo = pathTo;
            if (init() != 1) {
                //Destroy Object
                Console.WriteLine("1");
            }
            Console.WriteLine("0");
        }

        private int init()
        {
            string filePathName = System.IO.Path.Combine(m_pathFrom, m_name);
            Console.WriteLine($"My file is : {0}", filePathName);
            try {
                using (StreamReader sr = new StreamReader(filePathName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                return 1;
            }
            catch (Exception e) {
                System.Console.Error.WriteLine(e.ToString());
                return 0;
            }
        }
        ~SaveFile()
        {
            //File.close();
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
