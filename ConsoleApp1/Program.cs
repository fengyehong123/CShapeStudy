// 用户自定义的命名空间
using ConsoleApp1._01_数据类型;
using ConsoleApp1._02_判断与运算符;
using ConsoleApp1._03_字符串;
using ConsoleApp1._04_循环;
using ConsoleApp1._05_数组;
using ConsoleApp1._06_集合;
using ConsoleApp1._07_结构体;
using ConsoleApp1._08_类;
using ConsoleApp1._09_接口;
using ConsoleApp1._10_预处理器指令;
using ConsoleApp1._11_using的用法;
using ConsoleApp1._12_正则表达式;
using ConsoleApp1._13_IO处理;

// 系统的命名空间
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static void Main01(string[] args)
		{
			// 类的静态方法
			_01_基本数据类型.PrintInfo();
			_02_引用数据类型.PrintInfo();

			_01_字符串基本使用.PrintInfo1();
			_02_日期格式化为字符串.PrintInfo1();
			_02_日期格式化为字符串.PrintInfo2();

			// 创建对象的时候, 类型可以使用var
			var obj = new _03_常量();
			obj.PrintInfo();

			_01_if判断与运算符.PrintInfo();
			_02_switch语句.PrintInfo();
			_03_双问号运算符.PrintInfo();

			_01_while循环.PrintInfo();
			_02_for循环.PrintInfo();

			_02_交错数组.PrintInfo();
			_03_参数数组.PrintInfo("贾飞天", 1, 2, 3);
			_04_数组的属性方法.PrintInfo1();
			_04_数组的属性方法.PrintInfo2();

			// 创建一个结构体
			_01_定义结构体 struct1 = new(10, 20);
			struct1.Display();
			_01_定义结构体 struct2 = new _01_定义结构体(100, 200);
			struct2.Display();

			// 集合相关
			_01_List.PrintInfo1();
			_01_List.PrintInfo2();
			_02_Dictionary.PrintInfo();
			_03_HashSet.PrintInfo1();
			_03_HashSet.PrintInfo2();
			_04_SortedList.PrintInfo();
			_05_SortedDictionary.PrintInfo();
			集合表达式.PrintInfo();
		}

		static void Main02(string[] args)
		{
			_01_创建类_Utils.PrintInfo();
			_03_抽象类_Utils.PrintInfo();
			_04_密封类_Utils.PrintInfo();
			_05_枚举类_Utils.PrintInfo1();
			_05_枚举类_Utils.ForeachEnum();
			虚方法_Utils.PrintInfo();
			_09_策略模式接口_Utils.Exec();
		}

		static void Main03(string[] args)
		{
			预处理器指令.PrintInfo();
			Using的用法.PrintInfo();
			_01_正则表达式.PrintInfo();
		}

		static async Task Main04(string[] args)
		{
			_01_文件写入.PrintInfo();
			// 异步读取和写入文件
			await _01_文件写入.CopyFileAsync();
		}

		static void Main(string[] args)
		{
			// _02_文件读取.ReadFile();
			_03_目录操作.DirOperate();
		}
	}
}
