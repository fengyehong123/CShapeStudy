using System;

/*
 * 🔴virtual 关键字用于修饰方法、属性或事件
 *    表示这个成员可以在派生类（子类）中被重写（override）
 *    只是可以被重写, 并不是一定要被重写, 这个和 abstract 的抽象类不同
 *    
 * 🔴virtual 是为「多态」和「可扩展设计」服务的。
 *    它的作用不是立刻显现出来，而是在“允许将来子类改写父类行为”的场景中非常有用。
 *    abstract 要求必须实现，而很多情况下我们希望提供一个“默认实现”，
 *    但又允许子类选择性重写，这正是 virtual 的价值所在
 */
namespace ConsoleApp1._09_类
{
	public class Base_类中的virtual虚方法
	{
		// 父类中的方法
		public void Speak()
		{
			Console.WriteLine("动物发出声音");
		}

		// 🔴父类中的 Sleep 方法使用了 virtual 关键字, 表示该方法在子类中可以重写, 但并不是必须重写。
		public virtual void Sleep()
		{
			Console.WriteLine("动物在睡觉");
		}
	}

	class Cat : Base_类中的virtual虚方法
	{
		/*
			父类中的Speak方法没有添加virtual,因此无法通过 override 关键字进行重写
			public override void Speak()
			{
				Console.WriteLine("猫发出声音");
			}
		*/

		// 这种方式并不是重写了父类的Speak方法，而是写了一个子类自己的 Speak 方法
		public new void Speak()
		{
			Console.WriteLine("猫发出声音");
		}

		// 父类中的 Sleep 方法使用了 virtual 关键字，子类重写了父类中的 Sleep 方法
		public override void Sleep()
		{
			Console.WriteLine("猫在喵喵的叫");
		}
	}

	// ----------------------------------- ↓↓↓模板方法↓↓↓ -----------------------------------------------
	// 定义一个抽象类
	public abstract class Game
	{
		// 抽象类中的 Play() 方法, 子类无法重写
		public void Play()
		{
			Initialize();
			StartPlay();
			EndPlay();
		}

		// 使用 virtual 标记的方法, 子类可以重写, 也可以不重写
		public virtual void Initialize() => Console.WriteLine("初始化游戏");
		// 抽象方法, 继承抽象类的子类必须实现
		public abstract void StartPlay();
		public virtual void EndPlay() => Console.WriteLine("游戏结束");
	}

	class Football : Game
	{
		public override void StartPlay() => Console.WriteLine("足球比赛开始！");
	}

	class Basketball : Game
	{
		public override void StartPlay() => Console.WriteLine("篮球比赛开始！");
	}
	// ----------------------------------- ↑↑↑模板方法↑↑↑ -----------------------------------------------

	class 虚方法_Utils 
	{
		public static void PrintInfo() 
		{
			// 使用了多态
			// 因为父类中的.Speak()方法并没有使用virtual关键字, 所以子类无法对Speak()方法进行重写
			// 因此此时的 .Speak() 方法是父类的 
			Base_类中的virtual虚方法 cat1 = new Cat();
			cat1.Speak();  // 动物发出声音

			// 父类中的.Speak()方法使用了virtual关键字,子类可以对Speak()方法进行重写
			cat1.Sleep();  // 猫在喵喵的叫

			// 没有使用多态, 此时的 .Speak() 方法是子类的 
			Cat cat2 = new();
			cat2.Speak();  // 猫发出声音
			cat2.Sleep();  // 猫在喵喵的叫

			Console.WriteLine("________________________________________________");
			Football football = new();
			football.Play();

			Console.WriteLine("________________________________________________");
			Basketball basketball = new();
			basketball.Play();

			Console.WriteLine("________________________________________________");
			// 使用多态
			Game game = new Basketball();
			game.Play();
		}
	}
}
