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
			var path = Directory.GetCurrentDirectory();
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
			//		Name = "Ручка",
			//		Type = Enums.ProductType.Stationery,
			//		Price = 33.50
			//	},
			//	new Product
			//	{
			//		Id = 2,
			//		Name = "Молоко",
			//		Type = Enums.ProductType.Foodstuffs,
			//		Price = 70.60
			//	},
			//	new Product
			//	{
			//		Id = 3,
			//		Name = "Мыло",
			//		Type = Enums.ProductType.HouseholdGoods,
			//		Price = 20.50
			//	},
			//	new Product
			//	{
			//		Id = 4,
			//		Name = "Крем для рук",
			//		Type = Enums.ProductType.HouseholdGoods,
			//		Price = 70
			//	},
			//	new Product
			//	{
			//		Id = 5,
			//		Name = "Плюшевый мишка",
			//		Type = Enums.ProductType.Toys,
			//		Price = 150
			//	},
			//	new Product
			//	{
			//		Id = 6,
			//		Name = "Пазл",
			//		Type = Enums.ProductType.Toys,
			//		Price = 100.5
			//	},
			//	new Product
			//	{
			//		Id = 7,
			//		Name = "Тетрис",
			//		Type = Enums.ProductType.Toys,
			//		Price = 300.6
			//	},
			//	new Product
			//	{
			//		Id = 8,
			//		Name = "Мозаика",
			//		Type = Enums.ProductType.Toys,
			//		Price = 120
			//	},
			//	new Product
			//	{
			//		Id = 9,
			//		Name = "Бумага",
			//		Type = Enums.ProductType.Stationery,
			//		Price = 155.9
			//	},
			//	new Product
			//	{
			//		Id = 10,
			//		Name = "Карандаш",
			//		Type = Enums.ProductType.Stationery,
			//		Price = 20
			//	}
			//};

			//var list2 = new List<BaseInfo>
			//{
			//	new BaseInfo
			//	{
			//		Id = 1,
			//		Departament = "Отдел N3",
			//		Responsible = "Романова К.Л"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 2,
			//		Departament = "Отдел N2",
			//		Responsible = "Сорокина Т.Е"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 3,
			//		Departament = "Отдел N4",
			//		Responsible = "Иванова Г.А"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 4,
			//		Departament = "Отдел N4",
			//		Responsible = "Иванова Г.А"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 5,
			//		Departament = "Отдел N1",
			//		Responsible = "Попова В.И."
			//	},
			//	new BaseInfo
			//	{
			//		Id = 6,
			//		Departament = "Отдел N1",
			//		Responsible = "Попова В.И."
			//	},
			//	new BaseInfo
			//	{
			//		Id = 7,
			//		Departament = "Отдел N1",
			//		Responsible = "Попова В.И."
			//	},
			//	new BaseInfo
			//	{
			//		Id = 8,
			//		Departament = "Отдел N1",
			//		Responsible = "Попова В.И."
			//	},
			//	new BaseInfo
			//	{
			//		Id = 9,
			//		Departament = "Отдел N3",
			//		Responsible = "Романова К.Л"
			//	},
			//	new BaseInfo
			//	{
			//		Id = 10,
			//		Departament = "Отдел N3",
			//		Responsible = "Романова К.Л"
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
