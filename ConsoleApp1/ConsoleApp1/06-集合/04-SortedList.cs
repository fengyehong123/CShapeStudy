using System;
using System.Collections.Generic;
using System.Linq;

/*
	🔴SortedList 是 C# 中一种自动按键排序的键值对集合
	| 特性    | 说明                                         |
	| :------ | ------------------------------------------- |
	| 键类型   | 必须唯一（不能重复）                          |
	| 排序规则 | 自动按键（Key）排序（默认升序）                |
	| 索引访问 | 可以通过索引或键访问元素                      |
	| 实现接口 | `IDictionary`, `ICollection`, `IEnumerable` |
	| 适用场景 | 需要按键排序 + 随机访问                       |


	| 对比项          | SortedList<TKey,TValue>            | SortedDictionary<TKey,TValue>     |
	| ------------    | -----------------------------     | --------------------------------- |
	| 底层结构        | 动态数组 (Array + 二分查找)         | 红黑树 (平衡二叉搜索树)             |
	| 插入性能        | 较慢 — 插入时可能移动数组元素       | 较快 — 树结构插入复杂度稳定         |
	| 查找性能        | O(log n)（二分查找）                | O(log n)（树查找）                 |
	| 插入/删除复杂度  | O(n)（数组移动）                    | O(log n)                          |
	| 按索引访问      | ✅ 支持（`Keys[i]` / `Values[i]`）  | ❌ 不支持                         |
	| 内存占用        | 较低                                | 较高（树节点结构）                  |
	| 遍历顺序        | 按键升序                            | 按键升序（相同）                    |
	| 适合场景        | 数据量小、频繁按索引访问              | 数据量大、频繁插入/删除             |

*/
namespace ConsoleApp1._06_集合
{
	public class _04_SortedList
	{
		public static void PrintInfo() 
		{
			Console.WriteLine("__________________________________________");
			// 创建一个键为 string，值为 int 的 SortedList
			SortedList<string, int> scores = new()
			{
				{ "Tom", 80 },
				{ "Alice", 95 },
				{ "Bob", 70 }
			};

			// 🔴在遍历的时候, 会发现自动按照字典的顺序排序
			foreach (KeyValuePair<string, int> score in scores) 
			{
				Console.WriteLine($"{score.Key}: {score.Value}");
			}

			Console.WriteLine("__________________________________________");
			// 向 SortedList 中添加元素
			scores.Add("Claoe", 100);

			// 使用Linq的 .Append() 方法添加数据, 不会修改原先集合, 而是会返回一个新的集合
			IEnumerable<KeyValuePair<string, int>> enumerable = scores.Append(new KeyValuePair<string, int>("Sid", 58));
			foreach (KeyValuePair<string, int> score in enumerable)
			{
				Console.WriteLine($"{score.Key}: {score.Value}");
			}
			Console.WriteLine("__________________________________________");

			// 修改元素
			scores["Tom"] = 800;

			// 删除元素
			scores.Remove("Tom");
			// 删除第一个元素
			scores.RemoveAt(0);

			// 🔴获取元素
			//   根据键获取值
			Console.WriteLine(scores["Bob"]);
			//   根据索引获取值
			Console.WriteLine(scores.Values[0]);
			//   根据索引获取键
			Console.WriteLine(scores.Keys[0]);

			// 判断是否存在key
			if (scores.ContainsKey("Bob"))
			{
				Console.WriteLine("包含 Bob");
			}

			// 判断是否存在value
			if (scores.ContainsValue(70))
			{
				Console.WriteLine("有人得了 70 分");
			}

			// 自定义排序规则
			SortedList<string, int> SortedList = new(Comparer<string>.Create((a, b) => b.CompareTo(a))) { };
			SortedList.Add("A", 100);
			SortedList.Add("B", 200);
			SortedList.Add("C", 300);
		}
	}
}
