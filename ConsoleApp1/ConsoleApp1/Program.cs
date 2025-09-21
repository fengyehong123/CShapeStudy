using ConsoleApp1._01_数据类型;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			// 类的静态方法
			_01_基本数据类型.PrintInfo();
			_02_引用数据类型.PrintInfo();
			_03_字符串.PrintInfo1();
			_04_日期格式化为字符串.PrintInfo1();
			_04_日期格式化为字符串.PrintInfo2();

			// 创建对象的时候, 类型可以使用var
			var obj = new _05_常量();
			obj.PrintInfo();
		}
	}
}
