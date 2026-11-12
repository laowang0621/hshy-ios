using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

[Serializable]
public class Player_StateData : IStateData
{
	private PlayerController _mMono;

	private PlayerMono组件 _mMono组件;

	[Header("玩家基础数值")]
	[SerializeField]
	public int _mMax等级;

	[SerializeField]
	private int _mCurr等级;

	[SerializeField]
	private float _mMax生命;

	[SerializeField]
	private float _mCurr生命;

	[SerializeField]
	private float _mMax法力;

	[SerializeField]
	private float _mCurr法力;

	[SerializeField]
	private float _mMax气力;

	[SerializeField]
	private float _mCurr气力上限百分比;

	[SerializeField]
	private float _mCurr气力;

	[SerializeField]
	private float _气力回转timer;

	[SerializeField]
	private float _m攻击力;

	[SerializeField]
	private float _mMax蓄力;

	[SerializeField]
	private float _mCurr蓄力;

	private int _mCurr豆子;

	[SerializeField]
	public float _mMax下一级所需经验;

	[SerializeField]
	private float _mCurr经验;

	[SerializeField]
	private int _mMax酒;

	[SerializeField]
	private int _mCurr酒;

	[SerializeField]
	private float _m葫芦_数量;

	[SerializeField]
	private float _m葫芦_恢复;

	[SerializeField]
	private float _m棍势_一段;

	[SerializeField]
	private float _m棍势_二段;

	[SerializeField]
	private float _m棍势_三段;

	[SerializeField]
	private float _m气力_恢复速度;

	[SerializeField]
	private float _m气力_回转时间;

	private int _第二豆等级;

	private int _第三豆等级;

	public float _广智_生命值 = 200f;

	public float _广智_棍式 = 100f;

	public float _广智_攻击力 = 10f;

	private float _m蓄力状态承受伤害;

	public float 影身状态承受伤害;

	public int _释放豆子;

	public bool _触发完美闪避;

	public bool _可以受击;

	public bool _释放蓄力状态;

	public bool _化形状态;

	public bool _可以识破;

	public bool _能保留豆子;

	public bool _成功识破;

	public bool _Is死亡;

	public bool _强制退出化形;

	public bool _Is聚形散气;

	public bool _Is免疫远程;

	public bool _Is腾空;

	public float _成功识破_Timer = -1f;

	public bool _识破_打中人;

	public Player_StateName_Type 当前状态_仅显示;

	public float _CurrHeight;

	public MyDictionary<string, float> _Dict_AniName_time;

	public bool _朝向右边 = true;

	public Vector2 _Dir;

	public I定身术 _定身目标;

	public List<I定身术> _List_定身目标;

	public bool _是影身蓄力;

	public int _定身术TimerID = 10001;

	public int _聚形散气TimerID = 10002;

	public int _身外身法TimerID = 10003;

	public int _化形TimerID = 10004;

	public int _精魄TimerID = 10005;

	private bool _mIsOk_定身术;

	private bool _mIsOk_聚形散气;

	private bool _mIsOk_身外身法;

	private bool _mIsOk_化形;

	private bool _mIsOk_精魄;

	public float _本次受击时长 = 0.3f;

	public bool _本次硬直是虚弱动作;

	private float _m默认受击时长 = 0.3f;

	public bool _名刀司命;

	public Player_BuffInfo player_BuffInfo_永久;

	public Player_StateName_Type _State_攻击连段_判定;

	public bool _重攻击衔接切手技;

	public float _State_攻击连段_判定Timer;

	private DataInfo_Player招式 猪八戒猪八戒被动 = MyDataManager.F_Get_Player_招式系数(2015);

	public bool _已经触发四姐名刀;

	public bool _下地狱100层_可以跳跃;

	public PlayerController _Mono
	{
		get
		{
			if (_mMono == null)
			{
				throw new Exception("_mMono为空！");
			}
			return _mMono;
		}
	}

	public PlayerMono组件 _Mono组件
	{
		get
		{
			if (_mMono组件 == null)
			{
				_mMono组件 = _Mono.GetComponent<PlayerMono组件>();
				if (_mMono组件 == null)
				{
					throw new Exception("没有<<PlayerMono组件>>");
				}
			}
			return _mMono组件;
		}
	}

	public float 蓄力状态承受伤害 => Mathf.Clamp(_m蓄力状态承受伤害 - DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1303), 0.01f, _m蓄力状态承受伤害);

	public bool _可以被敌人发现
	{
		get
		{
			if (_Is聚形散气)
			{
				return false;
			}
			if (_Is死亡)
			{
				return false;
			}
			return true;
		}
	}

	public float _蓄力额外速度 { get; set; }

	public float _攻击力 => (_m攻击力 + player_BuffInfo_永久._攻击力) * (float)((!GameManager._10倍攻击) ? 1 : 20);

	public float _Max生命 => _mMax生命 + player_BuffInfo_永久._生命值;

	public float _Max法力 => _mMax法力 + player_BuffInfo_永久._法力值;

	public float _Max气力 => _mMax气力 + player_BuffInfo_永久._气力值;

	public float _葫芦_恢复 => _m葫芦_恢复;

	public bool _IsOk_定身术
	{
		get
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_定身术)
			{
				return false;
			}
			if (!_mIsOk_定身术)
			{
				EventArgs_Player状态提示.Trigger("冷却中...", _Mono组件._TipsUI);
				return false;
			}
			if (_Curr法力 < MyDataManager.F_Get_Player_技能(101)._法力消耗)
			{
				EventArgs_Player状态提示.Trigger("法力不足!", _Mono组件._TipsUI);
				return false;
			}
			return true;
		}
	}

	public bool _IsOk_出租云
	{
		get
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_出租云)
			{
				return false;
			}
			return true;
		}
	}

	public bool _IsOk_AK
	{
		get
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_AK)
			{
				return false;
			}
			return true;
		}
	}

	public bool _IsOk_聚形散气
	{
		get
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_聚形散气)
			{
				return false;
			}
			if (!_mIsOk_聚形散气)
			{
				EventArgs_Player状态提示.Trigger("冷却中...", _Mono组件._TipsUI);
				return false;
			}
			if (_Curr法力 < MyDataManager.F_Get_Player_技能(102)._法力消耗)
			{
				EventArgs_Player状态提示.Trigger("法力不足!", _Mono组件._TipsUI);
				return false;
			}
			return true;
		}
	}

	public bool _IsOk_身外身法
	{
		get
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_身外身法)
			{
				return false;
			}
			if (!_mIsOk_身外身法)
			{
				EventArgs_Player状态提示.Trigger("冷却中...", _Mono组件._TipsUI);
				return false;
			}
			if (_Curr法力 < MyDataManager.F_Get_Player_技能(104)._法力消耗)
			{
				EventArgs_Player状态提示.Trigger("法力不足!", _Mono组件._TipsUI);
				return false;
			}
			return true;
		}
	}

	public bool _IsOk_化形
	{
		get
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断当前化形是否已经解锁())
			{
				return false;
			}
			if (!_mIsOk_化形)
			{
				EventArgs_Player状态提示.Trigger("冷却中...", _Mono组件._TipsUI);
				return false;
			}
			return true;
		}
	}

	public bool _IsOk_精魄
	{
		get
		{
			int 当前使用精魄 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用精魄;
			if (!DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_精魄.Contains(当前使用精魄))
			{
				return false;
			}
			if (!_mIsOk_精魄)
			{
				EventArgs_Player状态提示.Trigger("冷却中...", _Mono组件._TipsUI);
				return false;
			}
			return true;
		}
	}

	public float _默认受击时长 => _m默认受击时长;

	public int _Curr等级
	{
		get
		{
			return _mCurr等级;
		}
		set
		{
			_mCurr等级 = Mathf.Clamp(value, 0, _mMax等级);
			DSB_MonoSingleton<MyDataManager>.Instance._玩家等级 = _mCurr等级;
			F_初始化属性(游戏中升级: true);
			F_触发所有事件();
			EventArgs_Player_等级_Change.Trigger(_mCurr等级);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_等级();
		}
	}

	public int _Curr酒
	{
		get
		{
			return _mCurr酒;
		}
		set
		{
			_mCurr酒 = value;
			_mCurr酒 = Mathf.Clamp(_mCurr酒, 0, _mMax酒);
			EventArgs_Player_酒_Change.Trigger(_mCurr酒, 1, _mMax酒);
		}
	}

	public float _Curr生命
	{
		get
		{
			return _mCurr生命;
		}
		set
		{
			_mCurr生命 = value;
			_mCurr生命 = Mathf.Clamp(_mCurr生命, -0.01f, _Max生命);
			EventArgs_Player状态提示_生命过低.Trigger(_mCurr生命 / _Max生命 < 0.2f);
			EventArgs_Player_生命_Change.Trigger(_mCurr生命, 1f, _Max生命);
		}
	}

	public float _Curr法力
	{
		get
		{
			return _mCurr法力;
		}
		set
		{
			_mCurr法力 = value;
			_mCurr法力 = Mathf.Clamp(_mCurr法力, -0.01f, _Max法力);
			EventArgs_Player_法力_Change.Trigger(_mCurr法力, 1f, _Max法力);
		}
	}

	public float _Curr气力
	{
		get
		{
			return _mCurr气力;
		}
		set
		{
			_mCurr气力 = value;
			_气力回转timer = _m气力_回转时间;
			_mCurr气力 = Mathf.Clamp(_mCurr气力, -0.01f, _CurrMax气力);
			EventArgs_Player_气力_Change.Trigger(_mCurr气力, 1f, _CurrMax气力, _Max气力);
		}
	}

	public float _CurrMax气力 => _Max气力 * _mCurr气力上限百分比;

	public float _Curr当前气力上限百分比
	{
		get
		{
			return _mCurr气力上限百分比;
		}
		set
		{
			_mCurr气力上限百分比 = Mathf.Clamp(value, 0.1f, 666f);
			_mCurr气力 = Mathf.Clamp(_mCurr气力, -0.01f, _CurrMax气力);
			EventArgs_Player_气力_Change.Trigger(_mCurr气力, -1f, _CurrMax气力, _Max气力);
		}
	}

	public float _Curr蓄力
	{
		get
		{
			return _mCurr蓄力;
		}
		set
		{
			_mCurr蓄力 = value;
			_mCurr蓄力 = Mathf.Clamp(_mCurr蓄力, -0.01f, _mMax蓄力);
			if (_mCurr蓄力 + 0.01f >= _mMax蓄力 && _Curr豆子 != F_获取当前豆子上限())
			{
				_Curr豆子++;
				if (_Curr豆子 != F_获取当前豆子上限())
				{
					_mCurr蓄力 = 0f;
				}
			}
			EventArgs_Player_蓄力_Change.Trigger(_mCurr蓄力, 1f, _mMax蓄力);
		}
	}

	public int _Curr豆子
	{
		get
		{
			if (GameManager._3豆状态)
			{
				return 3;
			}
			return _mCurr豆子;
		}
		set
		{
			if (GameManager._3豆状态)
			{
				value = 3;
			}
			if (_mCurr豆子 != value)
			{
				int num = F_获取当前豆子上限();
				if (num == _mCurr豆子 && value == num - 1)
				{
					_Curr蓄力 = 0f;
				}
				_mCurr豆子 = value;
				_mCurr豆子 = Mathf.Clamp(_mCurr豆子, 0, num);
				if (_mCurr豆子 == 0)
				{
					_mMax蓄力 = _m棍势_一段;
				}
				else if (num > 1 && _mCurr豆子 == 1)
				{
					_mMax蓄力 = _m棍势_二段;
				}
				else if (num > 2 && _mCurr豆子 == 2)
				{
					_mMax蓄力 = _m棍势_三段;
				}
				if (_mCurr豆子 == num)
				{
					_mCurr蓄力 = _mMax蓄力;
					EventArgs_Player_蓄力_Change.Trigger(_mCurr蓄力, 1f, _mMax蓄力);
				}
				EventArgs_Player_豆子_Change.Trigger(_mCurr豆子, 1, 3);
			}
		}
	}

	public Player_StateData(PlayerController mono)
	{
		_mMono = mono;
		_ = _Mono组件 == null;
	}

	public void F_设置技能CD(int id, float time)
	{
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(id);
		if (id == _定身术TimerID)
		{
			MyLog.Log($"定身术CD:{time}");
			_mIsOk_定身术 = false;
			DSB_MonoSingleton<UIManager>.Instance.F_UI_定身术CD(time);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, id, time, delegate
			{
				_mIsOk_定身术 = true;
			});
		}
		else if (id == _聚形散气TimerID)
		{
			MyLog.Log($"聚形散气CD:{time}");
			_mIsOk_聚形散气 = false;
			DSB_MonoSingleton<UIManager>.Instance.F_UI_聚形散气CD(time);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, id, time, delegate
			{
				_mIsOk_聚形散气 = true;
			});
		}
		else if (id == _身外身法TimerID)
		{
			MyLog.Log($"身外身法CD:{time}");
			_mIsOk_身外身法 = false;
			DSB_MonoSingleton<UIManager>.Instance.F_UI_身外身法CD(time);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, id, time, delegate
			{
				_mIsOk_身外身法 = true;
			});
		}
		else if (id == _化形TimerID)
		{
			MyLog.Log($"化形CD:{time}");
			_mIsOk_化形 = false;
			DSB_MonoSingleton<UIManager>.Instance.F_UI_化形CD(time);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, id, time, delegate
			{
				_mIsOk_化形 = true;
			});
		}
		else if (id == _精魄TimerID)
		{
			MyLog.Log($"精魄CD:{time}");
			_mIsOk_精魄 = false;
			DSB_MonoSingleton<UIManager>.Instance.F_UI_精魄CD(time);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, id, time, delegate
			{
				_mIsOk_精魄 = true;
			});
		}
	}

	public void F_重生技能CD()
	{
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(_定身术TimerID);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(_聚形散气TimerID);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(_身外身法TimerID);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(_化形TimerID);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(_精魄TimerID);
		_mIsOk_定身术 = false;
		_mIsOk_聚形散气 = false;
		_mIsOk_身外身法 = false;
		_mIsOk_化形 = false;
		_mIsOk_精魄 = false;
		float num = 1f;
		DSB_MonoSingleton<UIManager>.Instance.F_UI_定身术CD(num);
		DSB_MonoSingleton<UIManager>.Instance.F_UI_聚形散气CD(num);
		DSB_MonoSingleton<UIManager>.Instance.F_UI_身外身法CD(num);
		DSB_MonoSingleton<UIManager>.Instance.F_UI_化形CD(num);
		DSB_MonoSingleton<UIManager>.Instance.F_UI_精魄CD(num);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, _定身术TimerID, num, delegate
		{
			_mIsOk_定身术 = true;
		});
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, _聚形散气TimerID, num, delegate
		{
			_mIsOk_聚形散气 = true;
		});
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, _身外身法TimerID, num, delegate
		{
			_mIsOk_身外身法 = true;
		});
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, _化形TimerID, num, delegate
		{
			_mIsOk_化形 = true;
		});
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, _精魄TimerID, num, delegate
		{
			_mIsOk_精魄 = true;
		});
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		if (_名刀司命)
		{
			return;
		}
		if (_Is免疫远程 && attackInfo.atk_Is远程)
		{
			_Curr气力 -= 5f;
			MyLog.LogError("格挡弓箭");
			UnityEngine.Object.Instantiate(_Mono组件._特效_火花Prefab, attackInfo.atk_Pos, Quaternion.identity).F_Init(((Vector2)_Mono组件.transform.position - attackInfo.atk_Pos).normalized);
			return;
		}
		_Mono组件._Rb2D.drag = 5f;
		_Mono._BeAttack_Pos = attackInfo.atk_Pos;
		_Mono._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (attackInfo.atk_可以被闪避 && _可以识破)
		{
			if (!_成功识破)
			{
				_Mono组件._PS_识破特效.Play();
				_成功识破 = true;
			}
		}
		else if (attackInfo.atk_可以被闪避 && _触发完美闪避)
		{
			_Mono.ChangeToState(Player_StateName_Type.完美闪避);
			_Curr豆子++;
		}
		else
		{
			if (!_可以受击)
			{
				return;
			}
			if (attackInfo.atk_强制受击)
			{
				f_受击();
			}
			else
			{
				if (_化形状态)
				{
					return;
				}
				if (_释放蓄力状态)
				{
					_Curr生命 -= attackInfo.atk_生命 * 蓄力状态承受伤害;
					if (_Curr生命 <= 0f)
					{
						_Mono.ChangeToState(Player_StateName_Type.死亡);
					}
				}
				else if (_Is聚形散气)
				{
					_Curr生命 -= attackInfo.atk_生命 * 影身状态承受伤害;
					if (_Curr生命 <= 0f)
					{
						_Mono.ChangeToState(Player_StateName_Type.死亡);
					}
				}
				else
				{
					f_受击();
				}
			}
		}
		void f_受击()
		{
			_Curr生命 -= attackInfo.atk_生命;
			if (_Curr生命 > 0f)
			{
				MyLog.LogError("atk_有僵直=" + attackInfo.atk_有僵直);
				if (attackInfo.atk_有僵直)
				{
					if (attackInfo.atk_受击时长 <= 0.05f)
					{
						_本次受击时长 = _默认受击时长;
					}
					else
					{
						_本次受击时长 = attackInfo.atk_受击时长;
					}
					_本次硬直是虚弱动作 = attackInfo.atk_硬直是虚弱动作;
					_Mono.ChangeToState(Player_StateName_Type.受击);
				}
			}
			else
			{
				_Mono.ChangeToState(Player_StateName_Type.死亡);
				EventArgs_Player_死亡.Trigger();
			}
		}
	}

	public void F_初始化组件()
	{
		_Mono组件._Player_蓄力特效.F_Init(this);
		_Mono组件._C_攻击判断._阵营 = _Mono._阵营;
		_Dict_AniName_time = new MyDictionary<string, float>();
		AnimationClip[] animationClips = _Mono组件._Ani.runtimeAnimatorController.animationClips;
		foreach (AnimationClip animationClip in animationClips)
		{
			_Dict_AniName_time.TryAdd(animationClip.name, animationClip.length);
		}
		_Mono._FSM.OnStateChange += delegate(Player_StateName_Type a, Player_StateName_Type b)
		{
			当前状态_仅显示 = b;
		};
	}

	public void F_初始化属性(bool 游戏中升级 = false)
	{
		_释放豆子 = 0;
		_触发完美闪避 = false;
		_可以受击 = true;
		_释放蓄力状态 = false;
		_可以识破 = false;
		_能保留豆子 = false;
		_成功识破 = false;
		_Is死亡 = false;
		_Is聚形散气 = false;
		_重攻击衔接切手技 = false;
		_Is腾空 = false;
		_已经触发四姐名刀 = false;
		_名刀司命 = false;
		_mCurr气力上限百分比 = 1f;
		_State_攻击连段_判定 = Player_StateName_Type.轻棍1;
		_State_攻击连段_判定Timer = 0f;
		SOData_Player sOData_Player = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player;
		_mMax等级 = sOData_Player._List_Player等级.Count - 1;
		_mCurr等级 = DSB_MonoSingleton<MyDataManager>.Instance._玩家等级;
		player_BuffInfo_永久 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player_BuffInfo_永久;
		_mCurr等级 = Mathf.Clamp(_mCurr等级, 0, _mMax等级);
		_mMax酒 = sOData_Player.葫芦_数量;
		_m葫芦_恢复 = sOData_Player.葫芦_恢复;
		_m棍势_一段 = sOData_Player.棍势_一段;
		_m棍势_二段 = sOData_Player.棍势_二段;
		_m棍势_三段 = sOData_Player.棍势_三段;
		_m气力_恢复速度 = sOData_Player.气力_恢复速度;
		_m气力_回转时间 = sOData_Player.气力_回转时间;
		_第二豆等级 = sOData_Player.第二个豆等级;
		_第三豆等级 = sOData_Player.第三个豆等级;
		_m蓄力状态承受伤害 = sOData_Player.蓄力状态承受伤害;
		影身状态承受伤害 = sOData_Player.影身状态承受伤害;
		DataInfo_Player等级 dataInfo_Player等级 = sOData_Player._List_Player等级[_mCurr等级];
		F_计算加点加成();
		_mMax下一级所需经验 = dataInfo_Player等级._下一级所需经验;
		_mCurr生命 = _Max生命;
		_mCurr法力 = _Max法力;
		_mCurr气力 = _Max气力;
		_mMax蓄力 = _m棍势_一段;
		_mCurr蓄力 = 0f;
		if (!游戏中升级)
		{
			_朝向右边 = true;
			_Mono.transform.localScale = new Vector3(1f, 1f, 1f);
			_mCurr蓄力 = 0f;
			_mCurr豆子 = 0;
			_mCurr经验 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player经验;
			_mCurr酒 = _mMax酒;
		}
	}

	public void F_计算加点加成()
	{
		SOData_Player sOData_Player = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player;
		DataInfo_Player等级 dataInfo_Player等级 = sOData_Player._List_Player等级[_mCurr等级];
		if (Lunch._开启旧升级_等级数值)
		{
			_mMax生命 = dataInfo_Player等级._生命 * DSB_MonoSingleton<MyDataManager>.Instance._已三连;
			_mMax法力 = dataInfo_Player等级._法力;
			_mMax气力 = dataInfo_Player等级._气力;
			_m攻击力 = dataInfo_Player等级._攻击力;
			_蓄力额外速度 = dataInfo_Player等级._蓄力额外速度;
		}
		else
		{
			DataInfo_Player等级 dataInfo_Player等级2 = sOData_Player._List_Player等级[0];
			_mMax生命 = dataInfo_Player等级2._生命 * DSB_MonoSingleton<MyDataManager>.Instance._已三连;
			_mMax法力 = dataInfo_Player等级2._法力;
			_mMax气力 = dataInfo_Player等级2._气力;
			_m攻击力 = dataInfo_Player等级2._攻击力;
			_蓄力额外速度 = dataInfo_Player等级2._蓄力额外速度;
		}
		_mMax酒 = sOData_Player.葫芦_数量;
		_m葫芦_恢复 = sOData_Player.葫芦_恢复;
		if (Lunch._开启新升级系统)
		{
			_m攻击力 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1001);
			_mMax生命 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1002);
			_mMax法力 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1003);
			_mMax气力 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1004);
			_蓄力额外速度 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1301);
			_mMax酒 += Mathf.RoundToInt(DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1802));
			_m葫芦_恢复 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1801);
		}
	}

	public void F_加点后调用()
	{
		if (_mCurr法力 > _Max法力)
		{
			_mCurr法力 = _Max法力;
		}
		if (_mCurr生命 > _Max生命)
		{
			_mCurr生命 = _Max生命;
		}
		if (_mCurr气力 > _Max气力)
		{
			_mCurr气力 = _Max气力;
		}
		F_触发所有事件();
	}

	public void F_触发所有事件()
	{
		EventArgs_Player_等级_Change.Trigger(_mCurr等级);
		EventArgs_Player_酒_Change.Trigger(_mCurr酒, 1, _mMax酒);
		EventArgs_Player_生命_Change.Trigger(_mCurr生命, 1f, _Max生命);
		EventArgs_Player_法力_Change.Trigger(_mCurr法力, 1f, _Max法力);
		EventArgs_Player_气力_Change.Trigger(_mCurr气力, 1f, _CurrMax气力, _Max气力);
		EventArgs_Player_蓄力_Change.Trigger(_mCurr蓄力, 1f, _mMax蓄力);
		EventArgs_Player_豆子_Change.Trigger(_mCurr豆子, 1, 3);
		EventArgs_Player_经验_Change.Trigger(_mCurr经验, 0f, _mMax下一级所需经验);
		EventArgs_Player_豆子_Change.Trigger(_mCurr豆子, 1, 3);
		EventArgs_Player状态提示_生命过低.Trigger(_mCurr生命 / _Max生命 < 0.2f);
		EventArgs_Player_化形_生命_Change.Trigger(1f, 1f, 1f);
		EventArgs_Player_化形_神力_Change.Trigger(1f, 1f, 1f);
		EventArgs_Player_化形_蓄力_Change.Trigger(0f, 1f, 1f);
	}

	private int F_获取当前豆子上限()
	{
		if (_Curr等级 >= _第三豆等级)
		{
			return 3;
		}
		if (_Curr等级 >= _第二豆等级)
		{
			return 2;
		}
		return 1;
	}

	public void F_增加经验(float val)
	{
		if (_Curr等级 != _mMax等级)
		{
			_mCurr经验 += val;
			while (_mCurr经验 >= _mMax下一级所需经验)
			{
				MyLog.LogError("======升级======");
				_mCurr经验 -= _mMax下一级所需经验;
				_Curr等级++;
			}
			EventArgs_Player_经验_Change.Trigger(_mCurr经验, val, _mMax下一级所需经验);
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player经验 = _mCurr经验;
			DSB_MonoSingleton<MyDataManager>.Instance.F_保存数据();
		}
	}

	public void OnUpdate()
	{
		if (_气力回转timer <= 0f)
		{
			if (_mCurr气力 < _CurrMax气力)
			{
				_mCurr气力 += Time.deltaTime * (_m气力_恢复速度 + F_猪八戒额外气力恢复速度());
				_mCurr气力 = Mathf.Clamp(_mCurr气力, 0f, _CurrMax气力);
				EventArgs_Player_气力_Change.Trigger(_mCurr气力, 1f, _CurrMax气力, _Max气力);
			}
		}
		else
		{
			_气力回转timer -= Time.deltaTime;
		}
	}

	public bool F_可以触发四姐名刀()
	{
		if (_Mono._FSM.lastState == Player_StateName_Type.打飞机)
		{
			return false;
		}
		if (_Mono._FSM.lastState == Player_StateName_Type.下地狱100层)
		{
			return false;
		}
		if (_Mono._FSM.lastState == Player_StateName_Type.划沙)
		{
			return false;
		}
		if (!_已经触发四姐名刀 && DSB_MonoSingleton<MyDataManager>.Instance.F_判断同伴是否解锁并且使用(3002))
		{
			return true;
		}
		return false;
	}

	private float F_猪八戒额外气力恢复速度()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断同伴是否解锁并且使用(3001))
		{
			return 猪八戒猪八戒被动._气力消耗;
		}
		return 0f;
	}
}
