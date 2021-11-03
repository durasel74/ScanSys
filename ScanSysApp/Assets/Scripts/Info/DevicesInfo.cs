using System;
using System.Collections.Generic;

[Serializable]
public struct DevicesInfo
{
	public List<MonitorInfo> MonitorsInfo;
	public List<SoundCardInfo> SoundCardsInfo;
	public List<PnPInfo> PnPInfo;
}
