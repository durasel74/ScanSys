using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private InfoServer infoServer;

		public ViewModel()
		{
			InfoUpdater = new InfoUpdater();
			infoServer = new InfoServer(InfoUpdater);
			infoServer.Start();
		}

		public InfoUpdater InfoUpdater { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
