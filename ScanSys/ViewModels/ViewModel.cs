using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private string testText;

		WeatherCollector weatherCollector;
		CPUCollector cpuCollector;
		AudioCollector audioCollector;
		DevicesCollector devicesCollector;

		public ViewModel()
		{
			weatherCollector = new WeatherCollector("1508290"); // "1508290" = Челябинск
			cpuCollector = new CPUCollector();
			audioCollector = new AudioCollector();
			devicesCollector = new DevicesCollector();
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			TestText += weatherCollector.GetInfo();
			TestText += cpuCollector.GetTemperature();
			TestText += audioCollector.GetInfo();
			TestText += devicesCollector.GetInfo();
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
