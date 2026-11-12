public class EnemyState_石双双_受击 : EnemyState_狼剑客_受击
{
	private EnemyAI_石双双 _Mono_石双双;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono_石双双 = (EnemyAI_石双双)data._Mono;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		_Mono_石双双._是伪装 = false;
		base.OnEnter();
	}
}
