using UnityEngine;

public class EnemyState_Boss_站立 : EnemyState_Boss
{
	private float _1 = 0.7575777f;

	private float _2 = 1.8114514f;

	public int _AniID_站立 = Animator.StringToHash("站立");

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		F_计算攻击欲望();
		return this;
	}

	public void F_计算攻击欲望()
	{
		_1 = _Data._站立时间_x;
		_2 = _Data._站立时间_y;
	}

	public override void OnEnter()
	{
		base._Ani.Play(_AniID_站立);
		base._Rb2D.velocity = Vector2.zero;
		_Timer = Random.Range(_1, _2);
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
