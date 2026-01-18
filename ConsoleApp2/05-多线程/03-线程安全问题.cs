using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2._05_多线程
{
	public class _04_线程安全问题
	{
		/*
			Parallel.For = 并行版的 for 循环
				会自动使用多个线程，把循环拆分后同时执行
				顺序 不保证
			Parallel.For 的本质
				基于 Task + 线程池
				自动根据 CPU 核心数 调度
				属于 TPL（Task Parallel Library）
			
			✅ 非常适合
				1. 大量计算
				2. 图像处理
				3. 加密 / 哈希
				4. 数值运算
			❌ 非常不适合
				1. IO 操作（HTTP / DB / File）
				2. 需要顺序的逻辑
				3. UI 线程
				4. 访问共享变量但不加锁
		 */
		public static void PrintInfo1() 
		{
			int count = 0;
			
			// 并发修改count
			Parallel.For(0, 1000, i =>
			{
				count++;
			});

			// ❌ 结果不一定是 1000
			Console.WriteLine(count);
		}

		public static void PrintInfo2() 
		{
			// 通过 Interlocked 解决并发问题
			int count = 0;

			Parallel.For(0, 100000, _ =>
			{
				// 自增
				Interlocked.Increment(ref count);

				// 除了自增之外, 还有自减
				// Interlocked.Decrement(ref count);

				// 还有加法
				// Interlocked.Add(ref count, 5);
			});

			// 一定是 100000
			Console.WriteLine(count);
		}
	}
}
