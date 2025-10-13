using System;

namespace ConsoleApp1._05_数组
{
	internal class _02_交错数组
	{
		public static void PrintInfo() 
		{
			Console.WriteLine("__________________________________________");
			/*
				在 C# 中，交错数组（Jagged Array）指的是数组的数组，即每个元素本身又是一个数组。
				和多维数组（int[,]）不同，交错数组允许每个“子数组”的长度不一样。
			 */

			// 声明一个包含3个元素的交错数组
			int[][] jaggedArray1 = new int[3][];

			// 初始化交错数组中的子数组
			jaggedArray1[0] = new int[2];  // 长度为 2
			jaggedArray1[1] = new int[4];  // 长度为 4
			jaggedArray1[2] = new int[3];  // 长度为 3

			// 向错数组中的子数组中的元素赋值
			jaggedArray1[0][0] = 1;
			jaggedArray1[0][1] = 2;
			jaggedArray1[1][0] = 3;
			jaggedArray1[1][1] = 4;
			jaggedArray1[1][2] = 5;
			jaggedArray1[1][3] = 6;
			jaggedArray1[2][0] = 7;
			jaggedArray1[2][1] = 8;
			jaggedArray1[2][2] = 9;

			// 访问交错数组中的子元素值
			Console.WriteLine(jaggedArray1[1][2]); // 5

			// 也可以在声明交错数组的时候, 直接初始化
			int[][] jaggedArray2 =
			{
				new int[] {1, 2},
				new int[] {3, 4, 5, 6},
				new int[] {7, 8, 9}
			};

			// 遍历交错数组
			for (int i = 0; i < jaggedArray2.Length; i++)
			{
				for (int j = 0; j < jaggedArray2[i].Length; j++)
				{
					Console.Write(jaggedArray2[i][j] + " ");
				}
				Console.WriteLine();
			}
		}
	}
}
