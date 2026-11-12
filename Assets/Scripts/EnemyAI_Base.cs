using System;
using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public abstract class EnemyAI_Base<TAI, TState, TStateData> : MonoBehaviour, IEnemy, IBeAttack, I定身术 where TAI : EnemyAI_Base<TAI, TState, TStateData> where TState : EnemyState_Base<TAI, TState, TStateData> where TStateData : Enemy_StateData_Base<TAI, TState, TStateData>
{
	public EnemyStateName_Type 当前状态_仅显示;

	public DSB_FSM<EnemyStateName_Type, TState> _FSM = new DSB_FSM<EnemyStateName_Type, TState>();

	protected bool _首次更新Data = true;

	protected TStateData _mData;

	public bool _开启游荡模式 = true;

	protected float _m连续移动撞墙次数;

	protected Coroutine _Cor_受击变色;

	public virtual TStateData _Data
	{
		get
		{
			if (_首次更新Data || _mData == null)
			{
				_首次更新Data = false;
				Type typeFromHandle = typeof(TStateData);
				object obj = typeFromHandle.GetConstructor(new Type[1] { typeof(TAI) })?.Invoke(new object[1] { this });
				_mData = obj as TStateData;
				if (_mData == null)
				{
					throw new Exception("调用构造失败<<" + base.name + "<<" + typeFromHandle.Name + ">>");
				}
			}
			return _mData;
		}
	}

	public virtual int _阵营
	{
		get
		{
			return 2;
		}
		set
		{
			MyLog.LogError("WTF??????");
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public bool _Is被定 { get; set; }

	public virtual float _连续移动撞墙次数
	{
		get
		{
			return _m连续移动撞墙次数;
		}
		set
		{
			_m连续移动撞墙次数 = value;
			if (_m连续移动撞墙次数 >= 12f)
			{
				_m连续移动撞墙次数 = 0f;
				_Data.F_隐藏();
				ChangeState(EnemyStateName_Type.站立);
				_Data._Is虚幻 = true;
				_Data._Mono组件._SR_Model.DOFade(0f, 0.75f).OnComplete(delegate
				{
					_Data._Is虚幻 = false;
					base.transform.position = _Data._StartPos;
					_Data._Mono组件._SR_Model.DOFade(1f, 0.5f);
				});
			}
		}
	}

	public virtual bool _能活动
	{
		get
		{
			if (_Data._Is死亡)
			{
				return false;
			}
			if (_Data._Is虚幻)
			{
				return false;
			}
			if (_Is被定)
			{
				return false;
			}
			return true;
		}
	}

	public virtual float _受击僵直时长 => 1.8f;

	public int F_得到阵营()
	{
		return _阵营;
	}

	public bool F_判断撞墙次数是否增加(Vector2 startPos, Vector2 targetPos, out Vector2 dir)
	{
		Vector2 vector = targetPos - startPos;
		float magnitude = vector.magnitude;
		dir = vector / magnitude;
		if (magnitude < 6.6f)
		{
			_连续移动撞墙次数 = 0f;
			return false;
		}
		if ((bool)Physics2D.Raycast(startPos, dir, magnitude, LayerMask.GetMask("Default", "中立", "障碍")))
		{
			_连续移动撞墙次数++;
			return true;
		}
		_连续移动撞墙次数 = 0f;
		return false;
	}

	protected virtual void Awake()
	{
		Init();
		_FSM.OnStateChange += delegate(EnemyStateName_Type a, EnemyStateName_Type b)
		{
			if (b == EnemyStateName_Type.死亡)
			{
				_Data._Mono组件._Event_Evemy死亡();
			}
		};
	}

	protected virtual void Update()
	{
		if (_能活动)
		{
			_FSM.OnUpdate();
		}
	}

	protected virtual void LateUpdate()
	{
		_Data.F_设置UI位置(_Data._Mono组件._Tsf_HP.position);
	}

	public abstract void Init();

	public virtual void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		if (_Is被定)
		{
			MyLog.Log($"<<{base.name}>>_Is被定前_被攻击<<{attackInfo.atk_生命}>>");
			attackInfo.atk_生命 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1402);
			attackInfo.atk_生命 *= 1f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1403);
		}
		MyLog.Log($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
		_BeAttack_Pos = attackInfo.atk_Pos;
		_BeAttack_击退力度 = attackInfo.atk_击退力度;
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
			ChangeState(EnemyStateName_Type.死亡);
		}
		else if (attackInfo.atk_有僵直)
		{
			_Data._Curr僵直数值 += attackInfo.atk_僵直;
			if (_Data._Curr僵直数值 >= _Data._Max僵直数值)
			{
				_Data._Curr僵直数值 = 0f;
				_Data._Max僵直数值 += _Data._僵直数值增长;
				ChangeState(EnemyStateName_Type.受击);
			}
			else
			{
				_Data._Mono组件._Rb2D.velocity = 0.8f * _BeAttack_击退力度 * new Vector2((base.transform.position.x > _BeAttack_Pos.x) ? 1 : (-1), 0f);
			}
		}
	}

	protected IEnumerator Cor_受击变色()
	{
		_Data._Mono组件._SR_Model.color = new Color(1f, 0.5f, 0.5f);
		yield return new WaitForSeconds(0.15f);
		_Data._Mono组件._SR_Model.color = new Color(1f, 1f, 1f);
		_Cor_受击变色 = null;
	}

	public virtual void F_定身(float 时间)
	{
		if (_Is被定 || _Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		_Is被定 = true;
		foreach (Transform item in _Data._Mono组件._C_攻击判断.transform)
		{
			if (item.TryGetComponent<Collider2D>(out var component))
			{
				component.enabled = false;
			}
		}
		_Data._Mono组件._Rb2D.velocity = Vector2.zero;
		_Data._Mono组件._Ani.speed = 0f;
		_Data._Mono组件._GO_定身术特效.SetActive(value: true);
		_Data._Mono组件._GO_定身术特效.transform.localScale = base.transform.localScale;
		Invoke("F_定身结束", 时间);
	}

	public virtual void F_定身结束()
	{
		if (!_Is被定)
		{
			return;
		}
		foreach (Transform item in _Data._Mono组件._C_攻击判断.transform)
		{
			if (item.TryGetComponent<Collider2D>(out var component))
			{
				component.enabled = true;
			}
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		_Data._Mono组件._Ani.speed = 1f;
		_Is被定 = false;
		_Data._Mono组件._GO_定身术特效.SetActive(value: false);
	}

	public virtual void F_隐藏()
	{
		_Data._Mono组件._Event_Evemy隐藏();
		_Data.F_隐藏();
		if (_Is被定)
		{
			F_定身结束();
		}
		base.gameObject.SetActive(value: false);
	}

	private void F_在_Event_Evemy显示_中设置为死亡()
	{
		_Data._Is死亡 = true;
	}

	public virtual void F_显示()
	{
		_Data._Mono组件._Event_Evemy显示(F_在_Event_Evemy显示_中设置为死亡);
		if (_Data._Is死亡)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		_Data._Mono组件._SR_Model.color = new Color(1f, 1f, 1f);
		_Data.F_显示();
		ChangeState(EnemyStateName_Type.站立);
	}

	public virtual void F_重置()
	{
		_Data.F_重置死亡状态();
	}

	public virtual void ChangeState(EnemyStateName_Type state)
	{
		if (!_Data._Is死亡 && !_Data._Is虚幻)
		{
			_FSM.ChangeState(state);
			if (state == EnemyStateName_Type.受击 && _Is被定)
			{
				_Data._Mono组件._GO_定身术特效.transform.localScale = base.transform.localScale;
			}
		}
	}

	protected virtual void OnDestroy()
	{
		if (_mData != null)
		{
			_mData.F_销毁();
		}
	}
}
