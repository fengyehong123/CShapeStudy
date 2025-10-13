using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._13_IO处理
{
	public class _04_判断
	{
		public static void PrintInfo() 
		{
			// 获取当前用户的桌面路径
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			// 拼接文件路径
			string filePath = Path.Combine(desktopPath, "常用英文词汇.txt");

			// 🔴判断文件是否存在
			if (!File.Exists(filePath))
			{
				Console.WriteLine("文件存在...");
			}

			// 🔴判断文件夹是否存在
			if (!Directory.Exists(desktopPath))
			{
				Console.WriteLine("文件夹是存在的...");
			}

			// 🔴判断当前路径是文件还是文件夹
		}
	}
}
