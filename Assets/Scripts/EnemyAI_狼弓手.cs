public class EnemyAI_狼弓手 : EnemyAI_Base<EnemyAI_狼弓手, EnemyState_狼弓手, Enemy_StateData_狼弓手>
{
	protected bool _已经进入初始化;

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
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_狼弓手_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼弓手_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼弓手_死亡().Init(_Data));
		ChangeState(EnemyStateName_Type.站立);
		_Data.F_隐藏();
		_Data.F_显示();
	}
}
