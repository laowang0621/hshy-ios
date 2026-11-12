using UnityEngine;

public class EnemyAI_蜘蛛二姐 : EnemyAI_Boss
{
	public C_蛇司药毒瓶 _蛇司药毒瓶;

	public Transform _Tsf_手;

	public GameObject _GO_落地扬尘_Prefab;

	public GameObject _GO_蜘蛛旋转特效;

	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_蜘蛛二姐_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_蜘蛛二姐_攻击().Init(_Data));
		_GO_蜘蛛旋转特效.SetActive(value: false);
	}
}
