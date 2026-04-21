using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMonitorService;
using MyMonitorService.Config;
using MyMonitorService.Services.Implementations;
using MyMonitorService.Services.Interfaces;
using MyMonitorService.Tool;
using System.Threading;

Mutex? mutex = null;

try
{
    mutex = new Mutex(true, "MyMonitorService", out bool createdNew);
    if (!createdNew)
    {
        return;
    }

	// 获取当前Host的Builder对象
	IHostBuilder builder = Host.CreateDefaultBuilder(args);

	// 为Builder对象添加服务
	builder.ConfigureServices((context, services) =>
	{
		// 添加服务所需的配置文件类
		services.Configure<MonitorOptions>(
			context.Configuration.GetSection("MonitorConfig")
		);

		// ==================================================================
		// 注册服务
		// services.AddSingleton<IProcessMonitor, ProcessMonitor>();
		// services.AddSingleton<IHistoryCollector, HistoryCollector>();

		// 添加服务所需的Worker
		// services.AddHostedService<Worker_v1>();
		// services.AddHostedService<Worker_v2>();
		// services.AddHostedService<Worker_v3>();
		// services.AddSingleton<TaskTool_v4>();
		// ==================================================================

		// 注册任务工具类, 让DI自动注入类
		services.AddSingleton<TaskTool_v5>();

		// 注册服务
		services.AddSingleton<IProcessMonitor_v4, ProcessMonitor_v4>();
		services.AddSingleton<IHistoryCollector_v4, HistoryCollector_v4>();

		// 添加服务所需的Worker
		services.AddHostedService<Worker_v5>();
	});

	IHost host = builder.Build();
	host.Run();
}
finally
{
    mutex?.ReleaseMutex();
    mutex?.Dispose();
}
