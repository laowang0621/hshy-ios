using UnityEngine;

public class EnemyState_狼剑客_受击 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		base._Ani.Play("受击");
		base._Rb2D.velocity = new Vector2((base._Mono.transform.position.x > base._Mono._BeAttack_Pos.x) ? 1 : (-1), 0f) * base._Mono._BeAttack_击退力度;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? num : (0f - num), num, num);
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
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}
}
