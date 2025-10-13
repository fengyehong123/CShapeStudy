using System;

namespace ConsoleApp1._02_判断与运算符
{
	class _02_switch语句
	{
		public static void PrintInfo()
		{
			// 定义一个局部变量
			int day = 4;

			// ______ 普通的switch语句进行判断	______ 
			switch (day)
			{
				case 1:
					Console.WriteLine("Monday");
					break;
				case 2:
					Console.WriteLine("Tuesday");  // Thursday
					break;
				case 3:
					Console.WriteLine("Wednesday");
					break;
				case 4:
					Console.WriteLine("Thursday");
					break;
				case 5:
					Console.WriteLine("Friday");
					break;
				case 6:
					Console.WriteLine("Saturday");
					break;
				case 7:
					Console.WriteLine("Sunday");
					break;
				default:
					Console.WriteLine("Unknown");
					break;
			}

			// ______ C# 7+ 的模式匹配 ______ 
			object obj = 42;
			switch (obj)
			{
				case int n when n > 0:
					Console.WriteLine($"Positive integer: {n}");  // Positive integer: 42
					break;
				case int n:
					Console.WriteLine($"Integer: {n}");
					break;
				case string s:
					Console.WriteLine($"String: {s}");
					break;
				default:
					Console.WriteLine("Other type");
					break;
			}

			// ______ C# 8+ 的 switch expression（函数式风格）______ 
			int number = 2;
			string result = number switch
			{
				1 => "One",
				2 => "Two",
				3 => "Three",
				_ => "Other"
			};
			Console.WriteLine(result);  // Two
		}
	}
}
