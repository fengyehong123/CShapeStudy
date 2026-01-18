using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp2._05_多线程
{
	public class _02_Task_aysnc_await_4
	{
		/*
			传统foreach同步：
				1. 数据必须一次性准备好
				2. 中途无法 await
				3. 不适合 IO / 网络 / 流式数据
		 */
		static async Task<List<string>> GetAllAsync()
		{
			List<string> result = [];

			// 模拟从数据库 / 网络 / 文件读取
			for (int i = 1; i <= 5; i++)
			{
				// 模拟 IO 延迟
				await Task.Delay(1000);
				result.Add($"item-{i}");
			}

			return result;
		}

		// await foreach 只能遍历 IAsyncEnumerable<T>
		static async IAsyncEnumerable<string> GetNumbersAsync()
		{
			for (int i = 1; i <= 5; i++)
			{
				await Task.Delay(1000);
				// 必须先 await，再 yield
				yield return $"item-{i}";
			}
		}

		public static async Task PrintInfo1Async() 
		{
			// 需要等待5秒, 等待数据全部获取完毕之后, 才能打印
			List<string> list = await GetAllAsync();
			foreach (string item in list)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("--------- 分割线 ---------");

			/*
				await foreach 
					1. 代表的是 C# 的“异步流（Async Stream）”模型。
					2. 异步版的 foreach，用来逐个、异步地消费数据流。

				特点：
					1. 来一个，处理一个
					2. 每次迭代都可以 await
					3. 不阻塞线程
					4. 非常适合流式 IO，例如读取文本
			*/
			await foreach (string item in GetNumbersAsync())
			{
				Console.WriteLine(item);
			}

		}
	}
}
