using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyMonitorService.Config;

namespace MyMonitorService.Tool;

public class TaskTool_v4(ILogger<TaskTool_v4> logger)
{
	private readonly ILogger<TaskTool_v4> _logger = logger;

	public async Task RunTaskMonitor(
		string taskName,
		TaskFlag taskFlag,
		Func<CancellationToken, Task> action,
		Func<int> getIntervalSeconds,
		CancellationToken token)
	{
		try
		{
			while (!token.IsCancellationRequested)
			{
				// 每次执行任务的时候, 都主动获取一次Task的时间(因为appsettings.json文件可能热更新)
				var interval = getIntervalSeconds();

				// 加锁执行任务
				await RunWithLockAsync(
					taskFlag,
					action,
					taskName,
					token
				);

				// 延时
				await Task.Delay(TimeSpan.FromSeconds(interval), token);
			}
		}
		// 捕获任务取消异常
		catch (OperationCanceledException)
		{
			_logger.LogInformation("{taskName} stopped", taskName);
		}
	}

	// 加锁运行任务
	private async Task RunWithLockAsync(
		TaskFlag flag,
		Func<CancellationToken, Task> action,
		string name,
		CancellationToken token)
	{
		if (Interlocked.Exchange(ref flag.Value, 1) != 0)
		{
			_logger.LogInformation("[{Time}] {Name} 正在运行，跳过", DateTime.Now, name);
			return;
		}

		try
		{
			_logger.LogInformation("[{Time}] {Name} 开始", DateTime.Now, name);
			// 执行任务
			await action(token);
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
	}
}
