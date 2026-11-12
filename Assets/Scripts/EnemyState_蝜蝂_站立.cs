using UnityEngine;

public class EnemyState_蝜蝂_站立 : EnemyState_Boss
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
		base._Rb2D.velocity = Vector2.zero;
		_Timer = Random.Range(1f, 3f);
		_Mono_蝜蝂._易伤 = false;
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
	}
}
