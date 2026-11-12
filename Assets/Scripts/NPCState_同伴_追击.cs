using DSB;
using UnityEngine;

public class NPCState_同伴_追击 : NPCState_同伴
{
	public float _跟随距离 = 2f;

	public float _移动速度 = 10f;

	private float _Timer;

	public override void OnEnter()
	{
		_Timer = Random.Range(0.5f, 0.8f);
		_跟随距离 = _Mono_同伴._跟随距离;
		_移动速度 = _Mono_同伴._移动速度;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (!_Mono_同伴._视野范围有敌人)
		{
			_Mono_同伴.F_强制清空追击();
			_Mono_同伴.ChangeState(EnemyStateName_Type.站立);
			return;
		}
		if (_Timer <= 0f)
		{
			if (Random.value < 0.4f)
			{
				_Mono_同伴.ChangeState(EnemyStateName_Type.攻击);
				return;
			}
			_Timer = Random.Range(0.4f, 0.7f);
			Vector2 vector = _Mono_同伴.transform.position;
			Vector2 vector2 = _Mono_同伴._Pos_追击目标位置;
			Vector2 normalized = (vector2 - vector).normalized;
			float num = Vector2.Distance(vector, vector2);
			if (num > _跟随距离)
			{
				Vector2 vector3 = normalized * Random.Range(_Mono_同伴._跟随位置偏移.x, _Mono_同伴._跟随位置偏移.y);
				vector3.Rotate(Random.Range(-60f, 60f));
				Vector2 vector4 = vector2 + vector3;
				_Mono_同伴._Dir = (vector4 - vector2).normalized;
			}
			else
			{
				_Mono_同伴._Dir = Vector2.zero;
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
		if (_Mono_同伴.F_PlayerMove(_Mono_同伴._Dir, _移动速度))
		{
			_Mono组件._Ani.Play("移动");
		}
		else
		{
			_Mono组件._Ani.Play("站立");
		}
	}
}
