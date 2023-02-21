using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RemoteConsole;

namespace RemoteConsole
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mw = new MainWindow();
        protected override void OnStartup(StartupEventArgs e)
        {
                BackgroundWorker Job = new BackgroundWorker();
                Job.WorkerReportsProgress = true;
                Job.DoWork += mw.Job_work;
                Job.ProgressChanged += mw.Job_ProgressChanged;
                Job.RunWorkerAsync();
                mw.State = true;
                base.OnStartup(e);
        }
    }
}
