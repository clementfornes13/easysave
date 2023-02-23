using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace RemoteConsole
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public bool State = false; 
		private BackgroundWorker _backgroundWorker;  
 
		public MainWindow()
		{
			InitializeComponent();
			//Create a worker for gettings progression
			_backgroundWorker = new BackgroundWorker();
			//_backgroundWorker.DoWork += new DoWorkEventHandler(Job_work);
			//_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(Job_ProgressChanged);
			//_backgroundWorker.WorkerReportsProgress = true;

			//Run it
			//_backgroundWorker.RunWorkerAsync();
		}

		// Méthode qui initialise la barre de progression 
		public void Job_work(object sender, DoWorkEventArgs e)
		{
			State = true;
			for (int i = 1; i <= 100; i++)
			{
				(sender as BackgroundWorker).ReportProgress(i);

				Thread.Sleep(50);
			}
			State = false;
		}
		public void Job_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//initialisation de la barre de progression avec le pourcentage de progression
			ProgressBarJobStatus.Value = e.ProgressPercentage;

			//Affichage de la progression sur un label
			lb_etat_prog_server.Content = ProgressBarJobStatus.Value.ToString() + "%";
		}

		// lancer la barre de progression en créant un objet de type BackgroundWorker
		//BackgroundWorker :
	}
	
}
