using DSB;
using UnityEngine;

public class PlayerState_喝酒 : Player_StateBase
{
	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("喝酒");
		_Timer = 0.6f;
		_NextState = Player_StateName_Type.正常移动;
		_Data._Curr酒--;
		_Data._Curr生命 += _Data._Max生命 * _Data._葫芦_恢复;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_喝酒();
	}

	public override void OnExit()
	{
	}
}
