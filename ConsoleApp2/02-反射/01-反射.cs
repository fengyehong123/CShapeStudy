using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp2._02_反射
{
	// 🔷定义一个特性
	[
		AttributeUsage(
			// 值允许作用在类上
			AttributeTargets.Class,
			// 不允许多次使用
			AllowMultiple = false
		)
	]
	public class AuthorizeAttribute(string role) : Attribute
	{
		public string Role { get; } = role;
	}

	[Authorize("Admin")]
	public class User
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Age { get; set; }
		// 声明 Address 并不是必须项目, 可以为 null
		public string? Address { get; set; }

		// 非静态方法
		public void PrintUerInfo1(string msg1, string msg2)
		{
			Console.WriteLine($"{Name} {msg1}, {msg2}");
		}

		// 静态方法
		private static void PrintUerInfo2(string msg1, string msg2)
		{
			Console.WriteLine($"你好{msg1}, {msg2}");
		}
	}

	/*
		🔷反射 = 在运行时检查和操作程序集、类型、成员的能力
			简单说就是：
			程序在“运行中”，还能去看自己是什么类、有什么方法、属性、特性，然后调用它们
	 */
	public class _01_反射
	{
		// 创建一个类对象
		private static readonly User user1 = new()
		{
			Id = 110120,
			Name = "Tom",
			Age = 25,
			Address = "宇宙"
		};

		// 🔷Type 是整个 C# 的反射的核心入口
		public static void PrintInfo1()
		{

			// 获取Type的方式1：编译期确定, 最常用
			Type t1 = typeof(User);
			Console.WriteLine(t1);

			// 获取Type的方式2：运行期获取
			Type t2 = user1.GetType();
			Console.WriteLine(t2);

			// 获取Type的方式3：通过字符串来获取, 完全动态
			Type? t3 = Type.GetType("ConsoleApp2._02_反射.User");
			Console.WriteLine(t3);
		}

		public static void PrintInfo2() 
		{
			// 🔷查看类的基本信息
			Type userType = typeof(User);

			// 类名称
			Console.WriteLine(userType.Name);  // User
			// 全名称
			Console.WriteLine(userType.FullName);  // ConsoleApp2._02_反射.User
			// 命名空间
			Console.WriteLine(userType.Namespace);  // ConsoleApp2._02_反射
			// 是否是类
			Console.WriteLine(userType.IsClass);  // True

			// 🔷获取属性
			PropertyInfo[] properties = userType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (PropertyInfo property in properties)
			{
				Console.WriteLine(property.Name);
				// Id
				// Name
				// Age
				// Address
			}

			// 🔷获取方法名称
			MethodInfo[] methods = userType.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				Console.WriteLine(methodInfo.Name);
				// get_Id
				// set_Id
				// get_Name
				// set_Name
				// get_Age
				// set_Age
				// get_Address
				// set_Address
				// PrintUerInfo
				// GetType
				// ToString
				// Equals
				// GetHashCode
			}

			// 调用普通方法
			Type typeUser = user1.GetType();

			// 因为 MethodInfo 可能为null ,所以使用 ? 来修饰
			MethodInfo? method1 = typeUser.GetMethod("PrintUerInfo1");
			// 避免为null的	method使用Invoke方法报错, 所以使用 method?.Invoke
			method1?.Invoke(user1, ["admin", "123456"]);  // Tom admin, 123456

			// 因为 PrintUerInfo2 是私有方法, 因此在反射的时候必须指定 BindingFlags.NonPublic
			// 否则会按照默认的共有方法规则去寻找, 然后会找不到方法
			MethodInfo? method2 = typeUser.GetMethod("PrintUerInfo2", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
			method2?.Invoke(user1, ["admin", "123456"]);  // 你好admin, 123456
		}

		public static void PrintInfo3() 
		{
			// 创建Type对象
			Type userType = typeof(User);
			// 通过反射创建对象
			object? obj = Activator.CreateInstance(userType);
			if (obj != null) 
			{
				// 通过反射动态的设置类的属性值
				PropertyInfo? propId = userType.GetProperty("Id");
				propId?.SetValue(obj, 110120);

				PropertyInfo? propName = userType.GetProperty("Name");
				propName?.SetValue(obj, "贾飞天");

				PropertyInfo? propAge = userType.GetProperty("Age");
				propAge?.SetValue(obj, 18);

				// 转换为User对象
				User user = (User)obj;
				Console.WriteLine(user);
			}

			// 获取类上的特性
			AuthorizeAttribute? authorizeAttribute = userType.GetCustomAttribute<AuthorizeAttribute>();
			if (authorizeAttribute != null)
			{
				// 获取类特性上设定的值
				Console.WriteLine(authorizeAttribute.Role);  // Admin
			}
		}
	}
}
