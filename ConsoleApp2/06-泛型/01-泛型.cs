using System;

namespace ConsoleApp2._06_泛型
{
	public class User
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Age { get; set; }
	}

	// 泛型类
	public class _01_泛型<T>
	{
		public T? Attr1 { get; set; }

		// 泛型方法
		public static void Print<T1>(T1 value)
		{
			Console.WriteLine(value);
		}
	}

	/*
		🔷泛型约束
			where T : class / struct
			| 约束        | 含义     |
			| ----------- | ------ |
			| `class`     | 引用类型   |
			| `struct`    | 值类型    |
			| `notnull`   | 非 null |
			| `unmanaged` | 非托管类型  |

	 */
	// 必须是引用类型
	class Cache1<T> where T : class
	{

	}
	
	// 必须是值类型
	class Cache2<T> where T : struct
	{

	}

	public class TestUtils 
	{
		public static void PrintInfo1() 
		{
			User user = new()
			{
				Id = 10,
				Name = "测试Name",
				Age = 18
			};

			// 创建一个泛型类对象
			_01_泛型<User> obj1 = new() 
			{
				Attr1 = user
			};
			Console.WriteLine(obj1.Attr1);

			// 使用泛型方法
			_01_泛型<User>.Print("你好");
		}

		class Point { }

		public static void PrintInfo2() 
		{
			// 引用类型
			Cache1<Point> cache1 = new();
			Console.WriteLine(cache1);

			// 报错, 因为只能是引用类型
			// Cache1<int> error = new();

			// 值类型
			Cache2<int> cache2 = new();
			Console.WriteLine(cache2);

			// 报错, 因为只能是值类型
			// Cache2<Point> cache2 = new();
		}
	}
}
