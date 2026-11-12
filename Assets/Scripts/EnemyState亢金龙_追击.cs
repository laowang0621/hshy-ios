using DSB;

public class EnemyState亢金龙_追击 : EnemyState_Boss_追击
{
	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_MoveSpeed = 18f;
		return this;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话(3002))
		{
			DSB_MonoSingleton<UIManager>.Instance.F_触发对话(Enum_对话.亢金龙前置对话);
		}
	}
}
