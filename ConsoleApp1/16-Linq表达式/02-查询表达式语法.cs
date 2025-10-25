using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1._16_Linq表达式
{
	// 定义一个 Student 结构体
	struct Student
	{
		public string name;
		public int age;
		public int grade;
		public float score;
	}

	// 构造一个 Champions 结构体
	struct Champions
	{
		public string name;
		public string country;
	}

	public class _02_查询表达式语法
	{
		// 创建一个Person集合 
		private static readonly List<Person> peopleList =
		[
			// 方式一: 初始化集合元素
			new() { Name = "Tom", Age = 25 },
			new() { Name = "Jerry", Age = 18 },

			// 方式二: 初始化集合元素
			new Person { Name = "Alice", Age = 30 },
			new Person { Name = "Jack", Age = 48 }
		];

		// 定义一个Student集合
		private static readonly List<Student> studentlist = [];

		// 定义一个Champions集合
		private static readonly List<Champions> championlist = [];

		// 静态构造函数（只在类第一次被使用的时候, 向studentlist中添加数据）
		static _02_查询表达式语法()
		{
			// 创建一个随机对象
			Random pRandom = new();
			for (int i = 1; i < 50; i++)
			{
				// 获取随机值
				float sc = pRandom.Next(0, 100);
				int age = pRandom.Next(8, 13);
				int gde = pRandom.Next(1, 6);

				// 创建随机名称
				string name = pRandom.Next(0, 6) switch
				{
					1 => "周xxx",
					2 => "李xxx",
					3 => "孙xxx",
					4 => "钱xxx",
					_ => "赵xxx",
				};

				Student stu = new()
				{
					name = name,
					age = age,
					grade = gde,
					score = sc
				};
				studentlist.Add(stu);
			}
			// ---------------------------------------
			championlist.Add(new Champions() { name = "张**", country = "中国" });
			championlist.Add(new Champions() { name = "赵**", country = "中国" });
			championlist.Add(new Champions() { name = "李**", country = "中国" });
			championlist.Add(new Champions() { name = "李**", country = "中国" });
			championlist.Add(new Champions() { name = "Peter", country = "美国" });
			championlist.Add(new Champions() { name = "Hune", country = "美国" });
			championlist.Add(new Champions() { name = "Hune", country = "美国" });
			championlist.Add(new Champions() { name = "Jack", country = "俄罗斯" });
			championlist.Add(new Champions() { name = "Jack", country = "俄罗斯" });
			championlist.Add(new Champions() { name = "Jimi", country = "英国" });
		}

		public static void Test1() 
		{
			// 方法链语法（method syntax）
			var list1 = peopleList
				.Where(static p => p.Age > 20)
				.OrderByDescending(static p => p.Age)
				.Select(static p => new { p.Name, p.Age })
				.ToList();
			foreach (var item in list1)
			{
				Console.WriteLine($"{item.Name} -> {item.Age}");
			}
			Console.WriteLine("+ ------------------------------------ +");

			// 查询表达式语法（query syntax）
			var list2 = from p in peopleList
						where p.Age > 20
						orderby p.Age descending
						select new { p.Name, p.Age };
			foreach (var item in list2)
			{
				Console.WriteLine($"{item.Name} -> {item.Age}");
			}
		}

		public static void Test2() 
		{
			// 学生中选择出不及格的人员名单并按分数降序排列
			var filterList = from student in studentlist 
						where student.score < 60 
						orderby student.score 
						descending select student;

			foreach (Student st in filterList)
			{
				Console.WriteLine("***************");
				Console.WriteLine("姓名：" + st.name);
				Console.WriteLine("班级：" + st.grade);
				Console.WriteLine("年龄：" + st.age);
				Console.WriteLine("分数：" + st.score);
			}
		}

		// 筛选出特定类型的数据
		public static void Test3() 
		{
			// 创建一个集合, 里面有各种类型的数据
			object[] itemList = [1, "one", 2, "two", 3, "three"];

			// 🔴通过.OfType<类型>方法, 从中筛选出字符串类型的数据
			var resultList = itemList.OfType<string>();
			foreach (var item in resultList)
			{
				Console.WriteLine(item);
			}
		}

		// 🔴分组查询
		public static void Test4() 
		{
			// 查询表达式语法（query syntax）
			var result1List = from champion in championlist
						group champion by champion.country into country
						orderby country.Count() descending, country.Key
						where
							country.Count() >= 2
						select
							new { country = country.Key, count = country.Count() };
			foreach (var result in result1List)
			{
				Console.WriteLine($"国家：{result.country}，冠军数：{result.count}个");
			}
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

			// 方法链语法（method syntax）
			var result2List = championlist
								.GroupBy(champion => champion.country)
								.Where(country => country.Count() >= 2)
								.OrderByDescending(country => country.Count())
								.ThenBy(country => country.Key)
								.Select(country => new { country = country.Key, count = country.Count() });
			foreach (var result in result2List)
			{
				Console.WriteLine($"国家：{result.country}，冠军数：{result.count}个");
			}
		}

		// 并行查询
		public static void Test5() 
		{
			//构造大数组
			const int count = 100000000;
			var data = new int[count];
			var r = new Random();
			for (int i = 0; i < count; i++)
			{
				data[i] = r.Next(40);
			}

			// 普通的Linq表达式
			DateTime st1 = DateTime.Now;
			int sum = (from num in data where num > 20 select num).Sum();

			// 使用并行Linq表达式进行查询
			DateTime st2 = DateTime.Now;
			int sum2 = (from num in data.AsParallel() where num > 20 select num).Sum();

			DateTime st3 = DateTime.Now;
			Console.WriteLine($"常规linq耗时：{(st2 - st1).TotalSeconds}s");  // 常规linq耗时：1.1014558s
			Console.WriteLine($"并行linq耗时：{(st3 - st2).TotalSeconds}s");  // 并行linq耗时：0.4025952s
		}
	}
}
