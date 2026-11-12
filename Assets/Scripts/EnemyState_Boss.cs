public class EnemyState_Boss : EnemyState_Base<EnemyAI_Boss, EnemyState_Boss, Enemy_StateData_Boss>
{
	protected EnemyAI_Boss _Mono_Boss;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_Mono_Boss = data._Mono;
		return this;
	}
}
