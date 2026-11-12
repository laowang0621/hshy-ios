using DSB;
using UnityEngine;

public class C_特效 : MonoBehaviour
{
	public void Start()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
	}

	private void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
	}

	public void F_Ani_Play_爆炸()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_爆炸();
	}

	private void F_On_Event_切换地图(DSB_EventArgs args)
	{
		Object.Destroy(base.gameObject);
	}
}
