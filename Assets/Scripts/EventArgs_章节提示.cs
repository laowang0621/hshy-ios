using DSB;

public class EventArgs_章节提示 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_章节提示).GetHashCode();

	public int _章节ID { get; set; }

	public static void Trigger(int 章节ID)
	{
		EventArgs_章节提示 eventArgs_章节提示 = new EventArgs_章节提示();
		eventArgs_章节提示._章节ID = 章节ID;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_章节提示);
	}
}
