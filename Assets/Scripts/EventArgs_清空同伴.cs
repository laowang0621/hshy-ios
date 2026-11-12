using DSB;

public class EventArgs_清空同伴 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_清空同伴).GetHashCode();

	public bool _有烟雾生成;

	public static void Trigger(bool 有烟雾生成)
	{
		EventArgs_清空同伴 eventArgs_清空同伴 = new EventArgs_清空同伴();
		eventArgs_清空同伴._有烟雾生成 = 有烟雾生成;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_清空同伴);
	}
}
