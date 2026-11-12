using UnityEngine;

public class EnemyState_青蝠_站立 : EnemyState_狼剑客
{
	private EnemyAI_青蝠 _Mono青蝠;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono青蝠 = (EnemyAI_青蝠)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
		_Mono青蝠._MoveTimer += _Timer;
		base._Ani.SetBool("飞_高", _Mono青蝠._飞_高);
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
			ChangeState(EnemyStateName_Type.散步);
		}
	}
}
