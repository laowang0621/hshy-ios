using DSB;

public class EventArgs_开始游戏 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_开始游戏).GetHashCode();

	public static void Trigger()
	{
		EventArgs_开始游戏 args = new EventArgs_开始游戏();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, args);
	}
}
