using System;
using System.Collections.Generic;
using System.Threading;
using System.Management;

namespace ScanSys
{
	/// <summary>
	/// Собирает информацию о подключенных устройствах компьютера.
	/// </summary>
	public class DevicesCollector : IDisposable
	{
		DevicesInfo devicesInfo;
		private Timer timer;

		public DevicesCollector()
		{
			devicesInfo = new DevicesInfo();
			TimerCallback tm = new TimerCallback(LoadDevicesInfo);
			timer = new Timer(tm, null, 0, 1_000);
		}
		~DevicesCollector() => Dispose();
		public void Dispose() => timer.Dispose();

		/// <summary>
		/// Возвращает информацию о подключенных устройствах.
		/// </summary>
		/// <returns>Структура с информацией о подключенных устройствах.</returns>
		public DevicesInfo GetInfo()
		{
			return devicesInfo;
		}

		/// <summary>
		/// Возвращает информацию о выбранном устройстве и его поле.
		/// </summary>
		/// <param name="WIN32_Class">Класс устройства.</param>
		/// <param name="ClassField">Поле класса устройства.</param>
		/// <returns>Список найденной информации.</returns>
		public List<string> GetWMIInfo(string WIN32_Class, string ClassField)
		{
			List<string> result = new List<string>();

			var searcher = new ManagementObjectSearcher($"SELECT * FROM {WIN32_Class}");
			try
			{
				foreach (ManagementObject obj in searcher.Get())
				{
					if (obj[ClassField] == null) return result;
					result.Add(obj[ClassField].ToString().Trim());
				}
			}
			catch (ManagementException) { return result; }
			return result;
		}
		
		/// <summary>
		/// Получает информацию о мониторах компьютера.
		/// </summary>
		/// <returns>Список информации о мониторах.</returns>
		public List<MonitorInfo> GetMonitorInfo()
		{
			const string WMIClass = "Win32_DesktopMonitor";
			List<MonitorInfo> result = new List<MonitorInfo>();
			List<string> monitorsField;

			monitorsField = GetWMIInfo(WMIClass, "Description");
			foreach (var name in monitorsField)
				result.Add(new MonitorInfo() { Description = name });

			monitorsField = GetWMIInfo(WMIClass, "MonitorManufacturer");
			for (int i = 0; i < monitorsField.Count; i++)
				result[i].Manufacturer = monitorsField[i];

			monitorsField = GetWMIInfo(WMIClass, "ScreenWidth");
			for (int i = 0; i < monitorsField.Count; i++)
				result[i].ScreenWidth = monitorsField[i];

			monitorsField = GetWMIInfo(WMIClass, "ScreenHeight");
			for (int i = 0; i < monitorsField.Count; i++)
				result[i].ScreenHeight = monitorsField[i];

			monitorsField = GetWMIInfo(WMIClass, "Status");
			for (int i = 0; i < monitorsField.Count; i++)
				result[i].Status = monitorsField[i];

			monitorsField = GetWMIInfo(WMIClass, "SystemName");
			for (int i = 0; i < monitorsField.Count; i++)
				result[i].SystemName = monitorsField[i];

			return result;
		}

		/// <summary>
		/// Получает информацию о звуковых картах компьютера.
		/// </summary>
		/// <returns>Список информации о звуковых картах.</returns>
		public List<SoundCardInfo> GetSoundCardInfo()
		{
			const string WMIClass = "Win32_SoundDevice";
			List<SoundCardInfo> result = new List<SoundCardInfo>();
			List<string> soundCardsField;

			soundCardsField = GetWMIInfo(WMIClass, "Description");
			foreach (var name in soundCardsField)
				result.Add(new SoundCardInfo() { Description = name });

			soundCardsField = GetWMIInfo(WMIClass, "Manufacturer");
			for (int i = 0; i < soundCardsField.Count; i++)
				result[i].Manufacturer = soundCardsField[i];

			soundCardsField = GetWMIInfo(WMIClass, "Status");
			for (int i = 0; i < soundCardsField.Count; i++)
				result[i].Status = soundCardsField[i];

			soundCardsField = GetWMIInfo(WMIClass, "SystemName");
			for (int i = 0; i < soundCardsField.Count; i++)
				result[i].SystemName = soundCardsField[i];

			return result;
		}

		/// <summary>
		/// Получает информацию о Plug and Play устройствах компьютера.
		/// </summary>
		/// <returns>Список информации об устройствах PnP.</returns>
		public List<PnPInfo> GetPnPInfo()
		{
			const string WMIClass = "Win32_PnPEntity";
			List<PnPInfo> result = new List<PnPInfo>();
			List<int> cameraIndex = new List<int>();
			List<string> PnPField;

			PnPField = GetWMIInfo(WMIClass, "PnPClass");
			foreach (var name in PnPField)
			{
				if (name == "Camera")
				{
					result.Add(new PnPInfo() { PnPClass = name });
					cameraIndex.Add(PnPField.IndexOf(name));
				}
			}

			PnPField = GetWMIInfo(WMIClass, "Description");
			for (int i = 0; i < cameraIndex.Count; i++)
				result[i].Description = PnPField[cameraIndex[i]];

			PnPField = GetWMIInfo(WMIClass, "Manufacturer");
			for (int i = 0; i < cameraIndex.Count; i++)
				result[i].Manufacturer = PnPField[cameraIndex[i]];

			PnPField = GetWMIInfo(WMIClass, "Status");
			for (int i = 0; i < cameraIndex.Count; i++)
				result[i].Status = PnPField[cameraIndex[i]];

			PnPField = GetWMIInfo(WMIClass, "SystemName");
			for (int i = 0; i < cameraIndex.Count; i++)
				result[i].SystemName = PnPField[cameraIndex[i]];

			return result;
		}

		private void LoadDevicesInfo(object obj)
		{
			devicesInfo.MonitorsInfo = GetMonitorInfo();
			devicesInfo.SoundCardsInfo = GetSoundCardInfo();
			devicesInfo.PnPInfo = GetPnPInfo();
		}
	}
}
