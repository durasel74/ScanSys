using System.Collections.Generic;

public struct DevicesInfo
{
	public List<MonitorInfo> MonitorsInfo { get; set; }
	public List<SoundCardInfo> SoundCardsInfo { get; set; }
	public List<PnPInfo> PnPInfo { get; set; }
}
