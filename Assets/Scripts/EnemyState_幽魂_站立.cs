using UnityEngine;

public class EnemyState_幽魂_站立 : EnemyState_Boss
{
	public override void OnEnter()
	{
		base._Ani.Play("站立");
		base._Rb2D.velocity = Vector2.zero;
		if (_Data._视野范围有敌人)
		{
			_Timer = Random.Range(0.4f, 0.6f);
		}
		else
		{
			_Timer = Random.Range(1.5f, 2f);
		}
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
				ChangeState(EnemyStateName_Type.散步);
			}
		}
	}
}
