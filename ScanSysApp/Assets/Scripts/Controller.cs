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
        Text text;
        if (!systemInfo.WeatherInfo.IsErrorConnection)
		{
            text = GameObject.Find("TemperatureValue").GetComponent<Text>();
            text.text = ((int)systemInfo.WeatherInfo.TemperatureCelsius).ToString() + "℃";
            text = GameObject.Find("HumidityValue").GetComponent<Text>();
            text.text = systemInfo.WeatherInfo.Humidity.ToString() + "%";
            text = GameObject.Find("PressureValue").GetComponent<Text>();
            text.text = systemInfo.WeatherInfo.Pressure.ToString() + "mm";
        }
        if (!systemInfo.CPUInfo.IsError)
        {
            text = GameObject.Find("CPUValue").GetComponent<Text>();
            text.text = systemInfo.CPUInfo.Temperature.ToString() + "℃";
        }
        text = GameObject.Find("AudioValue").GetComponent<Text>();
        text.text = ((int)(systemInfo.AudioInfo)).ToString();
    }
}
