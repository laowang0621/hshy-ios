using DSB;
using UnityEngine;

public class PlayerState_轻棍1 : Player_StateBase
{
	private DataInfo_Player招式 _力竭系数;

	private DataInfo_Player招式 _力竭系数_气息不绝;

	public PlayerState_轻棍1()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1002);
		_力竭系数 = MyDataManager.F_Get_Player_招式系数(1001);
		_力竭系数_气息不绝 = _Data_Player_招式系数 * DSB_MonoSingleton<MyDataManager>.Instance._SOData_PlayerLevel.F_Geto_PlayerLevel_Buff(102)._每一级数值;
	}

	public override void OnEnter()
	{
		base._Ani.Play("轻攻击1");
		base._Rb2D.velocity = Vector2.zero;
		F_玩家转向(MyInputManager.Horizontal);
		base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * (6f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1202)), 0f);
		base._Mono组件._C_攻击判断._击退力度 = 6f;
		if (_Data._Curr气力 < _Data_Player_招式系数._气力消耗)
		{
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(102) > 0.1f)
			{
				F_创建攻击命中回调(_力竭系数_气息不绝);
			}
			else
			{
				F_创建攻击命中回调(_力竭系数);
			}
			EventArgs_Player状态提示.Trigger("气力不足!", _Data._Mono组件._TipsUI);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_力竭();
		}
		else
		{
			F_创建攻击命中回调();
		}
		float num = Mathf.Clamp(1f - DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1201), 0.3f, 1f);
		_Timer = _Data._Dict_AniName_time["轻攻击1"] * num;
		base._Ani.speed = 1f / num;
		_NextState = Player_StateName_Type.正常移动;
		_Data._State_攻击连段_判定Timer = _Timer + 0.3f;
		_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍2;
		_Data._重攻击衔接切手技 = true;
	}

	public override void F_攻击命中回调()
	{
		_Data._Curr蓄力 += _Data_Player_招式系数._棍势值 * (1f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1203));
	}

	public override void OnExit()
	{
		base._Ani.speed = 1f;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			if (_NextState == Player_StateName_Type.破棍式)
			{
				ChangeToState(Player_StateName_Type.破棍式);
				_Data._Curr豆子--;
			}
			else if (_NextState == Player_StateName_Type.轻棍2)
			{
				ChangeToState(Player_StateName_Type.轻棍2);
			}
			else if (_NextState == Player_StateName_Type.精魄)
			{
				ChangeToState(Player_StateName_Type.精魄);
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_精魄();
			}
			else
			{
				ChangeState(Player_StateName_Type.正常移动);
			}
		}
	}

	public override void F_轻攻击Down()
	{
		_NextState = Player_StateName_Type.轻棍2;
	}

	public override void F_精魄()
	{
		MyLog.Log("尝试释放>>>>>精魄");
		if (_Data._IsOk_精魄)
		{
			_NextState = Player_StateName_Type.精魄;
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_精魄();
		}
		else
		{
			MyLog.LogWarning("精魄 在冷却 或 未解锁");
		}
	}

	public override void F_重攻击Down()
	{
		if (_Data._Curr豆子 >= 1)
		{
			_NextState = Player_StateName_Type.破棍式;
		}
	}

	public override void F_翻滚()
	{
		ChangeState(Player_StateName_Type.翻滚);
	}
}
