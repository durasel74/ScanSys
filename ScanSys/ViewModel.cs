using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private string testText;
		private Weather weather;
		private WeatherLoader loader;

		public ViewModel()
		{
			loader = new WeatherLoader("1508290");
			loader.LoadEventHandler += UpdateWeather;
			loader.LoadAsync();
		}

		public void UpdateWeather()
		{
			weather = loader.GetResult();
			TestText = weather.ToString();
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
