using DSB;
using UnityEngine;

public class EnemyAI_御剑道士 : EnemyAI_狼剑客
{
	public bool _开启音效 = true;

	public int _舞剑ID = 1;

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_狼剑客_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.飞升, new EnemyState_御剑道士_舞剑().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_狼剑客_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_御剑道士_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
	}

	public void F_Ani_御剑道士_Play_挥砍()
	{
		if (_开启音效)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_挥砍();
		}
	}

	public void F_Ani_御剑道士_Play_大风车()
	{
		if (_开启音效)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大风车();
		}
	}

	public bool F_是否回归原点()
	{
		if (_Data._Is死亡)
		{
			return false;
		}
		if (_FSM.currStateName == EnemyStateName_Type.飞升)
		{
			return false;
		}
		bool num = _Data._StartScale.x > 0f;
		bool flag = base.transform.localScale.x > 0f;
		if (num == flag && Vector2.Distance(_Data._StartPos, base.transform.position) < 0.8f)
		{
			return true;
		}
		return false;
	}

	public void F_舞剑(int 舞剑ID)
	{
		_舞剑ID = 舞剑ID;
		ChangeState(EnemyStateName_Type.飞升);
	}
}
