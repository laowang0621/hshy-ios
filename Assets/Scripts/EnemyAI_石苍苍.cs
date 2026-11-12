public class EnemyAI_石苍苍 : EnemyAI_石双双
{
	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经喷射 = true;
		_是伪装 = _设置_是伪装;
		_解除伪装后直接攻击 = _设置_是伪装;
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_石苍苍_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_石双双_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_石双双_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_石苍苍_攻击().Init(_Data));
	}

	public override void F_重置()
	{
		base.F_重置();
		_解除伪装后直接攻击 = _设置_是伪装;
	}
}
