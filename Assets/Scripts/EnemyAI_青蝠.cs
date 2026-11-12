using DSB;
using UnityEngine;

public class EnemyAI_青蝠 : EnemyAI_狼剑客
{
	public bool _m飞_高;

	[HideInInspector]
	public Collider2D _PlayerCo;

	private float _mMoveTimerVal;

	private float _mMoveTimer;

	public bool _飞_高
	{
		get
		{
			return _m飞_高;
		}
		set
		{
			_m飞_高 = value;
			Physics2D.IgnoreCollision(_Data._Mono组件._Co2D_自身碰撞器, _PlayerCo, _m飞_高);
		}
	}

	public float _MoveTimer
	{
		get
		{
			return _mMoveTimer;
		}
		set
		{
			_mMoveTimer = value;
			if (_mMoveTimer > _mMoveTimerVal)
			{
				_飞_高 = !_飞_高;
				_mMoveTimer -= _mMoveTimerVal;
				_mMoveTimerVal = Random.Range(4f, 8f);
			}
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_m飞_高)
		{
			base.BeAttack(attackInfo);
		}
	}

	public override void F_重置()
	{
		base.F_重置();
		_飞_高 = false;
		_MoveTimer = 0f;
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_PlayerCo = DSB_MonoSingleton<GameManager>.Instance._PC.GetComponent<Collider2D>();
		_飞_高 = false;
		_mMoveTimerVal = Random.Range(4f, 8f);
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_青蝠_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_青蝠_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_青蝠_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_青蝠_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_青蝠_攻击().Init(_Data));
	}

	public void F_强制改变飞行状态(bool is高飞)
	{
		_飞_高 = is高飞;
		_mMoveTimer = 0f;
	}
}
