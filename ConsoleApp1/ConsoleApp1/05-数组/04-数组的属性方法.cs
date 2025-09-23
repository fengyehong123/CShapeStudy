using System;

namespace ConsoleApp1._05_数组
{
	internal class _04_数组的属性方法
	{
		public static void PrintInfo1()
		{
			Console.WriteLine("__________________________________________");
			// Array 类是 C# 中所有数组的基类，它是在 System 命名空间中定义。
			// Array 类提供了各种用于数组的属性和方法。
			int[] arr1 = { 1, 2, 3 };

			// 获取数组中元素的总个数
			Console.WriteLine($"数组元素的数量是: {arr1.Length}");  // 数组元素的数量是: 3

			// 获取获取数组的维数
			Console.WriteLine($"数组的维度是: {arr1.Rank}");  // 数组的维度是: 1

			// 判断数组是否为只读
			Console.WriteLine($"是否为只读数组: {arr1.IsReadOnly}");  // 是否为只读数组: False
		}
	}
}
