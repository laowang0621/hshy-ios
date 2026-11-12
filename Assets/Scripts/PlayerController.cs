using DG.Tweening;
using DSB;
using UnityEngine;

public class PlayerController : MonoBehaviour, IBeAttack
{
	public bool _m开启反弹弹道;

	private float _受击保护时间 = 0.25f;

	private float _受击保护timer;

	public DSB_FSM<Player_StateName_Type, Player_StateBase> _FSM = new DSB_FSM<Player_StateName_Type, Player_StateBase>();

	public bool _首次更新Data = true;

	[SerializeField]
	private Player_StateData _mData;

	public bool _静止玩家;

	private bool _已经进入初始化;

	public Player_StateName_Type _当前状态;

	public C_爆竹 _Curr爆竹;

	private bool _已经注册交互;

	public bool _Is_正常移动 => _FSM.currStateName == Player_StateName_Type.正常移动;

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public bool _开启反弹弹道 => _m开启反弹弹道;

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

	public Player_StateData _Data
	{
		get
		{
			if (_首次更新Data || _mData == null)
			{
				_首次更新Data = false;
				_mData = new Player_StateData(this);
			}
			return _mData;
		}
	}

	public bool _可以被敌人发现 => _Data._可以被敌人发现;

	private bool _能否操作
	{
		get
		{
			if (!GameManager._CanPlay || _Data._Is死亡 || GameManager._暂停游戏 || _静止玩家)
			{
				return false;
			}
			return true;
		}
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		MyLog.LogError($"玩家>>被攻击<<{attackInfo.atk_生命}");
		if (GameManager._免疫伤害)
		{
			attackInfo.atk_生命 = 0f;
		}
		if (_能否操作 && (!attackInfo.atk_Is地面 || !_Data._Is腾空) && (!(_受击保护timer > 0f) || attackInfo.atk_Is远程))
		{
			_受击保护timer = _受击保护时间;
			if (attackInfo.atk_强制受击)
			{
				_Data.BeAttack(attackInfo);
			}
			else
			{
				_FSM.currState.BeAttack(attackInfo);
			}
		}
	}

	public PlayerController Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log("玩家>>已经初始化！");
			return null;
		}
		MyLog.Log("初始化玩家>>>");
		_已经进入初始化 = true;
		_ = _Data;
		_Data.F_初始化组件();
		_Data.F_初始化属性();
		_Data.F_触发所有事件();
		F_初始化状态();
		MyLog.Log("<<<初始化玩家结束");
		return this;
	}

	private void Start()
	{
		if ((bool)Init())
		{
			DSB_MonoSingleton<GameManager>.Instance._PC = this;
		}
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_切换场景_PlayerTo正常移动);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_回到主页._EventID, F_切换场景_PlayerTo正常移动);
		_FSM.OnStateChange += delegate(Player_StateName_Type a, Player_StateName_Type b)
		{
			_当前状态 = b;
		};
	}

	public void F_切换场景_PlayerTo正常移动(DSB_EventArgs args)
	{
		if (!_Data._Is死亡)
		{
			DSB_FSM<Player_StateName_Type, Player_StateBase> fSM = _FSM;
			if (fSM == null || fSM.currStateName != Player_StateName_Type.AK)
			{
				_Data.F_触发所有事件();
				_Data._强制退出化形 = true;
				ChangeToState(Player_StateName_Type.正常移动);
			}
		}
	}

	private void F_初始化状态()
	{
		_FSM.AddState(Player_StateName_Type.正常移动, new PlayerState_正常移动().Init(_Data));
		_FSM.AddState(Player_StateName_Type.跳跃重击, new PlayerState_跳跃重击().Init(_Data));
		_FSM.AddState(Player_StateName_Type.完美闪避, new PlayerState_完美闪避().Init(_Data));
		_FSM.AddState(Player_StateName_Type.释放蓄力, new PlayerState_释放蓄力().Init(_Data));
		_FSM.AddState(Player_StateName_Type.聚形散气, new PlayerState_聚形散气().Init(_Data));
		_FSM.AddState(Player_StateName_Type.猴子猴孙, new PlayerState_身外身法().Init(_Data));
		_FSM.AddState(Player_StateName_Type.化形, new PlayerState_化形().Init(_Data));
		_FSM.AddState(Player_StateName_Type.破隐一击, new PlayerState_破隐一击().Init(_Data));
		_FSM.AddState(Player_StateName_Type.破棍式, new PlayerState_破棍式().Init(_Data));
		_FSM.AddState(Player_StateName_Type.斩棍式, new PlayerState_斩棍式().Init(_Data));
		_FSM.AddState(Player_StateName_Type.定身术, new PlayerState_定身术().Init(_Data));
		_FSM.AddState(Player_StateName_Type.出租云, new PlayerState_出租云().Init(_Data));
		_FSM.AddState(Player_StateName_Type.轻棍1, new PlayerState_轻棍1().Init(_Data));
		_FSM.AddState(Player_StateName_Type.轻棍2, new PlayerState_轻棍2().Init(_Data));
		_FSM.AddState(Player_StateName_Type.轻棍3, new PlayerState_轻棍3().Init(_Data));
		_FSM.AddState(Player_StateName_Type.棍花, new PlayerState_棍花().Init(_Data));
		_FSM.AddState(Player_StateName_Type.跳跃, new PlayerState_跳跃().Init(_Data));
		_FSM.AddState(Player_StateName_Type.受击, new PlayerState_受击().Init(_Data));
		_FSM.AddState(Player_StateName_Type.翻滚, new PlayerState_翻滚().Init(_Data));
		_FSM.AddState(Player_StateName_Type.蓄力, new PlayerState_蓄力().Init(_Data));
		_FSM.AddState(Player_StateName_Type.喝酒, new PlayerState_喝酒().Init(_Data));
		_FSM.AddState(Player_StateName_Type.死亡, new PlayerState_死亡().Init(_Data));
		_FSM.AddState(Player_StateName_Type.打坐, new PlayerState_打坐().Init(_Data));
		_FSM.AddState(Player_StateName_Type.精魄, new PlayerState_精魄().Init(_Data));
		_FSM.AddState(Player_StateName_Type.划沙, new PlayerState_划沙().Init(_Data));
		_FSM.AddState(Player_StateName_Type.打飞机, new PlayerState_打飞机().Init(_Data));
		_FSM.AddState(Player_StateName_Type.AK, new PlayerState_AK().Init(_Data));
		_FSM.AddState(Player_StateName_Type.爆竹, new PlayerState_爆竹().Init(_Data));
		_FSM.AddState(Player_StateName_Type.下地狱100层, new PlayerState_下地狱100层().Init(_Data));
	}

	public void F_SetPos(Vector2 pos)
	{
		base.transform.position = pos;
	}

	private void F_触发切换场景()
	{
		if (_FSM.currStateName != Player_StateName_Type.死亡)
		{
			_Data._能保留豆子 = true;
			ChangeToState(Player_StateName_Type.正常移动);
		}
	}

	public void F_开始下地狱100层()
	{
		ChangeToState(Player_StateName_Type.下地狱100层);
	}

	public void F_地狱100层中让玩家转起来()
	{
		if (_FSM.currStateName == Player_StateName_Type.下地狱100层)
		{
			F_特殊操作_1();
		}
	}

	public void F_离开地狱100层()
	{
		ChangeToState(Player_StateName_Type.正常移动);
	}

	public bool F_玩家已经死亡()
	{
		return _FSM.currStateName == Player_StateName_Type.死亡;
	}

	public void F_地狱100层_碰撞器关闭一段时间(float time)
	{
		_Data._下地狱100层_可以跳跃 = false;
		Invoke("F_地狱100层_碰撞器关闭一段时间_Invoke", time);
		_Data._Mono组件._Co_自身碰撞器.enabled = false;
	}

	public void F_地狱100层_碰撞器关闭一段时间_Invoke()
	{
		_Data._下地狱100层_可以跳跃 = true;
		_Data._Mono组件._Co_自身碰撞器.enabled = true;
	}

	public void F_杀死玩家()
	{
		if (_FSM.currStateName != Player_StateName_Type.死亡)
		{
			_Data._Curr生命 = -1f;
			_FSM.ChangeState(Player_StateName_Type.死亡);
		}
	}

	public void F_外部施加跳跃高度(float 高度)
	{
		_FSM.currState.F_外部施加跳跃高度(高度);
	}

	public void F_进入滑沙场地()
	{
		ChangeToState(Player_StateName_Type.划沙);
	}

	public void F_离开滑沙场地()
	{
		ChangeToState(Player_StateName_Type.翻滚);
	}

	public void F_开始打飞机()
	{
		ChangeToState(Player_StateName_Type.打飞机);
	}

	public void F_结束打飞机()
	{
		_静止玩家 = true;
	}

	public void F_拿起爆竹(C_爆竹 爆竹)
	{
		Physics2D.IsTouching(_Data._Mono组件._Co_自身碰撞器, 爆竹._Co2D);
		_Curr爆竹 = 爆竹;
		_FSM.currState.F_拿起爆竹();
	}

	public void F_打坐(Vector2 pos)
	{
		base.transform.position = pos;
		ChangeToState(Player_StateName_Type.打坐);
	}

	public void ChangeToState(Player_StateName_Type state)
	{
		_FSM.ChangeState(state);
	}

	public void ChangeState(Player_StateName_Type state)
	{
		if (_FSM.states.TryGetValue(state, out var value))
		{
			if (value._Data_Player_招式系数._气力消耗 > 0f && _Data._Curr气力 < value._Data_Player_招式系数._气力消耗)
			{
				MyLog.LogError("气力不足，无法切换:" + state);
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_力竭();
				EventArgs_Player状态提示.Trigger("气力不足!", _Data._Mono组件._TipsUI);
				_FSM.ChangeState(Player_StateName_Type.正常移动);
			}
			else
			{
				_FSM.ChangeState(state);
			}
		}
		else
		{
			MyLog.LogError("无法切换到:" + state);
		}
	}

	private void Update()
	{
		if (!_能否操作)
		{
			if (_Data._Is死亡 && _FSM.currStateName != Player_StateName_Type.死亡)
			{
				ChangeToState(Player_StateName_Type.死亡);
			}
			return;
		}
		if (_受击保护timer >= 0f)
		{
			_受击保护timer -= Time.deltaTime;
		}
		if (_Data._State_攻击连段_判定Timer > 0f)
		{
			_Data._State_攻击连段_判定Timer -= Time.deltaTime;
			if (_Data._State_攻击连段_判定Timer <= 0f)
			{
				_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍1;
				_Data._重攻击衔接切手技 = false;
			}
		}
		_FSM.OnUpdate();
		_Data.OnUpdate();
	}

	public bool F_PlayerMove(float speed, bool can转向 = true)
	{
		float horizontal = MyInputManager.Horizontal;
		float num = MyInputManager.Vertical / 2f;
		_Data._Dir = new Vector2(horizontal, num).normalized;
		_Data._Mono组件._Rb2D.velocity = Vector2.Lerp(_Data._Mono组件._Rb2D.velocity, speed * _Data._Dir, Time.deltaTime * 20f);
		if (can转向)
		{
			F_玩家转向(horizontal);
		}
		if (horizontal == 0f && num == 0f)
		{
			return false;
		}
		return true;
	}

	public void F_玩家转向(float x)
	{
		if (x > 0f && !_Data._朝向右边)
		{
			_Data._朝向右边 = true;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (x < 0f && _Data._朝向右边)
		{
			_Data._朝向右边 = false;
			base.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	public void F_玩家转向_强制(float x)
	{
		if (x > 0f)
		{
			_Data._朝向右边 = true;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			_Data._朝向右边 = false;
			base.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	public void F_重置状态()
	{
		DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main.F_关闭化形UI();
		_静止玩家 = false;
		_Data._Mono组件._Rb2D.gravityScale = 0f;
		_Data._Mono组件._打飞机云.SetActive(value: false);
		_Data._Mono组件._SR_名刀.DOKill();
		_Data._Mono组件._SR_名刀.SetAlpha(0f);
		_Data.F_初始化属性();
		_Data.F_触发所有事件();
		_Data.F_重生技能CD();
		_受击保护timer = -1f;
		ChangeToState(Player_StateName_Type.正常移动);
	}

	public void F_加点后调用()
	{
		_Data.F_加点后调用();
	}

	public void F_计算加点加成()
	{
		_Data.F_计算加点加成();
	}

	public void F_临时_设置气力上限百分比(float val)
	{
		_Data._Curr当前气力上限百分比 = val;
	}

	public void F_注册交互事件()
	{
		if (!_已经注册交互)
		{
			_已经注册交互 = true;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_饮酒 += F_喝酒;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_定身术 += F_定身术;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_隐身 += F_隐身;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_猴子猴孙 += F_猴子猴孙;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_化形 += F_化形;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_精魄 += F_精魄;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_跳跃 += F_跳跃Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_跳跃 += F_跳跃Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_轻攻击 += F_轻攻击Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_轻攻击 += F_轻攻击Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_翻滚 += F_翻滚;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_棍花 += F_棍花Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_棍花 += F_棍花Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_重攻击 += F_重攻击Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_重攻击 += F_重攻击Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_AK += F_装备AK;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_出租云 += F_出租云;
		}
	}

	public void F_注销交互事件()
	{
		if (_已经注册交互)
		{
			_已经注册交互 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_饮酒 -= F_喝酒;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_定身术 -= F_定身术;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_隐身 -= F_隐身;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_猴子猴孙 -= F_猴子猴孙;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_化形 -= F_化形;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_精魄 -= F_精魄;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_跳跃 -= F_跳跃Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_跳跃 -= F_跳跃Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_轻攻击 -= F_轻攻击Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_轻攻击 -= F_轻攻击Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_翻滚 -= F_翻滚;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_棍花 -= F_棍花Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_棍花 -= F_棍花Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_重攻击 -= F_重攻击Down;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Up_重攻击 -= F_重攻击Up;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_AK -= F_装备AK;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_出租云 -= F_出租云;
		}
	}

	public void F_喝酒()
	{
		if (_能否操作)
		{
			_FSM.currState.F_喝酒();
		}
	}

	public void F_定身术()
	{
		if (_能否操作)
		{
			_FSM.currState.F_定身术();
		}
	}

	public void F_隐身()
	{
		if (_能否操作)
		{
			_FSM.currState.F_隐身();
		}
	}

	public void F_猴子猴孙()
	{
		if (_能否操作)
		{
			_FSM.currState.F_猴子猴孙();
		}
	}

	public void F_化形()
	{
		if (_能否操作)
		{
			_FSM.currState.F_化形();
		}
	}

	public void F_精魄()
	{
		if (_能否操作)
		{
			_FSM.currState.F_精魄();
		}
	}

	public void F_装备AK()
	{
		if (_能否操作)
		{
			_FSM.currState.F_装备AK();
		}
	}

	public void F_出租云()
	{
		if (_能否操作)
		{
			_FSM.currState.F_出租云();
		}
	}

	public void F_跳跃Down()
	{
		if (_能否操作)
		{
			_FSM.currState.F_跳跃Down();
		}
	}

	public void F_跳跃Up()
	{
		if (_能否操作)
		{
			_FSM.currState.F_跳跃Up();
		}
	}

	public void F_特殊操作_1()
	{
		if (_能否操作)
		{
			_FSM.currState.F_特殊操作_1();
		}
	}

	public void F_轻攻击Down()
	{
		if (_能否操作)
		{
			_FSM.currState.F_轻攻击Down();
		}
	}

	public void F_轻攻击Up()
	{
		if (_能否操作)
		{
			_FSM.currState.F_轻攻击Up();
		}
	}

	public void F_翻滚()
	{
		if (_能否操作)
		{
			_FSM.currState.F_翻滚();
		}
	}

	public void F_棍花Down()
	{
		if (_能否操作)
		{
			_FSM.currState.F_棍花Down();
		}
	}

	public void F_棍花Up()
	{
		if (_能否操作)
		{
			_FSM.currState.F_棍花Up();
		}
	}

	public void F_重攻击Down()
	{
		if (_能否操作)
		{
			_FSM.currState.F_重攻击Down();
		}
	}

	public void F_重攻击Up()
	{
		if (_能否操作)
		{
			_FSM.currState.F_重攻击Up();
		}
	}
}
