using UnityEngine;

public class PlayerState_受击 : Player_StateBase
{
	public override void OnEnter()
	{
		if (_Data._本次硬直是虚弱动作)
		{
			base._Ani.Play("虚弱动作");
		}
		else
		{
			base._Ani.Play("受击");
		}
		base._Rb2D.drag = 5f;
		base._Rb2D.velocity = new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized * base._Mono._BeAttack_击退力度;
		base._Mono组件._Player_蓄力特效.gameObject.SetActive(value: false);
		F_玩家转向(base._Mono._BeAttack_Pos.x - base._Mono.transform.position.x);
		if (_Data._本次受击时长 <= 0.05f)
		{
			_Data._本次受击时长 = _Data._默认受击时长;
		}
		_Timer = _Data._本次受击时长;
		_NextState = Player_StateName_Type.正常移动;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}
}
