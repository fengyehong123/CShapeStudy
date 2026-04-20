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

public class Worker_v4(
	ILogger<Worker_v4> logger,
	IProcessMonitor_v4 processMonitor,
	IHistoryCollector_v4 historyCollector,
	IOptionsMonitor<MonitorOptions> optionsMonitor,
	TaskTool taskTool) : BackgroundService
{
	// 接口对象
	private readonly IProcessMonitor_v4 _processMonitor = processMonitor;
	private readonly IHistoryCollector_v4 _historyCollector = historyCollector;
	// 日志对象
	private readonly ILogger<Worker_v4> _logger = logger;
	// 配置文件对象
	private readonly IOptionsMonitor<MonitorOptions> _optionsMonitor = optionsMonitor;

	// 工具类
	// 使用【services.AddSingleton<TaskTool>();】注册过, 此处会自动注入 
	private readonly TaskTool _taskTool = taskTool;

	// 定义任务状态的Flag
	private readonly TaskFlag _processFlag = new();
	private readonly TaskFlag _historyFlag = new();

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker started");

		// 进程监控任务
		var processTask = _taskTool.RunTaskMonitor(
			"ProcessMonitor",
			_processFlag,
			_processMonitor.Run,
			// 此处传递的只是方法的引用, 并不是执行方法
			() => _optionsMonitor.CurrentValue.ProcessCheckIntervalSeconds,
			stoppingToken
		);

		// 历史记录查询任务
		var historyTask = _taskTool.RunTaskMonitor(
			"HistoryCollector",
			_historyFlag,
			_historyCollector.Run,
			() => _optionsMonitor.CurrentValue.BrowerHistoryCheckIntervalSeconds,
			stoppingToken
		);

		// 等待全部结束（一般是停止时）
		await Task.WhenAll(processTask, historyTask);
		_logger.LogInformation("Worker stopped");
	}
}