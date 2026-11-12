using System;
using DSB;
using UnityEngine;

public class EnemyAI_石双双 : EnemyAI_狼剑客
{
	public bool _设置_是伪装;

	[HideInInspector]
	public bool _是伪装;

	[HideInInspector]
	public bool _已经喷射;

	[HideInInspector]
	public bool _喷射中;

	[HideInInspector]
	public bool _Is召唤;

	public C_弹道_弓箭 _喷射Prefab;

	public bool _解除伪装后直接攻击;

	private Action _OnDie = delegate
	{
	};

	protected Enemy_StateData_石双双 _mData_石双双;

	public override Enemy_StateData_狼剑客 _Data
	{
		get
		{
			if (_首次更新Data || _mData == null)
			{
				_首次更新Data = false;
				_mData_石双双 = new Enemy_StateData_石双双(this);
				_mData = _mData_石双双;
			}
			return _mData;
		}
	}

	public Enemy_StateData_石双双 _Data_石双双 => _mData_石双双;

	protected override void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		if (_Data._UI_HP != null)
		{
			UnityEngine.Object.Destroy(_Data._UI_HP.gameObject);
		}
	}

	private void F_On_Event_切换地图(DSB_EventArgs args)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public virtual void F_召唤(Action onDie)
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		Init();
		if (onDie != null)
		{
			_OnDie = (Action)Delegate.Combine(_OnDie, onDie);
		}
		_FSM.AddState(EnemyStateName_Type.飞升, new EnemyState_石双双_空降().Init(_Data));
		_Is召唤 = true;
		_设置_是伪装 = false;
		_Data.F_显示();
		F_重置();
		ChangeState(EnemyStateName_Type.飞升);
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_Is召唤 = false;
		_是伪装 = _设置_是伪装;
		_已经进入初始化 = true;
		_已经喷射 = false;
		_喷射中 = false;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data_石双双));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_狼剑客_追击().Init(_Data_石双双));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data_石双双));
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_石双双_站立().Init(_Data_石双双));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_石双双_受击().Init(_Data_石双双));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_石双双_攻击().Init(_Data_石双双));
	}

	public override void F_重置()
	{
		_是伪装 = _设置_是伪装;
		_已经喷射 = false;
		_喷射中 = false;
		base.F_重置();
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		MyLog.Log($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
		base._BeAttack_Pos = attackInfo.atk_Pos;
		base._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (_Cor_受击变色 != null)
		{
			StopCoroutine(_Cor_受击变色);
		}
		_Cor_受击变色 = StartCoroutine(Cor_受击变色());
		_Data._Curr生命 -= attackInfo.atk_生命;
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		if (_Data._Curr生命 <= 0f)
		{
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
			_OnDie?.Invoke();
			ChangeState(EnemyStateName_Type.死亡);
			return;
		}
		if (!_已经喷射 && _Data._Curr生命 <= _Data._MaxHP / 2f)
		{
			_已经喷射 = true;
			_喷射中 = true;
			ChangeState(EnemyStateName_Type.攻击);
			return;
		}
		_Data._Curr僵直数值 += attackInfo.atk_僵直;
		if (_Data._Curr僵直数值 >= _Data._Max僵直数值)
		{
			_Data._Curr僵直数值 = 0f;
			_Data._Max僵直数值 += _Data._僵直数值增长;
			ChangeState(EnemyStateName_Type.受击);
		}
		else
		{
			_Data._Mono组件._Rb2D.velocity = 0.8f * base._BeAttack_击退力度 * new Vector2((base.transform.position.x > base._BeAttack_Pos.x) ? 1 : (-1), 0f);
		}
	}

	public void F_Ani_生成地_石双双()
	{
		_Data._StartPos = base.transform.position;
		float x = base.transform.localScale.x;
		UnityEngine.Object.Instantiate(_Data._Mono组件._拍地板特效Prefab, base.transform.position + new Vector3((x > 0f) ? 1 : (-1), 0f, 0f), Quaternion.identity).transform.localScale = new Vector3((x > 0f) ? 1 : (-1), 1f, 1f);
	}
}
