using System;
using System.IO;
using System.Diagnostics;

namespace CryptoSoft
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //Start timer
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Wrong arguments
            if (args.Length != 2 || File.Exists(args[0]) == false)
            {
                stopwatch.Stop();
                Console.Error.WriteLine("Wrong arguments : need file in and file out");
                return -1;
            }
            else
            {
                try
                {
                    //Get input
                    string OutputFile = args[1];
                    byte[] data = File.ReadAllBytes(args[0]);

                    //read key
                    byte[] key = File.ReadAllBytes(@".\key.txt");

                    //Do a XOR for each byte
                    for (int i = 0; i < data.Length; i++)
                    {
                        byte x = key[(byte)(i % key.Length)];
                        data[i] = (byte)(data[i] ^ x);
                    }
                    File.WriteAllBytes(OutputFile, data);

                    stopwatch.Stop();
                    return (int)(stopwatch.Elapsed.TotalSeconds * 1000); ;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                    return -1;
                }

            }
        }
    }
}