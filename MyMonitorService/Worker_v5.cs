using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMonitorService.Config;
using MyMonitorService.Services.Interfaces;
using MyMonitorService.Tool;
using System.Threading;
using System.Threading.Tasks;

// 更加简洁的定义namespace的方法
namespace MyMonitorService;

// Worker_v4版本就能满足需求
// Worker_v5版本只是为了体验SemaphoreSlim并发控制的用法而已
public class Worker_v5(
	ILogger<Worker_v5> logger,
	IProcessMonitor_v4 processMonitor,
	IHistoryCollector_v4 historyCollector,
	IOptionsMonitor<MonitorOptions> optionsMonitor,
	TaskTool_v5 taskTool) : BackgroundService
{
	// 接口对象
	private readonly IProcessMonitor_v4 _processMonitor = processMonitor;
	private readonly IHistoryCollector_v4 _historyCollector = historyCollector;
	// 日志对象
	private readonly ILogger<Worker_v5> _logger = logger;
	// 配置文件对象
	private readonly IOptionsMonitor<MonitorOptions> _optionsMonitor = optionsMonitor;

	// 工具类
	// 使用【services.AddSingleton<TaskTool_v5>();】注册过, 此处会自动注入 
	private readonly TaskTool_v5 _taskTool = taskTool;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker started");

		// 进程监控任务
		var processTask = _taskTool.RunTaskMonitor(
			taskName: "ProcessMonitor",
			action: _processMonitor.Run,
			// 此处传递的只是方法的引用, 并不是执行方法
			getIntervalSeconds: () => _optionsMonitor.CurrentValue.ProcessCheckIntervalSeconds,
			maxConcurrency: 1,
			stoppingToken
		);

		// 历史记录查询任务
		var historyTask = _taskTool.RunTaskMonitor(
			taskName: "HistoryCollector",
			action: _historyCollector.Run,
			getIntervalSeconds: () => _optionsMonitor.CurrentValue.BrowerHistoryCheckIntervalSeconds,
			maxConcurrency: 1,
			stoppingToken
		);

		// 等待全部结束（一般是停止时）
		await Task.WhenAll(processTask, historyTask);
		_logger.LogInformation("Worker stopped");
	}
}