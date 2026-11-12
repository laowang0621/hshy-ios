public class EnemyAI_双刀僧 : EnemyAI_狼剑客
{
	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_狼剑客_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_石苍苍_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_双刀僧_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
	}
}
