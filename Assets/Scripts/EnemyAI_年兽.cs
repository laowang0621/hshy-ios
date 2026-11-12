public class EnemyAI_年兽 : EnemyAI_Boss
{
	public override float _受击僵直时长 => 2.85f;

	public override Enum_对话 _对话类型_见面 => Enum_对话.NULL;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.NULL;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_年兽_攻击().Init(_Data));
	}

	public override void F_播放BossBGM()
	{
	}

	public override void F_关闭BossBGM()
	{
	}
}
