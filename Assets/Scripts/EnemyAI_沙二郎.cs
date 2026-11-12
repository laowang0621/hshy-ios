using UnityEngine;

public class EnemyAI_沙二郎 : EnemyAI_Boss
{
	public EnemyAI_沙国王 _EnemyAI_沙国王Prefab;

	private GameObject bossGO;

	public override Enum_对话 _对话类型_见面 => Enum_对话.沙国王前置对话;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_沙二郎_攻击().Init(_Data));
	}

	public override void Init()
	{
		base.Init();
		EnemyAI_沙国王 沙国王 = Object.Instantiate(_EnemyAI_沙国王Prefab, base.transform.position + new Vector3(4f, 0.5f, 0f), Quaternion.identity);
		沙国王.transform.localScale = new Vector3(-1f, 1f, 1f);
		沙国王._Boss死亡 += F_沙国王死亡;
		bossGO = 沙国王.gameObject;
		base._Boss死亡 += EventArgs_沙二郎死亡.Trigger;
		base._Boss死亡 += delegate
		{
			沙国王._Boss死亡 -= F_沙国王死亡;
		};
	}

	private void F_沙国王死亡()
	{
		_Data._站立时间_x = 0.3f;
		_Data._站立时间_y = 0.5f;
		_Data._追击时间_x = 0.4f;
		_Data._追击时间_y = 0.8f;
		if (_FSM.states.TryGetValue(EnemyStateName_Type.站立, out var value))
		{
			((EnemyState_Boss_站立)value).F_计算攻击欲望();
		}
		if (_FSM.states.TryGetValue(EnemyStateName_Type.追击, out var value2))
		{
			((EnemyState_Boss_追击)value2).F_计算攻击欲望();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (bossGO != null)
		{
			Object.Destroy(bossGO);
			bossGO = null;
		}
	}
}
