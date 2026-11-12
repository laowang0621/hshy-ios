using DSB;

public class EventArgs_Player重生 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player重生).GetHashCode();

	public static void Trigger()
	{
		EventArgs_Player重生 args = new EventArgs_Player重生();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, args);
	}
}
