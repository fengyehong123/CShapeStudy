using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ConsoleApp1._09_类
{
	/*
	 * 🔴创建一个最基本的枚举类
	 * 默认：
	 *   Monday = 0
	 *   Tuesday = 1
	*/
	public enum _05_枚举类
	{
		Monday,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday
	}

	/*
	 * 枚举（enum）的底层类型只能是以下几种整数类型
	 *   byte, sbyte, short, ushort, int, uint, long, ulong
	 * 而 不能是 string、char、float、double 等类型
	*/
	public enum HttpStatus : short
	{
		OK = 200,
		NotFound = 404,
		InternalServerError = 500
	}

	// 也可以通过创建一个映射字典的方式来让每个枚举都有字符串说明
	public static class HttpMsgEumHelper
	{
		public static readonly Dictionary<HttpStatus, string> Descriptions = new()
		{
			[HttpStatus.OK] = "成功了!!!哈哈!!!",
			[HttpStatus.NotFound] = "未找到!!!555!!!"
		};
	}

	// 创建一个带有描述信息的枚举类
	public enum HttpMsg
	{
		// 给每个枚举字段添加说明
		// 然后可以使用反射来获取字段的描述信息
		[Description("成功")]
		OK = 200,
		[Description("未找到")]
		NotFound = 404
	}

	/*
		🔴在 C# 中，[Flags] 特性（attribute）用于让一个枚举（enum）支持位运算的“组合”功能。
		这类枚举也被称为 “组合枚举类”或“位标志枚举（bit flag enum）”。
	 */
	[Flags]
	public enum UserPermission
	{
		None = 0,   // 无权限
		Read = 1,   // 读取
		Write = 2,  // 写入
		Delete = 4, // 删除
		Admin = 8   // 管理员（拥有更高权限）
	}

	public class User08
	{
		// 用户的名称
		public string Name { get; set; }
		// 用户的权限
		public UserPermission Permissions { get; set; }

		// 有参构造函数
		public User08(string name, UserPermission permissions)
		{
			Name = name;
			Permissions = permissions;
		}

		// 检查是否拥有某个权限
		public bool HasPermission(UserPermission p) => Permissions.HasFlag(p);

		// 添加权限
		public void AddPermission(UserPermission p) => Permissions |= p;

		// 移除权限
		public void RemovePermission(UserPermission p) => Permissions &= ~p;

		// 打印当前权限
		public void ShowPermissions()
		{
			Console.WriteLine($"{Name} 的当前权限: {Permissions}");
		}
	}

	// 枚举类的遍历
	public static class _05_枚举类_Utils
	{
		public static void PrintInfo1()
		{
			// 创建一个枚举类对象
			_05_枚举类 today = _05_枚举类.Monday;
			Console.WriteLine(today);  // Monday
			Console.WriteLine((int)today);  // 0

			// 获取枚举类对应的说明
			Console.WriteLine(HttpMsgEumHelper.Descriptions[HttpStatus.NotFound]);  // 未找到!!!555!!!

			// 创建一个枚举对象
			HttpMsg code = HttpMsg.OK;
			// 通过反射来获取每个枚举字段上的描述信息
			string desc = code.GetType().GetField(code.ToString())?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? code.ToString();
			Console.WriteLine(desc);  // 成功

			Console.WriteLine("________________________________________________");
			// 创建用户并赋予初始权限
			User08 user08 = new("Tom", UserPermission.Read | UserPermission.Write);
			user08.ShowPermissions(); // Tom 的当前权限: Read, Write

			// 检查权限
			Console.WriteLine($"Tom 是否可以删除？ {user08.HasPermission(UserPermission.Delete)}");  // Tom 是否可以删除？ False

			// 添加权限
			user08.AddPermission(UserPermission.Delete);
			user08.ShowPermissions(); // Tom 的当前权限: Read, Write, Delete

			// 移除权限
			user08.RemovePermission(UserPermission.Write);
			user08.ShowPermissions(); // Tom 的当前权限: Read, Delete

			// 赋予管理员权限
			user08.AddPermission(UserPermission.Admin);
			user08.ShowPermissions(); // Tom 的当前权限: Read, Delete, Admin
		}

		// 遍历枚举值
		public static void ForeachEnum()
		{
			// 遍历所有枚举值
			foreach (HttpMsg enumObj in Enum.GetValues(typeof(HttpMsg)))
			{
				Console.WriteLine($"{enumObj} = {(int)enumObj}");
				// OK = 200
				// NotFound = 404
			}

			// 只遍历枚举类的名称
			foreach (string enumName in Enum.GetNames(typeof(HttpMsg)))
			{
				Console.WriteLine(enumName);
				// OK
				// NotFound
			}
		}

	}
}
