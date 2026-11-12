using DSB;

public class EventArgs_回到主页 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_回到主页).GetHashCode();

	public static void Trigger()
	{
		EventArgs_回到主页 args = new EventArgs_回到主页();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, args);
	}
}
