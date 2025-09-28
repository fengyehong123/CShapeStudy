using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1._06_集合
{
	public class _02_Dictionary
	{
		public static void PrintInfo()
		{
			Console.WriteLine("__________________________________________");
			// 创建一个字典
			Dictionary<string, int> personDict = new()
			{
				["Tom"] = 20,
				["Jerry"] = 18
			};

			// 向字典中添加数据
			personDict["Cat"] = 58;

			// ------ 遍历字典的方式1 ------
			foreach (KeyValuePair<string, int> kvPair in personDict)
			{
				Console.WriteLine($"{kvPair.Key}: {kvPair.Value}");
			}
			Console.WriteLine("__________________________________________");
			
			// 使用var的话, 可以不用强制指定类型
			foreach (var kvPair in personDict)
			{
				Console.WriteLine($"{kvPair.Key}: {kvPair.Value}");
			}
			Console.WriteLine("__________________________________________");

			// ------ 遍历所有的key ------
			foreach (string key in personDict.Keys)
			{
				Console.WriteLine($"{key}: {personDict[key]}");
			}
			Console.WriteLine("__________________________________________");

			// ------ 遍历所有的value ------
			foreach (int value in personDict.Values)
			{
				Console.WriteLine(value);
			}
			Console.WriteLine("__________________________________________");

			// ------ 使用Linq表达式 ------
			personDict.ToList().ForEach(kvPair => Console.WriteLine($"{kvPair.Key}: {kvPair.Value}"));
		}
	}
}
