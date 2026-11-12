using DSB;
using UnityEngine;

public class EnemyState_蝜蝂_攻击 : EnemyState_Boss
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
		base._Rb2D.drag = 0f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Ani.Play("跳跃");
		_Timer = _Data._Dict_Anis_name_time["跳跃"];
		_Mono_蝜蝂._MoveDir = (DSB_MonoSingleton<GameManager>.Instance._PC.transform.position - base._Mono.transform.position).normalized;
		base._Rb2D.velocity = _Mono_蝜蝂._MoveDir * _Mono_蝜蝂._MoveSpeed2;
		_Data._Mono.transform.localScale = new Vector3((_Mono_蝜蝂._MoveDir.x < 0f) ? 1 : (-1), 1f, 1f);
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
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}
}
