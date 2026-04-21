using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyMonitorService.Config;

namespace MyMonitorService.Tool;

public class TaskTool_v5(ILogger<TaskTool_v5> logger)
{
	private readonly ILogger<TaskTool_v5> _logger = logger;

	public async Task RunTaskMonitor(
		string taskName,
		Func<CancellationToken, Task> action,
		Func<int> getIntervalSeconds,
		int maxConcurrency,
		CancellationToken token)
	{
		var semaphore = new SemaphoreSlim(maxConcurrency);

		try
		{
			while (!token.IsCancellationRequested)
			{
				// 并发控制执行
				_ = RunWithLimitAsync(semaphore, action, taskName, token);

				// 每次执行任务的时候, 都主动获取一次Task的时间(因为appsettings.json文件可能热更新)
				var interval = getIntervalSeconds();
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
	private async Task RunWithLimitAsync(
		SemaphoreSlim semaphore,
		Func<CancellationToken, Task> action,
		string taskName,
		CancellationToken token)
	{
		// 如果达到了最大并发数
		if (!await semaphore.WaitAsync(0, token))
		{
			_logger.LogWarning("{TaskName} skipped (max concurrency reached)", taskName);
			return;
		}

		try
		{
			_logger.LogInformation("[{Time}] {Name} 开始", DateTime.Now, taskName);
			// 执行任务
			await action(token);
			_logger.LogInformation("[{Time}] {Name} 完成", DateTime.Now, taskName);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Kill error: {Process}", ex.Message);
		}
		finally
		{
			semaphore.Release();
		}
	}
}
