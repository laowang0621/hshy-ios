using UnityEngine;

public class NPCState_同伴_受击 : NPCState_同伴
{
	private float _Timer;

	public override void OnEnter()
	{
		_Mono组件._Ani.Play("受击");
		_Mono_同伴._Mono组件._Rb2D.velocity = new Vector2(_Mono_同伴.transform.position.x - _Mono_同伴._BeAttack_Pos.x, 0f).normalized * _Mono_同伴._BeAttack_击退力度;
		_Mono_同伴.F_玩家转向(_Mono_同伴._BeAttack_Pos.x - _Mono_同伴.transform.position.x);
		_Timer = 0.25f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_Mono_同伴.ChangeState(EnemyStateName_Type.站立);
		}
	}
}
