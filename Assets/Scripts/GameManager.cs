using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class GameManager : DSB_MonoSingleton<GameManager>
{
	private bool _可以生成同伴 = true;

	public float _SaveTimeTimer = 10f;

	public bool _只读_暂停游戏;

	public bool _只读_CanPlay;

	private PlayerController _mPC;

	public static bool _10倍攻击;

	public static bool _10倍移速;

	public static bool _3豆状态;

	public static bool _免疫伤害;

	public static bool _Can上香 = true;

	public static bool _Can传送 = true;

	public static bool _传送动画结束 = false;

	public static bool _开启时间积累 = false;

	[Obsolete]
	public NPCAI_同伴 _m_NPCAI_猪八戒;

	[Obsolete]
	public NPCAI_同伴 _m_NPCAI_四姐;

	public static bool _暂停游戏 { get; set; } = true;


	public static bool _CanPlay { get; set; } = false;


	public PlayerController _PC
	{
		get
		{
			if (_mPC == null)
			{
				_mPC = UnityEngine.Object.Instantiate(Resources.Load<PlayerController>("Player"), base.transform).Init();
			}
			return _mPC;
		}
		set
		{
			_mPC = value;
		}
	}

	private void Start()
	{
		MyLog.Log("设备模型：" + SystemInfo.deviceModel);
		MyLog.Log("设备名称：" + SystemInfo.deviceName);
		MyLog.Log("设备类型：" + SystemInfo.deviceType);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_开始游戏._EventID, F_开始游戏Args);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_结束._EventID, F_EventArgs_切换地图_结束Args);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_特殊场景._EventID, F_特殊场景);
	}

	private void F_EventArgs_切换地图_结束Args(DSB_EventArgs args)
	{
		F_生成同伴();
	}

	public void F_特殊场景(DSB_EventArgs args)
	{
		EventArgs_特殊场景 eventArgs_特殊场景 = args as EventArgs_特殊场景;
		_可以生成同伴 = !eventArgs_特殊场景._是进入;
	}

	private void F_开始游戏Args(DSB_EventArgs args)
	{
		_可以生成同伴 = true;
	}

	private void Update()
	{
		_只读_暂停游戏 = _暂停游戏;
		_只读_CanPlay = _CanPlay;
		if (_CanPlay)
		{
			if (_开启时间积累)
			{
				DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._该存档游玩时间 += Time.deltaTime;
			}
			if (_SaveTimeTimer >= 0f)
			{
				_SaveTimeTimer -= Time.deltaTime;
				return;
			}
			_SaveTimeTimer = 5f;
			DSB_MonoSingleton<MyDataManager>.Instance.F_保存数据();
		}
	}

	public Transform F_EnemyGetPlayerPos()
	{
		return _PC.transform;
	}

	public PlayerController F_显示玩家()
	{
		return _PC;
	}

	public void F_获得经验(float val)
	{
		_PC._Data.F_增加经验(val);
	}

	public void F_敲钟(int 铜钟ID)
	{
		MyLog.LogError("敲钟");
		DSB_MonoSingleton<MyDataManager>.Instance.F_敲钟(铜钟ID);
	}

	public void F_上香()
	{
		MyLog.LogError($"_Can上香<<{_Can上香}");
		if (_CanPlay && _Can上香)
		{
			MapController mapController = DSB_MonoSingleton<MapManager>.Instance.F_得到当前地图();
			F_上香(mapController._MapID);
		}
	}

	public void F_上香(int mapID, bool 需要提示 = true)
	{
		if (_CanPlay || _Can上香)
		{
			_Can上香 = false;
			DSB_MonoSingleton<MyDataManager>.Instance.F_上香(mapID);
			DSB_MonoSingleton<MapManager>.Instance.F_传入地图ID获得大小地图名字(mapID, out var 大地图名字, out var 小地图名字);
			EventArgs_上香.Trigger(大地图名字, 小地图名字, 需要提示);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_上香();
			F_重生();
		}
	}

	public void F_重生()
	{
		DSB_MonoSingleton<UIManager>.Instance.F_重生();
		EventArgs_Player重生.Trigger();
	}

	public void F_开箱(int 物品ID)
	{
		DataInfo_Porp dataInfo_Porp = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Prop.F_Porp_ID_Get_Info(物品ID);
		if (dataInfo_Porp == null)
		{
			MyLog.LogError($"没有找到物品ID<<{物品ID}");
			return;
		}
		MyLog.LogError($"获得物品ID<<{物品ID}");
		EventArgs_道具提示.Trigger(dataInfo_Porp);
		if (dataInfo_Porp._效果ID == 101)
		{
			F_获得经验(dataInfo_Porp._效果Val);
			return;
		}
		if (dataInfo_Porp._效果ID == 102)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player_BuffInfo_永久._攻击力 += dataInfo_Porp._效果Val;
		}
		else if (dataInfo_Porp._效果ID == 103)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player_BuffInfo_永久._生命值 += dataInfo_Porp._效果Val;
		}
		else if (dataInfo_Porp._效果ID == 104)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player_BuffInfo_永久._法力值 += dataInfo_Porp._效果Val;
		}
		else if (dataInfo_Porp._效果ID == 105)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player_BuffInfo_永久._气力值 += dataInfo_Porp._效果Val;
		}
		switch (物品ID)
		{
		case 1001:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得醒酒石 = true;
			break;
		case 1002:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得金片 = true;
			break;
		case 1003:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得石之厉 = true;
			break;
		case 102:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_102++;
			break;
		case 103:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_103++;
			break;
		case 104:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_104++;
			break;
		case 105:
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_105++;
			break;
		}
		DSB_MonoSingleton<MyDataManager>.Instance.F_保存数据();
	}

	public void F_喝假酒()
	{
		_PC._Data._Curr酒++;
		_PC._Data._Curr法力 += 66f;
		_PC.F_喝酒();
	}

	public void F_打坐(int 打坐点ID, Vector2 pos)
	{
		if (_CanPlay)
		{
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断并保存该打坐点(打坐点ID))
			{
				_PC._Data.F_增加经验(_PC._Data._mMax下一级所需经验);
			}
			_mPC.F_打坐(pos);
		}
	}

	public void F_对话结束回调(int 对话类型ID, int 对话回调事件ID)
	{
		MyLog.LogError($"对话类型>>{(Enum_对话)对话类型ID}----对话回调事件>>{(Enum_对话回调事件)对话回调事件ID}");
		switch (对话回调事件ID)
		{
		case 1:
			F_解锁_定身术();
			break;
		case 4:
			F_解锁_聚形散气();
			break;
		case 2:
			F_解锁技能_化形(2001);
			break;
		case 5:
			F_解锁_身外身法();
			break;
		case 3:
			F_解锁技能_精魄(1005);
			break;
		case 6:
			F_解锁_出租云();
			break;
		case 7:
			F_解锁_AK();
			break;
		case 9:
			F_解锁同伴(3001);
			break;
		case 10:
			F_解锁同伴(3002);
			break;
		case 11:
			F_解锁技能_化形(2002);
			break;
		case 12:
			F_解锁技能_化形(2003);
			break;
		}
		DSB_MonoSingleton<MyDataManager>.Instance.F_完成对话(对话类型ID);
	}

	public void F_解锁_定身术()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_定身术 = true;
		_PC._Data.F_设置技能CD(_PC._Data._定身术TimerID, 1f);
	}

	public void F_解锁_聚形散气()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_聚形散气 = true;
		_PC._Data.F_设置技能CD(_PC._Data._聚形散气TimerID, 1f);
	}

	public void F_解锁技能_化形(int 技能ID)
	{
		if (!DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_存在该ID技能(技能ID))
		{
			MyLog.LogError($"不存在技能ID>>{技能ID}");
			return;
		}
		List<int> list_已解锁_变身 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_变身;
		if (list_已解锁_变身.Count <= 0)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用变身 = 技能ID;
		}
		if (!list_已解锁_变身.Contains(技能ID))
		{
			list_已解锁_变身.Add(技能ID);
		}
		_PC._Data.F_设置技能CD(_PC._Data._化形TimerID, 1f);
	}

	public void F_解锁_身外身法()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_身外身法 = true;
		_PC._Data.F_设置技能CD(_PC._Data._身外身法TimerID, 1f);
	}

	public void F_解锁技能_精魄(int 技能ID)
	{
		if (!DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_存在该ID技能(技能ID))
		{
			MyLog.LogError($"不存在技能ID>>{技能ID}");
			return;
		}
		List<int> list_已解锁_精魄 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_精魄;
		if (list_已解锁_精魄.Count <= 0)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用精魄 = 技能ID;
		}
		if (!list_已解锁_精魄.Contains(技能ID))
		{
			list_已解锁_精魄.Add(技能ID);
		}
		DataInfo_Player技能 dataInfo_Player技能 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(技能ID);
		EventArgs_Tips_获得精魄.Trigger(dataInfo_Player技能._技能图标, dataInfo_Player技能._技能Name);
		_PC._Data.F_设置技能CD(_PC._Data._精魄TimerID, 1f);
	}

	public void F_解锁_出租云()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_出租云 = true;
		List<int> list_所有上香的地图ID = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_所有上香的地图ID;
		list_所有上香的地图ID.TryAdd(101);
		list_所有上香的地图ID.TryAdd(104);
		list_所有上香的地图ID.TryAdd(105);
		list_所有上香的地图ID.TryAdd(107);
		list_所有上香的地图ID.TryAdd(110);
		list_所有上香的地图ID.TryAdd(201);
		list_所有上香的地图ID.TryAdd(202);
		DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main._MyBtn_电话?.gameObject.SetActive(value: true);
	}

	public void F_解锁_AK()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_AK = true;
		DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main._MyBtn_AK?.gameObject.SetActive(value: true);
	}

	public bool F_解锁同伴(int 同伴id)
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断是否解锁同伴(同伴id))
		{
			MyLog.LogWarning("已经解锁该同伴>>" + 同伴id);
			return false;
		}
		if (!DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_存在该ID技能(同伴id))
		{
			MyLog.LogWarning("未找到该同伴ID>>" + 同伴id);
			return false;
		}
		List<int> list_已解锁_同伴 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_同伴;
		if (list_已解锁_同伴.Count <= 0)
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用同伴 = 同伴id;
		}
		if (!list_已解锁_同伴.Contains(同伴id))
		{
			list_已解锁_同伴.Add(同伴id);
		}
		return true;
	}

	public bool F_解锁并生成同伴(int 同伴id, Vector2? pos = null, bool 生成烟雾 = false, bool 强制生成 = false, bool 更改朝向 = false)
	{
		bool flag = F_解锁同伴(同伴id);
		if (!强制生成)
		{
			if (!flag)
			{
				return false;
			}
			if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_同伴.Count >= 2)
			{
				return false;
			}
		}
		F_生成同伴(pos, 生成烟雾, 更改朝向);
		return true;
	}

	public void F_生成同伴(Vector2? pos = null, bool 生成烟雾 = false, bool 更改朝向 = false)
	{
		if (!_可以生成同伴)
		{
			return;
		}
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断当前同伴是否解锁())
		{
			if (!pos.HasValue)
			{
				pos = (Vector2)_PC.transform.position + UnityEngine.Random.insideUnitCircle;
			}
			int 当前使用同伴 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用同伴;
			NPCAI_同伴 nPCAI_同伴 = null;
			switch (当前使用同伴)
			{
			case 3001:
				nPCAI_同伴 = UnityEngine.Object.Instantiate(Resources.Load<NPCAI_同伴>("NPCAI_猪八戒"), pos.Value, Quaternion.identity);
				break;
			case 3002:
				nPCAI_同伴 = UnityEngine.Object.Instantiate(Resources.Load<NPCAI_同伴>("NPCAI_四姐"), pos.Value, Quaternion.identity);
				break;
			case 3003:
				nPCAI_同伴 = UnityEngine.Object.Instantiate(Resources.Load<NPCAI_同伴>("NPCAI_四姐"), pos.Value, Quaternion.identity);
				break;
			default:
				MyLog.LogWarning($"当前同伴ID<<{当前使用同伴}>>未定义,无法生成同伴");
				break;
			}
			if (nPCAI_同伴 != null)
			{
				if (更改朝向)
				{
					nPCAI_同伴.F_玩家转向(-1f);
				}
				nPCAI_同伴.F_仅强制切换到站立状态();
				if (生成烟雾)
				{
					nPCAI_同伴.F_生成烟雾特效();
				}
			}
		}
		else
		{
			MyLog.LogWarning("当前同伴未解锁,无法生成同伴");
		}
	}
}
