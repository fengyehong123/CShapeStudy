using System;
using System.Collections.Generic;

/*
	✅集合表达式（Collection expressions）
		C# 12（随 .NET 8 一起发布）中新引入的特性
		C# 12 起，可以用类似数组字面量的语法 [] 来直接创建各种集合（不仅仅是数组）。
 */
namespace ConsoleApp1._06_集合
{
	public class 集合表达式
	{
		public static void PrintInfo() 
		{
			Console.WriteLine("__________________________________________");
			// 一. 数组初始化
			int[] arr = [1, 2, 3];
			Console.WriteLine("数组 arr: " + string.Join(", ", arr));

			// 二. List<T> 初始化
			List<string> fruits = ["Apple", "Banana", "Cherry"];
			Console.WriteLine("List fruits: " + string.Join(", ", fruits));

			// 三. HashSet 初始化
			HashSet<string> colors = ["Red", "Green", "Blue"];
			Console.WriteLine("HashSet colors: " + string.Join(", ", colors));

			// 四. 集合展开（spread）语法
			int[] moreNumbers = [.. arr, 4, 5, 6];
			Console.WriteLine("展开后的数组 moreNumbers: " + string.Join(", ", moreNumbers));
		}
	}
}
