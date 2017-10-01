using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VikaKursovoy.Models;

namespace VikaKursovoy.Services
{
	public static class ModelService
	{
		/// <summary>
		/// Получение полей(Перегрузка).
		/// </summary>
		/// <param name="product"><see cref="Product"/></param>
		/// <returns>Строка.</returns>
		public static string GetPropertys(Product product)
			=> Task.Run(() => $"{product.Id}	{product.Name}	{product.Type}	{product.Price}").Result;

		/// <summary>
		/// Получение полей(Перегрузка).
		/// </summary>
		/// <param name="baseInfo"><see cref="BaseInfo"/></param>
		/// <returns>Строка.</returns>
		public static string GetPropertys(BaseInfo baseInfo)
			=> Task.Run(() => $"{baseInfo.Id}	{baseInfo.Responsible}	{baseInfo.Departament}").Result;

		/// <summary>
		/// Получение полей(Перегрузка).
		/// </summary>
		/// <param name="result"><see cref="ResultFile"/></param>
		/// <returns>Строка.</returns>
		public static string GetPropertys(ResultFile result)
			=> Task.Run(() => $"{result.Name}	{result.Department}	{result.Type}	{result.Price}").Result;

		/// <summary>
		/// Создание итоговой таблицы.
		/// </summary>
		/// <param name="path">Путь к файлу.</param>
		/// <param name="fileProduct">Имя файла с продуктами.</param>
		/// <param name="fileResult">Имя файла с основной информацией.</param>
		/// <returns>Список.</returns>
		public static List<ResultFile> CreateSelectionStudent(string path, string fileProduct, string fileInfo, string fileResult)
		{
			return Task.Run(async () =>
			{
				var result = new List<ResultFile>();
				var products = await FileService.ReadJsonFromFile<Product>(path, fileProduct);
				var info = await FileService.ReadJsonFromFile<BaseInfo>(path, fileInfo);

				if (products.Count == 0)
				{
					throw new ArgumentNullException(nameof(products));
				}
				if (info.Count == 0)
				{
					throw new ArgumentNullException(nameof(info));
				}

				foreach (var product in products)
				{
					foreach (var i in info)
					{
						if (product.Id == i.Id)
						{
							result.Add(new ResultFile
							{
								Type = product.Type,
								Department = i.Departament,
								Name = product.Name,
								Price = product.Price
							});
						}
					}
				}

				GetResultPrice(result);

				await FileService.WriteObjectToFile(path, fileResult, result);

				return result;
			}).Result;
		}

		public static void GetResultPrice(List<ResultFile> list)
		{
				Console.WriteLine("Канцелярия : " + list.Where(s => s.Type == Enums.ProductType.Chancery).Sum(s => s.Price));

				Console.WriteLine("Расходные материалы : " + list.Where(s => s.Type == Enums.ProductType.Consumables).Sum(s => s.Price));

				Console.WriteLine("Оборудоние : " + list.Where(s => s.Type == Enums.ProductType.Equipment).Sum(s => s.Price));	
		}
	}
}
