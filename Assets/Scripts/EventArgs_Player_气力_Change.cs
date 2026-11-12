using DSB;

public class EventArgs_Player_气力_Change : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player_气力_Change).GetHashCode();

	public float _当前Val { get; private set; }

	public float _变动Val { get; private set; }

	public float _当前最大Val { get; private set; }

	public float _最大Val { get; private set; }

	public static void Trigger(float 当前Val, float 变动Val, float 当前最大Val, float 最大Val)
	{
		EventArgs_Player_气力_Change eventArgs_Player_气力_Change = new EventArgs_Player_气力_Change();
		eventArgs_Player_气力_Change._当前Val = 当前Val;
		eventArgs_Player_气力_Change._变动Val = 变动Val;
		eventArgs_Player_气力_Change._当前最大Val = 当前最大Val;
		eventArgs_Player_气力_Change._最大Val = 最大Val;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Player_气力_Change);
	}
}
