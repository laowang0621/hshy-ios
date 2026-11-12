using UnityEngine;

public class EnemyState_石蛛_站立 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		if (_Data._视野范围有敌人)
		{
			base._Ani.Play("站立");
		}
		else
		{
			base._Ani.Play("站立_石头");
		}
		base._Rb2D.velocity = Vector2.zero;
		_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f && _Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
	}
}
