public class EnemyState_沙国王 : EnemyState_Boss
{
	protected EnemyAI_沙国王 _mMono_沙国王;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_mMono_沙国王 = (EnemyAI_沙国王)data._Mono;
		return base.Init(data);
	}
}
