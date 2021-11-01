using System;

namespace ScanSysLib
{
	public struct SystemInfo
	{
		public WeatherInfo WeatherInfo { get; set; }
		public double CPUInfo { get; set; }
		public double AudioInfo { get; set; }
		public DevicesInfo DevicesInfo { get; set; }
	}
}
