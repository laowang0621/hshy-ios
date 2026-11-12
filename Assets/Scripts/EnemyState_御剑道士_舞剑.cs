using UnityEngine;

public class EnemyState_御剑道士_舞剑 : EnemyState_狼剑客
{
	private EnemyAI_御剑道士 _Mono_御剑道士;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono_御剑道士 = data._Mono as EnemyAI_御剑道士;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Ani.Play("攻击_踢砍刺", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		_Timer = 1.66f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			if (_Data._视野范围有敌人)
			{
				ChangeState(EnemyStateName_Type.追击);
			}
			else
			{
				ChangeState(EnemyStateName_Type.站立);
			}
		}
	}
}
