using System;
using System.IO;
using System.Text;

namespace ConsoleApp1._14_IO处理
{
	public class _04_文件操作
	{
		// 获取当前用户的桌面路径
		private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		// 拼接桌面文件所在的路径
		private static readonly string filePath = Path.Combine(desktopPath, "Linux命令.txt");

		public static void FileOperate1() 
		{
			// 创建一个文件对象
			FileInfo fileObj = new(filePath);

			// 判断文件是否存在
			Console.WriteLine(fileObj.Exists);

			// 获取文件的后缀名
			Console.WriteLine(fileObj.Extension);

			// 获取文件名, 不含路径
			Console.WriteLine(fileObj.Name);

			// 获取文件的全路径
			Console.WriteLine(fileObj.FullName);

			// 文件所在目录的路径字符串
			Console.WriteLine(fileObj.DirectoryName);

			// 获取文件所在的目录对象(DirectoryInfo 对象)
			DirectoryInfo? dir = fileObj.Directory;
			Console.WriteLine(dir?.FullName);

			// 复制文件到指定位置
			// fileObj.CopyTo("");

			// 移动文件到指定位置
			// fileObj.MoveTo("");

			// 删除文件
			// fileObj.Delete();

			// 打开文件, 并返回 FileStream
			FileStream fs1 = fileObj.Open(FileMode.Open);
			Console.WriteLine(fs1.Length);
			fs1.Close();

			// 以只读方式打开文件
			FileStream fs2 = fileObj.OpenRead();
			Console.WriteLine(fs2.Length);
			fs2.Close();

			// 以写入的方式打开文件
			using FileStream fs3 = fileObj.OpenWrite();
			// 使用 utf-8 写入文件
			using StreamWriter wr3 = new(fs3, Encoding.UTF8);
			wr3.WriteLine("你好");
		}
	}
}
