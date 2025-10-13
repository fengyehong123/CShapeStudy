using System;
using System.Collections.Generic;

// SortedDictionary<TKey, TValue> 是一个自动按键排序的字典集合，
// 与普通的 Dictionary 类似，但会 始终保持键的有序性。
namespace ConsoleApp1._06_集合
{
	public class _05_SortedDictionary
	{
		public static void PrintInfo() 
		{
			Console.WriteLine("__________________________________________");
			// 创建一个有序的字典, 是一个会自动按照键排序的字典集合
			SortedDictionary<string, int> scores1 = new()
			{
				{ "Tom", 80 },
				{ "Alice", 95 },
				{ "Bob", 70 }
			};

			// 创建一个普通的字典
			Dictionary<string, int> scores2 = new()
			{
				{ "Tom", 80 },
				{ "Alice", 95 },
				{ "Bob", 70 }
			};

			// 分别遍历两个字典
			foreach (var score in scores1)
			{
				Console.WriteLine($"{score.Key}: {score.Value}");
			}
			Console.WriteLine("__________________________________________");

			foreach (var score in scores2)
			{
				Console.WriteLine($"{score.Key}: {score.Value}");
			}

		}
	}
}
