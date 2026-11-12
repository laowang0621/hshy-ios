using DSB;

public class EventArgs_道具提示 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_道具提示).GetHashCode();

	public DataInfo_Porp info { get; set; }

	public static void Trigger(DataInfo_Porp info)
	{
		EventArgs_道具提示 eventArgs_道具提示 = new EventArgs_道具提示();
		eventArgs_道具提示.info = info;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_道具提示);
	}
}
