using DSB;

public class EnemyAI_广智 : EnemyAI_Boss
{
	public override Enum_对话 _对话类型_见面 => Enum_对话.广智前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.击败广智对话;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_广智_攻击().Init(_Data));
	}

	public override void F_播放BossBGM()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_广智BGM();
	}
}
