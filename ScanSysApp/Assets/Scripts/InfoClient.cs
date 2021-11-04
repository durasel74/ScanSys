using System;
using System.Text;
using System.Net.Sockets;

public class InfoClient
{
    private string addres;
    private int port;
    private TcpClient client;

    public InfoClient(string addres, int port)
	{
        this.addres = addres;
        this.port = port;
	}

    public string SendRequest(string message)
	{
        client = new TcpClient(addres, port);
        Byte[] data = Encoding.UTF8.GetBytes(message);
        NetworkStream stream = client.GetStream();
        try
        {
            stream.Write(data, 0, data.Length);

            Byte[] readingData = new Byte[256];
            String responseData = String.Empty;
            StringBuilder completeMessage = new StringBuilder();
            int numberOfBytesRead = 0;
            do
            {
                numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
            }
            while (stream.DataAvailable);
            return completeMessage.ToString();
        }
        finally
        {
            stream.Close();
            client.Close();
        }
	}
}
