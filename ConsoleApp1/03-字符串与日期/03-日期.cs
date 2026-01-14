using System;
using System.Globalization;

namespace ConsoleApp1._03_日期
{
	public class _03_日期
	{
		public static void PrintInfo1()
		{
			// 🔷日期字符串转换为日期格式
			DateTime date1 = DateTime.ParseExact("20260114093015123", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
			DateTime date2 = DateTime.ParseExact("20260114093015999", "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

			// 🔷1. 方式1：两个日期对象进行比较
			if (date1 > date2)
			{
				Console.WriteLine("日期1 > 日期2");
			}
			else if (date1 < date2)
			{
				Console.WriteLine("日期1 < 日期2");
			}
			else
			{
				Console.WriteLine("日期1 = 日期2");
			}

			// 🔷2. 方式2：两个日期对象进行比较
			int result = DateTime.Compare(date1, date2);
			if (result > 0)
			{
				Console.WriteLine("日期1 > 日期2");
			}
			else if (result < 0)
			{
				Console.WriteLine("日期1 < 日期2");
			}
			else
			{
				Console.WriteLine("日期1 = 日期2");
			}

			// 🔷两个日期相差多少时间
			TimeSpan diffTime = date2 - date1;
			Console.WriteLine($"相差{diffTime.TotalMilliseconds}毫秒");  // 相差876毫秒
			Console.WriteLine($"相差{diffTime.TotalSeconds}秒");  // 相差0.876秒
		}

		public static void PrintInfo2() 
		{
			// 现在的时间
			DateTime t1 = DateTime.Now;

			// 1天后
			DateTime t2 = t1.AddDays(1);
			// 2小时后
			DateTime t3 = t1.AddHours(2);
			// 30分钟后
			DateTime t4 = t1.AddMinutes(30);
			// 30秒后
			t1.AddSeconds(30);
			// 500毫秒后
			DateTime t5 = t1.AddMilliseconds(500);
			// 1年后
			DateTime t6 = t1.AddYears(1);

			// 月初
			DateTime firstDay = new(t1.Year, t1.Month, 1);
			Console.WriteLine($"月初：{firstDay}");

			// 月末
			DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
			Console.WriteLine($"月末：{lastDay}");
		}
	}
}
