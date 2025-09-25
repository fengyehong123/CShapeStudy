using System;
using System.Collections.ObjectModel;

namespace ConsoleApp1._05_数组
{
	internal class _04_数组的属性方法
	{
		public static void PrintInfo1()
		{
			Console.WriteLine("__________________________________________");
			int[] arr1 = { 1, 2, 3 };

			// 获取数组中元素的总个数
			Console.WriteLine($"数组元素的数量是: {arr1.Length}");  // 数组元素的数量是: 3

			// 获取获取数组的维数
			Console.WriteLine($"数组的维度是: {arr1.Rank}");  // 数组的维度是: 1

			// 判断数组是否为只读
			Console.WriteLine($"是否为只读数组: {arr1.IsReadOnly}");  // 是否为只读数组: False
		}

		public static void PrintInfo2() 
		{
			Console.WriteLine("__________________________________________");
			// Array 类是 C# 中所有数组的基类，它是在 System 命名空间中定义。
			// Array 类提供了各种用于数组的属性和方法。

			int[] arr1 = { 1, 2, 3 };
			// 清空整个数组
			Array.Clear(arr1, 0, arr1.Length);
			Console.WriteLine($"数组的长度为: {arr1.Length}");

			int[] arr2 = { 1, 2, 3 };
			// 数组元素反转
			Array.Reverse(arr2);
			Console.WriteLine("逆转数组： ");

			// 判断数组内是否包含指定的元素, 只要有一个元素满足条件就返回 true
			bool result1 = Array.Exists(arr2, name => name == 3);
			Console.WriteLine(result1);

			// 判断所有的元素是否为正数
			int[] arr3 = { 1, 2, 3, 4, 5 };
			bool result2 = Array.TrueForAll(arr3, x => x > 0);
			Console.WriteLine(result2);

			// 将数组包装为只读数组
			ReadOnlyCollection<int> arr4 = Array.AsReadOnly(arr3);

			// 创建一个空数组的实例
			int[] arr5 = Array.Empty<int>();

		}

	}
}
