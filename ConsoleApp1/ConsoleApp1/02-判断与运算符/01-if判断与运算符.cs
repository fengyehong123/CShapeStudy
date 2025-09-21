using System;

namespace ConsoleApp1._02_判断与运算符
{
	class _01_if判断与运算符
	{
		public static void PrintInfo()
		{
			// 局部变量
			int a = 10;

			// ___________ 普通的 if else 语句 ___________ 
			if (a > 10)
			{
				Console.WriteLine($"a的值为: {a}");
			}
			else if (a == 10)
			{
				Console.WriteLine($"a的值为: {a}");
			}
			else
			{
				Console.WriteLine($"a的值为: {a}");
			}

			// ___________ 三元运算符语句 ___________ 
			Console.WriteLine(a > 10 ? "a的值>10" : "a的值<=10");
		}
	}
}
