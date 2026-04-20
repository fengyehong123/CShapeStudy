using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMonitorService.Config;
using MyMonitorService.Services.Interfaces;

// 更加简洁的定义namespace的方法
namespace MyMonitorService;

public class Worker_v2(
	ILogger<Worker_v2> logger,
	IProcessMonitor processMonitor,
	IHistoryCollector historyCollector,
	IOptionsMonitor<MonitorOptions> optionsMonitor) : BackgroundService
{
	// 接口对象
	private readonly IProcessMonitor _processMonitor = processMonitor;
	private readonly IHistoryCollector _historyCollector = historyCollector;
	// 日志对象
	private readonly ILogger<Worker_v2> _logger = logger;
	// 配置文件对象
	private readonly IOptionsMonitor<MonitorOptions> _optionsMonitor = optionsMonitor;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker started");

		while (!stoppingToken.IsCancellationRequested)
		{
			// 获取最新的配置
			var options = _optionsMonitor.CurrentValue;
			try
			{
				// 监控线程
				_processMonitor.Run();
				// 收集浏览器的历史记录
				_historyCollector.Run();
			}
			catch (TaskCanceledException)
			{
				return;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[Worker] Error: {ex.Message}");
			}

			// Task.Delay 的单位是毫秒 → 1 秒 = 1000 毫秒
			// 延时10秒, 每10秒检测一次
			await Task.Delay(options.ProcessCheckIntervalSeconds * 1000, stoppingToken);
		}

		_logger.LogInformation("Worker stopped");
	}
}