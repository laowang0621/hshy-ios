using UnityEngine;

public class EnemyAI_大呱呱4 : EnemyAI_Boss
{
	public Transform _Tsf_嘴巴;

	public Transform _Tsf_毒液随机生成范围;

	public C_蛇司药毒瓶 _蛇司药毒瓶;

	public C_弹道_弓箭 _激光_Prefab;

	private Vector2 _毒液生成位置中心;

	private Vector2 _毒液生成位置大小;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_大呱呱_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_大呱呱4_攻击().Init(_Data));
		_Tsf_毒液随机生成范围.parent = null;
		_毒液生成位置中心 = _Tsf_毒液随机生成范围.position;
		_毒液生成位置大小 = _Tsf_毒液随机生成范围.localScale / 2f;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (_Tsf_毒液随机生成范围 != null)
		{
			Object.Destroy(_Tsf_毒液随机生成范围.gameObject);
		}
	}

	public Vector2 F_获取随机生成毒液位置()
	{
		return new Vector2(_毒液生成位置中心.x + Random.Range(0f - _毒液生成位置大小.x, _毒液生成位置大小.x), _毒液生成位置中心.y + Random.Range(0f - _毒液生成位置大小.y, _毒液生成位置大小.y));
	}
}
