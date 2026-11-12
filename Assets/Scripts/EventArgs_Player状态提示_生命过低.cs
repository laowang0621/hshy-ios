using DSB;

public class EventArgs_Player状态提示_生命过低 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player状态提示_生命过低).GetHashCode();

	public bool _Is过低 { get; private set; }

	public static void Trigger(bool 过低)
	{
		EventArgs_Player状态提示_生命过低 eventArgs_Player状态提示_生命过低 = new EventArgs_Player状态提示_生命过低();
		eventArgs_Player状态提示_生命过低._Is过低 = 过低;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Player状态提示_生命过低);
	}
}
