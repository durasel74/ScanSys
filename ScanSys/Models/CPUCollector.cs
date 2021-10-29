using System;
using System.Management;

namespace ScanSys
{
	/// <summary>
	/// Собирает информацию о центральном процессоре.
	/// </summary>
	public class CPUCollector
	{
		/// <summary>
		/// Возвращает текущую температуру процессора.
		/// </summary>
		/// <returns>Значение температуры в формате double или NaN, 
		/// если получить информацию невозможно.</returns>
		public double GetTemperature()
		{
			double temperature = 0;
			try
			{
				var searcher = new ManagementObjectSearcher(@"root\WMI",
					"SELECT * FROM MSAcpi_ThermalZoneTemperature");
				foreach (ManagementObject obj in searcher.Get())
				{
					string temp = obj["CurrentTemperature"].ToString();
					temperature = Convert.ToDouble(temp);
					switch (temp.Length)
					{
						case 3:
							temperature = temperature - 273;
							break;
						case 4:
							temperature = (temperature - 2732) / 10.0;
							break;
					}
				}
			}
			catch (ManagementException) { temperature = double.NaN; }
			return temperature;
		}
	}
}
