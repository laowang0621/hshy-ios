using UnityEngine;

public class EnemyState_小呱呱_站立 : EnemyState_小呱呱
{
	private float _MaxTimer;

	private float _MaxTimer2;

	public override void OnEnter()
	{
		base._Ani.Play("站立");
		base._Rb2D.velocity = Vector2.zero;
		_MaxTimer = 0.3f + Random.value * 2.5f;
		_MaxTimer2 = Random.Range(0.25f, 0.45f);
		_Timer = 0f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer += Time.deltaTime;
		if (_Data._视野范围有敌人 && _Timer >= _MaxTimer2)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
		if (_Timer >= _MaxTimer)
		{
			ChangeState(EnemyStateName_Type.散步);
		}
	}
}
