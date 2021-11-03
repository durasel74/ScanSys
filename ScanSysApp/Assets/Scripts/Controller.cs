using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const string requestCommand = "system_info";
    private InfoClient infoClient;

    public int Port;
    public string Addres;

    void Start()
    {

    }

    void Update()
    {
		infoClient = new InfoClient(Addres, Port);
		string response = infoClient.SendMessage(requestCommand);
        SystemInfo systemInfo = JsonUtility.FromJson<SystemInfo>(response);
		print(systemInfo.AudioInfo);
	}
}
