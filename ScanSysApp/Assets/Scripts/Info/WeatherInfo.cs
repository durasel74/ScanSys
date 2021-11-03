using System;

[Serializable]
public struct WeatherInfo
{
	public bool IsErrorConnection;
	public double TemperatureKelvin;
	public double TemperatureCelsius;
	public double Humidity;
	public double Pressure;
}
