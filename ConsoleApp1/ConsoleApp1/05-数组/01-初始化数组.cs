using System;

namespace ConsoleApp1._05_数组
{
	internal class _01_初始化数组
	{
		public static void PrintInfo()
		{
			// 初始化一个可以存放10个元素的空数组
			string[] strArry1 = new string[10];
			// 向数组中存入元素
			strArry1[0] = "Hello World!";

			// 在创建数组的同时给数组赋值
			string[] strArry2 = new string[] { 
				"a",
				"b",
				"c"
			};

			// 也可以更加简单的创建数组
			string[] strArry3 = { 
				"d",
				"e",
				"f"
			};

			// 创建一个多维数组
			int[,] matrix1 = new int[2, 3]
			{
				{ 1, 2, 3 },
				{ 4, 5, 6 }
			};
			
			// 创建多维数组时, new 关键字也是可以省略的
			int[,] matrix2 = {
				{ 1, 2, 3 },
				{ 4, 5, 6 }
			};
		}
	}
}
