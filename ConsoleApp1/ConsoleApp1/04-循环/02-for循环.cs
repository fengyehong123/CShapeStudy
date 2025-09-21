using System;
using System.Collections.Generic;

namespace ConsoleApp1._04_循环
{
	internal class _02_for循环
	{
		public static void PrintInfo()
		{
			Console.WriteLine("__________________________________________");

			// ______ 普通的for循环 ______
			for (int a = 10; a < 20; a++)
			{
				Console.WriteLine("a 的值： {0}", a);
			}

			// ______ foreach循环 ______
			Console.WriteLine("__________________________________________");
			int[] numArrys = new int[] { 0, 1, 1, 2, 3, 5, 8, 13 };
			foreach (int num in numArrys)
			{
				Console.WriteLine(num);
			}

			Console.WriteLine("__________________________________________");
			// 创建一个字符串列表,并在初始化的时候, 就向其中添加元素
			List<string> myStrings = new()
			{
				"Google",
				"Runoob",
				"Taobao"
			};

			// 使用 foreach 循环遍历列表中的元素
			foreach (string str in myStrings)
			{
				Console.WriteLine(str);
			}
		}
	}
}
