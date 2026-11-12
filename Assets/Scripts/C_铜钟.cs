using DG.Tweening;
using DSB;
using UnityEngine;

public class C_铜钟 : MonoBehaviour, IBeAttack
{
	public int _铜钟ID;

	public Transform _Tsf_Model;

	private Vector3 _StartPos_Model;

	private Tween _Tween_震动;

	public int _阵营
	{
		get
		{
			return 2;
		}
		set
		{
			MyLog.LogError("??????");
		}
	}

	public Vector2 _BeAttack_Pos
	{
		get
		{
			return Vector2.zero;
		}
		set
		{
			MyLog.LogError("??????");
		}
	}

	public float _BeAttack_击退力度
	{
		get
		{
			return 1f;
		}
		set
		{
			MyLog.LogError("??????");
		}
	}

	private void Start()
	{
		_StartPos_Model = _Tsf_Model.position;
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		MyLog.LogWarning($"本次攻击造成伤害>>{attackInfo.atk_生命}");
		DSB_MonoSingleton<GameManager>.Instance.F_敲钟(_铜钟ID - 1);
		F_震动();
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_敲钟();
	}

	private void F_震动()
	{
		if (_Tween_震动 != null)
		{
			_Tween_震动.Kill();
		}
		MyLog.LogError("震动");
		_Tsf_Model.position = _StartPos_Model;
		_Tween_震动 = _Tsf_Model.DOShakePosition(0.5f, 0.2f);
	}
}
