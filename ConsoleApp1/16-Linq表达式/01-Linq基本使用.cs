using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
 */
// https://www.cnblogs.com/dullfish/p/6101912.html
// https://www.cnblogs.com/hellohxs/p/12266856.html
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

			// 映射为匿名类型
			// var result = people.Select(p => new { p.Name, IsAdult = p.Age >= 18 });
		}

		public static void OrderBy排序()
		{
			Console.WriteLine("+ ------------------------------------ +");

		}

		public static void GroupBy分组()
		{

		}

		public static void 统计_聚合()
		{

		}

		public static void Distinct去重()
		{

		}

		public static void Join连接()
		{

		}

		public static void 合并()
		{

		}

		public static void 取部分()
		{

		}

		public static void 类似于SQL的查询表达式()
		{

		}
	}
}
