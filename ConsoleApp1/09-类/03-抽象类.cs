using System;

/*
  ✅ 特点：
	1. 不能实例化。
	2. 可以包含：
		2.1 抽象方法（没有实现的，只定义签名）。
		2.2 普通方法（有实现的）。
		2.3 字段、属性等。
	3. 需要由子类继承并实现抽象方法。
  ✅ 常见用途：
	定义一个 通用的基类（模板），子类必须去实现某些具体逻辑。
*/
namespace ConsoleApp1._09_类
{
	public abstract class _03_抽象类
	{
		// 普通的属性
		public string? Name { get; set; }

		// 抽象属性, 子类必须实现
		public abstract int Age { get; set; }

		// 抽象方法（子类必须实现）
		public abstract void Speak();

		// 普通方法（子类可直接使用）
		public void Eat() => Console.WriteLine($"{Name} is eating.");
	}

	// Dog类继承  _03_抽象类 ,然后实现里面的方法
	public class Dog : _03_抽象类
	{
		public string? Address { get; set; } = "地球";

		// 实现父类的抽象属性
		public override int Age { get; set; } = 18;

		// 属性也可以是计算得到的
		public string Msg
		{
			get { return $"我来自{this.Address}"; }
		}

		// 无参的构造函数
		public Dog(){ }

		// 有参的构造函数
		public Dog(string address)
		{ 
			this.Address = address;
		}

		public override void Speak() => Console.WriteLine($"{Name} says: Woof!");
	}

	public class _03_抽象类_Utils
	{
		public static void PrintInfo()
		{
			// 创建一个类对象
			_03_抽象类 dog1 = new Dog();
			dog1.Name = "万里长城";
			dog1.Eat();
			dog1.Speak();

			// 使用对象初始化器的方式更加简洁的创建一个对象
			//（注意: 此处的属性是通过初始化器, 而不是有参构造函数来添加的）
			_03_抽象类 dog2 = new Dog { Name = "Buddy" };
			dog2.Eat();
			dog2.Speak();

			// 通过有参构造函数 + 对象初始化器的方式创建一个对象
			_03_抽象类 dog3 = new Dog("宇宙银河系") { Name = "Tom" };
			dog3.Eat();
			dog3.Speak();

			// 获取动态计算的属性
			Dog dog4 = new("超级宇宙银河系") { Name = "FengYeHong" };
			Console.WriteLine(dog4.Msg);
		}
	}
}
