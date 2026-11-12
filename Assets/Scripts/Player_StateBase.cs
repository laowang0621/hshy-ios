using DSB;
using UnityEngine;

public class Player_StateBase : DSB_State<Player_StateBase, Player_StateData>
{
	public float _Timer;

	public DataInfo_Player招式 _Data_Player_招式系数;

	public DataInfo_Player技能 _Data_Player_技能;

	public Player_StateName_Type _NextState;

	public bool _Is继续跳跃;

	public bool _Is继续翻滚;

	public PlayerController _Mono => _Data._Mono;

	public PlayerMono组件 _Mono组件 => _Data._Mono组件;

	public Rigidbody2D _Rb2D => _Mono组件._Rb2D;

	public Animator _Ani => _Mono组件._Ani;

	public void ChangeToState(Player_StateName_Type state)
	{
		_Mono.ChangeToState(state);
	}

	public void ChangeState(Player_StateName_Type state)
	{
		_Mono.ChangeState(state);
	}

	public bool F_PlayerMove(float speed, bool can转向 = true)
	{
		return _Mono.F_PlayerMove(speed, can转向);
	}

	public void F_玩家转向(float x)
	{
		_Mono.F_玩家转向(x);
	}

	public override void OnEnter()
	{
		_Timer = 1f;
		_NextState = Player_StateName_Type.正常移动;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			ChangeState(_NextState);
		}
	}

	public virtual void F_创建攻击命中回调(bool 是强破坏力 = false)
	{
		F_创建攻击命中回调(_Data_Player_招式系数, 是强破坏力);
	}

	public virtual void F_创建攻击命中回调(DataInfo_Player招式 Data_Player招式_系数, bool 是强破坏力 = false)
	{
		_Mono组件._C_攻击判断._伤害 = _Data._攻击力 * Data_Player招式_系数._伤害系数;
		_Data._Curr气力 -= Data_Player招式_系数._气力消耗;
		_Mono组件._C_攻击判断._攻击命中回调 += F_攻击命中回调;
		_Mono组件._C_攻击判断._僵直伤害 = Data_Player招式_系数._僵直伤害;
		_Mono组件._C_攻击判断._是强破坏力 = 是强破坏力;
	}

	public virtual void F_攻击命中回调()
	{
		_Data._Curr蓄力 += _Data_Player_招式系数._棍势值;
	}

	public virtual void F_喝酒()
	{
	}

	public virtual void F_棍花Down()
	{
	}

	public virtual void F_棍花Up()
	{
	}

	public virtual void F_跳跃Down()
	{
	}

	public virtual void F_跳跃Up()
	{
	}

	public virtual void F_轻攻击Down()
	{
	}

	public virtual void F_轻攻击Up()
	{
	}

	public virtual void F_重攻击Down()
	{
	}

	public virtual void F_重攻击Up()
	{
	}

	public virtual void F_翻滚()
	{
	}

	public virtual void F_定身术()
	{
	}

	public virtual void F_隐身()
	{
	}

	public virtual void F_猴子猴孙()
	{
	}

	public virtual void F_化形()
	{
	}

	public virtual void F_精魄()
	{
	}

	public virtual void F_装备AK()
	{
	}

	public virtual void F_出租云()
	{
	}

	public virtual void F_拿起爆竹()
	{
	}

	public virtual void F_特殊操作_1()
	{
	}

	public virtual void F_外部施加跳跃高度(float 高度)
	{
	}

	public virtual void BeAttack(AttackInfo attackInfo)
	{
		_Data.BeAttack(attackInfo);
	}
}
