using DSB;
using UnityEngine;

public class EventArgs_Tips_获得精魄 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Tips_获得精魄).GetHashCode();

	public Sprite _Sprite { get; set; }

	public string _Str { get; set; }

	public static void Trigger(Sprite sprite, string str)
	{
		EventArgs_Tips_获得精魄 eventArgs_Tips_获得精魄 = new EventArgs_Tips_获得精魄();
		eventArgs_Tips_获得精魄._Sprite = sprite;
		eventArgs_Tips_获得精魄._Str = str;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Tips_获得精魄);
	}
}
