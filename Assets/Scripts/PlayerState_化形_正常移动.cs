public class PlayerState_化形_正常移动 : PlayerState_化形_Base
{
	public override void OnEnter()
	{
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Data.F_Move();
		if (_Data._Curr神力 <= 0f)
		{
			F_化形();
		}
	}

	public override void F_化形()
	{
		_Data.F_退出化形();
	}

	public override void F_轻攻击()
	{
		if (!(_Data._Curr神力 <= 0f))
		{
			F_化形_ChangeState(TypeName_化形.轻攻击);
		}
	}

	public override void F_重攻击Down()
	{
		if (!(_Data._Curr神力 <= 0f))
		{
			_Data._重击选项 = Enum_化形_重攻击.普通;
			F_化形_ChangeState(TypeName_化形.重攻击);
		}
	}

	public override void F_翻滚()
	{
		if (!(_Data._Curr神力 <= 0f))
		{
			F_化形_ChangeState(TypeName_化形.闪避);
		}
	}
}
