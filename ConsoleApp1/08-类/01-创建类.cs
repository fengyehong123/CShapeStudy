using System;

namespace ConsoleApp1._08_类
{
	public class _01_创建类
	{
		// 字段（Field）   —— 内部存储数据
		private string name;
		private int age;

		// 字段 + get 和 set 的简写方式, 带默认值
		public string Address { get; set; } = "地球";
		// 没有带默认值
		public string Msg { get; set; }

		// 类中的静态成员, 并不依赖于类的实例对象
		public static int Count = 12336;

		// 属性（Property）—— 对字段的安全访问方式, 相当于java中的get 和 set 方法
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public int Age
		{
			get { return age; }
			set { age = value; }
		}

		// 有参构造函数
		public _01_创建类(string name, int age)
		{
			this.name = name;
			this.age = age;
		}

		// 无参的构造函数, 此处使用了 private 禁止无参实例化
		private _01_创建类() { }

		// 类的普通方法
		public void SayHello()
		{
			Console.WriteLine($"你好，我是 {this.name}，今年 {this.age} 岁。");
		}

		// 类的静态方法
		public static void SayHello1() 
		{
			Console.WriteLine("你好, 世界!");
		}
	}

	public class _01_创建类_Utils
	{
		public static void PrintInfo()
		{
			// 实例化一个类对象
			_01_创建类 cls1 = new("贾飞天", 18);
			// 类对象的方法
			cls1.SayHello();  // 你好，我是 贾飞天，今年 18 岁。

			// 类的静态方法
			_01_创建类.SayHello1();  // 你好, 世界!
								  // 类的静态属性
			Console.WriteLine(_01_创建类.Count);  // 12336

			// 类属性的默认值
			Console.WriteLine(cls1.Address);  // 地球
											  // 使用set方法来修改类的属性
			cls1.Name = "你的名字";
			cls1.Msg = "来自地球";
			Console.WriteLine(cls1.Name);  // 你的名字
			Console.WriteLine(cls1.Msg);  // 来自地球
		}
	}
}
