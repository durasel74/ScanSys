using System;

namespace ScanSys
{
	/// <summary>
	/// Предоставляет данные о погоде.
	/// </summary>
	public class WeatherInfo
	{
		private double temperature;
		private double humidity;
		private double pressure;

		public WeatherInfo(double temperature, double humidity, double pressure)
		{
			this.temperature = temperature;
			this.humidity = humidity;
			this.pressure = pressure;
		}

		public double TemperatureKelvin => temperature;
		public double TemperatureCelsius => temperature - 273d;
		public double Humidity => humidity;
		public double Pressure => pressure;

		public override string ToString()
		{
			return $"temp: {TemperatureCelsius}\nhumidity: {humidity}\npressure: {pressure}\n";
		}
	}
}
