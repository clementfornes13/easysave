using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySave
{

    /*class EasySave
     {
         static void Main(string[] args)
         {
            
            LogsFile mylog = new LogsFile();
        }

     }*/

    class LogsFile
    {
        //Variable 
        private static StreamReader m_readingStream;
        private static StreamWriter m_writinggStream;

        public static StreamReader ReadingStream { get => m_readingStream; set => m_readingStream = value; }
        public static StreamWriter WritingStream { get => m_writinggStream; set => m_writinggStream = value; }
        
        private string LogsFilePath = System.Environment.CurrentDirectory + @"\Logs\";

        public LogsFile()
        {
            string name = "name 2";
            string pathfrom = "from 2";
            string pathto = "to 2";
            string sizefile = "sizefile 2";
            string transfertTime = "transfertTime 2";

            WriteLog(name, pathfrom, pathto, sizefile, transfertTime);
        }

        bool CreateLogFile()
        {
            // Formatting the date
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy");

            if (!Directory.Exists(LogsFilePath)) //Check if the folder is created
            {
                DirectoryInfo di = Directory.CreateDirectory(LogsFilePath); //Function that creates the folder
            }

            string filePath = System.Environment.CurrentDirectory + @"\Logs\Log " + formattedDate + ".csv";

            try
            {
                // Create an empty file
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    WritingStream = new StreamWriter(filePath, true);
                    WritingStream.WriteLine("Timestamp;Name;Source path;Destination path;File size;Transfer time");
                    WritingStream.Close();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Console.Error.WriteLine(e.ToString());
                return false;
            }
        }

        bool WriteLog(string name, string pathfrom, string pathto, string sizefile, string transfertTime)
        {
            try
            {

                if (CreateLogFile() == true)
                {
                    // Formatting the date
                    DateTime currentDate = DateTime.Now;
                    string formattedDate = currentDate.ToString("dd-MM-yyyy");



                    string filePath = System.Environment.CurrentDirectory + @"\Logs\Log " + formattedDate + ".csv";


                    // Open the write stream to write to the file
                    WritingStream = new StreamWriter(filePath, true);

                    // Write log entries
                    WritingStream.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.f") + ";" + name + ";" + pathfrom + ";" + pathto + ";" + sizefile + ";" + transfertTime);

                    // Close the write stream
                    WritingStream.Close();

                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.Error.WriteLine(e.ToString());
                return false;
            }
        }

    }



}