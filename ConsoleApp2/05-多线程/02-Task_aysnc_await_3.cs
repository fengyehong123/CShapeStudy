using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp2._05_多线程
{
	public class _02_Task_aysnc_await_3
	{
		// 模拟内存缓存
		static readonly Dictionary<int, string> cache = new()
		{
			{ 1, "Alice" },
			{ 2, "Bob" },
			{ 3, "Charlie" }
		};

		// 🔷Task.FromResult 的用法
		public static async Task PrintInfo1Async() 
		{
			Console.WriteLine("==== 第一次：命中缓存 ====");
			string name1 = await GetUserNameAsync(1);
			Console.WriteLine($"用户 1 名字：{name1}");

			Console.WriteLine("\n==== 第二次：未命中缓存 ====");
			string name2 = await GetUserNameAsync(99);
			Console.WriteLine($"用户 99 名字：{name2}");
		}

		// 重点方法：无 async / await, 避免了状态
		private static Task<string> GetUserNameAsync(int id)
		{
			// 命中缓存：同步返回
			if (cache.TryGetValue(id, out var name))
			{
				Console.WriteLine("从缓存读取（同步完成）");
				// 👇👇👇👇👇👇 重点 👇👇👇👇👇👇
				/*
					把一个已有结果包装成 Task
					不启动线程
					不切换上下文
				 */
				return Task.FromResult(name);
				// 👆👆👆👆👆👆 重点 👆👆👆👆👆👆
			}

			// 未命中缓存：真正异步
			Console.WriteLine("从数据库读取（异步）");
			return GetFromDatabaseAsync(id);
		}

		// 模拟数据库异步查询
		private static async Task<string> GetFromDatabaseAsync(int id)
		{
			Console.WriteLine("查询数据库中...");
			// 模拟 IO 延迟
			await Task.Delay(2000);

			// 模拟查到了数据
			return $"User_{id}";
		}
	}
}
