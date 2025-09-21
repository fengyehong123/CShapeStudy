using System;

namespace ConsoleApp1._03_字符串
{
	class _01_字符串基本使用
	{
		public static void PrintInfo1()
		{
			// 定义一个字符串
			string str1 = "C:\\Windows";

			// C# 中的 @ 称作"逐字字符串", 可以将转义字符当做普通字符来对待
			string str2 = @"C:\Windows";

			// @ 字符串中可以任意换行，换行符及缩进空格都计算在字符串长度之内
			string str3 = @"
			你好
			世界
			";

			Console.WriteLine(str1);
			Console.WriteLine(str2);
			Console.WriteLine(str3);

			// 字符串长度获取
			Console.WriteLine(str1.Length);  // 10

			string str4 = "Hello ";
			string str5 = "World";

			// 字符串拼接
			Console.WriteLine(str4 + str5);  // Hello World
			Console.WriteLine(string.Concat(str4, str5));  // Hello World

			// 字符串比较
			Console.WriteLine(str4.Equals("hello"));  // False
			// 字符串比较, 忽略大小写
			Console.WriteLine(str4.Trim().Equals("hello", StringComparison.OrdinalIgnoreCase));  // True

			int age = 25;
			string name = "贾飞天";

			// _________ 格式化 _________
			// 插值字符串
			Console.WriteLine($"Name: {name}, Age: {age}");
			// Format 方法
			Console.WriteLine(string.Format("姓名: {0}, 年龄: {1}", name, age));

			// _________ 判断空白 _________
			Console.WriteLine(string.IsNullOrEmpty(""));  // True
			Console.WriteLine(string.IsNullOrWhiteSpace("  "));  // True

			// _________ 数组与字符串 _________
			// 数组元素拼接为字符串
			string[] arr = { "A", "B", "C" };
			Console.WriteLine(string.Join("_", arr));  // A_B_C
			// 字符串拆分为数组
			string[] parts = "A_B_C".Split('_');
			foreach (string part in parts) {
				Console.WriteLine(part);
			}

			// 重复字符串
			Console.WriteLine(new string('&', 5));  // &&&&&

			// 字符串包含
			Console.WriteLine(name.Contains("天"));   // True
			Console.WriteLine(name.StartsWith("贾")); // True
			Console.WriteLine(name.EndsWith("ld"));   // False

			// 字符串截取
			Console.WriteLine("Hello World".Substring(0, 5)); // Hello
		}
	}
}
