using DSB;
using UnityEngine;

public class EnemyState_猴子猴孙_站立 : EnemyState_猴子猴孙
{
	public float _跟随距离 = 4f;

	public override void OnEnter()
	{
		_Data._Dir = Vector2.zero;
		_Timer = Random.Range(0.5f, 0.8f);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (_Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
			return;
		}
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_Timer = Random.Range(0.5f, 1f);
			Vector2 vector = base._Mono.transform.position;
			Vector2 vector2 = _Data._PC.transform.position;
			Vector2 normalized = (vector2 - vector).normalized;
			float num = Vector2.Distance(vector, vector2);
			if (num > _跟随距离)
			{
				Vector2 vector3 = normalized * Random.Range(1f, 3f);
				vector3.Rotate(Random.Range(-80f, 80f));
				Vector2 vector4 = vector2 + vector3;
				_Data._Dir = (vector4 - vector2).normalized;
			}
			else
			{
				_Data._Dir = Vector2.zero;
			}
			if (num < 5f)
			{
				base._Mono._连续移动撞墙次数 = 0f;
			}
			else if ((bool)Physics2D.Raycast(vector, normalized, num, 2048))
			{
				base._Mono._连续移动撞墙次数++;
			}
			else
			{
				base._Mono._连续移动撞墙次数 = 0f;
			}
		}
		if (base._Mono.F_PlayerMove(_Data._Dir, 10f))
		{
			base._Ani.Play("跑步");
		}
		else
		{
			base._Ani.Play("站立");
		}
	}
}
