using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VikaKursovoy.Enums;

namespace VikaKursovoy.Models
{
	/// <summary>
	/// Модель итогового файла.
	/// </summary>
	public class ResultFile
	{
		/// <summary>
		/// Тип товара.
		/// </summary>
		[JsonProperty("type")]
		public ProductType Type { get; set; }

		/// <summary>
		/// Наименование товара.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Отдел.
		/// </summary>
		[JsonProperty("department")]
		public string Department { get; set; }

		/// <summary>
		/// Цена.
		/// </summary>
		[JsonProperty("price")]
		public float Price { get; set; }
	}
}
