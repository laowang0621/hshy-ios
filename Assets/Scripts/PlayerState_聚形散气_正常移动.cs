using DSB;

public class PlayerState_聚形散气_正常移动 : PlayerState_聚形散气_Base
{
	private float _移动速度;

	public override void OnEnter()
	{
		_移动速度 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.正常移动速度;
	}

	public override void OnUpdate()
	{
		if (_PlayerState_聚形散气.F_PlayerMove(_移动速度))
		{
			_PlayerState_聚形散气._Ani.Play("跑步");
		}
		else
		{
			_PlayerState_聚形散气._Ani.Play("站立");
		}
	}

	public override void OnExit()
	{
	}

	public override void F_喝酒()
	{
		if (_PlayerState_聚形散气._Data._Curr酒 > 0)
		{
			_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.喝酒);
		}
	}

	public override void F_翻滚()
	{
		_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.翻滚);
	}

	public override void F_跳跃Down()
	{
		_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.跳跃);
	}

	public override void F_重攻击Down()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(107) > 0.1f)
		{
			_PlayerState_聚形散气.F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State.蓄力);
		}
	}
}
