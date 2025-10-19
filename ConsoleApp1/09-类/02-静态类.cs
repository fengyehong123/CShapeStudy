/*
  ✅ 特点：
	1. 不能被实例化（不能用 new 创建对象）。
	2. 类中的所有成员都必须是 静态的（static）。
	3. 不能被继承，也不能继承其他类（除了 object）。

  ✅ 常见用途：
    1. 封装一些 工具方法 或 全局功能。
    2. 例如：数学运算类、日志类、配置访问类。
*/
namespace ConsoleApp1._09_类
{
	public static class _02_静态类
	{
		// 定义两个静态类的静态方法
		public static int Add(int a, int b) => a + b;
		public static int Sub(int a, int b) => a - b;
	}
}
