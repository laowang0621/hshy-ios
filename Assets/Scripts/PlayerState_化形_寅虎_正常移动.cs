public class PlayerState_化形_寅虎_正常移动 : PlayerState_化形_正常移动
{
	public override void F_跳跃Down()
	{
		F_化形_ChangeState(TypeName_化形.寅虎格挡);
	}
}
