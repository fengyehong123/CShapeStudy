using System.Threading;
using MyMonitorService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
		services.Configure<ProcessMonitorOptions>(
			context.Configuration.GetSection("ProcessMonitor")
		);

		// 添加服务所需的Worker
		services.AddHostedService<Worker>();
	});

	IHost host = builder.Build();
	host.Run();
}
finally
{
    mutex?.ReleaseMutex();
    mutex?.Dispose();
}
