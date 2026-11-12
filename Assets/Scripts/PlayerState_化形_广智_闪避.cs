public class PlayerState_化形_广智_闪避 : PlayerState_化形_闪避
{
	public override void F_轻攻击()
	{
		if (_Data._Can大招)
		{
			_Data._重击选项 = Enum_化形_重攻击.大风车;
			F_化形_ChangeState(TypeName_化形.重攻击);
		}
	}

	public override void F_重攻击Down()
	{
		if (_Data._Can大招)
		{
			_Data._重击选项 = Enum_化形_重攻击.撑杆跳;
			F_化形_ChangeState(TypeName_化形.重攻击);
		}
	}
}
