using UnityEngine;

public class EnemyStat_寅虎_追击 : EnemyState_Boss
{
	private EnemyAI_寅虎 _Mono_寅虎;

	private bool _是第一次 = true;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_寅虎 = (EnemyAI_寅虎)data._Mono;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		if (_是第一次)
		{
			_是第一次 = false;
			_Timer = 3.5f;
			_Mono_寅虎._可以磐石 = true;
			return;
		}
		_Timer = Random.Range(_Data._追击时间_x, _Data._追击时间_y);
		if (Random.value < 0.5f)
		{
			_Mono_寅虎._可以磐石 = true;
			_Timer += 0.66f;
		}
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer < 0f)
		{
			_Mono_寅虎._可以磐石 = false;
			base._Mono.ChangeState(EnemyStateName_Type.攻击);
		}
	}

	public override void OnExit()
	{
		_Timer = 5f;
	}
}
