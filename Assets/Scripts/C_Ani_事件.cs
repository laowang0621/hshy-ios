using DSB;
using UnityEngine;

public class C_Ani_事件 : MonoBehaviour
{
	public void F_销毁()
	{
		Object.Destroy(base.gameObject);
	}

	public void F_Ani_Play_爆炸()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_爆炸();
	}

	public void F_Ani_Play_爆炸_炸弹桶()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_炸弹桶();
	}

	public void F_Ani_屏幕震动(float 持续时间)
	{
		DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(持续时间, 0.15f, 12);
	}

	public void F_Ani_Play_SFX_爆炸电流()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_爆炸电流();
	}

	public void F_Ani_Play_SFX_大落雷()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
	}

	public void F_Ani_Play_SFX_小落雷()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_落雷();
	}
}
