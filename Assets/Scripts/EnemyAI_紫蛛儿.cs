using DSB;
using UnityEngine;

public class EnemyAI_紫蛛儿 : EnemyAI_蜘蛛二姐
{
	public Transform _Tsf_眼睛;

	public EnemyAI_紫蛛儿_眼睛 _EnemyAI_紫蛛儿_眼睛;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_蜘蛛二姐_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_紫蛛儿_攻击().Init(_Data));
		_GO_蜘蛛旋转特效.SetActive(value: false);
		_EnemyAI_紫蛛儿_眼睛.transform.parent = null;
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.紫蛛儿, 是进入: true);
		base._Boss死亡 += delegate
		{
			EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.紫蛛儿, 是进入: false);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.7114514f, delegate
			{
				DSB_MonoSingleton<UIManager>.Instance.F_触发对话(Enum_对话.紫蛛儿BOSS战战胜对话);
			});
		};
	}

	private void OnDisable()
	{
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.紫蛛儿, 是进入: false);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (_EnemyAI_紫蛛儿_眼睛 != null)
		{
			Object.Destroy(_EnemyAI_紫蛛儿_眼睛.gameObject);
		}
	}

	protected override void Update()
	{
		base.Update();
		_EnemyAI_紫蛛儿_眼睛.F_同步位置(_Tsf_眼睛.position);
	}

	public void F_Ani_启动_眼睛()
	{
		_EnemyAI_紫蛛儿_眼睛.F_启动();
	}

	public void F_Ani_关闭_眼睛()
	{
		_EnemyAI_紫蛛儿_眼睛.F_关闭();
	}

	public void F_Ani_强制关闭_眼睛()
	{
		_EnemyAI_紫蛛儿_眼睛.F_强制关闭();
	}
}
