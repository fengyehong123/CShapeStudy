using System;

namespace ConsoleApp1._05_数组
{
	internal class _03_参数数组
	{
		/*
			1. 一个方法里最多只能有一个 params 参数。
			2. params 参数必须是方法参数列表的最后一个。
			3. 传参时可以
				3.1 单个元素（会自动组成数组）
				3.2 一组元素
				3.3 已经存在的数组
		 */
		public static void PrintInfo(string name, params int[] numbers)
		{
			Console.WriteLine("__________________________________________");
			Console.WriteLine(name);
			// 在 C# 中，参数数组（params array） 是一种特殊的语法，用来让方法接受数量可变的参数。
			int sum = 0;
			foreach (int num in numbers)
			{
				sum += num;
			}
			Console.WriteLine($"和为:{sum}");
		}
	}
}
