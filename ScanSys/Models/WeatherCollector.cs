using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

namespace ScanSys
{
	/// <summary>
	/// Собирает информацию о погоде.
	/// </summary>
	public class WeatherCollector : IInfoCollector
	{
		private CollectedInfo currentInfo;
		private string requestAdres;
		private Timer timer;

		public WeatherCollector(string cityId)
		{
			requestAdres = $"http://api.openweathermap.org/data/2.5/" +
				$"weather?id={cityId}&appid=f8a1d7712cac470533b69df495a5027a";
			TimerCallback tm = new TimerCallback(LoadWeather);
			timer = new Timer(tm, null, 0, 300_000); // 300 000 мс = 5 мин
		}
		~WeatherCollector() => Dispose();
		public void Dispose() => timer.Dispose();

		/// <summary>
		/// Возвращает информацию о погоде.
		/// </summary>
		/// <returns>Строки с информацией о погоде.</returns>
		public CollectedInfo GetInfo()
		{
			return currentInfo;
		}

		// <summary>
		/// Загружает JSON файл погоды через сеть.
		/// </summary>
		/// <returns>JSON строка.</returns>
		public async Task<string> RequestJsonAsync()
		{
			WebRequest request = WebRequest.Create(requestAdres);
			request.Method = "POST";
			request.ContentType = "application/x-www-urlencoded";

			string answer;
			using (WebResponse response = await request.GetResponseAsync())
			{
				using (Stream s = response.GetResponseStream())
				{
					using (StreamReader reader = new StreamReader(s))
						answer = await reader.ReadToEndAsync();
				}
			}
			return answer;
		}

		/// <summary>
		/// Собирает информацию о погоде из JSON строки.
		/// </summary>
		/// <param name="Json">JSON строка</param>
		/// <returns>Строки с информацией о погоде.</returns>
		public CollectedInfo BuildInfo(string Json)
		{
			using JsonDocument doc = JsonDocument.Parse(Json);
			JsonElement main = doc.RootElement.GetProperty("main");

			var temp = main.GetProperty("temp").GetDouble();
			var humidity = main.GetProperty("humidity").GetDouble();
			var pressure = main.GetProperty("pressure").GetDouble();

			var result = new CollectedInfo();
			result.Info = $"{temp} {humidity} {pressure}";
			result.FormatedInfo =
				$"Температура воздуха: {temp}\n" +
				$"Влажность воздуха: {humidity}\n" +
				$"Давление воздуха: {pressure}";
			return result;
		}

		private void LoadWeather(object obj)
		{
			try
			{
				var json = RequestJsonAsync().Result;
				var result = BuildInfo(json);
				currentInfo = result;
			}
			catch (WebException)
			{
				currentInfo.Info = "Ошибка подключения к сети.";
				currentInfo.FormatedInfo = "Ошибка подключения к сети.";
			}
			catch (JsonException)
			{
				currentInfo.Info = "Ошибка обработки JSON файла.";
				currentInfo.FormatedInfo = "Ошибка обработки JSON файла.";
			}
		}
	}
}
