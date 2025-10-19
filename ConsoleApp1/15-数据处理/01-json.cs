using System;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

// 使用更加强大的第三方json处理对象
using NsJson = Newtonsoft.Json;
// ---- 因为 System.Text.Json 和 Newtonsoft.Json 都使用了 JsonSerializer
// ---- 为了避免类型名冲突, 所以此处使用了别名
// 使用系统内置的基础json处理对象
using SysJson = System.Text.Json;

namespace ConsoleApp1._15_数据处理
{
	// 定义一个类对象
	public class Person
	{
		// 声明 string 类型的 Name 值不可为 Null
		public required string Name { get; set; }
		// 声明 string 类型的 Address 值可以为 Null
		public string? Address { get; set; }
		public int Age { get; set; }
	}

	/// <summary>
	/// 定义一个json处理的配置对象
	/// 在性能敏感的场景, 微软建议把选项缓存起来
	/// </summary>
	public static class JsonOptions
	{
		public static readonly JsonSerializerOptions Pretty = new()
		{
			// 打印json的时候, 缩进
			WriteIndented = true,
			// 防止汉字被转义为Unicode
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
		};
	}

	public class _01_json
	{
		public static void PrintInfo1() 
		{
			Console.WriteLine("________________________________________________");
			// 定义一个json字符串
			string jsonStr1 = "{\"Name\":\"Tom\",\"Address\":\"地球\",\"Age\":25}";
			// 将json字符串转换为对象
			Person? person1 = SysJson.JsonSerializer.Deserialize<Person>(jsonStr1);
			Console.WriteLine($"{person1?.Name}\n{person1?.Address}\n{person1?.Age}");

			// 创建一个对象
			Person person2 = new() { Name = "Jerry", Address = "月球", Age = 18 };
			// 将对象转换为json字符串
			string jsonStr2 = SysJson.JsonSerializer.Serialize(person2, JsonOptions.Pretty);
			Console.WriteLine(jsonStr2);
		}

		public static void PrintInfo2() 
		{
			Console.WriteLine("________________________________________________");
			// 将json字符串转换为对象
			Person? person = NsJson.JsonConvert.DeserializeObject<Person>("{\"Name\":\"Tom\",\"Address\":\"地球\",\"Age\":25}");
			Console.WriteLine($"{person?.Name}\n{person?.Address}\n{person?.Age}");

			Console.WriteLine("________________________________________________");
			// 将对象转换为json字符串
			string json = NsJson.JsonConvert.SerializeObject(person, NsJson.Formatting.Indented);
			Console.WriteLine(json);
		}
	}
}
