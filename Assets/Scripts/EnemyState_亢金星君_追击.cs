public class EnemyState_亢金星君_追击 : EnemyState_Boss_追击
{
	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_MoveSpeed = 5f;
		return base.Init(data);
	}
}
