using DSB;
using UnityEngine;

public class EventArgs_Player状态提示 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_Player状态提示).GetHashCode();

	public string _Tips { get; private set; }

	public Transform _Tsf_PlayerUI { get; private set; }

	public static void Trigger(string tips, Transform tsf_PlayerUI)
	{
		EventArgs_Player状态提示 eventArgs_Player状态提示 = new EventArgs_Player状态提示();
		eventArgs_Player状态提示._Tips = tips;
		eventArgs_Player状态提示._Tsf_PlayerUI = tsf_PlayerUI;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_Player状态提示);
	}
}
