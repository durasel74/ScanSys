using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using ScanSysLib;

namespace ScanSys
{
	/// <summary>
	/// Обновляет информацию о системе.
	/// </summary>
	public class InfoUpdater : INotifyPropertyChanged
	{
		private Timer timer;
		private SystemInfoCollector systemInfoCollector;
		private SystemInfo info;
		private string jsonInfo;

		public InfoUpdater()
		{
			systemInfoCollector = new SystemInfoCollector();
			TimerCallback tm = new TimerCallback(UpdateInfo);
			timer = new Timer(tm, null, 0, 200);
		}
		~InfoUpdater() => Dispose();
		public void Dispose() => timer.Dispose();

		public SystemInfo Info
		{
			get { return info; }
			set
			{
				info = value;
				OnPropertyChanged("Info");
			}
		}

		public string JsonInfo
		{
			get { return jsonInfo; }
			set
			{
				jsonInfo = value;
				OnPropertyChanged("JsonInfo");
			}
		}

		private void UpdateInfo(object obj)
		{
			Info = systemInfoCollector.GetInfo();
			JsonInfo = JsonSerializer.Serialize<SystemInfo>(info);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
