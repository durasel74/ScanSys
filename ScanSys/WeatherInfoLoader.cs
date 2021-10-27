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
	public class WeatherInfoLoader
	{
		private string cityId;
		private string requestAdres;
		private WeatherInfo weatherInfo;
		private bool loadResult;

		public WeatherInfoLoader(string cityId)
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
			WeatherInfo loadedWeather;
			loadResult = false;
			try
			{
				json = await RequestJsonAsync();
				loadedWeather = BuildInfo(json);
				weatherInfo = loadedWeather;
				loadResult = true;
				LoadEventHandler?.Invoke();
				return true;
			}
			catch(WebException) { return false; }
			catch (JsonException) { return false; }
		}

		/// <summary>
		/// Возвращает экземпляр информации о погоде (Возможно только после 
		/// запуска Load()).
		/// </summary>
		/// <returns>Экземпляр погоды.</returns>
		public WeatherInfo GetResult()
		{
			if (loadResult) return this.weatherInfo;
			else throw new ApplicationException("Не удалось загрузить информацию о погоде");
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
		public WeatherInfo BuildInfo(string Json)
		{
			using JsonDocument doc = JsonDocument.Parse(Json);
			JsonElement main = doc.RootElement.GetProperty("main");

			var temp = main.GetProperty("temp").GetDouble();
			var humidity = main.GetProperty("humidity").GetDouble();
			var pressure = main.GetProperty("pressure").GetDouble();

			WeatherInfo weather = new WeatherInfo(temp, humidity, pressure);
			return weather;
		}
	}
}
