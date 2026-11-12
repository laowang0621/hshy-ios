using DSB;
using UnityEngine;

public class PlayerState_AK : Player_StateBase
{
	private float _受击Timer;

	private bool _开火;

	public override void OnEnter()
	{
		_Timer = 0f;
		_开火 = false;
		base._Mono._m开启反弹弹道 = false;
		_受击Timer = -1f;
		base._Ani.SetFloat("AK", 1f);
	}

	public override void OnExit()
	{
		base._Ani.SetFloat("AK", 0f);
	}

	public override void OnUpdate()
	{
		if (_受击Timer > 0f)
		{
			_受击Timer -= Time.deltaTime;
		}
		else if (_开火)
		{
			if (F_PlayerMove(6f, can转向: false))
			{
				base._Ani.Play("AK_移动");
			}
			else
			{
				base._Ani.Play("AK_站立");
			}
			_Timer -= Time.deltaTime;
			if (_Timer <= 0f)
			{
				F_开火();
			}
		}
		else if (F_PlayerMove(8f))
		{
			base._Ani.Play("AK_移动");
		}
		else
		{
			base._Ani.Play("AK_站立");
		}
	}

	private void F_开火()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_AK();
		Object.Instantiate(base._Mono组件._GO_枪焰, base._Mono组件._Tsf_枪焰.position, Quaternion.identity).transform.localScale = base._Mono组件.transform.localScale;
		_Timer = base._Mono组件._子弹间隔CD;
		Vector2 vector = base._Mono组件._Tsf_子弹.position + new Vector3(0f, Random.Range(-0.2f, 0.2f), 0f);
		Object.Instantiate(base._Mono组件._GO_子弹, vector, Quaternion.identity).F_Init(new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
	}

	public override void F_重攻击Down()
	{
		_Timer = 0f;
		_开火 = true;
	}

	public override void F_重攻击Up()
	{
		_开火 = false;
	}

	public override void F_轻攻击Down()
	{
		F_重攻击Down();
	}

	public override void F_轻攻击Up()
	{
		F_重攻击Up();
	}

	public override void F_装备AK()
	{
		ChangeToState(Player_StateName_Type.正常移动);
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
		_受击Timer = 0.22f;
		base._Ani.Play("受击");
		base._Rb2D.velocity = new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized * base._Mono._BeAttack_击退力度;
		F_玩家转向(base._Mono._BeAttack_Pos.x - base._Mono.transform.position.x);
	}
}
