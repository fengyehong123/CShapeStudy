using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._06_集合
{
	public class _01_List
	{
		public static void PrintInfo()
		{
			// 创建一个List对象, 并赋予初始值
			List<int> numbers = new() { 1, 2, 3 };
			// 向List集合中追加元素
			numbers.Add(4);
		}
	}
}
