// 接口的命名空间
using System.Threading;
using System.Threading.Tasks;

namespace MyMonitorService.Services.Interfaces;

// 接口
public interface IHistoryCollector_v4
{
	Task Run(CancellationToken token);
}
