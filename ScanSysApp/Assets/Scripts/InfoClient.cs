using System;
using System.Text;
using System.Net.Sockets;

public class InfoClient
{
    private TcpClient client;

    public InfoClient(string address, int port)
	{
        client = new TcpClient(address, port);
	}

    public string SendMessage(string message)
	{
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
            responseData = completeMessage.ToString();
            return responseData;
        }
        finally
        {
            stream.Close();
            client.Close();
        }
	}
}
