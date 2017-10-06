using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VikaKursovoy.Services;

namespace VikaKursovoy.Models
{
	/// <summary>
	/// Модель общей информации
	/// </summary>
	public class BaseInfo
	{
		private string _responsible;
		/// <summary>
		/// Регистрационный номер.
		/// </summary>
		[JsonProperty("id")]
		public int Id { get; set; }

		/// <summary>
		/// Отдел.
		/// </summary>
		[JsonProperty("department")]
		public string Departament { get; set; }

		/// <summary>
		/// Материально ответственное лицо.
		/// </summary>
		[JsonProperty("responsible")]
		public string Responsible { get; set; }
	}
}
