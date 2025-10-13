/*
	在 C# 里，结构体（struct） 是一种 值类型，和类（class）相似但有一些重要区别。
	🔴特点
		1. 值类型：存储在栈上（或者嵌套在引用类型对象里）。赋值时会 复制整个对象。
		2. 不能显式定义无参构造函数（C# 10 起支持 public Point() {}，但旧版本不行）。
		3. 可以有方法、属性、事件、索引器。
		4. 不能继承（不能从另一个 struct 或 class 派生），但能实现接口。
		5. 默认继承自 System.ValueType。
		6. 适合表示小型、轻量级的数据结构，比如坐标、颜色、时间段等。

	🔴与类的区别
		特性      struct（结构体）                     class（类）            
		类型      值类型                               引用类型                
		存储      栈（或作为字段存储在堆上对象中）       堆                   
		继承      不能继承，能实现接口                  可以继承（单继承）和实现接口      
		默认构造  自动提供无参构造，可为字段设为默认值    如果没有定义，编译器会自动生成无参构造 
		析构函数  不支持                               支持                  
		使用场景  小数据对象，高性能需求                 大型复杂对象              

	🔴创建结构体对象的简写和正常写法
	_01_定义结构体 struct1 = new(10, 20);
	struct1.Display();
	_01_定义结构体 struct2 = new _01_定义结构体(100, 200);
	struct2.Display();
*/
using System;

namespace ConsoleApp1._07_结构体
{
	public struct _01_定义结构体
	{
		public int X;
		public int Y;
		public string msg = "Hello Wrold";

		// 带参数的构造函数
		public _01_定义结构体(int x, int y)
		{
			X = x;
			Y = y;
		}

		// 方法
		public readonly void Display()
		{
			Console.WriteLine($"X = {X}, Y = {Y}");
		}
	}
}
