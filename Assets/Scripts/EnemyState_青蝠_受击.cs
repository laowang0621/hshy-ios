public class EnemyState_青蝠_受击 : EnemyState_狼剑客_受击
{
	public override void OnExit()
	{
		base._Ani.Play("飞_低");
	}
}
