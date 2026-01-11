using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApp2._01_特性
{
	/*
	 * 🔷在 C# 中，自定义特性（Attribute）主要用于给代码添加“元数据”，
	 * 再通过反射在运行时读取这些信息，从而影响程序行为（校验、映射、标记、配置等）。
	 * 
	 * 🔷特性的本质就是一个继承自 System.Attribute 的类
	 * 
	 * 🔷什么时候该用【特性】呢？
	 *    ✅ 适合：
	 * 	     标记（Tag）
	 *       元数据描述
	 *       权限 / 校验 / 映射
	 *       框架设计（ASP.NET / ORM / AOP）
	 *       
	 * 	  ❌ 不适合：
	 *      复杂业务逻辑
	 *      高频运行路径（反射慢）
	 */

	// 🔷定义一个最基本的特性 
	[AttributeUsage(AttributeTargets.All)]
	public class My1Attribute : Attribute
	{
	}

	// 🔷定义一个常见的完整写法的特性
	[
		// 特性作用的位置
		AttributeUsage(
			// 作用于类和方法上
			AttributeTargets.Class | AttributeTargets.Method,
			// 是否允许重复使用
			AllowMultiple = false,
			// 是否能被子类继承
			Inherited = true
		)
	]
	public class My2Attribute(string name) : Attribute
	{
		public string Name { get; } = name;
		public int Version { get; set; } = 1;
	}

	// 🔷定义一个特性, 允许多次使用
	[
		AttributeUsage(
			// 值允许作用在方法上
			AttributeTargets.Method,
			// 允许多次使用
			AllowMultiple = true
		)
	]
	public class AuthorizeAttribute(string role) : Attribute
	{
		public string Role { get; } = role;
	}

	// 自定义特性作用于类上
	[My2("class名", Version = 2)]
	public class 使用自定义特性的类
	{
		// 自定义特性作用于方法上
		[My2("method名")]
		public static void Print1()
		{
			Console.WriteLine("使用了自定义特性的方法");
		}

		// 同一个特性, 使用了多次
		[Authorize("Admin")]
		[Authorize("Manager")]
		public static void DeleteUser()
		{
			Console.WriteLine("User删除成功!");
		}
	}

	public class _02_自定义特性
	{
		public static void PrintInfo1()
		{
			// C#中使用反射直接使用类就行, 不必像java那样必须使用类对象
			Type type = typeof(使用自定义特性的类);

			// 读取类上的特性
			//   因为 type.GetCustomAttribute 得到的值有可能为 null 
			//   所以 类型使用 My2Attribute? 表示值可能为null
			//   如果类型使用 My2Attribute 的话, 就表示绝不可能为null, 这与 type.GetCustomAttribute 的结果相悖
			My2Attribute? attrClass = type.GetCustomAttribute<My2Attribute>();
			if (attrClass != null)
			{
				// 获取特性上的 Name 和 Version 属性
				Console.WriteLine(attrClass.Name);
				Console.WriteLine(attrClass.Version);
			}

			// 获取方法的反射对象
			MethodInfo? methodPrint1 = type.GetMethod("Print1");
			if (methodPrint1 != null)
			{
				// 读取方法上的特性
				My2Attribute? attr = methodPrint1.GetCustomAttribute<My2Attribute>();
				Console.WriteLine(attr?.Name);
			}
		}

		public static void PrintInfo2(string currentUserRole = "Guest") 
		{
			Console.WriteLine("________________________________________________");
			Type type = typeof(使用自定义特性的类);
			// 获取方法的反射对象
			MethodInfo? deleteUserMethod = type.GetMethod("DeleteUser");
			if (deleteUserMethod != null) 
			{
				// 读取多个特性
				var attrs = deleteUserMethod.GetCustomAttributes<AuthorizeAttribute>();
				bool allowed = attrs.Any(a => a.Role == currentUserRole);
				if (allowed) 
				{
					Console.WriteLine("有删除用户的权限");
				}
				else
				{
					Console.WriteLine("无法删除用户");
				}
			}
			
		}
	}
}