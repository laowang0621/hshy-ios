public class EnemyState_狼弓手_死亡 : EnemyState_狼弓手
{
	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		_Data.F_设置UI血量(1f, 1f, _可见性: false);
		base._Mono组件.F_死亡(base._Mono._BeAttack_Pos);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
