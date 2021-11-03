using System.Net;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const string requestCommand = "system_info";
    private InfoClient infoClient;
    private IPAddress localAddr;

    public int Port;
    public string Addres;

    void Start()
    {
        localAddr = IPAddress.Parse(Addres);
    }

    void Update()
    {
        infoClient = new InfoClient(Addres, Port);
        string response = infoClient.SendMessage(requestCommand);
        print(response);
    }
}
