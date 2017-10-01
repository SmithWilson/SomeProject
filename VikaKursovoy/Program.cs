using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VikaKursovoy.Models;
using VikaKursovoy.Services;

namespace VikaKursovoy
{
	class Program
	{
		static async Task Main(string[] args)
		{
			#region Fields
			string path = Directory.GetCurrentDirectory();
			string fileProduct;
			string fileInfo;
			string fileResult;
			#endregion

			#region Method
			int action;

			//product.json
			Console.WriteLine("Имя файла для продуктов : ");
			fileProduct = Console.ReadLine();
			//info.json
			Console.WriteLine("Имя файла для информации : ");
			fileInfo = Console.ReadLine();
			//result.json
			Console.WriteLine("Имя файла итоговой информации : ");
			fileResult = Console.ReadLine();

			#region CommentedData
			//var list1 = new List<Product>
			//{
			//	new Product
			//	{
			//		Id = 1,
			//		Name = $"Item 1",
			//		Type = Enums.ProductType.Chancery,
			//		Price = 3231
			//	},
			//	new Product
			//	{
			//		Id = 2,
			//		Name = $"Item 2",
			//		Type = Enums.ProductType.Equipment,
			//		Price = 2123
			//	},
			//	new Product
			//	{
			//		Id = 3,
			//		Name = $"Item 3",
			//		Type = Enums.ProductType.Equipment,
			//		Price = 4324
			//	},
			//	new Product
			//	{
			//		Id = 4,
			//		Name = $"Item 4",
			//		Type = Enums.ProductType.Consumables,
			//		Price = 3214
			//	},
			//	new Product
			//	{
			//		Id = 5,
			//		Name = $"Item 5",
			//		Type = Enums.ProductType.Chancery,
			//		Price = 1123
			//	}
			//};

			//var list2 = new List<BaseInfo>
			//{
			//	new BaseInfo
			//	{
			//		Id = 1,
			//		Departament = "D1",
			//		Responsible = "R1"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 3,
			//		Departament = "D3",
			//		Responsible = "R3"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 5,
			//		Departament = "D5",
			//		Responsible = "R5"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 4,
			//		Departament = "D4",
			//		Responsible = "R4"
			//	}
			//};

			//await FileService.WriteObjectToFile<Product>(path, fileProduct, list1);
			//await FileService.WriteObjectToFile<BaseInfo>(path, fileInfo, list2);
			#endregion

			do
			{
				Console.WriteLine("0 - Выход, 1 - Считать продукт, 2 - Считать информацию, 3 - Создать выборку, 4 - Считать выборку.");
				action = int.Parse(Console.ReadLine());
				Console.WriteLine();
				switch (action)
				{
					case 0:
						Console.WriteLine("Нажмите любую клавишу чтобы выйти.");
						break;
					case 1:
						var listStudent = await FileService.ReadJsonFromFile<Product>(path, fileProduct);

						foreach (var item in listStudent)
						{
							Console.WriteLine(ModelService.GetPropertys(item));
						}

						Console.WriteLine();
						break;
					case 2:
						var listInfo = await FileService.ReadJsonFromFile<BaseInfo>(path, fileInfo);

						foreach (var item in listInfo)
						{
							Console.WriteLine(ModelService.GetPropertys(item));
						}

						Console.WriteLine();
						break;
					case 3:
						var selected = ModelService.CreateSelectionStudent(path, fileProduct, fileInfo, fileResult);

						foreach (var item in selected)
						{
							Console.WriteLine(ModelService.GetPropertys(item));
						}
						Console.WriteLine();

						Console.WriteLine();
						break;
					case 4:
						var listSelect = await FileService.ReadJsonFromFile<ResultFile>(path, fileResult);

						foreach (var item in listSelect)
						{
							Console.WriteLine(ModelService.GetPropertys(item));
						}
						Console.WriteLine();


						Console.WriteLine();
						break;
					default:
						break;
				}

			} while (action != 0);

			Console.ReadKey();
			#endregion
		}
	}
}
