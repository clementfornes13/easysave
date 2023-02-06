using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySave
{
    class EasySave
    {
        static void Main()
        {
            LogsFile mylog = new LogsFile();
            
        }
    }
    class LogsFile
    {
        private static StreamReader m_readingStream;
        private static StreamWriter m_writinggStream;

        public static StreamReader ReadingStream { get => m_readingStream; set => m_readingStream = value; }
        public static StreamWriter WritinggStream { get => m_writinggStream; set => m_writinggStream = value; }

        public LogsFile()
        {
            open();
        }


         bool open()
        {

            string filePath = @"C:\Users\jorda\source\repos\eystone\prosoft\EasySave\Example.txt";
            File.WriteAllText(filePath, "Hello World");
            return true
        }
    }


    
}
