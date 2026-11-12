public class EnemyAI_大呱呱1 : EnemyAI_Boss
{
	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_大呱呱_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_大呱呱1_攻击().Init(_Data));
	}
}
