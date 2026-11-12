using UnityEngine;

public class EnemyState_石苍苍_追击 : EnemyState_狼剑客_追击
{
	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _DirSpeed, Time.deltaTime * 10.9f);
		if (_Timer < 0f)
		{
			if (F_判断出手())
			{
				base._Mono.ChangeState(EnemyStateName_Type.攻击);
			}
			else if (_脱离视野后追击Time > 0f)
			{
				base._Mono.ChangeState(EnemyStateName_Type.追击);
			}
			else
			{
				base._Mono.ChangeState(EnemyStateName_Type.站立);
			}
		}
	}
}
