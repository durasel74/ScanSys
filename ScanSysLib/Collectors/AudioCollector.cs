using System;
using NAudio.Wave;

namespace ScanSysLib
{
	/// <summary>
	/// Собирает информацию по аудио части системы.
	/// </summary>
	public class AudioCollector : IDisposable
	{
		private double levelInfo;
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
		/// <returns>Значение уровня сигнала.</returns>
		public double GetInfo()
		{
			return levelInfo;
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
				levelInfo = level;
			}
		}
	}
}
