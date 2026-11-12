using DSB;

public class NPCState_四姐_站立和跟随 : NPCState_同伴_站立和跟随
{
	private DataInfo_Player招式 回血招式参数;

	private NPCAI_四姐 _Mono同伴_四姐;

	public override NPCState_同伴 Init(NPCAI_同伴 mono)
	{
		base.Init(mono);
		_Mono同伴_四姐 = mono as NPCAI_四姐;
		回血招式参数 = MyDataManager.F_Get_Player_招式系数(2014);
		return this;
	}

	public override void OnEnter()
	{
		if (!F_判断玩家状态并回血())
		{
			base.OnEnter();
		}
	}

	public override bool F_判断玩家状态并回血()
	{
		if (_Mono同伴_四姐._回血术Timer > 0f)
		{
			return false;
		}
		if (DSB_MonoSingleton<GameManager>.Instance._PC._FSM.currStateName == Player_StateName_Type.死亡)
		{
			return false;
		}
		if (DSB_MonoSingleton<GameManager>.Instance._PC._FSM.currStateName == Player_StateName_Type.化形)
		{
			return false;
		}
		if (DSB_MonoSingleton<GameManager>.Instance._PC._Data._Curr生命 / DSB_MonoSingleton<GameManager>.Instance._PC._Data._Max生命 < 回血招式参数._僵直伤害)
		{
			_Mono_同伴.ChangeState(EnemyStateName_Type.飞升);
			return true;
		}
		return false;
	}
}
