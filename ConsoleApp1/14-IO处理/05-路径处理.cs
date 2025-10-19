using System;
using System.IO;

namespace ConsoleApp1._14_IO处理
{
	public class _05_路径处理
	{
		// 假设存在如下路径
		private static readonly string tmpPath = @"C:\Users\Admin\Documents\report.txt";

		public static void PathOperate() 
		{
			// 当前的系统目录分隔符
			Console.WriteLine(Path.DirectorySeparatorChar);

			// 获取桌面路径
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			Console.WriteLine(desktopPath);

			// 获取我的文档路径
			string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			Console.WriteLine(documents);

			// 获取路径中的文件夹部分
			Console.WriteLine(Path.GetDirectoryName(tmpPath));  // C:\Users\Admin\Documents

			// 获取路径中的文件名称(含拓展名)
			Console.WriteLine(Path.GetFileName(tmpPath));  // report.txt

			// 获取路径中的文件名称(不含拓展名)
			Console.WriteLine(Path.GetFileNameWithoutExtension(tmpPath));  // report

			// 获取路径中的文件的扩展名
			Console.WriteLine(Path.GetExtension(tmpPath));  // .txt

			// 路径拼接
			string combined = Path.Combine(@"C:\Users\Admin", "Desktop", "test.txt");
			Console.WriteLine(combined);  // C:\Users\Admin\Desktop\test.txt

			// 修改拓展名
			string newPath = Path.ChangeExtension(tmpPath, ".csv");
			Console.WriteLine(newPath);  // C: \Users\Admin\Documents\report.csv

			// 获取系统临时目录
			Console.WriteLine(Path.GetTempPath());

			// 获取随机文件名(不创建文件)
			Console.WriteLine(Path.GetRandomFileName());
		}
	}
}
