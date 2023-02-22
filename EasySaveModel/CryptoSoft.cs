using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EasySaveModel
{
    public class CryptoSoft
    {
        Process processCryptoSoft = new Process();
        processCryptoSoft.StartInfo.FileName = "..\CryptoSoft\CryptoSoft\bin\Debug\net6.0\";
        processCryptoSoft.Start();
    }
}
