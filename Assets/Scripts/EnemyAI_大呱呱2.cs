using UnityEngine;

public class EnemyAI_大呱呱2 : EnemyAI_Boss
{
	public Transform _Tsf_嘴巴;

	public C_攻击判断 _落雷Prefab;

	public C_弹道_弓箭 _小闪电Prefab;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_大呱呱_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_大呱呱2_攻击().Init(_Data));
	}
}
