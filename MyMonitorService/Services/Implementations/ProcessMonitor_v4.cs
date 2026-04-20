using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMonitorService.Config;
using MyMonitorService.Services.Interfaces;

// 命名空间
namespace MyMonitorService.Services.Implementations;

// 类实现接口
public class ProcessMonitor_v4(
	ILogger<ProcessMonitor_v4> logger,
	IOptionsMonitor<MonitorOptions> optionsMonitor) : IProcessMonitor_v4
{
	// 日志对象
	private readonly ILogger<ProcessMonitor_v4> _logger = logger;
	// 配置文件
	private readonly MonitorOptions _options = optionsMonitor.CurrentValue;

	public async Task Run(CancellationToken token)
	{
		foreach (var processName in _options.ProcessNames)
		{
			// 若【Ctrl + C】取消任务, 则抛出异常
			token.ThrowIfCancellationRequested();

			// 根据线程名获取线程对象
			var processes = Process.GetProcessesByName(processName);

			foreach (var proc in processes)
			{
				// 若【Ctrl + C】取消任务, 则抛出异常
				token.ThrowIfCancellationRequested();

				// 如果当前线程的运行时间 < 指定的最大时间的话
				var runTime = DateTime.Now - proc.StartTime;
				// 只允许线程存活1分钟
				if (runTime.TotalMinutes < _options.ProcessMaxMinutes)
				{
					// 跳过, 不杀死线程
					continue;
				}

				try
				{
					_logger.LogInformation("Killing {Process} PID={Pid}", processName, proc.Id);
					proc.Kill();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Kill error: {Process}", processName);
				}
			}
		}

		// 标记任务完成
		await Task.CompletedTask;
	}
}
