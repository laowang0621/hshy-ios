using System.Collections.Generic;
using DSB;
using UnityEngine;

public class MyDataManager : DSB_MonoSingleton<MyDataManager>
{
	private const string _PlayerData_Key = "PlayerData-0.2";

	private const string _已三连_Key = "已三连";

	public PlayerItemDataInfo _mData_SL;

	public SOData_Player _SOData_Player;

	public SOData_PlayerLevel _SOData_PlayerLevel;

	public SOData_PlayerOther _SOData_PlayerOther;

	public MyDictionary<int, float> _mDict_PlayerBuff_ID_Val = new MyDictionary<int, float>();

	public SOData_Map _SOData_Map;

	public SOData_Enemy _SOData_Enemy;

	public SOData_对话 _SOData_对话;

	public SOData_Audio _SOData_Audio;

	public SOData_Prop _SOData_Prop;

	private float _m已三连 = 66f;

	private int _m修行点;

	private bool _第一次 = true;

	public float _周目加成 => 1f + (float)_Data_SL._周目 * _SOData_Player.每周目敌人加成;

	public int _修行点
	{
		get
		{
			return _m修行点;
		}
		set
		{
			_m修行点 = value;
			EventArgs_Player_修行点_Change.Trigger(_m修行点);
		}
	}

	public PlayerItemDataInfo _Data_SL
	{
		get
		{
			if (_第一次 || _mData_SL == null)
			{
				_第一次 = false;
				F_读取数据();
			}
			return _mData_SL;
		}
	}

	public int _玩家等级
	{
		get
		{
			if (_Data_SL._Player等级 == -1)
			{
				return _SOData_Player.初始等级;
			}
			return _Data_SL._Player等级;
		}
		set
		{
			_Data_SL._Player等级 = value;
		}
	}

	public float _已三连
	{
		get
		{
			if (_m已三连 == 66f)
			{
				_m已三连 = PlayerPrefs.GetFloat("已三连", 1f);
			}
			return _m已三连;
		}
		set
		{
			_m已三连 = value;
			PlayerPrefs.SetFloat("已三连", _m已三连);
			PlayerPrefs.Save();
		}
	}

	private void Start()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_开始游戏._EventID, F_开始游戏_计算玩家加点);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_等级_Change._EventID, F_玩家升级);
	}

	public void F_开始游戏_计算玩家加点(DSB_EventArgs args)
	{
		_修行点 = _Data_SL._Player等级 - _Data_SL._Player_LevelInfo._已用技能点数;
		_mDict_PlayerBuff_ID_Val.Clear();
		foreach (KeyValuePair<int, int> item in _Data_SL._Player_LevelInfo._Dict_BtnID_点数)
		{
			DataInfo_PlayerLevel_Btn dataInfo_PlayerLevel_Btn = _SOData_PlayerLevel.F_Geto_PlayerLevel_Btn(item.Key);
			for (int i = 0; i < item.Value && i < dataInfo_PlayerLevel_Btn._Btn_等级上限; i++)
			{
				F_添加_PlayerLevel_BuffVal(dataInfo_PlayerLevel_Btn._List_BuffID[i]);
			}
		}
	}

	public float F_Get_PlayerLevel_BuffVal(int buffID)
	{
		if (_mDict_PlayerBuff_ID_Val.ContainsKey(buffID))
		{
			return _mDict_PlayerBuff_ID_Val[buffID];
		}
		return 0f;
	}

	public void F_添加_PlayerLevel_BuffVal(int buffID)
	{
		DataInfo_PlayerLevel_Buff dataInfo_PlayerLevel_Buff = _SOData_PlayerLevel.F_Geto_PlayerLevel_Buff(buffID);
		if (_mDict_PlayerBuff_ID_Val.ContainsKey(buffID))
		{
			_mDict_PlayerBuff_ID_Val[buffID] += dataInfo_PlayerLevel_Buff._每一级数值;
		}
		else
		{
			_mDict_PlayerBuff_ID_Val.Add(buffID, dataInfo_PlayerLevel_Buff._每一级数值);
		}
	}

	public void F_玩家升级(DSB_EventArgs args)
	{
		_修行点 = _Data_SL._Player等级 - _Data_SL._Player_LevelInfo._已用技能点数;
	}

	public void F_重置修行点()
	{
		_Data_SL.F_重置修行点();
		F_开始游戏_计算玩家加点(null);
	}

	public int F_Get_BtnID_加点点数(int btnID)
	{
		return _mData_SL._Player_LevelInfo.F_Get_BtnID_加点点数(btnID);
	}

	public int F_Get_BtnID_最大点数(int btnID)
	{
		return _SOData_PlayerLevel.F_Get_BtnID_最大点数(btnID);
	}

	public bool F_判断该BtnID是否满级(int btnID)
	{
		int num = F_Get_BtnID_加点点数(btnID);
		int num2 = F_Get_BtnID_最大点数(btnID);
		return num < num2;
	}

	public int F_得到下一级所需消耗点数(int btnID)
	{
		int num = F_Get_BtnID_加点点数(btnID);
		int buffID = _SOData_PlayerLevel.F_Geto_PlayerLevel_Btn(btnID)._List_BuffID[num];
		return _SOData_PlayerLevel.F_Geto_PlayerLevel_Buff(buffID)._每一级消耗点数;
	}

	public bool F_添加BtnID点数(int btnID, out bool is修行点足够)
	{
		is修行点足够 = true;
		if (F_判断该BtnID是否满级(btnID))
		{
			int num = F_得到下一级所需消耗点数(btnID);
			MyLog.LogError($"消耗点数<<{num}");
			if (_修行点 >= num)
			{
				_修行点 -= num;
				_mData_SL._Player_LevelInfo.F_添加点数(btnID);
				return true;
			}
			is修行点足够 = false;
		}
		return false;
	}

	public DataInfo_PlayerLevel_Buff F_Get_PlayerLevel_Buff(int btnID)
	{
		int buffID = _SOData_PlayerLevel._Dict_PlayerLevel_Info_Btn[btnID]._List_BuffID[F_Get_BtnID_加点点数(btnID) - 1];
		return _SOData_PlayerLevel.F_Geto_PlayerLevel_Buff(buffID);
	}

	public DataInfo_PlayerLevel_Buff F_Get_PlayerLevel_Buff_预选(int btnID)
	{
		int buffID = _SOData_PlayerLevel._Dict_PlayerLevel_Info_Btn[btnID]._List_BuffID[F_Get_BtnID_加点点数(btnID)];
		return _SOData_PlayerLevel.F_Geto_PlayerLevel_Buff(buffID);
	}

	public void F_读取数据()
	{
		MyLog.Log("读取--PlayerData-0.2");
		if (PlayerPrefs.HasKey("PlayerData-0.2"))
		{
			string @string = PlayerPrefs.GetString("PlayerData-0.2");
			MyLog.Log(@string);
			_mData_SL = JsonUtility.FromJson<PlayerItemDataInfo>(@string);
			if (_mData_SL._解锁_精魄_幽魂 && !_mData_SL._List_已解锁_精魄.Contains(1005))
			{
				_mData_SL._List_已解锁_精魄.Add(1005);
			}
			if (_mData_SL._Dict_精魄解锁状态 != null && _mData_SL._Dict_精魄解锁状态.Count > 0)
			{
				foreach (KeyValuePair<int, bool> item in _mData_SL._Dict_精魄解锁状态)
				{
					if (item.Value && !_mData_SL._List_已解锁_精魄.Contains(item.Key))
					{
						_mData_SL._List_已解锁_精魄.Add(item.Key);
					}
				}
			}
			if (_mData_SL._解锁_化形_广智 && !_mData_SL._List_已解锁_变身.Contains(2001))
			{
				_mData_SL._List_已解锁_变身.Add(2001);
			}
			if (_mData_SL._解锁_猪八戒 && !_mData_SL._List_已解锁_同伴.Contains(3001))
			{
				_mData_SL._List_已解锁_同伴.Add(3001);
			}
			if (_mData_SL._解锁_四姐 && !_mData_SL._List_已解锁_同伴.Contains(3002))
			{
				_mData_SL._List_已解锁_同伴.Add(3002);
			}
			if (F_判断Boss是否已经击败(20302) && !_mData_SL._List_已解锁_同伴.Contains(3001))
			{
				_mData_SL._List_已解锁_同伴.Add(3001);
			}
			if (_mData_SL._List_已解锁_同伴.Contains(3002) && !_mData_SL._List_已解锁_同伴.Contains(3001))
			{
				_mData_SL._List_已解锁_同伴.Add(3001);
			}
			if (F_判断Boss是否已经击败(20401) && !_Data_SL._List_已解锁_变身.Contains(2003))
			{
				DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_化形(2003);
			}
			if (F_判断Boss是否已经击败(20310) && !_Data_SL._List_已解锁_变身.Contains(2002))
			{
				DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_化形(2002);
			}
		}
		else
		{
			_mData_SL = new PlayerItemDataInfo();
		}
	}

	public void F_保存数据()
	{
		if (_mData_SL == null)
		{
			_mData_SL = new PlayerItemDataInfo();
		}
		DSB_MonoSingleton<GameManager>.Instance._SaveTimeTimer = 5f;
		string text = JsonUtility.ToJson(_mData_SL);
		MyLog.Log("保存-PlayerData-0.2-DataInfo:" + text);
		PlayerPrefs.SetString("PlayerData-0.2", text);
		PlayerPrefs.Save();
	}

	public void F_重置数据_再入轮回()
	{
		_mData_SL.F_重置数据_再入轮回();
		F_保存数据();
	}

	public void F_重置数据()
	{
		_mData_SL = new PlayerItemDataInfo();
		F_保存数据();
	}

	public void F_上香(int 上香地图ID)
	{
		MyLog.LogError($"上香地点<<{上香地图ID}>>");
		_Data_SL._List_所有上香的地图ID.TryAdd(上香地图ID);
		_Data_SL._上香地图ID = 上香地图ID;
		F_保存数据();
	}

	public bool F_判断并保存该章节提示(int 章节ID)
	{
		if (_Data_SL._List_章节提示_ID_已经触发.Contains(章节ID))
		{
			return false;
		}
		_Data_SL._List_章节提示_ID_已经触发.Add(章节ID);
		F_保存数据();
		return true;
	}

	public bool F_判断并保存该打坐点(int 打坐点ID)
	{
		if (_Data_SL._List_打坐点_ID_已经打坐.Contains(打坐点ID))
		{
			return false;
		}
		_Data_SL._List_打坐点_ID_已经打坐.Add(打坐点ID);
		F_保存数据();
		return true;
	}

	public bool F_判断对话(int 对话ID)
	{
		if (对话ID == 0)
		{
			MyLog.LogWarning($"对话:{(Enum_对话)对话ID}:0");
			return true;
		}
		if (_Data_SL._List_对话类型_ID_已完成.Contains(对话ID))
		{
			MyLog.LogWarning($"对话:{(Enum_对话)对话ID}:已经完成");
			return true;
		}
		MyLog.LogWarning($"对话:{(Enum_对话)对话ID}:未触发");
		return false;
	}

	public void F_完成对话(int 对话ID)
	{
		_Data_SL._List_对话类型_ID_已完成.TryAdd(对话ID);
		F_保存数据();
	}

	public void F_敲钟(int 钟id)
	{
		if (钟id >= 0 && 钟id < _Data_SL._钟1.Length)
		{
			_Data_SL._钟1[钟id] = true;
			F_保存数据();
		}
		else
		{
			MyLog.LogError($"敲钟失败<<{钟id}>>");
		}
	}

	public bool F_判断Boss是否已经击败(int bossID)
	{
		if (_Data_SL._List_Boss已击败.Contains(bossID))
		{
			return true;
		}
		return false;
	}

	public void F_击败Boss(int bossID)
	{
		MyLog.LogError($"击败Bosss<<{bossID}>>");
		_Data_SL._List_Boss已击败.TryAdd(bossID);
		F_保存数据();
	}

	public bool F_判断宝箱已开启_新(int 宝箱ID)
	{
		if (_Data_SL._List_已开启箱子ID_新.Contains(宝箱ID))
		{
			return true;
		}
		return false;
	}

	public bool F_判断宝箱已开启_旧(int 宝箱ID)
	{
		if (_Data_SL._List_已开启箱子ID_旧.Contains(宝箱ID))
		{
			return true;
		}
		return false;
	}

	public void F_打开宝箱_新(int 宝箱ID)
	{
		_Data_SL._List_已开启箱子ID_新.TryAdd(宝箱ID);
		F_保存数据();
	}

	public void F_打开宝箱_旧(int 宝箱ID)
	{
		_Data_SL._List_已开启箱子ID_新.TryAdd(宝箱ID);
		_Data_SL._List_已开启箱子ID_旧.TryAdd(宝箱ID);
		F_保存数据();
	}

	public void F_保存空降宝箱位置(int 宝箱ID, Vector2 pos)
	{
		_Data_SL._Dict_宝箱位置.TryAdd(宝箱ID, pos);
	}

	public bool F_TryGet空降宝箱位置(int 宝箱ID, out Vector2 宝箱pos)
	{
		if (_Data_SL._Dict_宝箱位置.TryGetValue(宝箱ID, out var value))
		{
			宝箱pos = value;
			return true;
		}
		宝箱pos = Vector2.zero;
		return false;
	}

	public bool F_判断当前化形是否已经解锁()
	{
		return _Data_SL._List_已解锁_变身.Contains(_Data_SL._当前使用变身);
	}

	public bool F_判断是否解锁同伴(int 同伴id)
	{
		return _Data_SL._List_已解锁_同伴.Contains(同伴id);
	}

	public bool F_判断当前同伴是否解锁()
	{
		return _Data_SL._List_已解锁_同伴.Contains(_Data_SL._当前使用同伴);
	}

	public bool F_判断同伴是否解锁并且使用(int 同伴id)
	{
		if (_Data_SL._当前使用同伴 == 同伴id)
		{
			return _Data_SL._List_已解锁_同伴.Contains(_Data_SL._当前使用同伴);
		}
		return false;
	}

	public static DataInfo_Player等级 F_Get_Player_等级数值(int level)
	{
		return DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_等级数值(level);
	}

	public static DataInfo_Player招式 F_Get_Player_招式系数(int 招式ID)
	{
		return DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_招式系数(招式ID);
	}

	public static DataInfo_Player技能 F_Get_Player_技能(int 技能ID)
	{
		return DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(技能ID);
	}
}
