using DSB;

public class EventArgs_Player_豆子_Change : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player_豆子_Change).GetHashCode();

	public int _当前Val { get; private set; }

	public int _变动Val { get; private set; }

	public int _最大Val { get; private set; }

	public static void Trigger(int 当前Val, int 变动Val, int 最大Val)
	{
		EventArgs_Player_豆子_Change eventArgs_Player_豆子_Change = new EventArgs_Player_豆子_Change();
		eventArgs_Player_豆子_Change._当前Val = 当前Val;
		eventArgs_Player_豆子_Change._变动Val = 变动Val;
		eventArgs_Player_豆子_Change._最大Val = 最大Val;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Player_豆子_Change);
	}
}
