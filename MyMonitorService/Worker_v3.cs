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

public class Worker_v3(
	ILogger<Worker_v3> logger,
	IProcessMonitor processMonitor,
	IHistoryCollector historyCollector,
	IOptionsMonitor<MonitorOptions> optionsMonitor) : BackgroundService
{
	// 接口对象
	private readonly IProcessMonitor _processMonitor = processMonitor;
	private readonly IHistoryCollector _historyCollector = historyCollector;
	// 日志对象
	private readonly ILogger<Worker_v3> _logger = logger;
	// 配置文件对象
	private readonly IOptionsMonitor<MonitorOptions> _optionsMonitor = optionsMonitor;
	
	// 定义任务状态的Flag
	private readonly TaskFlag _processFlag = new();
	private readonly TaskFlag _historyFlag = new();

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker started");

		var options = _optionsMonitor.CurrentValue;

		// 进程监控任务
		var processTask = RunTaskMonitor("ProcessMonitor", _processFlag, _processMonitor.Run, options.ProcessCheckIntervalSeconds, stoppingToken);
		// 历史记录查询任务
		var historyTask = RunTaskMonitor("HistoryCollector", _historyFlag, _historyCollector.Run, options.BrowerHistoryCheckIntervalSeconds,  stoppingToken);

		// 等待全部结束（一般是停止时）
		await Task.WhenAll(processTask, historyTask);
		_logger.LogInformation("Worker stopped");
	}

	private async Task RunTaskMonitor(
		string _taskName,
		TaskFlag _taskFlag,
		Action _action,
		int _checkIntervalSeconds,
		CancellationToken token)
	{
		using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_checkIntervalSeconds));

		try
		{
			while (await timer.WaitForNextTickAsync(token))
			{
				RunWithLock(_taskFlag, _action, _taskName, token);
			}
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("{taskName} stopped", _taskName);
		}
	}

	// 加锁运行任务
	private void RunWithLock(TaskFlag flag, Action action, string name, CancellationToken token)
	{
		if (Interlocked.Exchange(ref flag.Value, 1) != 0)
		{
			_logger.LogInformation("[{Time}] {Name} 正在运行，跳过", DateTime.Now, name);
			return;
		}

		_ = Task.Run(() =>
		{
			try
			{
				_logger.LogInformation("[{Time}] {Name} 开始", DateTime.Now, name);
				// 执行任务
				action();
				_logger.LogInformation("[{Time}] {Name} 完成", DateTime.Now, name);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Kill error: {Process}", ex.Message);
			}
			finally
			{
				Interlocked.Exchange(ref flag.Value, 0);
			}
		}, token);
	}
}