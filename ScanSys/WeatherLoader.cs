using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

namespace ScanSys
{
	/// <summary>
	/// Управляет загрузкой данных о пагоде.
	/// </summary>
	public class WeatherLoader
	{
		private string cityId;
		private string requestAdres;
		private Weather weather;
		private bool loadResult;

		public WeatherLoader(string cityId)
		{
			this.cityId = cityId;
			requestAdres = $"http://api.openweathermap.org/data/2.5/" +
				$"weather?id={cityId}&appid=f8a1d7712cac470533b69df495a5027a";
			loadResult = false;
		}

		public delegate void LoadHandler();
		/// <summary>
		/// Сообщает об успешной загрузке информации.
		/// </summary>
		public event LoadHandler LoadEventHandler;

		/// <summary>
		/// Загружает информацию о погоде и сохраняет в объект асинхронно.
		/// </summary>
		/// <returns>Удалось ли получить и сохранить информацию.</returns>
		public async Task<bool> LoadAsync()
		{
			string json;
			Weather loadedWeather;
			loadResult = false;
			try
			{
				json = await RequestJsonAsync();
				loadedWeather = BuildWeather(json);
				weather = loadedWeather;
				loadResult = true;
				LoadEventHandler?.Invoke();
				return true;
			}
			catch(WebException) { return false; }
			catch (JsonException) { return false; }
		}

		/// <summary>
		/// Возвращает экземпляр погоды (Возможно только после запуска Load()).
		/// </summary>
		/// <returns>Экземпляр погоды.</returns>
		public Weather GetResult()
		{
			if (loadResult) return this.weather;
			else throw new ApplicationException("Не удалось загрузить информацию о пагоде");
		}


		/// <summary>
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
		/// Создает объект погоды на основе JSON строки.
		/// </summary>
		/// <param name="Json">JSON строка.</param>
		/// <returns>Экземпляр данных о погоде.</returns>
		public Weather BuildWeather(string Json)
		{
			using JsonDocument doc = JsonDocument.Parse(Json);
			JsonElement main = doc.RootElement.GetProperty("main");

			var temp = main.GetProperty("temp").GetDouble();
			var humidity = main.GetProperty("humidity").GetDouble();
			var pressure = main.GetProperty("pressure").GetDouble();

			Weather weather = new Weather(temp, humidity, pressure);
			return weather;
		}
	}
}
