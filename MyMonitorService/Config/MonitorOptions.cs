using System.Diagnostics.CodeAnalysis;

namespace MyMonitorService.Config;

// 为了防止Trim发布时的误删
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class MonitorOptions
{
	public string[] ProcessNames { get; set; } = [];
	public int ProcessMaxMinutes { get; set; } = 1;
	public int ProcessCheckIntervalSeconds { get; set; } = 10;
	public int BrowerHistoryCheckIntervalSeconds { get; set; } = 60;
}