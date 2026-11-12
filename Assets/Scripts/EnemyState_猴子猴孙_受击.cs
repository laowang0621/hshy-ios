using UnityEngine;

public class EnemyState_猴子猴孙_受击 : EnemyState_猴子猴孙
{
	public override void OnEnter()
	{
		base._Ani.Play("受击");
		base._Rb2D.velocity = new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized * base._Mono._BeAttack_击退力度;
		base._Mono._Player_蓄力特效.gameObject.SetActive(value: false);
		base._Mono.F_玩家转向(base._Mono._BeAttack_Pos.x - base._Mono.transform.position.x);
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
			ChangeState(EnemyStateName_Type.站立);
		}
	}
}
