using System;

namespace ConsoleApp1._04_循环
{
	internal class _01_while循环
	{
		public static void PrintInfo()
		{
			/* 局部变量定义 */
			int a = 10;

			/* while 循环执行 */
			while (a < 20)
			{
				Console.WriteLine($"a 的值： {a}");
				a++;
			}
			Console.ReadLine();
		}
	}
}
