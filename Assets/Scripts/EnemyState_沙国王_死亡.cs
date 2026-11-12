using DSB;

public class EnemyState_沙国王_死亡 : EnemyState_沙国王
{
	public override void OnEnter()
	{
		DSB_MonoSingleton<MyDataManager>.Instance.F_击败Boss((int)base._Mono组件._EnemyID);
		_Mono_Boss.F_Action_Boss死亡();
		_Data._Is死亡 = true;
		_Data.F_设置UI血量(1f, 1f, _可见性: false);
		base._Mono组件.F_死亡(base._Mono._BeAttack_Pos);
		((EnemyAI_沙国王)base._Mono).F_注销_沙二郎死亡();
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
