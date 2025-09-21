using System;

/*
	特性                const                                   readonly
	初始化时机          编译期必须有值                          可以在声明时或构造函数里赋值
	值能否依赖运行时    ❌ 不行（只能是编译时常量）            ✅可以依赖运行时结果
	类型限制            只能是内置类型（数字、字符串、布尔等） 任意类型（对象、数组等）
	修改可能性          完全不能改                              只能在构造函数里改，之后就固定
	性能                编译时替换，效率高                      运行时存储，取值有一次字段访问开销
*/

/*
	const
		用于 真正不会变的固定值，比如数学常量、配置中的魔法数字、状态码等。

	readonly
		在实际项目里 readonly 用得更多，
		因为很多值要在运行时才能确定（比如时间戳、配置、依赖注入对象）。
	
	📝经验法则
		能确定编译期常量            → 用 const
		需要运行时赋值/依赖构造函数 → 用 readonly
*/

namespace ConsoleApp1._01_数据类型
{
	class _03_常量
	{
		// ___________________________ 静态常量（编译时常量）const	 ___________________________
		// 在编译时就确定了值，必须在声明时就进行初始化且之后不能进行更改，可在类和方法中定义。
		/*
			✅带有 private 私有访问修饰符的常量要以骆驼命名法命名
			即以下划线开头，第一个单词的首字母小写，余下单词首字母大写。 
		*/
		private const string _bookName = "新华字典";
		/*
			✅带有 public 公共修饰符、protected 受保护修饰符等的常量要以帕斯卡命名法命名
			即各个单词首字母都要大写。
		*/
		public const int BookPrice = 10;

		// ___________________________ 动态常量（运行时常量）readonly ___________________________
		// 在运行时确定值，只能在声明时或构造函数中初始化，只能在类中定义。
		public readonly DateTime CreatedAt;

		// readonly的变量也可以在编译时就指定好
		private readonly static string str1 = "Hello World!";
		
		// 构造方法, 在构造函数对象的时候，赋值常量
		public _03_常量()
		{
			// 通过构造函数, 在运行时赋值
			CreatedAt = DateTime.Now;
		}

		public void PrintInfo()
		{
			// 打印静态常量
			Console.WriteLine(_bookName);
			Console.WriteLine(str1);
			
			// 打印动态常量
			Console.WriteLine(this.CreatedAt);
		}

	}
}
