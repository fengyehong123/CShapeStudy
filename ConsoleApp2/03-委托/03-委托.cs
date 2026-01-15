using System;

/*
⚠️ 学习时你一定要记住的 3 条铁律
	1. 多播委托不适合有返回值的业务
	2. 不要依赖执行顺序
	3. 异常会中断后续调用（除非你手动处理）
*/
namespace ConsoleApp2._03_委托
{
	// Demo 1：事件模型（最典型）
	public class _03_委托使用_Demo1
	{
		class Button
		{
			// 这里的event指的是事件
			public event Action? Click;

			public void OnClick()
			{
				Console.WriteLine("按钮被点击");
				Click?.Invoke();
			}
		}

		public static void Demo_Event()
		{
			Button button = new();

			button.Click += () => Console.WriteLine("播放音效");
			button.Click += () => Console.WriteLine("记录日志");
			button.Click += () => Console.WriteLine("刷新 UI");

			button.OnClick();
		}
	}

	// Demo 2：Hook / 插件扩展点
	public class _03_委托使用_Demo2 
	{
		class RequestHandler
		{
			// 请求执行前要执行的操作
			public static Action? BeforeRequest;
			// 请求执行后要执行的操作
			public static Action? AfterRequest;

			public static void Handle()
			{
				// 请求发起前执行的操作
				BeforeRequest?.Invoke();

				Console.WriteLine("正在处理请求...");

				// 请求执行后要执行的操作
				AfterRequest?.Invoke();
			}
		}

		public static void Demo_Hook()
		{
			// 请求之前需要进行的处理
			RequestHandler.BeforeRequest += () => Console.WriteLine("权限校验");
			RequestHandler.BeforeRequest += () => Console.WriteLine("参数校验");

			// 请求之后需要进行的处理
			RequestHandler.AfterRequest += () => Console.WriteLine("记录访问日志");
			RequestHandler.AfterRequest += () => Console.WriteLine("性能统计");

			// 发起请求
			RequestHandler.Handle();
		}
	}

	// Demo 3：一个动作 + 多个副作用
	public class _03_委托使用_Demo3_1 
	{
		private static void SendSms() => Console.WriteLine("发送短信通知");

		private static void SendEmail() => Console.WriteLine("发送邮件通知");

		private static void WriteLog() => Console.WriteLine("写入订单日志");

		public static void Demo_SideEffects()
		{
			// 定义订单完成之后需要进行的各种操作
			Action? orderCompleted = null;
			orderCompleted += SendSms;
			orderCompleted += SendEmail;
			orderCompleted += WriteLog;

			Console.WriteLine("订单完成");
			orderCompleted?.Invoke();
		}
	}

	public class _03_委托使用_Demo3_2 
	{
		// 系统启动时，需要：
		//   1. 初始化数据库
		//   2. 加载配置
		//   3. 启动日志系统
		// 启动逻辑本身不关心具体做了什么
		class SystemModules
		{
			public static void InitDatabase()
			{
				Console.WriteLine("初始化数据库连接...");
			}

			public static void LoadConfig()
			{
				Console.WriteLine("加载系统配置...");
			}

			public static void InitLogger()
			{
				Console.WriteLine("初始化日志系统...");
			}
		}

		public static void Demo_Start_DB() 
		{
			Action startup = SystemModules.InitDatabase;
			startup += SystemModules.LoadConfig;
			startup += SystemModules.InitLogger;

			startup();
		}
	}

	// Demo 4：状态广播（观察者模式）
	public class _03_委托使用_Demo4 
	{
		class Downloader
		{
			// 这里的event指的是事件
			public event Action<int>? ProgressChanged;

			public void Start()
			{
				for (int i = 0; i <= 100; i += 50)
				{
					ProgressChanged?.Invoke(i);
				}
			}
		}

		public static void Demo_Observer()
		{
			Downloader downloader = new();

			downloader.ProgressChanged += p => Console.WriteLine($"UI 显示进度：{p}%");
			downloader.ProgressChanged += p => Console.WriteLine($"日志记录进度：{p}%");

			downloader.Start();
		}
	}
}
