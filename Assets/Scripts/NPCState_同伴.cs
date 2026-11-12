using DSB;

public class NPCState_同伴 : IState
{
	protected NPCAI_同伴 _Mono_同伴;

	protected EnemyMono组件 _Mono组件;

	public virtual NPCState_同伴 Init(NPCAI_同伴 mono)
	{
		_Mono_同伴 = mono;
		_Mono组件 = mono._Mono组件;
		return this;
	}

	public virtual void OnEnter()
	{
	}

	public virtual void OnExit()
	{
	}

	public virtual void OnUpdate()
	{
	}

	public virtual void BeAttack(AttackInfo attackInfo)
	{
		if (!_Mono_同伴._Is虚幻)
		{
			_Mono组件._Rb2D.drag = 5f;
			MyLog.Log($"<<{_Mono_同伴.name}>>被攻击<<{attackInfo.atk_生命}>>");
			_Mono_同伴._BeAttack_Pos = attackInfo.atk_Pos;
			_Mono_同伴._BeAttack_击退力度 = attackInfo.atk_击退力度;
			if (_Mono_同伴._Cor_受击变色 != null)
			{
				_Mono_同伴.StopCoroutine(_Mono_同伴._Cor_受击变色);
			}
			_Mono_同伴._Cor_受击变色 = _Mono_同伴.StartCoroutine(_Mono_同伴.Cor_受击变色());
			if (attackInfo.atk_有僵直)
			{
				_Mono_同伴.ChangeState(EnemyStateName_Type.受击);
			}
		}
	}
}
