using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public abstract class NPCAI_同伴 : MonoBehaviour, IBeAttack
{
	public bool _无敌_免疫攻击;

	public float _跟随距离 = 2f;

	public float _站立跟随距离 = 4f;

	public float _移动速度 = 10f;

	public Vector2 _跟随位置偏移 = new Vector2(0.75f, 1.5f);

	[HideInInspector]
	public EnemyMono组件 _Mono组件;

	[HideInInspector]
	public bool _朝向右边 = true;

	[HideInInspector]
	public bool _Is虚幻;

	[HideInInspector]
	public float _攻击力;

	[HideInInspector]
	public Vector2 _Dir;

	public DSB_FSM<EnemyStateName_Type, NPCState_同伴> _FSM;

	protected bool _已经进入初始化;

	protected int _m连续移动撞墙次数;

	protected bool _TO_死亡;

	public Coroutine _Cor_受击变色;

	public bool _受伤闪红 = true;

	public int _阵营
	{
		get
		{
			return 1;
		}
		set
		{
			MyLog.LogError("WTF??????");
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public int _连续移动撞墙次数
	{
		get
		{
			return _m连续移动撞墙次数;
		}
		set
		{
			_m连续移动撞墙次数 = value;
			if (_m连续移动撞墙次数 >= 5)
			{
				_m连续移动撞墙次数 = 0;
				ChangeState(EnemyStateName_Type.站立);
				_Is虚幻 = true;
				_Mono组件._SR_Model.DOFade(0f, 0.75f).OnComplete(delegate
				{
					_Is虚幻 = false;
					base.transform.position = DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
					_Mono组件._SR_Model.DOFade(1f, 0.5f);
				});
				MyLog.LogError("撞墙多次，刷新回玩家位置");
			}
		}
	}

	public virtual bool _视野范围有敌人
	{
		get
		{
			if (!_Mono组件._视野范围._范围内发现敌人)
			{
				return _Mono组件._攻击范围._范围内发现敌人;
			}
			return true;
		}
	}

	public virtual bool _攻击范围有敌人 => _Mono组件._攻击范围._范围内发现敌人;

	public virtual Vector3 _Pos_追击目标位置 => _Mono组件._视野范围._目标位置;

	public virtual bool _能活动
	{
		get
		{
			if (_Is虚幻)
			{
				return false;
			}
			if (!GameManager._CanPlay)
			{
				return false;
			}
			return true;
		}
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		if (!_无敌_免疫攻击 && _FSM.currStateName != EnemyStateName_Type.死亡)
		{
			_FSM.currState.BeAttack(attackInfo);
		}
	}

	public IEnumerator Cor_受击变色()
	{
		if (_受伤闪红)
		{
			_Mono组件._SR_Model.color = new Color(1f, 0.5f, 0.5f);
			yield return new WaitForSeconds(0.15f);
			_Mono组件._SR_Model.color = new Color(1f, 1f, 1f);
			_Cor_受击变色 = null;
		}
	}

	public void F_强制清空追击()
	{
		_Mono组件._视野范围.Clear();
		_Mono组件._攻击范围.Clear();
	}

	private void Awake()
	{
		_Mono组件 = GetComponent<EnemyMono组件>();
		_攻击力 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.八戒攻击力;
		_Is虚幻 = false;
		_FSM = new DSB_FSM<EnemyStateName_Type, NPCState_同伴>();
		_TO_死亡 = false;
		_朝向右边 = base.transform.localScale.x > 0f;
		F_添加状态();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_特殊场景._EventID, F_特殊场景);
	}

	protected abstract void F_添加状态();

	private void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_特殊场景._EventID, F_特殊场景);
	}

	public virtual void F_EventArgs_Player重生(DSB_EventArgs args)
	{
		F_强制清空追击();
		_TO_死亡 = false;
	}

	public void F_仅强制切换到站立状态()
	{
		F_强制清空追击();
		ChangeState(EnemyStateName_Type.站立);
	}

	public void F_切换场景_PlayerTo正常移动(DSB_EventArgs args)
	{
		F_强制清空追击();
		Vector2 vector = Random.Range(_跟随位置偏移.x, _跟随位置偏移.y) * Random.insideUnitCircle;
		vector.y *= 0.5f;
		base.transform.position = (Vector2)DSB_MonoSingleton<GameManager>.Instance._PC.transform.position + vector;
		ChangeState(EnemyStateName_Type.站立);
	}

	public void F_特殊场景(DSB_EventArgs args)
	{
		EventArgs_特殊场景 _args = args as EventArgs_特殊场景;
		F_强制清空追击();
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 5f, delegate
		{
			_TO_死亡 = _args._是进入;
			if (_args._是进入)
			{
				if (_args._特殊场景 == EventArgs_特殊场景.Type_EventArgs_特殊场景.滑沙 || _args._特殊场景 == EventArgs_特殊场景.Type_EventArgs_特殊场景.紫蛛儿)
				{
					F_生成烟雾特效();
				}
				_FSM.ChangeState(EnemyStateName_Type.死亡);
			}
			else if (_args._特殊场景 == EventArgs_特殊场景.Type_EventArgs_特殊场景.紫蛛儿)
			{
				F_切换场景_PlayerTo正常移动(null);
				F_生成烟雾特效();
			}
		}, 0f, DSB_UpdateTimeMode.帧计算);
	}

	public void F_生成烟雾特效()
	{
		if (_FSM.currStateName != EnemyStateName_Type.死亡)
		{
			Object.Instantiate(_Mono组件._烟雾_Prefab, _Mono组件._Tsf_烟雾.position, Quaternion.identity);
		}
	}

	public void F_To正常移动()
	{
		if (!_TO_死亡)
		{
			ChangeState(EnemyStateName_Type.站立);
		}
	}

	public virtual void ChangeState(EnemyStateName_Type state)
	{
		if (!_Is虚幻)
		{
			_FSM.ChangeState(state);
		}
	}

	protected virtual void Update()
	{
		if (_能活动)
		{
			_FSM.OnUpdate();
		}
	}

	public bool F_PlayerMove(Vector2 dir, float speed, bool can转向 = true)
	{
		_Dir = dir;
		_Mono组件._Rb2D.velocity = Vector2.Lerp(_Mono组件._Rb2D.velocity, speed * _Dir, Time.deltaTime * 20f);
		if (can转向)
		{
			F_玩家转向(dir.x);
		}
		if (dir.x == 0f && dir.y == 0f)
		{
			return false;
		}
		return true;
	}

	public void F_玩家转向(float x)
	{
		if (x > 0.01f && !_朝向右边)
		{
			_朝向右边 = true;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (x < -0.01f && _朝向右边)
		{
			_朝向右边 = false;
			base.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
}
