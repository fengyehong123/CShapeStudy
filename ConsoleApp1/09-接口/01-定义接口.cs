using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._09_接口
{
	// 定义一个	Animal 接口
	public interface IAnimal
	{
		void Eat();
		void Sleep();
	}

	// 定义多个接口
	public interface IFly
	{
		void Fly();
	}
	public interface IRun
	{
		void Run();
	}

	// 一个类只能继承一个父类, 但是可以实现多个接口
	public class Bird : IAnimal, IFly, IRun
	{
		// 接口中的方法
		public void Eat() => Console.WriteLine("鸟在吃虫");
		public void Sleep() => Console.WriteLine("鸟在睡觉");
		public void Fly() => Console.WriteLine("鸟在飞");
		public void Run() => Console.WriteLine("鸟在地上跑");
	}
}
