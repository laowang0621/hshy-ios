using UnityEngine;

public class EnemyState_狼剑客_站立 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		base._Ani.Play("站立");
		base._Rb2D.velocity = Vector2.zero;
		_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
		if (!base._Mono._开启游荡模式)
		{
			base._Mono.transform.localScale = _Data._StartScale;
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
		if (_Timer <= 0f)
		{
			_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
			ChangeState(EnemyStateName_Type.散步);
		}
	}
}
