using System;

namespace ConsoleApp2._03_委托
{
	/*
	 * 委托是【方法的类型安全引用】
	 * 也可以理解为Java里面的函数式接口
	 * 委托本身不是方法，而是【能指向方法的类型】
	 * 
	 * 委托在 C# 中非常常见，用于事件处理、回调函数、LINQ 等操作。
	 * 所有的委托（Delegate）都派生自 System.Delegate 类。
	 */
	public class _01_委托
	{
		// 现代 C#：几乎不再手写 delegate
		// 而是用 Action / Func, 此处仅为展示
		// ------------------------------ 自定义委托 ------------------------------
		// 1. 定义一个最基础的委托 → 任何无参数, 无返回值的方法都可以赋值给 MailSendDelegate
		delegate void MailSendDelegate();

		// 2. 带参数和返回值的委托
		delegate int CalcDelegate(int num1, int num2);
		// ------------------------------ 自定义委托 ------------------------------

		// ------------------------------ .NET内置委托 ------------------------------
		// ✅Action：无返回值
		private static readonly Action action1 = () => Console.WriteLine("Hi");
		private static readonly Action<string, string> action2 = (msg1, msg2) =>
		{
			Console.WriteLine($"你好 → {msg1}, {msg2}");
		};

		// ✅Func 有返回值
		// 前两个参数类型是 input, 最后一个参数类型是 output
		private static readonly Func<int, int, int> add = (a, b) => a + b;

		// ✅Predicate（返回 bool）
		private static readonly Predicate<int> isEven = x => x % 2 == 0;
		// ------------------------------ .NET内置委托 ------------------------------

		// 没有参数和返回值的方法
		private static void SendMailTo163()
		{
			Console.WriteLine("发送邮件给163邮箱");
		}

		// 有参数和返回值的方法
		private static int Add(int x, int y) => x + y;

		public static void PrintInfo() 
		{
			// 🔷使用自定义委托, 无参数和返回值
			MailSendDelegate mailSend = SendMailTo163;
			mailSend();  // 发送邮件给163邮箱

			// 🔷使用自定义委托, 有参数和返回值
			CalcDelegate calc = Add;
			Console.WriteLine(calc(3, 5));  // 8

			// 使用.Net内置的委托
			action1();  // Hi
			action2("世界", "World");  // 你好 → 世界, World
			Console.WriteLine(add(3, 5));  // 8
			Console.WriteLine(isEven(3));  // False
		}
	}


}
