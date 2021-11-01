using System;
using System.Collections.Generic;

namespace ScanSysLib
{
	public struct DevicesInfo
	{
		public List<MonitorInfo> MonitorsInfo { get; set; }
		public List<SoundCardInfo> SoundCardsInfo { get; set; }
		public List<PnPInfo> PnPInfo { get; set; }
	}
}
