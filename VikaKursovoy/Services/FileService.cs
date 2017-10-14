using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VikaKursovoy.Models;

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
				if (obj is Product prod && prod.Id == 0 || obj is BaseInfo info && info.Id == 0)
				{
					Console.WriteLine($"Обьект пуст. Тип : {obj.GetType()}");
				}
				var data = await ReadJsonFromFile<T>(path, fileName);

				if (data is List<Product> products && obj is Product product)
				{
					foreach (var p in products)
					{
						if (p.Id == product.Id || product.Id <= 0)
						{
							Console.WriteLine($"Недопустимый регистрационный номер : {product.Id}.");
							return new List<T>();
						}
					}
				}

				if (data is List<BaseInfo> baseInfo && obj is BaseInfo b)
				{
					foreach (var i in baseInfo)
					{
						if (i.Id == b.Id || b.Id <= 0)
						{
							Console.WriteLine($"Недопустимый регистрационный номер : {b.Id}.");
							return new List<T>();
						}
					}
				}

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
				if (obj is Product prod && prod.Id == 0 || obj is BaseInfo info && info.Id == 0)
				{
					Console.WriteLine($"Обьект пуст.");
					return new List<T>();
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
				using (var sw = new StreamWriter(path + "/" + fileName, false, System.Text.Encoding.UTF8))
				{
					if (!sw.BaseStream.CanWrite)
					{
						Console.WriteLine($"Ошибка записи в файл {path}/{fileName}.");
						return;
					}
					if (obj == null || obj.Count == 0)
					{
						Console.WriteLine($"Обьект пуст.");
						return;
					}
					if (!ValidedKey(obj))
					{
						return;
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
				using (var sr = new StreamReader(path + "/" + fileName, Encoding.UTF8))
				{
					if (!sr.BaseStream.CanRead)
					{
						Console.WriteLine($"Ошибка чтения файла {path}/{fileName}.");
						return new List<T>();
					}
					var json = await sr.ReadToEndAsync();

					var data = SerializationService.DeserializeJson<T>(json);

					if (data == null || data.Count == 0)
					{
						Console.WriteLine($"Считаный файл пуст. Имя файла : {fileName}");
						return new List<T>();
					}

					if (!ValidedKey(data))
					{
						
						return new List<T>();
					}

					if (data is List<BaseInfo> infos)
					{
						foreach (var info in infos)
						{
							if (!ValidedString(info.Responsible))
							{
								Console.WriteLine($"Поле «Материально ответсвтенное лицо», содержит недопустимые символы : {info.Responsible}");
								return new List<T>();
							} 
						}
						foreach (var info in infos)
						{
							if (!ValidedString(info.Departament, true))
							{
								Console.WriteLine($"Поле «Название отдела», содержит недопустимые символы : {info.Departament}");
								return new List<T>();
							}
						}
					}

					if (data is List<Product> products)
					{
						foreach (var product in products)
						{
							if (!ValidedString(product.Name))
							{
								Console.WriteLine($"Поле «Имя», содержит недопустимые символы : {product.Name}");
								return new List<T>();
							}
						}
					}

					return data;					
				}
			});
		}

		#region No-Public Method
		/// <summary>
		/// Проверка на уникальные значения при считывании.
		/// </summary>
		/// <typeparam name="T">Передаваемый тип.</typeparam>
		/// <param name="data">Коллекция данных типа Т.</param>
		/// <returns>Логическое значение.</returns>
		private static bool ValidedKey<T>(List<T> data)
		{
			if (data is List<Product> products)
			{
				foreach (var item in products)
				{
					if (products.Where(p => p.Id == item.Id).Count() > 1 || item.Id < 0)
					{
						Console.WriteLine($"Поле «регистрационный номер», содержит ошибку значения : {item.Id}.");
						return false;
					}
				}
				return true;
			}
			if (data is List<BaseInfo> info)
			{
				foreach (var item in info)
				{
					if (info.Where(p => p.Id == item.Id).Count() > 1 || item.Id < 0)
					{
						Console.WriteLine($"Поле «регистрационный номер», содержит ошибку значения : {item.Id}.");
						return false;
					}
				}
				return true;
			}

			if (data is List<ResultFile> result)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Проверка на содержание русского и латинского алфавита.
		/// </summary>
		/// <param name="value">Строка.</param>
		/// <returns>Логическое значение.</returns>
		private static bool ValidedString(string value)
		{
			var symbols = new char[] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-',
									  '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_',
									  '`', '{', '|', '}', '~', '0', '1', '2','3','4','5','6','7','8','9'};

			foreach (var symbol in symbols)
			{
				if (value.Contains(symbol))
				{
					return false;
				}
			}
			return true;
		}


		private static bool ValidedString(string value, bool flag)
		{
			var symbols = new char[] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-',
									  '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_',
									  '`', '{', '|', '}', '~'};

			foreach (var symbol in symbols)
			{
				if (value.Contains(symbol))
				{
					return false;
				}
			}
			return true;
		}
		#endregion
	}
}
