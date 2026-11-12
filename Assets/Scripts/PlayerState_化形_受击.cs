using UnityEngine;

public class PlayerState_化形_受击 : PlayerState_化形_Base
{
	public override void OnEnter()
	{
		_Data._Mono组件_化形._Ani.Play("受击");
		_Data._Rb2D.velocity = 1f * _Data._MonoPlayer._BeAttack_击退力度 * new Vector2(_Data._MonoPlayer.transform.position.x - _Data._MonoPlayer._BeAttack_Pos.x, 0f).normalized;
		_Data._MonoPlayer.F_玩家转向(_Data._MonoPlayer._BeAttack_Pos.x - _Data._MonoPlayer.transform.position.x);
		_Timer = 0.3f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			F_化形_ChangeState(TypeName_化形.正常移动);
		}
	}
}
