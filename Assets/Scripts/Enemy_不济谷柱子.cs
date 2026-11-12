using System;
using DG.Tweening;
using DSB;
using UnityEngine;

public class Enemy_不济谷柱子 : MonoBehaviour, IBeAttack, IEnemy
{
	[SerializeField]
	private Animator _Ani;

	[SerializeField]
	private Transform _柱子顶;

	[SerializeField]
	private Transform _Tsf_Model;

	private Vector3 _StartPos_Model;

	public bool _Is死亡;

	private Tween _Tween_震动;

	private float _Max生命 = 200f;

	private float _Curr生命;

	public int _阵营
	{
		get
		{
			return 3;
		}
		set
		{
			MyLog.LogError("？？？？？");
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public event Action<Enemy_不济谷柱子> _On_碎裂 = delegate
	{
	};

	private void Awake()
	{
		_Max生命 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_基础数值(10203)._生命值;
		_Curr生命 = _Max生命;
		_Is死亡 = false;
		_StartPos_Model = _Tsf_Model.position;
	}

	public Vector2 F_得到站立的位置()
	{
		return _柱子顶.position;
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_打碎罐子();
		MyLog.LogError($">>>>>>>>{_Is死亡}>>{_Curr生命}");
		if (!_Is死亡)
		{
			F_震动();
			_Curr生命 -= attackInfo.atk_生命;
			if (_Curr生命 <= 0f)
			{
				this._On_碎裂(this);
				_Ani.Play("柱子_1");
				_Is死亡 = true;
			}
		}
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

	public void F_隐藏()
	{
	}

	public void F_显示()
	{
		if (_Is死亡)
		{
			_Ani.Play("柱子_2");
		}
		else
		{
			_Ani.Play("柱子_0");
		}
	}

	public void F_重置()
	{
		_Is死亡 = false;
		_Curr生命 = _Max生命;
	}
}
