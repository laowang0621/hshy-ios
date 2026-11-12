using DSB;
using UnityEngine;

public class PlayerState_化形 : Player_StateBase
{
	private int _当前使用化形ID;

	private bool _化形中;

	private DSB_FSM<TypeName_化形, PlayerState_化形_Base> _FSM_化形;

	private PlayerState_化形Data _Data_化形;

	public float _State_攻击连段_判定Timer;

	public int _轻攻击序号;

	public bool _重攻击衔接切手技;

	public override void OnEnter()
	{
		_当前使用化形ID = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用变身;
		_Data_Player_技能 = MyDataManager.F_Get_Player_技能(_当前使用化形ID);
		DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main.F_开启化形UI(_当前使用化形ID);
		EventArgs_Player_化形_生命_Change.Trigger(1f, 1f, 1f);
		EventArgs_Player_化形_神力_Change.Trigger(1f, 1f, 1f);
		EventArgs_Player_化形_蓄力_Change.Trigger(0f, 1f, 1f);
		_Data_化形 = new PlayerState_化形Data();
		_FSM_化形 = new DSB_FSM<TypeName_化形, PlayerState_化形_Base>();
		_Data_化形._MonoPlayer = base._Mono;
		_Data_化形._PlayerState_化形 = this;
		_Data_化形._化形ID = _当前使用化形ID;
		_Data_化形._Mono组件Player = base._Mono组件;
		if (_当前使用化形ID == 2001)
		{
			_Data_化形._Mono组件_化形 = base._Mono组件._Mono组件_广智;
			_Data_化形.F_Init();
			F_化形_广智_OnEnter();
		}
		else if (_当前使用化形ID == 2002)
		{
			_Data_化形._Mono组件_化形 = base._Mono组件._Mono组件_寅虎;
			_Data_化形.F_Init();
			F_化形_寅虎_OnEnter();
		}
		else
		{
			if (_当前使用化形ID != 2003)
			{
				F_退出化形();
				return;
			}
			_Data_化形._Mono组件_化形 = base._Mono组件._Mono组件_黄龙;
			_Data_化形.F_Init();
			F_化形_黄龙_OnEnter();
		}
		base._Mono组件._SR_Model.SetAlpha(0f);
		_Data_化形._Mono组件_化形.gameObject.SetActive(value: true);
		_Data_化形._Mono组件_化形._Ani.Play("站立");
		_Data._强制退出化形 = false;
		_Data.F_设置技能CD(_Data._化形TimerID, 1f);
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		_轻攻击序号 = 1;
		_重攻击衔接切手技 = false;
		F_化形_ChangeState(TypeName_化形.正常移动);
		_化形中 = true;
	}

	public override void OnExit()
	{
		_化形中 = false;
		DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main.F_关闭化形UI();
		float num = _Data_Player_技能._冷却时间 - DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1602);
		if (num < 1f)
		{
			num = 1f;
		}
		_Data.F_设置技能CD(_Data._化形TimerID, num);
		base._Rb2D.drag = 5f;
		EventArgs_Player状态提示_生命过低.Trigger(_Data._Curr生命 / _Data._Max生命 < 0.2f);
		if (!_Data._强制退出化形)
		{
			Vector2 vector = base._Mono组件.transform.position;
			Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		}
		base._Mono组件._SR_Model.SetAlpha(1f);
		if (_当前使用化形ID == 2001)
		{
			F_化形_广智_OnExit();
		}
		else if (_当前使用化形ID == 2002)
		{
			F_化形_寅虎_OnExit();
		}
		else if (_当前使用化形ID == 2003)
		{
			F_化形_黄龙_OnExit();
		}
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base._Mono.gameObject, 1f, delegate
		{
			_Data_化形 = null;
			_FSM_化形 = null;
		}, 0f, DSB_UpdateTimeMode.帧计算);
	}

	private void F_化形_广智_OnEnter()
	{
		_FSM_化形.AddState(TypeName_化形.正常移动, new PlayerState_化形_正常移动().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.轻攻击, new PlayerState_化形_广智_轻攻击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.重攻击, new PlayerState_化形_广智_重攻击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.受击, new PlayerState_化形_受击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.闪避, new PlayerState_化形_广智_闪避().Init(_Data_化形));
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_变身广智();
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_广智BGM();
	}

	private void F_化形_寅虎_OnEnter()
	{
		DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main._MyBtn_跳跃.gameObject.SetActive(value: true);
		_FSM_化形.AddState(TypeName_化形.正常移动, new PlayerState_化形_寅虎_正常移动().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.轻攻击, new PlayerState_化形_寅虎_轻攻击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.重攻击, new PlayerState_化形_寅虎_重攻击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.寅虎格挡, new PlayerState_化形_寅虎_格挡().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.受击, new PlayerState_化形_受击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.闪避, new PlayerState_化形_寅虎_闪避().Init(_Data_化形));
	}

	private void F_化形_黄龙_OnEnter()
	{
		_Data_化形._Ani_移动ID = Animator.StringToHash("Player_移动");
		_Data_化形._Ani_站立ID = Animator.StringToHash("Player_站立");
		_FSM_化形.AddState(TypeName_化形.正常移动, new PlayerState_化形_黄龙_正常移动().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.轻攻击, new PlayerState_化形_黄龙_轻攻击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.重攻击, new PlayerState_化形_黄龙_重攻击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.受击, new PlayerState_化形_受击().Init(_Data_化形));
		_FSM_化形.AddState(TypeName_化形.闪避, new PlayerState_化形_闪避().Init(_Data_化形));
	}

	private void F_化形_广智_OnExit()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_黑神话_悟空();
		base._Mono组件._Mono组件_广智.gameObject.SetActive(value: false);
	}

	private void F_化形_寅虎_OnExit()
	{
		base._Mono组件._Mono组件_寅虎.gameObject.SetActive(value: false);
	}

	private void F_化形_黄龙_OnExit()
	{
		base._Mono组件._Mono组件_黄龙.gameObject.SetActive(value: false);
	}

	public override void F_轻攻击Down()
	{
		_FSM_化形.currState.F_轻攻击();
	}

	public override void F_重攻击Up()
	{
		_FSM_化形.currState.F_重攻击Up();
	}

	public override void F_重攻击Down()
	{
		_FSM_化形.currState.F_重攻击Down();
	}

	public override void F_翻滚()
	{
		_FSM_化形.currState.F_翻滚();
	}

	public override void F_跳跃Down()
	{
		_FSM_化形.currState.F_跳跃Down();
	}

	public override void F_跳跃Up()
	{
		_FSM_化形.currState.F_跳跃Up();
	}

	public override void F_化形()
	{
		MyLog.LogError("尝试释放>>>>>化形");
		if (_Data._IsOk_化形)
		{
			_FSM_化形.currState.F_化形();
			return;
		}
		EventArgs_Player状态提示.Trigger("冷却中...", base._Mono组件._TipsUI);
		MyLog.LogError("化形 在冷却 或 未解锁");
	}

	public override void OnUpdate()
	{
		if (!_化形中)
		{
			return;
		}
		_FSM_化形.currState.OnUpdate();
		_Data_化形._Curr神力 -= Time.deltaTime;
		if (_State_攻击连段_判定Timer > 0f)
		{
			_State_攻击连段_判定Timer -= Time.deltaTime;
			if (_State_攻击连段_判定Timer <= 0f)
			{
				_轻攻击序号 = 1;
				_重攻击衔接切手技 = false;
			}
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		_FSM_化形.currState.BeAttack(attackInfo);
	}

	public virtual void F_化形_ChangeState(TypeName_化形 typeName_化形)
	{
		_FSM_化形.ChangeState(typeName_化形);
	}

	public void F_退出化形()
	{
		ChangeState(Player_StateName_Type.正常移动);
	}
}
