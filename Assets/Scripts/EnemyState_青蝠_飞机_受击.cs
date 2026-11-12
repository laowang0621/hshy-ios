using UnityEngine;

public class EnemyState_青蝠_飞机_受击 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		base._Ani.Play("受击");
		base._Mono.transform.localScale = new Vector3(-1f, 1f, 1f);
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
