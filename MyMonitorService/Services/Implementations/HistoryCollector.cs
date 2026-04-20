using System;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using MyMonitorService.Services.Interfaces;

namespace MyMonitorService.Services.Implementations;

public class HistoryCollector(
	ILogger<HistoryCollector> logger) : IHistoryCollector
{
	// 日志对象
	private readonly ILogger<HistoryCollector> _logger = logger;

	public void Run()
	{
		// 拼接浏览器历史记录文件的绝对路径
		string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		string edgeBrowerHistoryPath = Path.Combine(
			localAppData,
			"Microsoft/Edge/User Data/Default/History"
		);

		// 将浏览器的历史记录文件复制到临时文件夹路径中
		string browerHistoryTempFilePath = Path.Combine(Path.GetTempPath(), "History.db");
		File.Copy(edgeBrowerHistoryPath, browerHistoryTempFilePath, true);

		// 拼接导出文件的绝对路径
		string browerHistoryFilePath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			"brower_history.csv"
		);

		_logger.LogInformation("开始收集浏览器历史记录...");

		// 创建sqlLite数据库的连接对象
		using var conn = new SqliteConnection($"Data Source={browerHistoryTempFilePath};Mode=ReadOnly");
		conn.Open();

		// 查询历史记录的sql
		string historyQuerySql = @"
            SELECT
				url,
				title,
				visit_count,
				last_visit_time 
            FROM
				urls 
			WHERE 
				visit_count > 0
			AND
				url NOT LIKE 'chrome://%'
            ORDER BY 
				last_visit_time DESC";

		// 执行查询
		using var cmd = new SqliteCommand(historyQuerySql, conn);
		using var reader = cmd.ExecuteReader();

		// 创建文件流对象
		using var fs = new FileStream(
			browerHistoryFilePath,
			// 强制覆盖
			FileMode.Create,
			FileAccess.Write
		);

		// 创建csv写入对象
		using var csvWriter = new StreamWriter(fs, new UTF8Encoding(true));

		// 表头
		csvWriter.WriteLine("访问时间,网站标题,URL,浏览次数");

		while (reader.Read())
		{
			// 读取查询到的数据
			string url = reader.IsDBNull(0) ? "" : reader.GetString(0);
			string title = reader.IsDBNull(1) ? "" : reader.GetString(1);
			int count = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
			long time = reader.IsDBNull(3) ? 0 : reader.GetInt64(3);
			DateTime visitTime = BrowerTimeToDateTime(time);

			// 写入一行数据到csv文件中
			string csvRowLine = string.Join(",",
				EscapeCsv(visitTime.ToString("yyyy-MM-dd HH:mm:ss")),
				EscapeCsv(title),
				EscapeCsv(url),
				count
			);
			csvWriter.WriteLine(csvRowLine);
		}

		_logger.LogInformation("浏览器历史记录收集完毕...");
	}

	// 时间转换
	static DateTime BrowerTimeToDateTime(long chromeTime)
	{
		return DateTime.FromFileTimeUtc(chromeTime * 10).ToLocalTime();
	}

	// CSV转义（防止逗号、换行、引号问题）
	static string EscapeCsv(string input)
	{
		if (string.IsNullOrEmpty(input)) return "";

		if (input.Contains(',') || input.Contains('"') || input.Contains('\n'))
		{
			input = input.Replace("\"", "\"\"");
			return $"\"{input}\"";
		}

		return input;
	}
}
