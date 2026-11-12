using DSB;
using UnityEngine;

public class PlayerState_聚形散气_翻滚 : PlayerState_聚形散气_Base
{
	private float _EnterDrag;

	private float _Timer;

	public override void OnEnter()
	{
		_EnterDrag = _PlayerState_聚形散气._EnterDrag;
		_PlayerState_聚形散气._Rb2D.drag = 0f;
		_PlayerState_聚形散气._Rb2D.velocity = Vector2.zero;
		_PlayerState_聚形散气._Ani.Play("翻滚");
		if (_PlayerState_聚形散气._Data._Dir == Vector2.zero)
		{
			_PlayerState_聚形散气._Rb2D.velocity = new Vector2((float)(_PlayerState_聚形散气._Data._朝向右边 ? 1 : (-1)) * _PlayerState_聚形散气._翻滚速度, 0f);
		}
		else
		{
			_PlayerState_聚形散气._Rb2D.velocity = _PlayerState_聚形散气._Data._Dir * _PlayerState_聚形散气._翻滚速度;
		}
		_PlayerState_聚形散气._Data._可以受击 = false;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃_翻滚();
		_Timer = 0.315f;
		_PlayerState_聚形散气._Data._Curr气力 -= _PlayerState_聚形散气._Data_Player_招式系数_翻滚._气力消耗;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
	}

	public override void OnExit()
	{
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		_PlayerState_聚形散气._Rb2D.drag = _EnterDrag;
		_PlayerState_聚形散气._Data._可以受击 = true;
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.正常移动);
		}
	}
}
