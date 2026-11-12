using DSB;
using UnityEngine;

public class PlayerState_爆竹 : Player_StateBase
{
	private float _受击Timer;

	private float _倒计时Time;

	private float _倒计时Timer;

	private float _反噬伤害;

	private bool _已经引爆;

	public override Player_StateBase Init(Player_StateData data)
	{
		DataInfo_Enemy基础数值 dataInfo_Enemy基础数值 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_基础数值(10309);
		_倒计时Time = dataInfo_Enemy基础数值._生命值;
		_反噬伤害 = dataInfo_Enemy基础数值._僵直增幅;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		_Timer = 0f;
		_已经引爆 = false;
		_受击Timer = -1f;
		_倒计时Timer = _倒计时Time;
		base._Mono._Curr爆竹._ItemUI_倒计时._可见性 = true;
		base._Mono._Curr爆竹._ItemUI_倒计时.F_SetTarget(base._Mono组件._TipsUI);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_投掷();
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (_已经引爆)
		{
			return;
		}
		_倒计时Timer -= Time.deltaTime;
		base._Mono._Curr爆竹._ItemUI_倒计时.F_SetText(_倒计时Timer.ToString("0.0"));
		if (_倒计时Timer <= 0f)
		{
			_已经引爆 = true;
			base._Mono._BeAttack_Pos = new Vector2(_Data._朝向右边 ? 1 : (-1), 0f);
			base._Mono._BeAttack_击退力度 = 20f;
			if (!GameManager._免疫伤害)
			{
				_Data._Curr生命 -= _反噬伤害;
			}
			if (_Data._Curr生命 <= 0f)
			{
				base._Mono.ChangeToState(Player_StateName_Type.死亡);
			}
			else
			{
				base._Mono.ChangeToState(Player_StateName_Type.受击);
			}
			base._Mono._Curr爆竹.F_引爆(base._Mono组件.transform.position + new Vector3(0f, 1f, 0f));
		}
		else if (_受击Timer > 0f)
		{
			_受击Timer -= Time.deltaTime;
		}
		else if (F_PlayerMove(8f))
		{
			base._Ani.Play("举爆竹_移动");
		}
		else
		{
			base._Ani.Play("举爆竹_站立");
		}
	}

	public override void F_重攻击Down()
	{
	}

	public override void F_轻攻击Down()
	{
	}

	public override void F_重攻击Up()
	{
		F_丢爆竹();
	}

	public override void F_轻攻击Up()
	{
		F_丢爆竹();
	}

	private void F_丢爆竹()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_投掷();
		base._Mono._Curr爆竹._ItemUI_倒计时.F_SetTarget(null);
		base._Mono._Curr爆竹.F_被丢出(_倒计时Timer, base._Mono.transform.position, new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
		ChangeToState(Player_StateName_Type.正常移动);
	}

	public override void F_装备AK()
	{
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		base._Mono组件._Rb2D.drag = 5f;
		base._Mono._BeAttack_Pos = attackInfo.atk_Pos;
		base._Mono._BeAttack_击退力度 = attackInfo.atk_击退力度;
		_Data._Curr生命 -= attackInfo.atk_生命;
		if (_Data._Curr生命 <= 0f)
		{
			base._Mono.ChangeToState(Player_StateName_Type.死亡);
			return;
		}
		_受击Timer = 0.12f;
		base._Rb2D.velocity = new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized * base._Mono._BeAttack_击退力度;
		F_玩家转向(base._Mono._BeAttack_Pos.x - base._Mono.transform.position.x);
	}
}
