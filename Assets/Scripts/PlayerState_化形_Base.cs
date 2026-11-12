using DSB;

public class PlayerState_化形_Base : DSB_State<PlayerState_化形_Base, PlayerState_化形Data>
{
	public float _Timer;

	public virtual void F_化形_ChangeState(TypeName_化形 typeName)
	{
		_Data._PlayerState_化形.F_化形_ChangeState(typeName);
	}

	public virtual void F_轻攻击()
	{
	}

	public virtual void F_重攻击Up()
	{
	}

	public virtual void F_重攻击Down()
	{
	}

	public virtual void F_翻滚()
	{
	}

	public virtual void F_化形()
	{
	}

	public virtual void F_棍花Down()
	{
	}

	public virtual void F_棍花Up()
	{
	}

	public virtual void F_跳跃Down()
	{
	}

	public virtual void F_跳跃Up()
	{
	}

	public virtual void BeAttack(AttackInfo attackInfo)
	{
		_Data.BeAttack(attackInfo);
	}
}
