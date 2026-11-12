using UnityEngine;

public class EnemyState_宝箱蟹_受击 : EnemyState_宝箱蟹
{
	public override void OnEnter()
	{
		base._Ani.Play("受击");
		base._Rb2D.velocity = new Vector2((base._Mono.transform.position.x > base._Mono._BeAttack_Pos.x) ? 1 : (-1), 0f) * base._Mono._BeAttack_击退力度;
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		_Timer = 0.3f;
	}

	public override void OnExit()
	{
		base._Mono.F_朝向玩家();
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.攻击);
		}
	}
}
