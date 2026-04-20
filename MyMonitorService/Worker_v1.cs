using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMonitorService.Config;

// 更加简洁的定义namespace的方法
namespace MyMonitorService;

/*
	C#12的 主构造函数（Primary Constructor），属于一个比较新的语法
	类定义时直接声明构造函数参数
	
	传统写法
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IOptionsMonitor<ProcessMonitorOptions> _options;

		public Worker(
			ILogger<Worker> logger,
			IOptionsMonitor<ProcessMonitorOptions> options)
		{
			_logger = logger;
			_options = options.CurrentValue;
		}
	}
	
	当使用 services.AddHostedService<Worker>(); 的时候
	Worker会被注册到DI容器, .NET在创建Worker时
		1. 会分析构造函数
			当发现需要ILogger<Worker> 和 IOptionsMonitor<ProcessMonitorOptions> 的时候
		2. 去容器里找services 里找
			CreateDefaultBuilder() 自动注册了 ILogger<Worker>
			Configure<>() 手动注册了 IOptionsMonitor<ProcessMonitorOptions>
		3. 然后会帮我们自动注入, 相当于替我们做了以下工作
			new Worker(logger实例, options实例)
	
	总结
		DI容器会根据构造函数自动解析依赖
*/
public class Worker_v1(ILogger<Worker_v1> logger, IOptionsMonitor<MonitorOptions> optionsMonitor) : BackgroundService
{
	// 日志对象
	private readonly ILogger<Worker_v1> _logger = logger;
	// 配置文件对象
	private readonly IOptionsMonitor<MonitorOptions> _optionsMonitor = optionsMonitor;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				// 每次循环都获取最新的配置
				var options = _optionsMonitor.CurrentValue;

				// 读取配置文件中的线程名
				foreach (var processName in options.ProcessNames)
				{
					// 获取线程名对象
					var processes = Process.GetProcessesByName(processName);

					foreach (var proc in processes)
					{
						// 如果当前线程的运行时间 < 指定的最大时间的话
						var runTime = DateTime.Now - proc.StartTime;
						if (runTime.TotalMinutes < options.ProcessMaxMinutes)
						{
							// 跳过, 不杀死线程
							continue;
						}

						// 杀死指定的线程
						try
						{
							_logger.LogWarning("Killing {Process} PID={Pid}", processName, proc.Id);
							proc.Kill();
						}
						catch (Exception ex)
						{
							_logger.LogError(ex, "Kill error: {Process}", processName);
						}
					}
				}

				// 延时
				await Task.Delay(options.ProcessCheckIntervalSeconds * 1000, stoppingToken);
			}
			catch (TaskCanceledException)
			{
				return;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Main loop error");
			}
		}
	}
}