using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private const string requestCommand = "system_info";
    private InfoClient infoClient;
    private string responseJson;
    private SystemInfo systemInfo;

    public int Port;
    public string Addres;

    void Start()
    {
        infoClient = new InfoClient(Addres, Port);
    }

    void Update()
    {
        UpdateSystemInfo();
        UpdateUI();
    }

    private void UpdateSystemInfo()
	{
		try
		{
            responseJson = infoClient.SendRequest(requestCommand);
            systemInfo = JsonUtility.FromJson<SystemInfo>(responseJson);
        }
        catch { }
    }

    private void UpdateUI()
	{
        UpdateWeatherUI();
        UpdateCpuUI();
        UpdateAudioUI();
        UpdateDevicesUI();
    }

    private void UpdateWeatherUI()
	{
        if (!systemInfo.WeatherInfo.IsErrorConnection)
        {
            Text text = GameObject.Find("TemperatureValue").GetComponent<Text>();
            text.text = ((int)systemInfo.WeatherInfo.TemperatureCelsius).ToString() + "℃";
            text = GameObject.Find("HumidityValue").GetComponent<Text>();
            text.text = systemInfo.WeatherInfo.Humidity.ToString() + "%";
            text = GameObject.Find("PressureValue").GetComponent<Text>();
            text.text = systemInfo.WeatherInfo.Pressure.ToString() + "mm";
        }
    }

    private void UpdateCpuUI()
	{
        if (!systemInfo.CPUInfo.IsError)
        {
            Text text = GameObject.Find("CPUValue").GetComponent<Text>();
            text.text = systemInfo.CPUInfo.Temperature.ToString() + "℃";
        }
    }

    private void UpdateAudioUI()
	{
        Text text = GameObject.Find("AudioValue").GetComponent<Text>();
        text.text = ((int)(systemInfo.AudioInfo)).ToString();
    }

    private void UpdateDevicesUI()
	{
        Text text = GameObject.Find("DevicesValue").GetComponent<Text>();

        text.text = "Монитор:\n";
        foreach (var monitor in systemInfo.DevicesInfo.MonitorsInfo)
		{
            if (monitor.Description != "") text.text += monitor.Description + "\n";
            if (monitor.Manufacturer != "") text.text += monitor.Manufacturer + "\n";
            if (monitor.ScreenWidth != "")
			{
                text.text += monitor.ScreenWidth + "x";
                text.text += monitor.ScreenHeight + "\n";
			}
            if (monitor.Status != "") text.text += monitor.Status + "\n";
            text.text += "\n";
		}

        text.text += "Звуковая карта:\n";
        foreach (var sound in systemInfo.DevicesInfo.SoundCardsInfo)
		{
            if (sound.Description != "") text.text += sound.Description + "\n";
            if (sound.Manufacturer != "") text.text += sound.Manufacturer + "\n";
            if (sound.Status != "") text.text += sound.Status + "\n";
            text.text += "\n";
        }

        text.text += "Камера:\n";
        foreach (var camera in systemInfo.DevicesInfo.PnPInfo)
		{
            if (camera.Description != "") text.text += camera.Description + "\n";
            if (camera.Manufacturer != "") text.text += camera.Manufacturer + "\n";
            if (camera.Status != "") text.text += camera.Status + "\n";
            text.text += "\n";
        }
    }
}
