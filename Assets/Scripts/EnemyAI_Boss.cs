using System;
using DSB;
using UnityEngine;

public abstract class EnemyAI_Boss : EnemyAI_Base<EnemyAI_Boss, EnemyState_Boss, Enemy_StateData_Boss>, IBoss, IEnemy
{
	protected bool _已经进入初始化;

	public virtual Enum_对话 _对话类型_见面 => Enum_对话.NULL;

	public virtual Enum_对话 _对话类型_死亡 => Enum_对话.NULL;

	public bool _开启Boss死亡UI { get; set; } = true;


	public event Action _Boss死亡 = delegate
	{
	};

	public event Action<Vector2> _Boss死亡_带位置 = delegate
	{
	};

	public event Action _Boss死亡_消失后 = delegate
	{
	};

	public event Action<Vector2> _Boss死亡_带位置_消失后 = delegate
	{
	};

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		MyLog.Log("初始化<<" + base.name + ">>开始");
		_已经进入初始化 = true;
		MyLog.LogError("-------------------");
		_ = _Data;
		_Data.F_重置死亡状态();
		_Data.F_显示();
		_Data._StartDrag = _Data._Mono组件._Rb2D.drag;
		F_添加状态();
		ChangeState(EnemyStateName_Type.站立);
		if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_对话类型_见面))
		{
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 0.2f, delegate
			{
				GameManager._CanPlay = false;
				DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_对话类型_见面);
			});
		}
		MyLog.Log("初始化<<" + base.name + ">>结束");
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		F_播放BossBGM();
		MyLog.LogWarning($"{base.name}生命值>>{_Data._Curr生命}");
	}

	public abstract void F_添加状态();

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		if (base._Is被定)
		{
			MyLog.Log($"<<{base.name}>>_Is被定前_被攻击<<{attackInfo.atk_生命}>>");
			attackInfo.atk_生命 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1402);
			attackInfo.atk_生命 *= 1f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1403);
		}
		MyLog.Log($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
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
				_Data._Mono组件._Rb2D.velocity = 0.8f * base._BeAttack_击退力度 * new Vector2((base.transform.position.x > base._BeAttack_Pos.x) ? 1 : (-1), 0f);
			}
		}
	}

	public virtual void F_播放BossBGM()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_云宫迅音_战斗();
	}

	public virtual void F_关闭BossBGM()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_黑神话_悟空();
	}

	public void F_Action_Boss死亡()
	{
		this._Boss死亡();
		this._Boss死亡_带位置(base.transform.position);
	}

	public void F_Action_Boss死亡_消失后()
	{
		this._Boss死亡_消失后();
		this._Boss死亡_带位置_消失后(base.transform.position);
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
	}

	protected virtual void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			_Data._Mono组件._Rb2D.velocity = Vector2.zero;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
	}

	protected new virtual void OnDestroy()
	{
		F_删除血条();
		F_关闭BossBGM();
		if (_Data._Mono组件._Tsf_移动目标 != null)
		{
			UnityEngine.Object.Destroy(_Data._Mono组件._Tsf_移动目标.gameObject);
		}
	}

	public void F_删除血条()
	{
		if (_Data._UI_BossHP != null)
		{
			UnityEngine.Object.Destroy(_Data._UI_BossHP.gameObject);
			_Data._UI_BossHP = null;
		}
	}

	public override void F_隐藏()
	{
	}

	public override void F_显示()
	{
	}

	public override void F_重置()
	{
	}

	protected override void LateUpdate()
	{
	}
}
