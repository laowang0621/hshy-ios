using UnityEngine;

public class EnemyAI_雷长老 : EnemyAI_狼弓手
{
	public C_弹道 _雷电球Prefab;

	public C_攻击判断 _落雷Prefab;

	public Transform _Tsf_法球;

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_狼弓手_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_雷长老_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼弓手_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼弓手_死亡().Init(_Data));
	}
}
