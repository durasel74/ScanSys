using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private InfoUpdater infoUpdater;
		private InfoServer infoServer;
		private string serverInfo;

		public ViewModel()
		{
			infoUpdater = new InfoUpdater();
			infoServer = new InfoServer(infoUpdater);
			infoServer.Start();
			ServerInfo += "Сервер запущен";
		}

		public string ServerInfo
		{
			get { return serverInfo; }
			set
			{
				serverInfo = value;
				OnPropertyChanged("ServerInfo");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
