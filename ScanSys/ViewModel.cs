using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Management;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private string testText;
		private WeatherInfoLoader weatherLoader;
		private CPUInfoLoader CPULoader;
		private WeatherInfo weather;
		private CPUInfo CPUInfo;

		public ViewModel()
		{
			weatherLoader = new WeatherInfoLoader("1508290");
			weatherLoader.LoadEventHandler += UpdateWeather;
			weatherLoader.LoadAsync();
			CPULoader = new CPUInfoLoader();
			CPUInfo = CPULoader.GetInfo();
		}

		public void UpdateWeather()
		{
			weather = weatherLoader.GetResult();
			TestText = weather.ToString();
			TestText += CPUInfo.ToString();
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
