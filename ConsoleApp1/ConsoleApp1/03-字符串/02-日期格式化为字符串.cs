using System;

namespace ConsoleApp1._03_字符串
{
	class _02_日期格式化为字符串
	{
		// 定义一个日期对象
		private readonly static DateTime dt = new DateTime(2025, 9, 21, 17, 16, 32, 108);

		public static void PrintInfo1()
		{
			// _________ 年的格式化 _________
			Console.WriteLine(string.Format("{0:y}", dt));     // 2025年9月
			Console.WriteLine(string.Format("{0:yy}", dt));    // 25
			Console.WriteLine(string.Format("{0:yyy}", dt));   // 2025
			Console.WriteLine(string.Format("{0:yyyy}", dt));  // 2025

			// _________ 月的格式化 _________
			Console.WriteLine(string.Format("{0:M}", dt));     // 9月21日
			Console.WriteLine(string.Format("{0:m}", dt));     // 9月21日
			Console.WriteLine(string.Format("{0:MM}", dt));    // 09
			Console.WriteLine(string.Format("{0:MMM}", dt));   // 9月
			Console.WriteLine(string.Format("{0:MMMM}", dt));  // 九月

			// _________ 日的格式化 _________
			Console.WriteLine(string.Format("{0:d}", dt));     // 2025/9/21
			Console.WriteLine(string.Format("{0:dd}", dt));    // 21
			Console.WriteLine(string.Format("{0:ddd}", dt));   // 周日
			Console.WriteLine(string.Format("{0:dddd}", dt));  // 星期日

			// _________ 时的格式化 _________
			Console.WriteLine(string.Format("{0:hh}", dt));    // 05
			Console.WriteLine(string.Format("{0:HH}", dt));    // 17

			// _________ 分的格式化 _________
			Console.WriteLine(string.Format("{0:mm}", dt));    // 16

			// _________ 秒的格式化 _________
			Console.WriteLine(string.Format("{0:ss}", dt));    // 32
		}

		public static void PrintInfo2()
		{
			// _________ 日期对象的格式化 _________
			Console.WriteLine(string.Format("{0:yyyy/MM/dd HH:mm:ss.fff}", dt));    // 2025/09/21 17:16:32.108
			Console.WriteLine(string.Format("{0:yyyy/MM/dd dddd}", dt));            // 2025/09/21 星期日
			Console.WriteLine(string.Format("{0:yyyy/MM/dd dddd tt hh:mm}", dt));   // 2025/09/21 星期日 下午 05:16
			Console.WriteLine(string.Format("{0:yyyyMMdd}", dt));                   // 20250921
			Console.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dt));    // 2025-09-21 17:16:32.108

			// 日期对象的ToString()方法也可以实现日期格式化
			Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss.fff"));              // 2025/09/21 17:16:32.108
			Console.WriteLine(dt.ToString("yyyy/MM/dd dddd"));                      // 2025/09/21 星期日
			Console.WriteLine(dt.ToString("yyyy/MM/dd dddd tt hh:mm"));             // 2025/09/21 星期日 下午 05:16
			Console.WriteLine(dt.ToString("yyyyMMdd"));                             // 20250921
			Console.WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss.fff"));              // 2025-09-21 17:16:32.108
		}
	}
}
