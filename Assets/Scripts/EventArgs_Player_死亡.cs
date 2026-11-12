using DSB;

public class EventArgs_Player_死亡 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player_死亡).GetHashCode();

	public static void Trigger()
	{
		EventArgs_Player_死亡 args = new EventArgs_Player_死亡();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, args);
	}
}
