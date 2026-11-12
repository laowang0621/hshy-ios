using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_狸侍长 : EnemyAI_狼剑客
{
	public LineRenderer _巡逻路径;

	private EnemyAI_狸侍长_相互通知 _相互通知;

	public Vector2 _巡逻目标位置;

	[HideInInspector]
	public bool _死追玩家;

	public override float _连续移动撞墙次数
	{
		get
		{
			return _m连续移动撞墙次数;
		}
		set
		{
			_m连续移动撞墙次数 = value;
			if (!(_m连续移动撞墙次数 >= 15f))
			{
				return;
			}
			_m连续移动撞墙次数 = 0f;
			_Data._Is虚幻 = true;
			_Data._Mono组件._SR_Model.DOFade(0f, 0.45f).OnComplete(delegate
			{
				_Data._Is虚幻 = false;
				_Data._Mono组件._SR_Model.DOFade(1f, 0.35f);
				if (_视野范围有敌人)
				{
					base.transform.position = _目标位置;
				}
				else
				{
					base.transform.position = _巡逻目标位置;
				}
			});
		}
	}

	public Vector2 _目标位置
	{
		get
		{
			if (_Data._Mono组件._视野范围._范围内有玩家)
			{
				_死追玩家 = true;
				_相互通知?.F_相互通知发现玩家();
			}
			if (_死追玩家)
			{
				return DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
			}
			return _Data._Pos_追击目标位置;
		}
	}

	public bool _视野范围有敌人
	{
		get
		{
			if (_死追玩家)
			{
				return true;
			}
			return _Data._视野范围有敌人;
		}
	}

	public void F_注册相互通知(EnemyAI_狸侍长_相互通知 相互通知)
	{
		_相互通知 = 相互通知;
	}

	public override void F_重置()
	{
		base.F_重置();
		_死追玩家 = false;
	}

	public override void F_隐藏()
	{
		base.F_隐藏();
		_死追玩家 = false;
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_狼剑客_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_狸侍长_追击().Init(_Data));
		if (_巡逻路径 == null)
		{
			_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data));
		}
		else
		{
			_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狸侍长_散步().Init(_Data));
		}
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_狸侍长_攻击().Init(_Data));
	}
}
