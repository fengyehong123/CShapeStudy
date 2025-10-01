using System;
using System.Collections.Generic;
using System.Linq;

/*
	1. List<T> 位于命名空间
		using System.Collections.Generic;
	2. 动态数组，长度可以自动扩展。
	3. 数组不同，List<T> 提供了许多方便的 增删查改方法。
*/
namespace ConsoleApp1._06_集合
{
	public class _01_List
	{
		public static void PrintInfo1()
		{
			Console.WriteLine("__________________________________________");
			// 创建一个List对象, 并赋予初始值
			List<int> numbers = new() { 1, 2, 3 };
			// 向List集合中追加1个元素
			numbers.Add(4);
			// 还可以批量追加元素
			numbers.AddRange(new int[] { 5, 6, 7 });

			// 还可以在创建List的时候, 指定容量（可以提升性能）
			List<int> scores = new(capacity: 10);

			// ______ 访问元素 ______ 
			// 访问下标
			Console.WriteLine(numbers[0]);  // 1
			// 获取元素个数
			Console.WriteLine(numbers.Count);  // 7
			// 遍历List
			foreach (int num in numbers)
			{
				Console.WriteLine(num);
			}

			// ______ 元素的插入与删除 ______ 
			// 在下标1处插入一个元素
			numbers.Insert(1, 1000);
			// 删除掉第一个匹配到的元素
			numbers.Remove(2);
			// 删除下标0的元素
			numbers.RemoveAt(0);
			// 清空List中的所有元素
			numbers.Clear();

			// 创建一个List对象, 并赋予初始值
			List<int> list1 = new() { 1, 2, 3, 4 };

			// 判断元素是否存在
			if (list1.Contains(2)) 
			{
				Console.WriteLine("2这个元素存在");
			}

			// 查找元素所对应的索引
			int index1 = list1.IndexOf(4);
			Console.WriteLine(index1);  // 3

			// 查找第一个 > 2 的数据
			int num1 = list1.Find(x => x > 2);
			Console.WriteLine(num1);  // 3

			// 查找所有 > 2 的数据
			List<int> list2 = list1.FindAll(x => x > 2);

			// 升序排序
			list2.Sort();
			// 自定义排序, 降序
			list2.Sort((a, b) => b.CompareTo(a));
			// 反转顺序
			list2.Reverse();
		}

		public static void PrintInfo2()
		{
			Console.WriteLine("__________________________________________");
			// Linq表达式

			// 创建一个List对象, 并赋予初始值
			List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8 };

			// 🔴过滤出偶数
			List<int> numArry1 = numbers.Where(x => x % 2 == 0).ToList();
			// List使用 _ 来间隔元素
			Console.WriteLine(string.Join("_", numArry1));  // 2_4_6_8

			// 🔴获取每个数的平方
			List<int> numArry2 = numbers.Select(x => x * x).ToList();
			foreach (int item in numArry2)
			{
				Console.WriteLine(item);
			}

			// 🔴获取List中元素的总和, 最大值
			Console.WriteLine(numbers.Sum());  // 36
			Console.WriteLine(numbers.Max());  // 8

		}
	}
}
