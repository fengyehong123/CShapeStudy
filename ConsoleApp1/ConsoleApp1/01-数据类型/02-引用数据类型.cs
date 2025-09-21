using System;

namespace ConsoleApp1._01_数据类型
{
	class _02_引用数据类型
	{
		/*
			⏹内置的 引用类型有
				object、dynamic 和 string。
		 */
		private readonly static object obj1 = 100;
		private readonly static string str1 = "Hello World!";

		public static void PrintInfo()
		{
			Console.WriteLine(obj1);
			Console.WriteLine(_02_引用数据类型.str1);
		}
	}
}
