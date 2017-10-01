using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VikaKursovoy.Services
{
	public static class SerializationService
	{
		/// <summary>
		/// Сериализация обьекта.
		/// </summary>
		/// <param name="obj">Обьект.</param>
		/// <returns>Json.</returns>
		public static string SerializationObject(object obj)
			=> JsonConvert.SerializeObject(obj);

		/// <summary>
		/// Десериализация обьекта.
		/// </summary>
		/// <typeparam name="T">Передаваемый тип.</typeparam>
		/// <param name="json">Json.</param>
		/// <returns>Список обьектов <typeparamref name="T"/>.</returns>
		public static List<T> DeserializeJson<T>(string json)
			=> JsonConvert.DeserializeObject<List<T>>(json);
	}
}
