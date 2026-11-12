using UnityEngine;

public class PlayerState_打坐 : Player_StateBase
{
	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("打坐");
		_Timer = 1.35f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			ChangeState(Player_StateName_Type.正常移动);
		}
	}
}
