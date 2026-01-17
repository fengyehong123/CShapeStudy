using System;

namespace ConsoleApp2._04_Event
{
	// 自定义事件参数（业务上下文）
	public class OrderEventArgs(int orderId, int productId, int quantity, decimal totalPrice) : EventArgs
	{
		public int OrderId { get; } = orderId;
		public int ProductId { get; } = productId;
		public int Quantity { get; } = quantity;
		public decimal TotalPrice { get; } = totalPrice;
	}

	public class OrderService
	{
		// 核心事件：订单创建完成
		public event EventHandler<OrderEventArgs>? OrderCreated;

		// 下单服务（事件发布者）
		public void CreateOrder(int productId, int quantity)
		{
			Console.WriteLine("开始创建订单...");

			// 模拟业务计算
			int orderId = Random.Shared.Next(1000, 9999);
			decimal pricePerUnit = 99.9m;
			decimal totalPrice = pricePerUnit * quantity;

			Console.WriteLine($"订单创建成功，订单号={orderId}");

			// 发布事件
			OrderEventArgs args = new(
				orderId,
				productId,
				quantity,
				totalPrice
			);
			OnOrderCreated(args);
		}

		protected virtual void OnOrderCreated(OrderEventArgs e)
		{
			OrderCreated?.Invoke(this, e);
		}
	}

	public class _03_订单系统
	{
		public static void OrderStart() 
		{
			// 事件源
			OrderService orderService = new();

			// --- 订阅者 ---
			// 库存服务
			InventoryService inventoryService = new();
			// 支付服务
			PaymentService paymentService = new();
			// 通知服务
			NotificationService notificationService = new();
			// --- 订阅者 ---

			// -------------- 订阅事件 --------------
			// 库存服务
			orderService.OrderCreated += inventoryService.OnOrderCreated;
			// 支付服务
			orderService.OrderCreated += paymentService.OnOrderCreated;
			// 通知服务
			orderService.OrderCreated += notificationService.OnOrderCreated;
			// -------------- 订阅事件 --------------

			// 下单
			orderService.CreateOrder(productId: 101, quantity: 2);
		}
	}
}
