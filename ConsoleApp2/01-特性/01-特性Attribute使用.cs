using System;
using System.Runtime.InteropServices;

/*
 * 🔴在C#中，特性（Attribute） 是一种用于给程序元素（类、方法、属性、字段等）添加元数据的机制。
 * 这些元数据在编译时被保存到程序集中（.dll / .exe），可以在运行时通过 反射（Reflection） 读取并使用。
 * 
 * 🔴特性（Attribute）本质上就是一个类，它继承自 System.Attribute。
 * 它的作用类似“标签”或“注释”，但比普通注释强大得多 —— 因为它能被程序识别和使用。
 */
namespace ConsoleApp2._01_特性
{
	public class Attribute1 
	{
		// 使用C#自带的 Obsolete 特性, 标记方法已经过时
		[Obsolete("请使用 NewMethod 替代")]
		public static void OldMethod()
		{
			Console.WriteLine("旧方法");
		}

		public static void NewMethod()
		{
			Console.WriteLine("新方法");
		}
	}

	// 使用C#自带的 Serializable 特性, 标记类可以序列化
	[Serializable]
	class Person
	{
		public required string Name;
		public int Age;
	}

	// Windows PowerShell 5.1（.NET Framework）的话, 可以使用 DllImport
	class WindowsAPI_1
	{
		// 使用C#自带的 DllImport 特性, 告诉C# 这个方法来自于外部DLL
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern void MessageBox(IntPtr hWnd, string text, string caption, uint type);

		// 如果我们想让自定类中的方法名的话, 需要通过 EntryPoint 指定 Windows API 中的方法名, 然后就可以自定义类方法名了
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MessageBox")]
		public static extern void MsgBox(IntPtr hWnd, string text, string caption, uint type);
	}

	/*
		在Powershell中使用的话：
			Powershell版本为PowerShell 7.0+（基于 .NET 5/6/7/8）的话, 才能使用 LibraryImport。
			Windows PowerShell 5.1（.NET Framework）的话, 只能使用 DllImport, 不能使用LibraryImport。
	 */
	public static partial class WindowsAPI_2
	{
		/*
			从 .NET 8 开始，使用 [LibraryImport] 时，
			字符串参数必须显式说明封送规则（Marshalling Rules），
			否则编译器不知道你希望用 ANSI 还是 Unicode 字符集去调用 Win32 API。

			在 Windows API 里，MessageBox 实际导出函数名是区分 ANSI 和 Unicode 的：
				ANSI 版本：MessageBoxA
				Unicode 版本：MessageBoxW
		 */
		[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
		private static partial int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

		// 提供一个安全封装方法, 避免直接调用 MessageBoxW
		public static void ShowMessage(string text, string caption = "提示")
		{
			_ = MessageBoxW(IntPtr.Zero, text, caption, 0);
		}
	}

	public class _01_特性Attribute使用
	{
		public static void PrintInfo() 
		{
			// 在使用过时方法的时候, 编译器会有提示
			Attribute1.OldMethod();

			// 使用兼容性更好的旧方法, 调用 Windows API
			WindowsAPI_1.MessageBox(IntPtr.Zero, "你好，世界！", "测试标题", 0);
			WindowsAPI_1.MsgBox(IntPtr.Zero, "你好，世界！", "测试标题", 0);

			// 使用.NET8之后的新方法, 调用 Windows API
			WindowsAPI_2.ShowMessage("Hello, World!");
		}
	}
}
