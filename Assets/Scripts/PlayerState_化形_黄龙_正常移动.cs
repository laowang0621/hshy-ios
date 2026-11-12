public class PlayerState_化形_黄龙_正常移动 : PlayerState_化形_正常移动
{
	public override void F_重攻击Down()
	{
		if (!(_Data._Curr神力 <= 0f))
		{
			if (_Data._重攻击衔接切手技)
			{
				_Data._重击选项 = Enum_化形_重攻击.识破;
			}
			else if (_Data._Curr蓄力 > 0.95f)
			{
				_Data._重击选项 = Enum_化形_重攻击.空中龙车;
			}
			else
			{
				_Data._重击选项 = Enum_化形_重攻击.普通;
			}
			F_化形_ChangeState(TypeName_化形.重攻击);
		}
	}
}
