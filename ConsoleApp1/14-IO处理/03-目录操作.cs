using System;
using System.IO;

/*
	🔴在C#中，操作目录（文件夹）的核心类主要有两个：
		System.IO.Directory — 提供一组静态方法，用于创建、删除、移动、枚举目录。
		System.IO.DirectoryInfo — 提供实例方法，功能类似 Directory，但可复用对象、面向对象化。
 */
namespace ConsoleApp1._14_IO处理
{
	public class _03_目录操作
	{
		// 获取当前用户的桌面路径
		private static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		// 拼接文件夹路径
		private static readonly string folderPath = Path.Combine(desktopPath, "测试文件夹");

		public static void DirOperate1()
		{
			Console.WriteLine("__________________________________________");

			// 如果文件夹不存在的话
			if (!Directory.Exists(folderPath)) 
			{
				// 新创建一个文件夹
				Directory.CreateDirectory(folderPath);
				Console.WriteLine("文件夹创建成功!");

				// 递归删除所有子目录和文件
				Directory.Delete(folderPath, recursive: true);
				Console.WriteLine("刚创建的文件夹被删除成功!");
			}

			// 获取目录中的子目录
			string[] dirs = Directory.GetDirectories(desktopPath);
			Console.WriteLine("\n获取到的文件夹为: \n" + string.Join("\n", dirs));

			// 获取目录中的所有文件
			string[] files = Directory.GetFiles(desktopPath);
			Console.WriteLine("\n获取到的文件列表为: \n" + string.Join("\n", files));

			// 搜索桌面文件中的所有 txt 文件
			string[] txtFiles = Directory.GetFiles(desktopPath, "*.txt", SearchOption.AllDirectories);
			Console.WriteLine("\n获取到的所有的txt文件列表为: \n" + string.Join("\n", txtFiles));

			// 获取当前的工作目录
			string currentDir = Directory.GetCurrentDirectory();
			Console.WriteLine($"\n当前的工作目录为: {currentDir}\n");

			// 获取程序所在的目录
			string exeDir = AppDomain.CurrentDomain.BaseDirectory;
			Console.WriteLine($"\n程序所在目录为: {exeDir}\n");
		}

		public static void DirOperate2() 
		{
			// 创建一个文件夹对象
			DirectoryInfo dir1 = new(folderPath);
			if (!dir1.Exists)
			{
				Console.WriteLine("测试文件夹不存在, 需要新建...");

				// 创建文件夹
				dir1.Create();
				Console.WriteLine("文件夹新建成功");

				// 删除文件夹
				dir1.Delete();
				Console.WriteLine("新建的文件夹删除成功");
			}

			DirectoryInfo dir2 = new(desktopPath);
			// 获取目录中的子目录
			DirectoryInfo[] subDirs = dir2.GetDirectories();
			foreach (DirectoryInfo subDir in subDirs)
			{
				// 获取文件夹的全路径
				Console.WriteLine(subDir.FullName);
			}

			// 获取一个文件夹对象
			DirectoryInfo subDir1 = subDirs[0];
			// 获取该文件夹对象的父路径对象（由于可能不存在, 所以使用了 ?）
			DirectoryInfo? parentObj = subDir1.Parent;

			// 设置文字为红色
			Console.ForegroundColor = ConsoleColor.Red;
			// 获取父路径文件夹的名称（非全路径）,由于可能不存在, 所以使用了 ?. 的方式来获取属性
			Console.WriteLine(parentObj?.Name);
			// 恢复默认颜色
			Console.ResetColor(); 

			// 获取目录中的子文件
			FileInfo[] files = dir2.GetFiles();
			foreach (FileInfo file in files)
			{
				Console.WriteLine(file.FullName);
			}
		}
	}
}
