using System;
using System.Threading.Tasks;

namespace ConsoleApp1._07_函数
{
	public class _01_函数基本使用
	{
		// 函数: 有参数, 无返回值, 参数还可以指定默认值
		public static void TestDefaultParam(string msg = "你好, 世界")
		{
			Console.WriteLine(msg);
		}

		// 函数: 有参数, 有返回值
		public static int Test2(int a, int b)
		{
			return a + b;
		}
		
		// 函数: 重载
		public static int Test2(string a, string b)
		{
			return 10;
		}

		// 使用 params 表示 参数的个数可变
		public static int Test3(params int[] nums)
		{
			return nums.Length;
		}

		// 当函数体只有一行代码时，可以用 => 简化
		public static int Test4(int x) => x * x;

		/// <summary>
		/// ref：传引用，可读可写。
		/// out：传引用，仅用于输出值。
		/// </summary>
		public static void Test_Ref_Out(ref int a, out int b) 
		{
			a++;
			b = 1000;
		}

		// 异步函数返回 Task 或 Task<T>
		public static async Task<int> GetDataAsync()
		{
			// 延时1秒
			await Task.Delay(1000);
			return 42;
		}

		// C#7.0+ 之后, 可以在函数的内部另外定义一个函数, 即内部函数
		public static void Test5()
		{
			// 函数内部定义的函数
			static void Inner()
			{
				Console.WriteLine("内部函数被调用了");
			}

			// 调用内部函数
			Inner();
		}
	}

	public class _01_函数基本使用_Utils 
	{
		public static async Task PrintInfo() 
		{
			// 不传递参数, 使用参数的默认值
			_01_函数基本使用.TestDefaultParam();

			// 传递参数的时候, 按照顺序依次传递
			_01_函数基本使用.Test2(10, 20);
			// 传递命名参数
			_01_函数基本使用.Test2(b: 20, a: 10);
			// 不固定参数
			_01_函数基本使用.Test3(10, 20, 30);
			// 一行代码的函数, 可以使用 => 简化
			_01_函数基本使用.Test4(10);

			Console.WriteLine("________________________________________________");
			// a 被重新赋值; b 用来接收输出值
			int a = 5;
			_01_函数基本使用.Test_Ref_Out(ref a, out int b);
			Console.WriteLine(a);
			Console.WriteLine(b);

			Console.WriteLine("________________________________________________");
			var result = await _01_函数基本使用.GetDataAsync();
			Console.WriteLine(result);

			Console.WriteLine("________________________________________________");
			// 函数内部的函数
			_01_函数基本使用.Test5();
		}
	}
}
