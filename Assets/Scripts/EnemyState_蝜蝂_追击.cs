using DSB;
using UnityEngine;

public class EnemyState_蝜蝂_追击 : EnemyState_Boss
{
	private EnemyAI_蝜蝂 _Mono_蝜蝂;

	private int _震荡波次数_我来助你;

	private int _震荡波次数_攻击;

	private float _我来助你Timer;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_Mono_蝜蝂 = (EnemyAI_蝜蝂)data._Mono;
		_震荡波次数_我来助你 = 0;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.drag = 0f;
		_Mono_蝜蝂._易伤 = false;
		_Mono_蝜蝂._在移动 = true;
		_Mono_蝜蝂._MoveDir = ((Random.value < 0.5f) ? Vector2.right : Vector2.left).Rotate(Random.Range(30, 60) * ((Random.value < 0.5f) ? 1 : (-1)));
		base._Rb2D.velocity = _Mono_蝜蝂._MoveDir * _Mono_蝜蝂._MoveSpeed;
		_Data._Mono.transform.localScale = new Vector3((_Mono_蝜蝂._MoveDir.x < 0f) ? 1 : (-1), 1f, 1f);
		_Timer = Random.Range(3f, 5f);
	}

	private void F_震荡波()
	{
		if (_震荡波次数_我来助你 >= Random.Range(3, 5))
		{
			_震荡波次数_我来助你 = 0;
			Vector2 vector = DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
			Vector2 vector2 = base._Mono.transform.position;
			Vector2 vector3 = (vector - vector2).normalized.Rotate(Random.Range(-60, 60));
			float num = Random.Range(6f, 9f);
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, vector3, num, 2048);
			if ((bool)raycastHit2D)
			{
				_Mono_蝜蝂._Tsf_我来助你.position = raycastHit2D.point;
			}
			else
			{
				_Mono_蝜蝂._Tsf_我来助你.position = vector + vector3 * num;
			}
			_Mono_蝜蝂._Tsf_我来助你.localScale = new Vector3((vector.x < vector2.x) ? 1 : (-1), 1f, 1f);
			_Mono_蝜蝂._Ani_我来助你.Play("我来助你");
			_我来助你Timer = 0.7f;
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_我来助你();
		}
		else if (_震荡波次数_攻击 >= Random.Range(4, 7))
		{
			_震荡波次数_攻击 = 0;
			ChangeState(EnemyStateName_Type.攻击);
		}
		else
		{
			_震荡波次数_我来助你++;
			_震荡波次数_攻击++;
			base._Ani.SetTrigger("震荡波");
		}
	}

	public override void OnUpdate()
	{
		base._Rb2D.velocity = _Mono_蝜蝂._MoveDir * _Mono_蝜蝂._MoveSpeed;
		if (_我来助你Timer > 0f)
		{
			_我来助你Timer -= Time.deltaTime;
			if (_我来助你Timer <= 0f)
			{
				_Mono_蝜蝂.BeAttack(new AttackInfo(_Data._MaxHP * 0.1f / 10f));
				ChangeState(EnemyStateName_Type.受击);
			}
		}
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_Timer = Random.Range(3f, 5f);
			F_震荡波();
		}
	}

	public override void OnExit()
	{
		_Mono_蝜蝂._在移动 = false;
	}
}
