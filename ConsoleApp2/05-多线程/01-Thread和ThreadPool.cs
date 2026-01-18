using System;
using System.Threading;

namespace ConsoleApp2._05_多线程
{
	public class _01_Thread和ThreadPool
	{
		public static void ThreadMethod() 
		{
			/*
				最原始的Thread，现在几乎不用，了解即可
					❌ 创建/销毁成本高
					❌ 不推荐日常使用
			 */
			Thread thread = new(() =>
			{
				Console.WriteLine("子线程开始");
				Thread.Sleep(2000);
				Console.WriteLine("子线程结束");
			});

			thread.Start();
		}

		public static void ThreadPoolMethod() 
		{
			/*
				了解即可，已被 Task 替代
					❌ 无法控制线程生命周期
					❌ 不支持返回值
			 */
			ThreadPool.QueueUserWorkItem(_ =>
			{
				Console.WriteLine("在线程池中执行");
			});
		}
	}
}
