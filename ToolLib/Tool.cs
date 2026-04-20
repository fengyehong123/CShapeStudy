using System.Runtime.InteropServices;

namespace ToolLib
{
	[ComVisible(true)]
	[Guid("1D4C98EC-D988-4D6C-8DD6-189E238D7B4E")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IToolCom
	{
		int Add(int a, int b);
		string Hello(string name);

	}

	// 设置Com可见, 否则vba无法使用
	[ComVisible(true)]
	// GUID 必须唯一, 可以使用 Visual Studio 生成
	[Guid("6143FB0B-9C17-4859-860C-6DA4A466ECD1")]
	// 显示声明ProgId, 便于vba通过 CreateObject("ToolLib.ToolCom") 创建对象
	[ProgId("ToolLib.ToolCom")]
	[ClassInterface(ClassInterfaceType.None)]
	public class ToolCom : IToolCom
	{
		public int Add(int a, int b)
		{
			return a + b;
		}

		public string Hello(string name)
		{
			return $"Hello, {name}";
		}
	}
}
