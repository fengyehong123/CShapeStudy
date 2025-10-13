using System;
/*
	🔴在 .NET 6 及以上版本（包括 .NET 8） 中，默认开启了 隐式 using（Implicit Usings） 功能，
	所以像 
		using System;
		using System.Collections.Generic; 
	这些常见命名空间不需要手动写也能直接使用。
 */

namespace ConsoleApp1._01_数据类型
{
	class _01_基本数据类型
	{
		public static void PrintInfo()
		{
			// 64位双精度浮点型
			double length = 4.5;
			double width = 3.5;
			Console.WriteLine(length + width);

			// 布尔值
			bool flag = true;
			Console.WriteLine(flag);
		}
	}
}