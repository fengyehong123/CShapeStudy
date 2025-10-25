using System;
using System.Collections.Generic;
using System.Linq;

// 参考资料：
// https://www.cnblogs.com/dullfish/p/6101912.html

/*
 * 🔴C# 的 LINQ（Language Integrated Query，语言集成查询） 是一种非常强大的功能
 * 可以让开发者像写 SQL 一样去查询、筛选、排序、分组集合数据。
 * 
 * 🔴LINQ 是一组扩展方法，主要用于对集合（如 List<T>、Array、Dictionary、DataTable、XML、JSON 等）进行：
 *   筛选（Where）
 *   排序（OrderBy）
 *   分组（GroupBy）
 *   投影（Select）
 *   聚合（Count、Sum、Max、Min、Average）
 *   连接（Join）
 *   去重（Distinct）
 * 使用它可以让代码更简洁、表达更清晰。
 * 
 * LINQ 查询在定义时不会立刻执行，只有当你遍历或调用 .ToList()、.Count() 等方法时才会执行。
 */
namespace ConsoleApp1._16_Linq表达式
{
	// 定义一个类
	public class Person
	{
		public required string Name { get; set; }
		public int Age { get; set; }
	}

	public class _01_Linq基本使用
	{
		// 创建一个int集合
		private static readonly List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];

		// 创建一个Person集合 
		private static readonly List<Person> peopleList =
		[
			// 方式一: 初始化集合元素
			new() { Name = "Tom", Age = 25 },
			new() { Name = "Jerry", Age = 18 },

			// 方式二: 初始化集合元素
			new Person { Name = "Alice", Age = 30 },
			new Person { Name = "Jack", Age = 48 }
		];

		// ---------------------------------------------------------------------------------------
		// 用来过滤偶数的lambda函数
		private static readonly Func<int, bool> evenFilter = (n) => n % 2 == 0;
		// ---------------------------------------------------------------------------------------

		public static void Where筛选() 
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 🔴获取偶数
			List<int> numList1 = [.. numbers.Where(n => n % 2 == 0)];
			List<int> numList2 = [.. numbers.Where(evenFilter)];
			Console.WriteLine(string.Join(" ", numList2));  // 2_4_6_8

			// 🔴获取出 > 20 岁的人
			List<Person> adultList = [.. peopleList.Where(p => p.Age > 20)];
			adultList.ForEach(item => Console.WriteLine($"{item.Name} -> {item.Age}"));
		}

		// 类似于java中的map
		public static void Select投影()
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 🔴将元素中的数字转换为字符串
			List<string> strList = [.. numbers.Select(num => num.ToString())];
			Console.WriteLine(string.Join(" ", strList));

			// 🔴只获取出Person对象中的name
			List<string> nameList = [.. peopleList.Select(item => item.Name)];
			Console.WriteLine(string.Join("\n", nameList));

			// 🔴映射为新的匿名类型
			var objList = peopleList.Select(p => new { p.Name, IsAdult = p.Age >= 20});
			foreach (var item in objList)
			{
				Console.WriteLine($"{item.Name} 成人: {item.IsAdult}");
			}

			// 🔴转换为字典类型
			Dictionary<string, bool> personDict = peopleList.Select(p => new { p.Name, IsAdult = p.Age >= 20 })
															 .ToDictionary(x => x.Name, x => x.IsAdult);
			foreach (KeyValuePair<string, bool> kvPair in personDict)
			{
				Console.WriteLine($"{kvPair.Key}: {kvPair.Value}");
			}
		}

		public static void OrderBy排序()
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 升序
			List<Person> sortedPeopleList = [.. peopleList.OrderBy(p => p.Age)];
			sortedPeopleList.ForEach(item => Console.WriteLine($"{item.Name} -> {item.Age}"));

			// 降序
			List<Person> sortedPeopleDescList = [.. peopleList.OrderByDescending(p => p.Age)];
			sortedPeopleDescList.ForEach(item => Console.WriteLine($"{item.Name} -> {item.Age}"));
		}

		public static void GroupBy分组()
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 按年龄段分组
			IEnumerable<IGrouping<bool, Person>> groupByAge = peopleList.GroupBy(p => p.Age > 26);
			foreach (IGrouping<bool, Person> ageGroup in groupByAge)
			{
				if (ageGroup.Key)
				{
					Console.WriteLine("===========");
					foreach (Person item in ageGroup)
					{
						Console.WriteLine($"{item.Name} -> {item.Age}");
					}
				}
				else
				{
					foreach (Person item in ageGroup)
					{
						Console.WriteLine($"{item.Name} -> {item.Age}");
					}
				}
			}
		}

		public static void 统计_聚合()
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 集合数量
			int count = numbers.Count;
			Console.WriteLine(count);

			// 总和
			int sum = numbers.Sum();
			Console.WriteLine(sum);

			// 最大值
			int max = numbers.Max();
			Console.WriteLine(max);

			// 平均值
			double avg = numbers.Average();
			Console.WriteLine(avg);
		}

		public static void Distinct去重()
		{
			Console.WriteLine("+ ------------------------------------ +");
			List<int> distinctNums = [1, 2, 2, 3, 3, 4];
			// 去重, 转换为List, 循环打印
			distinctNums.Distinct().ToList().ForEach(Console.WriteLine);
		}

		public static void Join连接()
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 创建两个对象
			var departments = new[]
			{
				new { Id = 1, Name = "开发部" },
				new { Id = 2, Name = "人事部" }
			};
			var employees = new[]
			{
				new { Name = "Tom", DeptId = 1 },
				new { Name = "Jerry", DeptId = 1 },
				new { Name = "Alice", DeptId = 2 }
			};

			// 通过查询表达式的写法关联查询数据
			var query = from emp in employees
						join dept in departments 
						on
							emp.DeptId equals dept.Id
						select 
							new { emp.Name, Department = dept.Name };
			foreach (var item in query) 
			{
				Console.WriteLine($"{item.Name} -> {item.Department}");
			}
		}

		public static void 取部分()
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 获取前2个元素
			List<int> numList1 = [.. numbers.Take(2)];
			Console.WriteLine(string.Join(" ", numList1));

			// 跳过前2个元素
			List<int> numList2 = [.. numbers.Skip(2)];
			Console.WriteLine(string.Join(" ", numList2));

			/*
				.where()
					1. 过滤所有符合条件的元素
					2. 遍历整个序列，所有满足条件的都保留
					3. 全局筛选
				.TakeWhile()
					1. 从头开始取，直到条件不成立为止
					2. 一旦有一个元素不符合条件，后面的全都不再检查
					3. 从头连续取
			 */
			// 根据条件获取元素
			List<int> numList3 = [.. numbers.TakeWhile(x => x < 3)];
			Console.WriteLine(string.Join(" ", numList3));

			// 根据条件跳过元素
			List<int> numList4 = [.. numbers.SkipWhile(x => x < 3)];
			Console.WriteLine(string.Join(" ", numList4));
		}

		public static void 数据生成() 
		{
			var r = new Random();
			const int count = 100_000;

			List<int> data = [.. Enumerable.Range(0, count).Select(_ => r.Next(40))];
			Console.WriteLine(data.Count);
		}
	}
}
