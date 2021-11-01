using System;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ScanSysLib;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private Timer timer;
		private string testText;
		ScanSysLib.SystemInfoCollector systemInfoCollector;

		public ViewModel()
		{
			systemInfoCollector = new ScanSysLib.SystemInfoCollector();
			TimerCallback tm = new TimerCallback(UpdateInfo);
			timer = new Timer(tm, null, 0, 200);
		}
		~ViewModel() => Dispose();
		public void Dispose() => timer.Dispose();

		public void UpdateInfo(object obj)
		{
			TestText = "";
			ScanSysLib.SystemInfo info = systemInfoCollector.GetInfo();
			WeatherInfo weatherInfo = info.WeatherInfo;
			DevicesInfo devicesInfo = info.DevicesInfo;

			TestText += weatherInfo.TemperatureCelsius + "\n";
			TestText += weatherInfo.Humidity + "\n";
			TestText += weatherInfo.Pressure + "\n";
			TestText += info.CPUInfo + "\n";
			TestText += info.AudioInfo + "\n";
			TestText += devicesInfo.MonitorsInfo + "\n";
			TestText += devicesInfo.SoundCardsInfo + "\n";
			TestText += devicesInfo.PnPInfo + "\n";
		}

		public string TestText
		{
			get { return testText; }
			set
			{
				testText = value;
				OnPropertyChanged("TestText");
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
