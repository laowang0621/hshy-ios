using DSB;
using UnityEngine;

public class MapBGM : MonoBehaviour
{
	public Enum_BGM _Enum_BGM_开始;

	public Enum_BGM _Enum_BGM_退出;

	private void OnEnable()
	{
		switch (_Enum_BGM_开始)
		{
		case Enum_BGM.黑神话_悟空:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_黑神话_悟空();
			break;
		case Enum_BGM.云宫迅音:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_云宫迅音();
			break;
		case Enum_BGM.主题曲:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_主题曲();
			break;
		case Enum_BGM.陕北说书:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_陕北说书();
			break;
		case Enum_BGM.好运来:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_好运来();
			break;
		case Enum_BGM.NULL:
			break;
		}
	}

	private void OnDisable()
	{
		switch (_Enum_BGM_退出)
		{
		case Enum_BGM.黑神话_悟空:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_黑神话_悟空();
			break;
		case Enum_BGM.云宫迅音:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_云宫迅音();
			break;
		case Enum_BGM.主题曲:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_主题曲();
			break;
		case Enum_BGM.陕北说书:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_陕北说书();
			break;
		case Enum_BGM.好运来:
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_好运来();
			break;
		case Enum_BGM.NULL:
			break;
		}
	}
}
