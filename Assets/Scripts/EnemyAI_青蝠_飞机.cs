using DSB;
using UnityEngine;

public class EnemyAI_青蝠_飞机 : EnemyAI_狼剑客
{
	protected override void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_特殊场景._EventID, F_飞机大战);
		if (_Data._UI_HP != null)
		{
			Object.Destroy(_Data._UI_HP.gameObject);
		}
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_Data._UI_HP._可见性 = false;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_青蝠_飞机_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_青蝠_飞机_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_青蝠_飞机_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_青蝠_飞机_死亡().Init(_Data));
		_FSM.ChangeState(EnemyStateName_Type.站立);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_特殊场景._EventID, F_飞机大战);
	}

	public void F_飞机大战(DSB_EventArgs args)
	{
		if (!(args as EventArgs_特殊场景)._是进入)
		{
			_FSM.ChangeState(EnemyStateName_Type.死亡);
		}
	}
}
