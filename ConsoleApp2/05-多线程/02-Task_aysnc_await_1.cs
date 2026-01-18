using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2._05_多线程
{
	/*
		Task = “一个可能正在执行、已经完成、或将来完成的工作”
		📌 注意这句话里：
			1. 它不等于线程
			2. 它代表的是 任务（Task）
			3. 底层通常跑在线程池线程上
	 */
	public class _02_Task_aysnc_await_1
	{
		// 传统的阻塞写法
		public static void PrintInfo1() 
		{
			// 🔷不带返回值的Task
			Task task1 = Task.Run(() =>
			{
				Console.WriteLine("任务开始");
				Thread.Sleep(3000);
				Console.WriteLine("任务结束");
			});
			Console.WriteLine($"Task的状态是：{task1.Status}");

			// 阻塞当前线程
			task1.Wait();
			Console.WriteLine("我执行了...");

			// 🔷带返回值的Task
			Task<int> task2 = Task.Run(async () =>
			{
				// 用来代替 Thread.Sleep 的写法
				await Task.Delay(2000);
				return 420;
			});

			// 获取Task的执行结果, 会阻塞当前线程
			int result = task2.Result;
			Console.WriteLine(result);
		}

		// 🔷推荐Task配合 async + await 的异步写法
		public static async Task PrintInfo2Async()
		{
			// 🔷 不带返回值的 Task
			Task task1 = Task.Run(async () =>
			{
				Console.WriteLine("任务开始");
				// 替代 Thread.Sleep
				await Task.Delay(3000); 
				Console.WriteLine("任务结束");
			});
			// 如果task失败了, 可以打印错误信息
			if (task1.IsFaulted)
			{
				Console.WriteLine(task1.Exception);
			}
			Console.WriteLine($"Task 的状态是：{task1.Status}");

			// 非阻塞等待
			await task1;
			Console.WriteLine("我执行了...");

			// 🔷 带返回值的 Task
			Task<int> task2 = Task.Run(async () =>
			{
				await Task.Delay(2000);
				return 42;
			});

			Console.WriteLine("我也执行了...");

			// 非阻塞获取结果
			int result = await task2;
			Console.WriteLine(result);
		}

		// 定义一个Action
		private static readonly Action action1 = async () => 
		{
			await Task.Delay(2000);
			Console.WriteLine("你好");
		};

		public static async Task PrintInfo3Async() 
		{
			// 定义了一个本地函数并调用
			static void localFunc() => Console.WriteLine("我执行了, 哈哈");
			localFunc();

			Task t1 = Task.Run(action1);

			// 定义一个本地函数
			static async Task action2()
			{
				await Task.Delay(2000);
				Console.WriteLine("世界");
			}
			// 使用本地函数
			Task t2 = Task.Run(() => action2());

			Task t3 = Task.Run(async () =>
			{
				await Task.Delay(2000);
				Console.WriteLine("哈哈");
			});

			// 🔷等待所有的任务执行结束
			await Task.WhenAll(t1, t2, t3);
			Console.WriteLine("3个Task终于全部执行结束了!");
		}

		public static async Task PrintInfo4Async() 
		{
			// 定义一个本地函数
			static async Task<string> Download(string name, int time)
			{
				await Task.Delay(time);
				return $"我{name}执行完了...";
			}
			Task<string> t1 = Task.Run(() => Download("A", 2000));
			Task<string> t2 = Task.Run(() => Download("B", 8000));

			// 🔷等待任意一个Task执行完毕即可
			Task<string> finished = await Task.WhenAny(t1, t2);
			Console.WriteLine(finished.Result);  // 我A执行完了...
		}
	}
}
