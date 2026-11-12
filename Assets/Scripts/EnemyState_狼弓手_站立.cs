using UnityEngine;

public class EnemyState_狼弓手_站立 : EnemyState_狼弓手
{
	private float _1 = 0.5f;

	private float _2 = 3.5f;

	private float _攻击间隔;

	public override EnemyState_狼弓手 Init(Enemy_StateData_狼弓手 data)
	{
		_攻击间隔 = 0f;
		base.Init(data);
		_1 = _Data._追击时间_x;
		_2 = _Data._追击时间_y;
		return this;
	}

	public override void OnEnter()
	{
		base._Ani.Play("站立");
		_Timer = 0.2f;
		_攻击间隔 = Random.Range(_1, _2);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_攻击间隔 > 0f)
		{
			_攻击间隔 -= Time.deltaTime;
		}
		if (!(_Timer <= 0f))
		{
			return;
		}
		if (_Data._视野范围有敌人)
		{
			float num = Mathf.Abs(base._Mono.transform.localScale.x);
			base._Mono.transform.localScale = new Vector3((_Data._Pos_追击目标位置.x > base._Mono.transform.position.x) ? num : (0f - num), num, num);
			if (_攻击间隔 <= 0f)
			{
				_攻击间隔 = Random.Range(_1, _2);
				ChangeState(EnemyStateName_Type.攻击);
			}
		}
		else
		{
			ChangeState(EnemyStateName_Type.站立);
		}
	}
}
