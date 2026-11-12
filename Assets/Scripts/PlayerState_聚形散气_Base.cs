using DSB;

public class PlayerState_聚形散气_Base : IState
{
	protected PlayerState_聚形散气 _PlayerState_聚形散气;

	public PlayerState_聚形散气_Base F_Init(PlayerState_聚形散气 playerState_聚形散气)
	{
		_PlayerState_聚形散气 = playerState_聚形散气;
		return this;
	}

	public virtual void OnEnter()
	{
	}

	public virtual void OnUpdate()
	{
	}

	public virtual void OnExit()
	{
	}

	public virtual void F_聚型散气_时间结束()
	{
	}

	public virtual void F_喝酒()
	{
	}

	public virtual void F_翻滚()
	{
	}

	public virtual void F_跳跃Down()
	{
	}

	public virtual void F_重攻击Down()
	{
	}

	public virtual void F_重攻击Up()
	{
	}
}
