using System;

namespace ConsoleApp1._09_接口
{
	// 🔴定义一个计算船舶运费的策略接口
	public interface IShippingStrategy
	{
		double CalculateShipping();
	}

	// 🔴不同的国家实现该接口, 不同的国家有不同的船舶运费
	public class JapanShipping : IShippingStrategy
	{
		public double CalculateShipping() => 500;
	}
	public class USShipping : IShippingStrategy
	{
		public double CalculateShipping() => 800;
	}
	public class ChinaShipping : IShippingStrategy
	{
		public double CalculateShipping() => 300;
	}

	// 🔴创建一个用来选择策略的上下文类
	public class ShippingCalculator
	{
		// 接口策略
		private readonly IShippingStrategy _strategy;

		public ShippingCalculator(IShippingStrategy strategy)
		{
			_strategy = strategy;
		}

		public double Calculate() => _strategy.CalculateShipping();
	}

	public class _09_策略模式接口_Utils
	{
		private static IShippingStrategy GetStrategy(string country) => country switch
		{
			"JP" => new JapanShipping(),
			"US" => new USShipping(),
			"CN" => new ChinaShipping(),
			_ => throw new ArgumentException("未知国家")
		};

		public static void Exec()
		{
			// 模拟前台传入的条件
			string country = "US";
			ShippingCalculator calculator = new(GetStrategy(country));
			Console.WriteLine("__________________________________________");
			Console.WriteLine($"运费：{calculator.Calculate()}");  // 运费：800
		}
	}

}
