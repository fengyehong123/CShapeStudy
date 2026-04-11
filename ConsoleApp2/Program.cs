using ConsoleApp2._01_特性;
using ConsoleApp2._02_反射;
using ConsoleApp2._03_委托;
using ConsoleApp2._04_Event;
using ConsoleApp2._05_多线程;
using System;
using System.Threading.Tasks;

// 也可以通过这种方式来写namespace
// 少了一层包裹, 更加的简洁
namespace ConsoleApp2;

class Program
{
	static void Main1(string[] args)
	{
		_01_特性Attribute使用.PrintInfo();
		_02_自定义特性.PrintInfo1();
		_02_自定义特性.PrintInfo2();
	}

	static void Main2(string[] args)
	{
		_01_反射.PrintInfo1();
		_01_反射.PrintInfo2();
		_01_反射.PrintInfo3();
		_02_反射封装.PrintInfo();
	}

	static void Main3(string[] args)
	{
		_01_委托.PrintInfo();
		_02_委托使用.PrintInfo();

		_03_委托使用_Demo1.Demo_Event();
		_03_委托使用_Demo2.Demo_Hook();

		_03_委托使用_Demo3_1.Demo_SideEffects();
		_03_委托使用_Demo3_2.Demo_Start_DB();

		_03_委托使用_Demo4.Demo_Observer();
	}

	static void Main4(string[] args)
	{
		_01_Event.Exec1();
		_02_Event.Exec1();
		_03_订单系统.OrderStart();
	}

	static async Task Main5(string[] args)
	{
		_01_Thread和ThreadPool.ThreadMethod();
		_01_Thread和ThreadPool.ThreadPoolMethod();

		_02_Task_aysnc_await_1.PrintInfo1();
		await _02_Task_aysnc_await_1.PrintInfo2Async();
		await _02_Task_aysnc_await_1.PrintInfo3Async();
		await _02_Task_aysnc_await_1.PrintInfo4Async();

		await _02_Task_aysnc_await_2.PrintInfo1Async();
		await _02_Task_aysnc_await_2.PrintInfo2Async();
		await _02_Task_aysnc_await_2.PrintInfo3Async();

		await _02_Task_aysnc_await_3.PrintInfo1Async();
		await _02_Task_aysnc_await_4.PrintInfo1Async();
	}

	static void Main(string[] args)
	{
		Console.WriteLine("Hello World!");
	}
}
