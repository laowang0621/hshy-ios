using DSB;
using UnityEngine;

public class PlayerState_跳跃 : Player_StateBase
{
	private float _跳跃移动速度 = 5f;

	public PlayerState_跳跃()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1013);
	}

	public override void OnEnter()
	{
		_Data._Is腾空 = true;
		base._Ani.Play("跳跃");
		_Timer = _Data._Dict_AniName_time["跳跃"];
		_Data._Curr气力 -= _Data_Player_招式系数._气力消耗;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃_翻滚();
	}

	public override void OnExit()
	{
		_Data._Is腾空 = false;
	}

	public override void OnUpdate()
	{
		F_PlayerMove(_跳跃移动速度, can转向: false);
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			ChangeState(Player_StateName_Type.正常移动);
		}
	}

	public override void F_轻攻击Down()
	{
		ChangeToState(Player_StateName_Type.跳跃重击);
	}

	public override void F_重攻击Down()
	{
		ChangeToState(Player_StateName_Type.跳跃重击);
	}
}
