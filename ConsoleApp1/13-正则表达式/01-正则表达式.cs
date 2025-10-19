using System;
// C# 中主要通过 RegularExpressions 支持正则表达式
using System.Text.RegularExpressions;

/*
 * 
功能               方法                返回类型
判断是否匹配        Regex.IsMatch()     bool  
获取第一个匹配      Regex.Match()       Match 
获取所有匹配        Regex.Matches()     MatchCollection 
替换文本            Regex.Replace()     string
拆分文本            Regex.Split()       string[]
 * 
*/
namespace ConsoleApp1._13_正则表达式
{
	public partial class _01_正则表达式
	{
		// 正则表达式
		[GeneratedRegex(@"Name:(?<name>\w+), Age:(?<age>\d+)")]
		private static partial Regex MyRegex();

		public static void PrintInfo() 
		{
			Console.WriteLine("________________________________________________");
			// 待匹配的文本
			string txt1 = "hello 123 world 456";

			// 匹配一个或多个数字的正则对象
			// 🔴字符串前面的 @，这是 C# 的逐字字符串，可以避免 \\ 这种转义太多的写法。
			Regex regex1 = new(@"\d+");
			// 匹配之后, 得到结果
			Match match = regex1.Match(txt1);
			Console.WriteLine(match.Value);

			Console.WriteLine("________________________________________________");
			string text = "apple 123 banana 456 cherry 789";
			// 🔴匹配之后, 得到多个结果
			Regex regex2 = new(@"\d+");
			MatchCollection matches = regex2.Matches(text);

			foreach (Match m in matches)
			{
				Console.WriteLine(m.Value);
			}

			Console.WriteLine("________________________________________________");
			// 🔴判断是否能匹配
			if (Regex.IsMatch("abc123", @"\d+"))
			{
				Console.WriteLine("包含数字！");
			}

			Console.WriteLine("________________________________________________");
			string input = "ID:123, Code:456";
			// 🔴使用正则进行文本替换
			string result = Regex.Replace(input, @"\d+", "###");
			Console.WriteLine(result);

			Console.WriteLine("________________________________________________");
			string text1 = "Name:Tom, Age:25";
			// 🔴分组 -> 旧的语法
			Regex regex = new(@"Name:(?<name>\w+), Age:(?<age>\d+)");
			Match match2 = regex.Match(text1);

			Console.WriteLine(match2.Groups["name"].Value);  // Tom
			Console.WriteLine(match2.Groups["age"].Value);   // 25

			Console.WriteLine("________________________________________________");
			string text2 = "Name:Tom, Age:25";
			// 🔴分组 -> 新的语法
			Regex regex3 = MyRegex();
			Match match3 = regex3.Match(text2);

			Console.WriteLine(match3.Groups["name"].Value);  // Tom
			Console.WriteLine(match3.Groups["age"].Value);   // 25
		}
	}
}
