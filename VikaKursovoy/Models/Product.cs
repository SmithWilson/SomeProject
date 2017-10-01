﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VikaKursovoy.Enums;

namespace VikaKursovoy.Models
{
	/// <summary>
	/// Модель продуктов.
	/// </summary>
	public class Product
	{
		/// <summary>
		/// Регистрационный номер.
		/// </summary>
		[JsonProperty("id")]
		public int Id { get; set; }

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
		/// Цена товара.
		/// </summary>
		[JsonProperty("price")]
		public float Price { get; set; }
	}
}
