public class NPCAI_猪八戒 : NPCAI_同伴
{
	protected override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new NPCState_猪八戒_站立和跟随().Init(this));
		_FSM.AddState(EnemyStateName_Type.追击, new NPCState_同伴_追击().Init(this));
		_FSM.AddState(EnemyStateName_Type.受击, new NPCState_同伴_受击().Init(this));
		_FSM.AddState(EnemyStateName_Type.攻击, new NPCState_猪八戒_攻击().Init(this));
		_FSM.AddState(EnemyStateName_Type.死亡, new NPCState_同伴_死亡().Init(this));
	}
}
