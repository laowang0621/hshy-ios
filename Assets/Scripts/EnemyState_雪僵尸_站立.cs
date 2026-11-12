using DSB;
using UnityEngine;

public class EnemyState_雪僵尸_站立 : EnemyState_狼剑客
{
	private EnemyAI_雪僵尸 _Mono雪僵尸;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono雪僵尸 = (EnemyAI_雪僵尸)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		if (_Mono雪僵尸._已重置需静止)
		{
			_Mono雪僵尸.F_随机静止姿势();
			base._Mono组件._SR_Model.SetAlpha(0f);
			base._Rb2D.velocity = Vector2.zero;
		}
		else
		{
			_Mono雪僵尸.F_取消静止伪装();
			base._Ani.Play("站立");
			base._Rb2D.velocity = Vector2.zero;
			_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
		}
	}

	public override void OnExit()
	{
		_Mono雪僵尸._SR_静止.SetAlpha(0f);
		base._Mono组件._SR_Model.SetAlpha(1f);
		if (_Mono雪僵尸._可以修改_已重置需静止 && _Mono雪僵尸._已重置需静止)
		{
			_Mono雪僵尸._已重置需静止 = false;
		}
	}

	public override void OnUpdate()
	{
		if (_Mono雪僵尸._已重置需静止)
		{
			if (_Data._攻击范围有敌人)
			{
				ChangeState(EnemyStateName_Type.攻击);
			}
		}
		else if (_Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
	}
}
