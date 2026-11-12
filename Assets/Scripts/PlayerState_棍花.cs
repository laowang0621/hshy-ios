using DSB;
using UnityEngine;

public class PlayerState_棍花 : Player_StateBase
{
	private float _棍势值;

	public PlayerState_棍花()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1010);
	}

	public override void OnEnter()
	{
		F_创建攻击命中回调();
		_Data._Is免疫远程 = true;
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(101) > 0.5f)
		{
			base._Mono._m开启反弹弹道 = true;
		}
		_棍势值 = _Data_Player_招式系数._棍势值 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1101);
	}

	public override void F_攻击命中回调()
	{
		_Data._Curr蓄力 += _棍势值;
	}

	public override void OnExit()
	{
		base._Mono._m开启反弹弹道 = false;
		_Data._Is免疫远程 = false;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
	}

	public override void OnUpdate()
	{
		if (F_PlayerMove(4f))
		{
			base._Ani.Play("移动棍花_new2");
		}
		else
		{
			base._Ani.Play("棍花");
		}
		_Data._Curr气力 -= _Data_Player_招式系数._棍势值 * Time.deltaTime;
		if (_Data._Curr气力 <= 2f)
		{
			ChangeState(Player_StateName_Type.正常移动);
		}
	}

	public override void F_棍花Up()
	{
		ChangeState(Player_StateName_Type.正常移动);
	}
}
