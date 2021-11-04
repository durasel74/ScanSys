using System;
using UnityEngine;

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
		print(systemInfo.AudioInfo);
	}

    private void UpdateSystemInfo()
	{
        responseJson = infoClient.SendRequest(requestCommand);
        systemInfo = JsonUtility.FromJson<SystemInfo>(responseJson);
    }
}
