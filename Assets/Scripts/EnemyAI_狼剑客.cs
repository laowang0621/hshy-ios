public class EnemyAI_狼剑客 : EnemyAI_Base<EnemyAI_狼剑客, EnemyState_狼剑客, Enemy_StateData_狼剑客>
{
	protected bool _已经进入初始化;

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
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_狼剑客_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_狼剑客_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
	}
}
