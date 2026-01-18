using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2._05_多线程
{
	public class _02_Task_aysnc_await_2
	{
		// 🔷检测超时的用法
		public static async Task PrintInfo1Async() 
		{
			// 定义一个本地函数
			static async Task work()
			{
				await Task.Delay(2000);
				Console.WriteLine("世界");
			}

			// 调用
			Task workTask = work();
			// work函数最多执行的时间 
			Task timeout = Task.Delay(3000);

			if (await Task.WhenAny(workTask, timeout) == timeout)
			{
				Console.WriteLine("超时了...");
			}
			else
			{
				 Console.WriteLine("没有超时...");
			}
		}

		// 🔷周期性执行指定任务
		public static async Task PrintInfo2Async() 
		{
			// 1. 创建 CancellationTokenSource 对象
			CancellationTokenSource cts = new();

			// 2. 启动后台任务
			Task workerTask = DoBackgroundWorkAsync(cts.Token);
			Console.WriteLine("后台任务已启动，5 秒后取消……");

			// 3️. 主线程等待 5 秒
			await Task.Delay(5000);

			// 4. 发送取消信号
			Console.WriteLine("发送取消信号");
			cts.Cancel();

			// 5️. 等待任务正常结束
			await workerTask;
			Console.WriteLine("后台任务已结束");

			// 后台任务
			static async Task DoBackgroundWorkAsync(CancellationToken token)
			{
				// 无限循环, 当检测到任务取消的时候, 抛出异常, 跳出无限循环
				while (true)
				{
					try
					{
						// 模拟周期性耗时工作
						await Task.Delay(1000, token);
						Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 检查数据库接口...");
					}
					catch (TaskCanceledException)
					{
						Console.WriteLine("任务被取消");
						break;
					}
				}
			}
		}

		// 并发控制 / 限流（Task 级）
		public static async Task PrintInfo3Async() 
		{
			static async Task taskAction(SemaphoreSlim sem, int taskId)
			{
				await sem.WaitAsync();
				try
				{
					Console.WriteLine($"Task {taskId} 开始 | 时间 {DateTime.Now:HH:mm:ss}");
					// 模拟耗时工作
					await Task.Delay(2000);
					Console.WriteLine($"Task {taskId} 结束 | 时间 {DateTime.Now:HH:mm:ss}");
				}
				finally
				{
					sem.Release();
				}
			}

			/*
				SemaphoreSlim = “可同时放行 N 个任务的闸机”
				它控制的是：同时并发的 Task 数量，不是线程数量
				
						  ┌───┐
					Task ─▶    │
					Task ─▶    │  最多 3 个能进
					Task ─▶    │
					Task ─▶    │  其余排队
						  └───┘
			 */
			SemaphoreSlim sem = new(3);
			// 任务列表
			List<Task> tasks = [];
			// 任务的数量
			int taskCount = 10;

			for (int i = 1; i <= taskCount; i++)
			{
				int taskId = i;
				// 将Task对象放入List中
				Task task = Task.Run(() => taskAction(sem, taskId));
				tasks.Add(task);
			}

			// 等待所有任务完成
			await Task.WhenAll(tasks);
			Console.WriteLine("所有任务完成");
		}

	}
}
