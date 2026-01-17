using System;

namespace ConsoleApp2._04_Event
{
	public class _02_Event
	{
		// 自定义一个事件参数 → 事件可以携带上下文数据
		private class OrderEventArgs(int orderId, decimal price) : EventArgs
		{
			public int OrderId { get; } = orderId;
			public decimal Price { get; } = price;
		}

		// 定义一个按钮类
		private class Button
		{
			// 1. 定义一个事件, 使用自定义事件参数
			public event EventHandler<OrderEventArgs>? OrderCreated;

			// 2. 定义触发事件的方法（通常是 protected virtual）
			protected virtual void OnClick(OrderEventArgs e)
			{
				OrderCreated?.Invoke(this, e);
			}

			// 3. 对外暴露的【业务动作】
			public void Press()
			{
				Console.WriteLine("Button.Press() 被调用");

				// 实例化一个事件参数, 假设用户通过点击按钮下单
				OrderEventArgs orderArgs = new(
					orderId: 10001,
					price: 199.99m
				);
				OnClick(orderArgs);
			}
		}

		// 定义一个 EventHandler 事件处理器 
		private static readonly EventHandler<OrderEventArgs> mailHandler = (sender, e) =>
		{
			Console.WriteLine($"发送邮件：订单号={e.OrderId}, 金额={e.Price}");
		};

		public static void Exec1()
		{
			Button btn = new();

			// 订阅按钮点击事件
			btn.OrderCreated += (sender, eventArgs) =>
			{
				Console.WriteLine($"发送短信：订单 {eventArgs.OrderId}，金额 {eventArgs.Price}");
			};

			// 再订阅一个按钮点击事件
			btn.OrderCreated += mailHandler;

			// 模拟用户按下按钮
			btn.Press();
		}
	}
}
