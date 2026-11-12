using DSB;

public class EventArgs_Player_等级_Change : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player_等级_Change).GetHashCode();

	public int _当前Val { get; private set; }

	public static void Trigger(int 当前Val)
	{
		EventArgs_Player_等级_Change eventArgs_Player_等级_Change = new EventArgs_Player_等级_Change();
		eventArgs_Player_等级_Change._当前Val = 当前Val;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Player_等级_Change);
	}
}
