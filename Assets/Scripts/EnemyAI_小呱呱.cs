using DSB;

public class EnemyAI_小呱呱 : EnemyAI_Base<EnemyAI_小呱呱, EnemyState_小呱呱, Enemy_StateData_小呱呱>
{
	private bool _已经进入初始化;

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_小呱呱_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_小呱呱_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_小呱呱_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_小呱呱_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_小呱呱_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_小呱呱_死亡().Init(_Data));
	}

	public void F_Ani_Play_AudioC_小呱呱攻击()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小呱呱攻击();
	}
}
