using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemoteConsole
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public bool State = false;

		public MainWindow()
		{
			InitializeComponent();
		}

		// Méthode qui initialise la barre de progression 
		public void Job_work(object sender, DoWorkEventArgs e)
		{
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
