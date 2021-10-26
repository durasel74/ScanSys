using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ScanSys
{
	/// <summary>
	/// Предоставляет данные о погоде.
	/// </summary>
	public class Weather
	{
		private double temp;
		private double humidity;
		private double pressure;

		public Weather(double temp, double humidity, double pressure)
		{
			this.temp = temp;
			this.humidity = humidity;
			this.pressure = pressure;
		}

		public double TempKelvin => temp;
		public double TempCelsius => temp - 273d;
		public double Humidity => humidity;
		public double Pressure => pressure;

		public override string ToString()
		{
			return $"temp: {TempCelsius}\nhumidity: {humidity}\npressure: {pressure}\n";
		}
	}
}
