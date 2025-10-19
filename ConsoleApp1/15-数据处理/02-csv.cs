using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;

namespace ConsoleApp1._15_数据处理
{
	// 定义一个类对象
	public class Student
	{
		// 声明 string 类型的 Name 值不可为 Null
		public required string Name { get; set; }
		// 声明 string 类型的 Address 值可以为 Null
		public string? Address { get; set; }
		public int Age { get; set; }
	}

	public class _02_csv
	{
		// 获取桌面文文件夹路径
		private readonly static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		// csv文件的路径
		private readonly static string csvFullPath = Path.Combine(desktopPath, "student.csv");

		// csv文件的配置对象
		public static class CsvOptions
		{
			public static readonly CsvConfiguration csvOpt = new(CultureInfo.InvariantCulture)
			{
				// 强制所有字段都加引号
				ShouldQuote = args => true
			};
		}

		public static void PrintInfo1() 
		{
			// 创建一个对象List
			List<Student> studentList =
			[
				new() { Name = "Tom", Address = "地球", Age = 25 },
				new() { Name = "Jerry", Address = "月球", Age = 18 },
				new() { Name = "Tester", Address = "火星", Age = 20 }
			];

			// 如果csv文件存在的话, 就删除
			if (File.Exists(csvFullPath))
			{
				File.Delete(csvFullPath);
			}

			// ============================ 作用域 ============================
			/*
				CsvWriter.WriteRecords() → 会把数据写入 writerStream 缓冲区，
				但即使 WriteRecords() 执行完，数据可能还没有真正刷新到文件。
				只有在：
					csvWriter.Flush()
					或 writerStream.Flush()
					或 离开 using 块后自动 Dispose
				时，文件数据才会真正落盘。
				
				也就是说同一个作用域里 写完文件后立刻去读文件，
				这时 writerStream 仍然处于打开状态，文件被占用（locked），
				Windows 不允许你再打开它进行读取，因此会报错。

				为了解决同一个作用域内写入文件后又读取文件时, 文件被占用的问题,
				可以使用 {} 来指定写入 和 读取 分别在不同的作用域中执行
			 */
			{
				// 创建一个流写入对象
				using var writerStream = new StreamWriter(csvFullPath);
				// 创建一个csv写入对象
				using var csvWriter = new CsvWriter(writerStream, CsvOptions.csvOpt);
				csvWriter.WriteRecords(studentList);
			}

			// ============================ 作用域 ============================
			{
				// 创建一个流读入对象
				using var readerStream = new StreamReader(csvFullPath);
				// 创建csv读取对象
				using var csvReader = new CsvReader(readerStream, CultureInfo.InvariantCulture);
				var peopleList = csvReader.GetRecords<Person>();

				foreach (var people in peopleList)
				{
					Console.WriteLine($"{people.Name}, {people.Address}, {people.Age}");
				}
			}
		}
	}
}
