using DSB;

public class EventArgs_特殊场景 : DSB_EventArgs
{
	public enum Type_EventArgs_特殊场景
	{
		滑沙,
		飞机大战,
		下地狱100层,
		紫蛛儿
	}

	public static readonly int _EventID = typeof(EventArgs_特殊场景).GetHashCode();

	public Type_EventArgs_特殊场景 _特殊场景 { get; set; }

	public bool _是进入 { get; set; }

	public static void Trigger(Type_EventArgs_特殊场景 特殊场景, bool 是进入)
	{
		EventArgs_特殊场景 eventArgs_特殊场景 = new EventArgs_特殊场景();
		eventArgs_特殊场景._特殊场景 = 特殊场景;
		eventArgs_特殊场景._是进入 = 是进入;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_特殊场景);
	}
}
