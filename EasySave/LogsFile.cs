using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySave
{
    class LogsFile
    {
        private static StreamReader m_readingStream;
        private static StreamWriter m_writinggStream;

        public static StreamReader ReadingStream { get => m_readingStream; set => m_readingStream = value; }
        public static StreamWriter WritingStream { get => m_writinggStream; set => m_writinggStream = value; }

        public LogsFile()
        {
            string name = "name";
            string pathfrom = "from";
            string pathto = "to";
            string sizefile = "sizefile";
            string transfertTime = "transfertTime";

            WriteLog(name, pathfrom, pathto, sizefile, transfertTime);
        }

        bool CreateLogFile()
        {
            // Formatting the date
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy");

            string filePath = @"C:\Users\jorda\source\repos\eystone\prosoft\EasySave\Log " + formattedDate + ".csv";

            try
            {
                // Create an empty file
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    WritingStream = new StreamWriter(filePath, true);
                    WritingStream.WriteLine("Horodatage; name; Chemin source; Chemin destination; Taille du fichier; Temps de transfert");
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

                    string filePath = @"C:\Users\jorda\source\repos\eystone\prosoft\EasySave\Log " + formattedDate + ".csv";

                    // Open the write stream to write to the file
                    WritingStream = new StreamWriter(filePath, true);

                    // Write log entries
                    WritingStream.WriteLine(DateTime.Now + " ; " + name + " ; " + pathfrom + " ; " + pathto + " ; " + sizefile + " ; " + transfertTime);

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