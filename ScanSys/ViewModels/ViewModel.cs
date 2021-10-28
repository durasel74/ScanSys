using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScanSys
{
	public class ViewModel : INotifyPropertyChanged
	{
		private string testText;
		private List<IInfoCollector> collectors;

		public ViewModel()
		{
			collectors = new List<IInfoCollector>()
			{
				new WeatherCollector("1508290"), // "1508290" = Челябинск
				new CPUCollector(),
				new AudioCollector(),
			};
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			foreach (var collector in collectors)
				TestText += collector.GetInfo().FormatedInfo + "\n";
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
