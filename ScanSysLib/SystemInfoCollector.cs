using System;

namespace ScanSysLib
{
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

		public SystemInfo GetInfo()
		{
			systemInfo.WeatherInfo = weatherCollector.GetInfo();
			systemInfo.CPUInfo = cpuCollector.GetTemperature();
			systemInfo.AudioInfo = audioCollector.GetInfo();
			systemInfo.DevicesInfo = devicesCollector.GetInfo();
			return systemInfo;
		}
	}
}
