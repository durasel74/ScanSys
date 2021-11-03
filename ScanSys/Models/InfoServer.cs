using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ScanSys
{
    /// <summary>
    /// Сервер для отправки информации о системе клиенту.
    /// </summary>
	public class InfoServer
    {
        private const string requestCommand = "system_info";
        private const int PORT = 8888;
        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        private InfoUpdater infoUpdater;
        private TcpListener server;
        private Task task;

        public InfoServer(InfoUpdater updater)
		{
            infoUpdater = updater;
            task = new Task(RunServer);
        }
        ~InfoServer() => Dispose();
        public void Dispose()
        {
            server.Stop();
            task.Dispose();
        }

        /// <summary>
        /// Запускает работу сервера.
        /// </summary>
		public void Start()
		{
            task.Start();
		}

        private void RunServer()
        {
            server = new TcpListener(localAddr, PORT);
            server.Start();

            while (true)
            {
                try { ReadRequest(); }
                catch
                {
                    server.Stop();
                    break;
                }
            }
        }

        private void ReadRequest()
		{
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            try
            {
                if (stream.CanRead)
                {
                    byte[] readBuffer = new byte[1024];
                    StringBuilder completeMessage = new StringBuilder();
                    do
                    {
                        int bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                        completeMessage.AppendFormat("{0}", 
                            Encoding.UTF8.GetString(readBuffer, 0, bytesRead));
                    }
                    while (stream.DataAvailable);
                    if (completeMessage.ToString() == requestCommand)
                        SendInfo(stream);
                }
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        private void SendInfo(NetworkStream stream)
		{
            Byte[] responseData = Encoding.UTF8.GetBytes(infoUpdater.JsonInfo);
            stream.Write(responseData, 0, responseData.Length);
        }
    }
}
