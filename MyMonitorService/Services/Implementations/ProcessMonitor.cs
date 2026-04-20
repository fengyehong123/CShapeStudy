using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMonitorService.Config;
using MyMonitorService.Services.Interfaces;

// 命名空间
namespace MyMonitorService.Services.Implementations;

// 类实现接口
public class ProcessMonitor(
	ILogger<ProcessMonitor> logger,
	IOptionsMonitor<MonitorOptions> optionsMonitor) : IProcessMonitor
{
	// 日志对象
	private readonly ILogger<ProcessMonitor> _logger = logger;
	// 配置文件
	private readonly MonitorOptions _options = optionsMonitor.CurrentValue;

	public void Run()
	{
		foreach (var processName in _options.ProcessNames)
		{
			// 根据线程名获取线程对象
			var processes = Process.GetProcessesByName(processName);

			foreach (var proc in processes)
			{
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
	}
}
