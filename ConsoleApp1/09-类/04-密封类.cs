using System;

/*
  ✅ 特点：
	1. 不能被继承。
	2. 但可以正常实例化。
	3. 通常用于防止别人继承和修改类的行为。
*/
namespace ConsoleApp1._09_类
{
	public sealed class _04_密封类
	{
		// 用于单例模式
		private static _04_密封类? _instance;
		// 私有构造，配合单例模式
		private _04_密封类() { } 

		// 创建对象, 如果对象存在的话, 就使用既存的, 如果不存在就新创建一个
		public static _04_密封类 Instance => _instance ??= new _04_密封类();

		// 密封类的方法
		public string GetConfig(string key)
		{
			return $"配置项 {key}";
		}
	}

	public class _04_密封类_Utils
	{
		public static void PrintInfo()
		{
			// 创建一个密封类（此处使用了单例模式来创建对象）
			_04_密封类 sealed_cls = _04_密封类.Instance;
			string result = sealed_cls.GetConfig("user_token");
			Console.WriteLine(result);  // 配置项 user_token
		}
	}
}
