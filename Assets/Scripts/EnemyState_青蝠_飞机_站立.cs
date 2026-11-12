using UnityEngine;

public class EnemyState_青蝠_飞机_站立 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		base._Ani.Play("飞_低_悬浮");
		base._Rb2D.velocity = Vector2.zero;
		_Timer = Random.Range(_Data._追击时间_x, _Data._追击时间_y);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			ChangeState(EnemyStateName_Type.攻击);
		}
	}
}
