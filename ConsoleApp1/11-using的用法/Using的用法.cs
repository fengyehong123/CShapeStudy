using System;
using System.IO;
// 🔴为类型取别名
using IntList = System.Collections.Generic.List<int>;
// 🔴为命名空间取别名
using SuperIO = System.IO;
// 🔴导入类的静态成员
using static ConsoleApp1._08_类._01_创建类;

namespace ConsoleApp1._11_using的用法
{
	public class Using的用法
	{
		public static void PrintInfo()
		{
			// 🔴 ================ 作用1：================
			// 因为导入了 【using System;】 其实下面的 System 可以省略
			System.Console.WriteLine("Hello");

			// 获取当前用户的桌面路径
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			// 拼接文件路径
			string filePath = Path.Combine(desktopPath, "常用英文词汇.txt");


			// 💦之前的做法
			// 当一个对象实现了 IDisposable 接口（如文件流、数据库连接、网络连接等），
			// 需要手动释放资源，否则会造成内存或句柄泄漏。

			FileStream fs1 = new(filePath, FileMode.Open);
			try
			{
				// 读写文件操作
				Console.WriteLine(fs1.Length);
			}
			finally
			{
				// 手动释放资源
				fs1.Dispose();
			}

			// 🔴 ================ 作用2：================
			// 使用using语句块简写, 自动释放资源
			using FileStream fs = new(filePath, FileMode.Open);
			// 自动在块结束时调用 fs.Dispose()
			Console.WriteLine(fs.Length);

			// 🔴 ================ 作用3：================
			// 命名空间或类型名字太长，或者重名，可以给它取个短别名。
			IntList numbers = new() { 1, 2, 3 };
			Console.WriteLine(string.Join(", ", numbers));

			// 命名空间别名
			// 写入内容到文件
			SuperIO.File.WriteAllText(Path.Combine(desktopPath, "aa.txt"), "Hello World!");
			// 删除刚创建的文件
			SuperIO.File.Delete(Path.Combine(desktopPath, "aa.txt"));

			// 🔴 ================ 作用4：================
			// 从 C#6起，可以导入一个类的静态成员，直接使用方法名而不写类名。
			SayHello1();
		}
	}
}
