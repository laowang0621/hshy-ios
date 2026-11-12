using DSB;

public class EventArgs_上香 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_上香).GetHashCode();

	public string 大地图名字 { get; set; }

	public string 小地图名字 { get; set; }

	public bool 需要提示 { get; set; }

	public static void Trigger(string 大地图名字, string 小地图名字, bool 需要提示 = true)
	{
		EventArgs_上香 eventArgs_上香 = new EventArgs_上香();
		eventArgs_上香.大地图名字 = 大地图名字;
		eventArgs_上香.小地图名字 = 小地图名字;
		eventArgs_上香.需要提示 = 需要提示;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_上香);
	}
}
