using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class C_金铙 : MonoBehaviour
{
	public Animator _Ani;

	public C_传送区域_筋斗云_Boss _传送区域_筋斗云_Boss;

	public Transform _Tsf_摄像机1;

	public Transform _Tsf_猪八戒生成位置;

	public GameObject _烟雾_Prefab;

	public Enum_对话 _对话类型_死亡 => Enum_对话.击败亢金星君;

	public void F_Ani_触发对话()
	{
		MyLog.LogWarning($"_Tsf_猪八戒生成位置:{_Tsf_猪八戒生成位置.position}");
		bool 成功生成 = DSB_MonoSingleton<GameManager>.Instance.F_解锁并生成同伴(3001, _Tsf_猪八戒生成位置.position);
		DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_对话类型_死亡, delegate
		{
			if (!成功生成)
			{
				Object.Instantiate(_烟雾_Prefab, _Tsf_猪八戒生成位置.position + new Vector3(0f, 1f), Quaternion.identity);
			}
		});
	}

	public void F_Boss_生成()
	{
		_Ani.Play("金铙_挂起");
	}

	public void F_Boss_死亡()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断是否解锁同伴(3001))
		{
			_传送区域_筋斗云_Boss?.F_显示();
			return;
		}
		DSB_MonoSingleton<GameManager>.Instance._PC.ChangeToState(Player_StateName_Type.正常移动);
		GameManager._CanPlay = false;
		StartCoroutine(Cor_Boss死亡());
	}

	private IEnumerator Cor_Boss死亡()
	{
		yield return new WaitForEndOfFrame();
		DSB_MonoSingleton<GameManager>.Instance._PC._Data._Mono组件._Ani.Play("站立");
		yield return new WaitForSeconds(1f);
		Camera camera = DSB_MonoSingleton<CameraController>.Instance._Camera;
		DSB_MonoSingleton<CameraController>.Instance._由外部控制中 = true;
		yield return new WaitForEndOfFrame();
		float 运动时间1 = Vector2.Distance(camera.transform.position, _Tsf_摄像机1.position) / 3.5f;
		camera.DOOrthoSize(12f, 运动时间1);
		camera.transform.DOMove(_Tsf_摄像机1.position, 运动时间1);
		yield return new WaitForSeconds(运动时间1 + 0.2f);
		_Ani.Play("金铙_降落");
		yield return new WaitForSeconds(4.6f);
		camera.DOOrthoSize(Lunch._镜头默认大小, 运动时间1);
		DSB_MonoSingleton<CameraController>.Instance._由外部控制中 = false;
		camera.transform.DOLocalMove(new Vector2(0f, Lunch._镜头偏移.y), 运动时间1);
		yield return new WaitForSeconds(运动时间1 + 0.2f);
		_传送区域_筋斗云_Boss?.F_显示();
	}

	public void F_Boss_已经死亡()
	{
		_Ani.Play("金铙_已落地");
	}
}
