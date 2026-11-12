using UnityEngine;

public class EnemyState_蝜蝂_受击 : EnemyState_Boss
{
	private EnemyAI_蝜蝂 _Mono_蝜蝂;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_Mono_蝜蝂 = (EnemyAI_蝜蝂)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.drag = 5f;
		_Mono_蝜蝂._易伤 = true;
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.SetTrigger("受击");
		_Timer = Random.Range(7f, 10f);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			ChangeState(EnemyStateName_Type.站立);
		}
	}

	public override void OnExit()
	{
		_Mono_蝜蝂._易伤 = false;
		if (_Data._Curr生命 > 0f)
		{
			base._Ani.SetTrigger("受击结束");
		}
	}
}
