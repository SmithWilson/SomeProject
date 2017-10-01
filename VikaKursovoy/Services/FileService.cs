using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikaKursovoy.Services
{
	/// <summary>
	/// Сервис для работы с файлами.
	/// </summary>
	public static class FileService
	{
		/// <summary>
		/// Добавление одного обьекта в конец.
		/// </summary>
		/// <typeparam name="T">Передаваемый тип.</typeparam>
		/// <param name="path">Путь к файлу.</param>
		/// <param name="fileName">Имя файла.</param>
		/// <param name="obj">Обьект.</param>
		/// <returns>Задача. (Используйте await, чтобы дождаться List<typeparamref name="T"/></returns>
		public static Task<List<T>> AddObjectToFile<T>(string path, string fileName, T obj)
		{
			return Task.Run(async () =>
			{
				if (obj == null)
				{
					throw new ArgumentNullException(nameof(obj), $"Обьект пуст. Тип : {obj.GetType()}");
				}
				var data = await ReadJsonFromFile<T>(path, fileName);
				data.Add(obj);

				await WriteObjectToFile(path, fileName, data);

				return data;
			});
		}

		/// <summary>
		/// Удаление обькта из списка.
		/// </summary>
		/// <typeparam name="T">Передаваемый тип.</typeparam>
		/// <param name="path">путь к файлу.</param>
		/// <param name="fileName">Имя файла.</param>
		/// <param name="obj">Обьект.</param>
		/// <returns>Задача. (Используйте await, чтобы дождаться List<typeparamref name="T"/></returns>
		public static Task<List<T>> DeleteObjectFromFile<T>(string path, string fileName, T obj)
		{
			return Task.Run(async () =>
			{
				if (obj == null)
				{
					throw new ArgumentNullException(nameof(obj), $"Обьект пуст. Тип : {obj.GetType()}");
				}
				var data = await ReadJsonFromFile<T>(path, fileName);
				data.Remove(obj);

				await WriteObjectToFile(path, fileName, data);

				return data;
			});

		}

		/// <summary>
		/// Запись в файл данных.
		/// </summary>
		/// <typeparam name="T">Передаваемый тип.</typeparam>
		/// <param name="path">Путь к файлу.</param>
		/// <param name="fileName">Имя файла.</param>
		/// <param name="obj">Обьект.</param>
		/// <returns>Задача.</returns>
		public static Task WriteObjectToFile<T>(string path, string fileName, List<T> obj)
		{
			return Task.Run(async () =>
			{
				using (var sw = new StreamWriter(path + "/" + fileName, false, System.Text.Encoding.Default))
				{
					if (!sw.BaseStream.CanWrite)
					{
						throw new ArgumentNullException(nameof(obj), $"Ошибка записи в файл {path}/{fileName}.");
					}
					if (obj.Count == 0)
					{
						throw new ArgumentNullException(nameof(obj), $"Обьект пуст. Тип : {obj.GetType()}");
					}

					var json = SerializationService.SerializationObject(obj);

					await sw.WriteAsync(json);
				}
			});
		}

		/// <summary>
		/// Чтение из файла данных.
		/// </summary>
		/// <typeparam name="T">Передаваемый тип.</typeparam>
		/// <param name="path">Путь к файлу.</param>
		/// <param name="fileName">Имя файла.</param>
		/// <returns>Задача. (Используйте await, чтобы дождаться List<typeparamref name="T"/></returns>
		public static Task<List<T>> ReadJsonFromFile<T>(string path, string fileName)
		{
			return Task.Run(async () =>
			{
				using (var sr = new StreamReader(path + "/" + fileName))
				{
					if (!sr.BaseStream.CanRead)
					{
						throw new ArgumentNullException($"Ошибка чтения файла {path}/{fileName}.");
					}
					var json = await sr.ReadToEndAsync();

					var data = SerializationService.DeserializeJson<T>(json);

					if (data.Count == 0)
					{
						throw new Exception($"Считаный файл пуст. Принадлежит типу {data.GetType()}");
					}

					return data;
				}
			});
		}
	}
}
