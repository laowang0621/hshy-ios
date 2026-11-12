using DSB;
using UnityEngine;

public abstract class NPCState_同伴_站立和跟随 : NPCState_同伴
{
	public float _站立跟随距离 = 4f;

	public float _移动速度 = 10f;

	private float _Timer;

	private int 连续跟随但是不动次数;

	public override void OnEnter()
	{
		_Mono_同伴._Dir = Vector2.zero;
		_Timer = Random.Range(0.5f, 0.8f);
		_站立跟随距离 = _Mono_同伴._站立跟随距离;
		_移动速度 = _Mono_同伴._移动速度;
		连续跟随但是不动次数 = 0;
	}

	public virtual bool F_判断玩家状态并回血()
	{
		return false;
	}

	public virtual bool F_判断是否追击()
	{
		return false;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			if (F_判断玩家状态并回血())
			{
				return;
			}
			_Timer = Random.Range(0.5f, 1f);
			Vector2 vector = _Mono_同伴.transform.position;
			Vector2 vector2 = DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
			Vector2 normalized = (vector2 - vector).normalized;
			float num = Vector2.Distance(vector, vector2);
			if (num > _站立跟随距离)
			{
				Vector2 vector3 = normalized * Random.Range(1f, 3f);
				vector3.Rotate(Random.Range(-80f, 80f));
				Vector2 vector4 = vector2 + vector3;
				_Mono_同伴._Dir = (vector4 - vector2).normalized;
				连续跟随但是不动次数 = 0;
			}
			else
			{
				_Mono_同伴._Dir = Vector2.zero;
				if (连续跟随但是不动次数 > 1)
				{
					_Mono_同伴.F_玩家转向(normalized.x);
				}
				else
				{
					连续跟随但是不动次数++;
				}
			}
			if ((bool)Physics2D.Raycast(vector, normalized, num, 2048))
			{
				_Mono_同伴._连续移动撞墙次数++;
			}
			else
			{
				_Mono_同伴._连续移动撞墙次数 = 0;
			}
		}
		if (F_判断是否追击() && _Mono_同伴._视野范围有敌人)
		{
			_Mono_同伴.ChangeState(EnemyStateName_Type.追击);
		}
		else if (_Mono_同伴.F_PlayerMove(_Mono_同伴._Dir, _移动速度))
		{
			_Mono组件._Ani.Play("移动");
		}
		else
		{
			_Mono组件._Ani.Play("站立");
		}
	}
}
