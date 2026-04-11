using System.Diagnostics.CodeAnalysis;

namespace MyMonitorService;

// 为了防止Trim发布时的误删
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class ProcessMonitorOptions
{
	public string[] ProcessNames { get; set; } = [];
	public int MaxMinutes { get; set; } = 1;
	public int CheckIntervalSeconds { get; set; } = 10;
}
