using ConsoleApp1._01_数据类型;
using ConsoleApp1._02_判断与运算符;
using ConsoleApp1._03_字符串;
using ConsoleApp1._04_循环;
using ConsoleApp1._05_数组;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
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
		}
	}
}
