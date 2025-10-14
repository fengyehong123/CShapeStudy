using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1._13_IO处理
{
	public class _02_文件读取
	{
		// 获取当前用户的桌面路径
		private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		public static void ReadFile()
		{
			// 拼接文件路径
			string filePath1 = Path.Combine(desktopPath, "测试文件.txt");

			Console.WriteLine("__________________________________________");
			// 🔴方式1：读取整个文件内容
			string content1 = File.ReadAllText(filePath1, Encoding.UTF8);
			Console.WriteLine(content1);

			Console.WriteLine("__________________________________________");
			// 🔴方式2：读取为字符串数组（按行分割）
			string[] contentLines = File.ReadAllLines(filePath1, Encoding.UTF8);
			foreach (string line1 in contentLines)
			{
				Console.WriteLine(line1);
			}

			Console.WriteLine("__________________________________________");
			// 🔴方式3：流式读取, 逐行读取
			//   适合读取大文件，不会一次性加载到内存。
			using StreamReader sr1 = new(filePath1, Encoding.UTF8);
			string? line2 = null;
			while ((line2 = sr1.ReadLine()) != null)
			{
				Console.WriteLine(line2);
			}

			Console.WriteLine("__________________________________________");
			// 🔴方式4：读取为完整的字符串
			using StreamReader sr2 = new(filePath1, Encoding.UTF8);
			string content2 = sr2.ReadToEnd();
			Console.WriteLine(content2);

			Console.WriteLine("__________________________________________");
			// 🔴方式5：按字节读取（常用于二进制文件）
			byte[] bytes = File.ReadAllBytes(filePath1);
			Console.WriteLine($"文件长度: {bytes.Length} 字节");

			// 或者通过 FileStream 手动控制读取
			string filePath2 = Path.Combine(desktopPath, "常用英文词汇.txt");

			// 设置每次要读取的字节数
			byte[] buffer = new byte[1024];
			// 读取到的字节数
			int bytesRead;

			using FileStream fs = new(filePath2, FileMode.Open);
			while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
			{
				Console.WriteLine($"读取 {bytesRead} 字节");
			}

			Console.WriteLine("__________________________________________");
			// 🔴方式6：逐行读取（懒加载）
			// 与 ReadAllLines 不同，ReadLines 是惰性读取（不会一次性加载整个文件）。
			foreach (string line in File.ReadLines(filePath1))
			{
				Console.WriteLine(line);
			}

			Console.WriteLine("__________________________________________");
			// 🔴方式7：LINQ 风格处理文件内容
			var lines = File.ReadLines(filePath1)
							.Where(line => !string.IsNullOrWhiteSpace(line))
							.Select(line => line.Trim());
			foreach (string line in lines) 
			{
				  Console.WriteLine(line);
			}
		}
	}
}
