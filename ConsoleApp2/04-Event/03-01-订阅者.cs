using System;

namespace ConsoleApp2._04_Event
{
	// 库存服务（订阅者）
	public class InventoryService
	{
		public void OnOrderCreated(object? sender, OrderEventArgs e)
		{
			Console.WriteLine(
				$"[库存] 扣减商品 {e.ProductId} 数量 {e.Quantity}"
			);
		}
	}

	// 支付服务（订阅者）
	public class PaymentService
	{
		public void OnOrderCreated(object? sender, OrderEventArgs e)
		{
			Console.WriteLine(
				$"[支付] 扣款金额 {e.TotalPrice} 元（订单 {e.OrderId}）"
			);
		}
	}

	// 通知服务（订阅者）
	public class NotificationService
	{
		public void OnOrderCreated(object? sender, OrderEventArgs e)
		{
			Console.WriteLine(
				$"[通知] 订单 {e.OrderId} 创建成功，金额 {e.TotalPrice}"
			);
		}
	}
}
