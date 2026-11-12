using DSB;
using UnityEngine;

public class NPCAI_四姐 : NPCAI_同伴
{
	[HideInInspector]
	public float _回血术Timer = 5f;

	protected override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new NPCState_四姐_站立和跟随().Init(this));
		_FSM.AddState(EnemyStateName_Type.受击, new NPCState_同伴_受击().Init(this));
		_FSM.AddState(EnemyStateName_Type.飞升, new NPCState_四姐_回血().Init(this));
		_FSM.AddState(EnemyStateName_Type.死亡, new NPCState_同伴_死亡().Init(this));
	}

	public override void F_EventArgs_Player重生(DSB_EventArgs args)
	{
		base.F_EventArgs_Player重生(args);
		_回血术Timer = 3f;
	}

	protected override void Update()
	{
		if (_回血术Timer > 0f)
		{
			_回血术Timer -= Time.deltaTime;
		}
		base.Update();
	}
}
