using DSB;
using UnityEngine;

public class PlayerState_聚形散气_喝酒 : PlayerState_聚形散气_Base
{
	private float _Timer;

	public override void OnEnter()
	{
		_PlayerState_聚形散气._Rb2D.velocity = Vector2.zero;
		_PlayerState_聚形散气._Ani.Play("喝酒");
		_PlayerState_聚形散气._Data._Curr酒--;
		_PlayerState_聚形散气._Data._Curr生命 += _PlayerState_聚形散气._Data._Max生命 * _PlayerState_聚形散气._Data._葫芦_恢复;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_喝酒();
		_Timer = 0.55f;
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.正常移动);
		}
	}

	public override void OnExit()
	{
	}
}
