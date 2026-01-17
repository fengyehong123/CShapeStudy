using System;

/*
	🔷Event = 对“委托”的一种“安全封装”，用于发布 / 订阅模式
		一个对象发生了某件事, 其他对象可以订阅并响应
		这就是典型的 观察者模式（Observer Pattern）
	
	🔷Event 的本质
		event 不是新类型，只是对委托的访问控制
		Event = “只能被订阅的委托，用来做对象之间的通知”
 */
namespace ConsoleApp2._04_Event
{
	public class _01_Event
	{
		// 定义一个按钮类
		private class Button
		{
			// 1. 定义一个事件
			public event EventHandler? Click;

			// 2. 定义触发事件的方法（通常是 protected virtual）
			protected virtual void OnClick(EventArgs e)
			{
				Click?.Invoke(this, e);
			}

			// 3. 对外暴露的【业务动作】
			public void Press()
			{
				Console.WriteLine("Button.Press() 被调用");
				// 因为没有事件参数, 所以此使用 EventArgs.Empty
				OnClick(EventArgs.Empty);
			}
		}

		// 定义一个 EventHandler 事件处理器 
		private static readonly EventHandler mailHandler = (sender, e) =>
		{
			Console.WriteLine("发送邮件...");
		};

		public static void Exec1()
		{
			Button btn = new();

			// 订阅按钮点击事件
			btn.Click += (sender, eventArgs) =>
			{
				Console.WriteLine("发送短信...");
			};

			// 再订阅一个按钮点击事件
			btn.Click += mailHandler;

			// 模拟用户按下按钮
			btn.Press();
		}
	}
}
