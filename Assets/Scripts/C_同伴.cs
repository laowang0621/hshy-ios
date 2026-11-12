using DSB;
using UnityEngine;

public class C_同伴 : MonoBehaviour
{
	private NPCAI_同伴 _MonoAI;

	private void Awake()
	{
		_MonoAI = GetComponent<NPCAI_同伴>();
	}

	public void Start()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_清空同伴._EventID, F_EventArgs_清空同伴);
	}

	private void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_清空同伴._EventID, F_EventArgs_清空同伴);
	}

	private void F_On_Event_切换地图(DSB_EventArgs args)
	{
		Object.Destroy(base.gameObject);
	}

	private void F_EventArgs_清空同伴(DSB_EventArgs args)
	{
		if (((EventArgs_清空同伴)args)._有烟雾生成)
		{
			_MonoAI?.F_生成烟雾特效();
		}
		Object.Destroy(base.gameObject);
	}
}
