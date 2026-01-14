using System;

namespace ConsoleApp1._07_函数
{
	// 详情可以查看【委托】章节
	public class _02_函数进阶使用
	{
		// 有返回值
		private static readonly Func<int, int, int> add = (a, b) => a + b;
		// 无返回值
		private static readonly Action<string> sayHello = name => Console.WriteLine($"Hello, {name}");
		// 判断, 返回布尔值
		private static readonly Predicate<int> isEven = n => n % 2 == 0;

		public static void PrintInfo() 
		{
			Console.WriteLine("+ ------------------------------------ +");
			// 🔴定义一个本地匿名函数
			static int multiply(int x, int y) => x * y;
			Console.WriteLine(multiply(3, 4));  // 12

			// 🔴有返回值
			Console.WriteLine(add(2, 3));  // 5

			// 🔴无返回值
			sayHello("Jerry");

			// 🔴判断是否为偶数
			Console.WriteLine(isEven(4)); // true
			Console.WriteLine(isEven(5)); // false
		}
	}
}
