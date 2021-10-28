using System;
using NAudio.Wave;

namespace ScanSys
{
	/// <summary>
	/// Собирает информацию по аудио части системы.
	/// </summary>
	public class AudioCollector : IInfoCollector
	{
		private CollectedInfo currentInfo;
		private WaveInEvent waveIn;

		public AudioCollector()
		{
			waveIn = new WaveInEvent();
			waveIn.DataAvailable += WaveOnDataAvailable;
			waveIn.WaveFormat = new WaveFormat(8000, 1);
			waveIn.StartRecording();
		}
		~AudioCollector() => Dispose();
		public void Dispose() => waveIn.StopRecording();

		/// <summary>
		/// Возвращает информацию об уровне сигнала микрофона.
		/// </summary>
		/// <returns>Строки с уровнем сигнала.</returns>
		public CollectedInfo GetInfo()
		{
			return currentInfo;
		}

		private void WaveOnDataAvailable(object sender, WaveInEventArgs e)
		{
			short sample;
			float amplitude;
			float level;
			for (int index = 0; index < e.BytesRecorded; index += 2)
			{
				sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);
				amplitude = sample / 32768f;
				level = Math.Abs(amplitude) * 100;
				currentInfo = new CollectedInfo()
				{
					Info = level.ToString(),
					FormatedInfo = level.ToString()
				};
			}
		}
	}
}
