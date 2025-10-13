using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._13_IO处理
{
	public class _01_文件写入
	{
		public static void PrintInfo() 
		{
			// 定义一个List
			List<string> msgList =
			[
				"内容1",
				"内容2",
				"内容3"
			];

			// 获取当前用户的桌面路径
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			// 拼接文件路径
			string filePath1 = Path.Combine(desktopPath, "aaaa.txt");
			string filePath2 = Path.Combine(desktopPath, "bbbb.txt");
			string filePath3 = Path.Combine(desktopPath, "cccc.txt");
			string filePath4 = Path.Combine(desktopPath, "dddd.txt");
			string filePath5 = Path.Combine(desktopPath, "eeee.exe");

			// 🔴方式1：简单的文件写入
			if (!File.Exists(filePath1))
			{
				string msg = @"
					Hello World!
					你好, 世界
				";
				// 将整个字符串写入文件
				File.WriteAllText(filePath1, msg);

				// 将所有的行写入文件
				File.WriteAllLines(filePath2, msgList);
				// 追加内容到文件中
				File.AppendAllText(filePath2, "追加的内容1\r\n");
				// 一次追加多行
				File.AppendAllLines(filePath2, new List<string> { "追加的内容2", "追加的内容3" });

				// 将字符串转换为字节数组
				byte[] bytes = Encoding.UTF8.GetBytes(msg);
				// 将字节数组写入到文件中
				File.WriteAllBytes(filePath3, bytes);
			}

			// 🔴方式2：按行写入文件
			if (!File.Exists(filePath4))
			{
				// 🔴创建一个文件流和写入流, 因为使用了 C#8 起的using关键字, 因此可以自动关闭流对象
				using FileStream fs = new(filePath4, FileMode.Create);
				// 使用 utf-8 写入文件
				using StreamWriter wr = new(fs, Encoding.UTF8);

				foreach (string msg in msgList)
				{
					// 写入的时候, 不会自动换行
					wr.Write("--- ");
					// 写入的时候会自动换行
					wr.WriteLine(msg);
				}
			}

			// ==================================================================
			// 大文件
			string bigFilePath = @"E:\VMware-workstation-full-16.0.0-16894299.exe";
			// ==================================================================

			// 🔴方式3：读取文件的字节码, 然后写入
			if (!File.Exists(filePath5))
			{
				// 文件读入和写入对象
				using FileStream fsRead = new(bigFilePath, FileMode.Open, FileAccess.Read);
				using FileStream fsWrite = new(filePath5, FileMode.Create, FileAccess.Write);

				// 每次读取 1M
				byte[] buffer = new byte[10240];
				// 已经读取完毕的字节
				int bytesRead;

				while ((bytesRead = fsRead.Read(buffer, 0, buffer.Length)) > 0)
				{
					fsWrite.Write(buffer, 0, bytesRead);
				}
			}
		}

		// 🔴方式4：异步版本（大文件 + 高性能），不阻塞主线程（适合 GUI / Web 场景）
		public static async Task CopyFileAsync()
		{
			// 待读取和写入的文件路径
			string bigFilePath = @"E:\VMware-workstation-full-16.0.0-16894299.exe";
			string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ffff.exe");

			// 文件读取和写入对象
			using FileStream fsRead = new(bigFilePath, FileMode.Open, FileAccess.Read);
			using FileStream fsWrite = new(dest, FileMode.Create, FileAccess.Write);

			// 每次读取1MB
			byte[] buffer = new byte[1024 * 1024]; 
			int bytesRead;

			// 异步读取文件
			while ((bytesRead = await fsRead.ReadAsync(buffer)) > 0)
			{
				await fsWrite.WriteAsync(buffer.AsMemory(0, bytesRead));
			}
		}
	}
}
