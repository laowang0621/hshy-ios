using UnityEngine;

public class EnemyState_石双双_站立 : EnemyState_狼剑客
{
	private EnemyAI_石双双 _Mono_石双双;

	private bool _伪装中;

	private bool _解除伪装中;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono_石双双 = (EnemyAI_石双双)data._Mono;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (_Mono_石双双._是伪装)
		{
			_伪装中 = true;
			_解除伪装中 = false;
			base._Ani.Play("伪装");
		}
		else
		{
			_伪装中 = false;
			base._Ani.Play("站立");
			_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (_解除伪装中)
		{
			_Timer -= Time.deltaTime;
			if (_Timer <= 0f)
			{
				_解除伪装中 = false;
				if (_Mono_石双双._解除伪装后直接攻击)
				{
					ChangeState(EnemyStateName_Type.攻击);
				}
				else
				{
					ChangeState(EnemyStateName_Type.追击);
				}
			}
		}
		else if (_伪装中)
		{
			if (_Data._视野范围有敌人)
			{
				_解除伪装中 = true;
				_Mono_石双双._是伪装 = false;
				base._Ani.Play("解除伪装");
				_Timer = _Data._Dict_Anis_name_time["解除伪装"];
			}
		}
		else
		{
			_Timer -= Time.deltaTime;
			if (_Data._视野范围有敌人)
			{
				ChangeState(EnemyStateName_Type.追击);
			}
			if (_Timer <= 0f)
			{
				ChangeState(EnemyStateName_Type.散步);
			}
		}
	}
}
