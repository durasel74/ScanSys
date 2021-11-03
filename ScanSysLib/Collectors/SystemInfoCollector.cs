using System;

namespace ScanSysLib
{
	/// <summary>
	/// Собирает информацию о системе.
	/// </summary>
	public class SystemInfoCollector
	{
		WeatherCollector weatherCollector;
		CPUCollector cpuCollector;
		AudioCollector audioCollector;
		DevicesCollector devicesCollector;
		SystemInfo systemInfo;

		public SystemInfoCollector()
		{
			weatherCollector = new WeatherCollector("1508290"); // "1508290" = Челябинск
			cpuCollector = new CPUCollector();
			audioCollector = new AudioCollector();
			devicesCollector = new DevicesCollector();
		}

		/// <summary>
		/// Возвращает информацию о погоде.
		/// </summary>
		/// <returns>Структура с информацией о погоде.</returns>
		public SystemInfo GetInfo()
		{
			systemInfo.WeatherInfo = weatherCollector.GetInfo();
			systemInfo.CPUInfo = cpuCollector.GetInfo();
			systemInfo.AudioInfo = audioCollector.GetInfo();
			systemInfo.DevicesInfo = devicesCollector.GetInfo();
			return systemInfo;
		}
	}
}
