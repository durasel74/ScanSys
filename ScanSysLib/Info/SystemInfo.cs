using System;

namespace ScanSysLib
{
	public struct SystemInfo
	{
		public WeatherInfo WeatherInfo { get; set; }
		public CPUInfo CPUInfo { get; set; }
		public double AudioInfo { get; set; }
		public DevicesInfo DevicesInfo { get; set; }
	}
}
