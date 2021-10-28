using System;

namespace ScanSys
{
	/// <summary>
	/// Общий формат возвращаемой информации из IInfoCollector.
	/// </summary>
	public struct CollectedInfo
	{
		/// <summary>
		/// Строка с информацией без форматирования 
		/// (Заточено под конвертацию в нужный тип)
		/// </summary>
		public string Info { get; set; }

		/// <summary>
		/// Строка с форматированной информацией для удобного вывода.
		/// </summary>
		public string FormatedInfo { get; set; }
	}
}
