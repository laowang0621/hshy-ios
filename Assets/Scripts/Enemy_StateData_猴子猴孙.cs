using System;
using UnityEngine;

[Serializable]
public class Enemy_StateData_猴子猴孙 : Enemy_StateData_Base<EnemyAI_猴子猴孙, EnemyState_猴子猴孙, Enemy_StateData_猴子猴孙>
{
	protected EnemyAI_猴子猴孙 _mMono;

	public PlayerController _PC;

	public Transform _Tsf_PC;

	public Vector2 _Dir;

	public bool _朝向右边 = true;

	public bool _可以受击;

	public bool _释放蓄力状态;

	public new EnemyAI_猴子猴孙 _Mono
	{
		get
		{
			if (_mMono == null)
			{
				throw new Exception("_mMono为空！");
			}
			return _mMono;
		}
	}

	public Enemy_StateData_猴子猴孙(EnemyAI_猴子猴孙 mono)
		: base(mono)
	{
		_mMono = mono;
	}

	public void F_删除血条()
	{
		if (_UI_HP != null)
		{
			UnityEngine.Object.Destroy(_UI_HP.gameObject);
		}
	}

	public override void F_重置死亡状态()
	{
		base.F_重置死亡状态();
		_可以受击 = true;
		_释放蓄力状态 = false;
	}
}
