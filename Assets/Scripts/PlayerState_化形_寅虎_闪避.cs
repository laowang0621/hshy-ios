public class PlayerState_化形_寅虎_闪避 : PlayerState_化形_闪避
{
	public override void F_轻攻击()
	{
		_NextState = TypeName_化形.轻攻击;
	}

	public override void F_跳跃Down()
	{
		_NextState = TypeName_化形.寅虎格挡;
	}

	public override void F_重攻击Down()
	{
		_NextState = TypeName_化形.重攻击;
	}
}
