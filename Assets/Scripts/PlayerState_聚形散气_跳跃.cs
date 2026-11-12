using DSB;
using UnityEngine;

public class PlayerState_聚形散气_跳跃 : PlayerState_聚形散气_Base
{
	private float _跳跃移动速度 = 5f;

	private float _Timer;

	public override void OnEnter()
	{
		_PlayerState_聚形散气._Data._Is腾空 = true;
		_PlayerState_聚形散气._Ani.Play("跳跃");
		_Timer = _PlayerState_聚形散气._Data._Dict_AniName_time["跳跃"];
		_PlayerState_聚形散气._Data._Curr气力 -= _PlayerState_聚形散气._Data_Player_招式系数_跳跃._气力消耗;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃_翻滚();
	}

	public override void OnExit()
	{
		_PlayerState_聚形散气._Data._Is腾空 = false;
	}

	public override void OnUpdate()
	{
		_PlayerState_聚形散气.F_PlayerMove(_跳跃移动速度, can转向: false);
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.正常移动);
		}
	}
}
