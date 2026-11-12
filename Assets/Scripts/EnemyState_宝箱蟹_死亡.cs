using DSB;

public class EnemyState_宝箱蟹_死亡 : EnemyState_宝箱蟹
{
	public override void OnEnter()
	{
		DSB_MonoSingleton<MyDataManager>.Instance.F_打开宝箱_新(base._Mono._宝箱蟹ID);
		_Data._Is死亡 = true;
		_Data._UI_HP._可见性 = false;
		base._Mono组件.F_死亡(base._Mono._BeAttack_Pos);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
