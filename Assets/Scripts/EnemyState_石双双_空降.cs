using UnityEngine;

public class EnemyState_石双双_空降 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		base._Ani.Play("空降");
		_Timer = _Data._Dict_Anis_name_time["空降"];
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
