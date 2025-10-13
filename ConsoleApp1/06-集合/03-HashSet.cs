using System;
using System.Collections.Generic;
using System.Linq;

/*
	🔴基本特性
		1. 不允许重复元素（自动去重）
		2. 无序（不保证插入顺序）
		3. 查找、添加、删除元素的效率高（通常是 O(1)）
		4. 支持集合运算：并集、交集、差集、对称差集 
*/
namespace ConsoleApp1._06_集合
{
	public class _03_HashSet
	{
		public static void PrintInfo1() 
		{
			Console.WriteLine("__________________________________________");
			// 创建一个空集合
			HashSet<int> set1 = new();
			// 向集合中添加数据
			set1.Add(1);
			set1.Add(2);

			// 初始化一个有数据的集合
			HashSet<int> set2 = new() { 1, 2, 3, 3, 4 };

			// 判断集合中是否包含指定的元素
			if (set2.Contains(1))
			{
				Console.WriteLine("集合中包含指定的元素");
			}

			// 遍历集合
			foreach (int item in set2) 
			{
				 Console.WriteLine(item);
			}

			// 清空集合中的元素
			set2.Clear();
		}

		public static void PrintInfo2() 
		{
			Console.WriteLine("__________________________________________");
			// 定义两个集合
			var setA = new HashSet<int> { 1, 2, 3, 4 };
			var setB = new HashSet<int> { 3, 4, 5, 6 };

			/* 
			 * ______ ↓↓↓ 集合的运算 ↓↓↓ ______
			 * UnionWith IntersectWith ExceptWith 的方法返回值是 Void
			 * 这些方法会直接修改原来的集合, 并不会返回新的集合
			*/
			// 集合的并集
			setA.UnionWith(setB);
			Console.WriteLine($"并集: {string.Join(", ", setA)}");  // 并集: 1, 2, 3, 4, 5, 6

			// 集合的交集
			setA.IntersectWith(setB);
			Console.WriteLine($"交集: {string.Join(", ", setA)}");  // 交集: 3, 4, 5, 6

			// 集合的差集
			setA.ExceptWith(setB);
			Console.WriteLine($"差集: {string.Join(", ", setA)}");

			Console.WriteLine("__________________________________________");
			// 🔴使用Linq表达式可以返回新的集合
			var setC = new HashSet<int> { 1, 2, 3, 4 };
			var setD = new HashSet<int> { 3, 4, 5, 6 };

			var resultSet1 = setC.Union(setD);
			Console.WriteLine($"并集: {string.Join(", ", resultSet1)}");  // 并集: 1, 2, 3, 4, 5, 6

			var resultSet2 = setC.Intersect(setD);
			Console.WriteLine($"交集: {string.Join(", ", resultSet2)}");  // 交集: 3, 4

			IEnumerable<int> resultSet3 = setC.Except(setD);
			Console.WriteLine($"差集: {string.Join(", ", resultSet3)}");  // 差集: 1, 2

			// 🔴判断集合之间的关系
			var A = new HashSet<int> { 1, 2 };
			var B = new HashSet<int> { 1, 2, 3 };

			// 判断A是否是B的子集
			Console.WriteLine($"A是否是B的子集: {A.IsSubsetOf(B)}");

			// 判断B是否是A的超集
			Console.WriteLine($"B是否是A的超集: {B.IsSupersetOf(A)}");

			// 判断两个集合是否相等
			Console.WriteLine(A.SetEquals(B));
		}
	}
}
