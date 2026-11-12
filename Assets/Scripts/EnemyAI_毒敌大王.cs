using DSB;
using UnityEngine;

public class EnemyAI_毒敌大王 : EnemyAI_Boss
{
	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public Transform _Tsf_尾巴末端节点;

	public EnemyAI_毒敌大王_尾巴 _毒敌大王_尾巴;

	public LineRenderer _LR_尾巴_影子;

	public EdgeCollider2D _Edge_尾巴攻击区域;

	public Transform _Tsf_尾巴_3;

	public Transform _Tsf_尾巴_4;

	public Transform _Tsf_尾巴_5;

	public C_攻击判断 _C_毒液_Prefab;

	public override Enum_对话 _对话类型_见面 => Enum_对话.毒敌大王前置对话_碎罐后;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击(10f).Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_毒敌大王_攻击().Init(_Data));
	}

	public void F_Ani_Play_SFX_尾后针()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_尾后针();
	}
}
