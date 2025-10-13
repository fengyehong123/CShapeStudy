using System;

namespace ConsoleApp1._02_判断与运算符
{
	internal class _03_双问号运算符
	{
		public static void PrintInfo()
		{

			// ? 单问号用于对 int、double、bool 等无法直接赋值为 null 的数据类型进行 null 的赋值
			// 意思是这个数据类型是 Nullable 类型的。
			double? num1 = null;
			double? num2 = 3.14157;

			// 和 JS 中的 ?? 的用法类似, 用于防止值为null
			Console.WriteLine("num1 的值： {0}", num1 ?? 5.34);
			Console.WriteLine("num2 的值： {0}", num2 ?? 5.34);
		}
	}
}
