using System;

namespace ConsoleApp2._03_委托
{
	public class _02_委托使用
	{
		// 定义两个委托方法
		private static readonly Action action1 = () => Console.WriteLine("Hi");
		private static readonly Action<string, string> action2 = (msg1, msg2) =>
		{
			Console.WriteLine($"你好 → {msg1}, {msg2}");
		};

		public static void PrintInfo()
		{
			// 委托是可以【多播】的, 即多个方法绑定到一个委托
			// 下面这种用法就是 【多播委托】（Multicast Delegate）
			Action action = action1;
			// 委托的是方法的引用, 如果我们这样写就会报错, 因为这样写就变成了 将方法的返回值绑定了
			// 所以需要使用 Lambda 来生成一个新的委托实例
			//   action += action2("世界", "World");
			action += () => action2("世界", "World");

			// 通过多播委托, 一口气执行绑定的两个方法
			action();
		}
	}
}
