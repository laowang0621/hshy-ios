using DSB;

public class EventArgs_沙二郎死亡 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_回到主页).GetHashCode();

	public static void Trigger()
	{
		EventArgs_沙二郎死亡 args = new EventArgs_沙二郎死亡();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, args);
	}
}
