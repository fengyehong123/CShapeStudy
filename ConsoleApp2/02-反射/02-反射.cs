using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp2._02_反射
{

	public class Student
	{
		public int Id { get; set; }
		// 声明 Name 并不是必须项目, 可以为 null
		public string? Name { get; set; }
		public int Age { get; set; }
		// 声明 Address 并不是必须项目, 可以为 null
		public string? Address { get; set; }
	}

	public class _02_反射封装 
	{
		public static void PrintInfo()
		{
			// 定义一个字典
			Dictionary<string, object> paramDict = new()
			{
				["Id"] = 10086,
				["Name"] = "李四",
				["Age"] = 20
			};

			Type studentType = typeof(Student);
			Student student1 = (Student)MapToObject1(studentType, paramDict);

			// 打印类对象中的信息
			Console.WriteLine(student1.Id);
			Console.WriteLine(student1.Name);
			Console.WriteLine(student1.Age);
			Console.WriteLine("________________________________________________");

			// 使用更加进阶版的封装方法
			Student student2 = MapToObject2<Student>(paramDict);

			// 打印类对象中的信息
			Console.WriteLine(student2.Id);
			Console.WriteLine(student2.Name);
			Console.WriteLine(student2.Age);
		}

		// 封装一个通过反射进行赋值的方法
		public static object MapToObject1(Type type, Dictionary<string, object> data)
		{
			// ! 的作用是强行指定创建后的obj对象不可能为null, 防止编译器的检测
			object obj = Activator.CreateInstance(type)!;

			foreach (var kv in data)
			{
				PropertyInfo prop = type.GetProperty(kv.Key)!;
				if (prop == null) continue;
				if (!prop.CanWrite) continue;

				// 类型转换（非常关键）
				object value = Convert.ChangeType(kv.Value, prop.PropertyType);
				prop.SetValue(obj, value);
			}

			return obj;
		}

		// 更加进阶版的封装
		public static T MapToObject2<T>(Dictionary<string, object> data) where T : new()
		{
			var obj = new T();
			var type = typeof(T);

			foreach (var kv in data)
			{
				var prop = type.GetProperty(kv.Key);
				if (prop == null || !prop.CanWrite)
				{
					continue;
				}

				object value = kv.Value;
				if (value == null)
				{
					continue;
				}

				Type targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

				if (targetType.IsEnum)
				{
					// 若 value 为 null 的话, 早就 continue 了, 因此此处不可能为null, 因此使用 ! 强行指定
					value = Enum.Parse(targetType, value.ToString()!);
				}
				else
				{
					value = Convert.ChangeType(value, targetType);
				}

				prop.SetValue(obj, value);
			}

			return obj;
		}
	}
}
