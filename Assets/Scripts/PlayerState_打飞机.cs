using DSB;
using UnityEngine;

public class PlayerState_打飞机 : Player_StateBase
{
	private float _棍势值;

	private bool _NextState_Is死亡状态;

	public PlayerState_打飞机()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1010);
	}

	public override void OnEnter()
	{
		base._Ani.Play("站立");
		base._Mono组件._打飞机云.SetActive(value: true);
		_NextState_Is死亡状态 = false;
	}

	public override void F_攻击命中回调()
	{
		_Data._Curr蓄力 += _棍势值;
	}

	public override void OnExit()
	{
		base._Mono._静止玩家 = false;
		if (!_NextState_Is死亡状态)
		{
			base._Mono组件._打飞机云.SetActive(value: false);
		}
	}

	public override void OnUpdate()
	{
		F_PlayerMove(16f, can转向: false);
		if (_Data._Is免疫远程)
		{
			_Data._Curr气力 -= _Data_Player_招式系数._棍势值 * Time.deltaTime * 5f;
			if (_Data._Curr气力 < 1f)
			{
				F_棍花Up();
			}
		}
	}

	public override void F_棍花Down()
	{
		if (!_Data._Is免疫远程 && !(_Data._Curr气力 < 1f))
		{
			base._Ani.Play("棍花");
			F_创建攻击命中回调();
			_Data._Is免疫远程 = true;
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(101) > 0.5f)
			{
				base._Mono._m开启反弹弹道 = true;
			}
			_棍势值 = _Data_Player_招式系数._棍势值 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1101);
		}
	}

	public override void F_棍花Up()
	{
		base._Ani.Play("站立");
		base._Mono._m开启反弹弹道 = false;
		_Data._Is免疫远程 = false;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (base._Mono._静止玩家)
		{
			return;
		}
		if (_Data._Is免疫远程 && attackInfo.atk_Is远程)
		{
			_Data._Curr气力 -= 25f;
			MyLog.LogError("格挡弓箭");
			Object.Instantiate(base._Mono组件._特效_火花Prefab, attackInfo.atk_Pos, Quaternion.identity).F_Init(((Vector2)base._Mono组件.transform.position - attackInfo.atk_Pos).normalized);
			return;
		}
		_Data._Curr生命 -= attackInfo.atk_生命;
		base._Mono._BeAttack_Pos = attackInfo.atk_Pos;
		base._Mono._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (!(_Data._Curr生命 > 0f))
		{
			_NextState_Is死亡状态 = true;
			base._Mono.ChangeToState(Player_StateName_Type.死亡);
			EventArgs_Player_死亡.Trigger();
		}
	}
}
