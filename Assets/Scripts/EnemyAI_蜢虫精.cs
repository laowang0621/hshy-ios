using UnityEngine;

public class EnemyAI_蜢虫精 : EnemyAI_狼剑客
{
	public bool _需要隐藏 = true;

	[HideInInspector]
	public bool _茧状态;

	public float _追击速度 = 6f;

	[HideInInspector]
	public bool _下次攻击_破茧攻击;

	public override void BeAttack(AttackInfo attackInfo)
	{
		base.BeAttack(attackInfo);
		if (_Data._Curr生命 > 0f && _茧状态)
		{
			F_取消茧状态();
		}
	}

	public override void F_重置()
	{
		base.F_重置();
		_茧状态 = _需要隐藏;
		_下次攻击_破茧攻击 = false;
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_茧状态 = _需要隐藏;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_蜢虫精_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data));
		EnemyState_狼剑客 enemyState_狼剑客 = new EnemyState_石苍苍_追击().Init(_Data);
		(enemyState_狼剑客 as EnemyState_石苍苍_追击)._Speed = _追击速度;
		_FSM.AddState(EnemyStateName_Type.追击, enemyState_狼剑客);
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_蜢虫精_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
	}

	public void F_取消茧状态()
	{
		_茧状态 = false;
		_下次攻击_破茧攻击 = true;
		ChangeState(EnemyStateName_Type.攻击);
	}
}
