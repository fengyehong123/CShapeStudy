using System;

namespace ConsoleApp1._08_结构体
{
	// 定义一个接口
	public interface IShape
	{
		void PrintMsg1();
	}

	// 结构体实现接口
	public readonly struct _02_实现接口的结构体 : IShape
	{
		private readonly string msg = "Hello Wrold";

		// 定义一个无参构造器
		public _02_实现接口的结构体()
		{
		}

		// 实现的接口中的方法
		public readonly void PrintMsg1()
		{
			Console.WriteLine(this.msg);
		}

		// 结构体中也支持静态方法
		public static void PrintMsg2()
		{
			Console.WriteLine("你好, 世界!");
		}
	}
}
