public class EnemyAI_幽魂 : EnemyAI_Boss
{
	public override Enum_对话 _对话类型_死亡 => Enum_对话.击败幽魂;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_幽魂_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_幽魂_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_幽魂_攻击().Init(_Data));
	}
}
