using System;

namespace ScanSys
{
	/// <summary>
	/// Предоставляет данные о процессоре.
	/// </summary>
	public class CPUInfo
	{
		private double temperature;

		public CPUInfo(double temperature)
		{
			this.temperature = temperature;
		}

		public double TemperatureCelsius => temperature;

		public override string ToString()
		{
			string output = "Temperature: ";
			if (double.IsNaN(temperature)) output += "Нет данных";
			else output += temperature;
			return output;
		}
	}
}
