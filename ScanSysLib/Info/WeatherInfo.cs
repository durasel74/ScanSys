using System;

namespace ScanSysLib
{
	public struct WeatherInfo
	{
		public bool IsErrorConnection { get; set; }
		public double TemperatureKelvin { get; set; }
		public double TemperatureCelsius => TemperatureKelvin - 273d;
		public double Humidity { get; set; }
		public double Pressure { get; set; }
	}
}
