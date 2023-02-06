using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave
{
    class TransfertStatesItems
    {
        private SaveFile m_file;
        private bool m_actualStates = false; //Active/Waiting
        private uint m_elapsedTransfertTime = 0;
        private uint m_remaingElapsedTime = 0;

        public TransfertStatesItems(SaveFile file)
        {
            m_file = file;
            //Calc Transfert Time ??
            m_remaingElapsedTime = file.TransfertTime;
        }

        public void MoveFromTo ()
        {
            string sourceFile = System.IO.Path.Combine(m_file.PathFrom, m_file.Name);
            string targetFile = System.IO.Path.Combine(m_file.PathTo, m_file.Name);

            m_actualStates = true;
            try
            {
                System.IO.File.Copy(sourceFile, targetFile);
            }
            catch (Exception e) { Console.Error.Write(e.ToString()); }
            m_actualStates = false;
        }

        public bool ActualStates { get => m_actualStates; set => m_actualStates = value; }
        public uint ElapsedTransfertTime { get => m_elapsedTransfertTime; set => m_elapsedTransfertTime = value; }
        public uint RemaingElapsedTime { get => m_remaingElapsedTime; set => m_remaingElapsedTime = value; }
        internal SaveFile File { get => m_file;}
    }
}
