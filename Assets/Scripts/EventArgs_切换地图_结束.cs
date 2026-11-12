using DSB;

public class EventArgs_切换地图_结束 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_切换地图_结束).GetHashCode();

	public static void Trigger()
	{
		EventArgs_切换地图_结束 args = new EventArgs_切换地图_结束();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, args);
	}
}
