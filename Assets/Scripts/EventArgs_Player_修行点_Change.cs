using DSB;

public class EventArgs_Player_修行点_Change : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player_修行点_Change).GetHashCode();

	public int _当前Val { get; private set; }

	public static void Trigger(int 当前Val)
	{
		EventArgs_Player_修行点_Change eventArgs_Player_修行点_Change = new EventArgs_Player_修行点_Change();
		eventArgs_Player_修行点_Change._当前Val = 当前Val;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Player_修行点_Change);
	}
}
