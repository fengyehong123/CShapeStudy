// 自定义的符号
#define DEBUG_MODE
using System;

/*
 * 🔴 C# 的预处理指令（Preprocessor Directives） 是编译前由编译器处理的特殊指令，
 * 它们不属于 C# 语法本身，而是用来控制编译过程的。
 * 这些指令都以 # 开头
 * 例如：
 *     #define                        定义一个符号（常用于条件编译）
 *     #undef                         取消定义一个符号
 *     #if / #elif / #else / #endif   条件编译
 *     #region / #endregion           折叠代码区域（IDE 特性）
 *     #pragma                        控制编译器行为（如关闭警告）
 *     #warning                       生成一个编译警告
 *     #error                         生成一个编译错误
 *     
 * 🧩 一、什么是预处理指令
 * 预处理指令是告诉编译器「编译哪些代码、忽略哪些代码」或「生成警告/错误」的命令。
 * 它们不是运行时执行的，而是编译时生效。
 * 
 * 🧠 二、配合编译器内置符号使用
 * C# 编译器自动定义了一些符号
 * DEBUG                              在 Debug 模式下自动定义
 * TRACE                              可用于日志跟踪
 * NET8_0_OR_GREATER                  表示运行时版本条件（C# 10+）
 */
namespace ConsoleApp1._10_预处理器指令
{
	public class 预处理器指令
	{
		// 🔴 代码折叠区域指令, 在IDE里面, 会被折叠城一个可以展开的代码区域。
		#region 数据访问层
		public class Repository
		{
			public void Add() { }
			public void Delete() { }
		}
		#endregion

		public static void PrintInfo() 
		{
			// 如果自定义的符号存在的话
			#if DEBUG_MODE
				Console.WriteLine("调试模式：输出调试信息");
			#else
				Console.WriteLine("发布模式：不输出调试信息");
			#endif

			// 使用C#自带的符号
			#if DEBUG
				Console.WriteLine("当前是 Debug 模式");
			#else
				Console.WriteLine("当前是 Release 模式");
			#endif

			// 两个符号若都存在的话
			#if DEBUG && DEBUG_MODE
				// 当编译时，这条指令不会中断编译，但会在“错误列表”或“输出窗口”中显示警告信息。
				#warning TODO：有未完成的功能
			#endif
		}
	}
}
