using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

[Serializable]
public class PlayerItemDataInfo
{
	public int _周目 = 1;

	public int _Player等级;

	public float _Player经验;

	public int _上香地图ID = -1;

	public float _该存档游玩时间;

	public bool[] _钟1 = new bool[3];

	public bool _解锁_定身术;

	public bool _解锁_聚形散气;

	[Obsolete]
	public bool _解锁_化形_广智;

	public bool _解锁_身外身法;

	[Obsolete]
	public bool _解锁_精魄_幽魂;

	public int _当前使用精魄 = 1005;

	public int _当前使用变身 = 2001;

	public int _当前使用套装 = 4001;

	public int _当前使用同伴 = 3001;

	[Obsolete]
	public MyDictionary<int, bool> _Dict_精魄解锁状态;

	public bool _解锁_出租云;

	public bool _解锁_AK;

	[Obsolete]
	public bool _解锁_猪八戒;

	[Obsolete]
	public bool _解锁_四姐;

	public List<int> _List_对话类型_ID_已完成 = new List<int>();

	public List<int> _List_章节提示_ID_已经触发 = new List<int>();

	public List<int> _List_打坐点_ID_已经打坐 = new List<int>();

	public List<int> _List_所有上香的地图ID = new List<int>();

	public List<int> _List_已开启箱子ID_旧 = new List<int>();

	public List<int> _List_已开启箱子ID_新 = new List<int>();

	public List<int> _List_Boss已击败 = new List<int>();

	public MyDictionary<int, Vector2> _Dict_宝箱位置 = new MyDictionary<int, Vector2>();

	public Player_BuffInfo _Player_BuffInfo_永久 = new Player_BuffInfo();

	public Player_LevelInfo _Player_LevelInfo = new Player_LevelInfo();

	public int _猪员外_阶段 = 1;

	public bool _获得醒酒石;

	public bool _获得金片;

	public bool _获得石之厉;

	public bool _地窖入口已开启;

	public int _道具_102;

	public int _道具_103;

	public int _道具_104;

	public int _道具_105;

	public List<int> _List_已解锁_精魄 = new List<int>();

	public List<int> _List_已解锁_变身 = new List<int>();

	public List<int> _List_已解锁_同伴 = new List<int>();

	public List<int> _List_已解锁_套装 = new List<int>();

	public PlayerItemDataInfo()
	{
		_周目 = 1;
		_Player等级 = 0;
		_Player经验 = 0f;
		_上香地图ID = -1;
		_该存档游玩时间 = 0f;
		_钟1 = new bool[3];
		_解锁_定身术 = false;
		_解锁_聚形散气 = false;
		_解锁_化形_广智 = false;
		_解锁_身外身法 = false;
		_解锁_精魄_幽魂 = false;
		_当前使用精魄 = 1005;
		_当前使用变身 = 2001;
		_当前使用套装 = 4001;
		_当前使用同伴 = 3001;
		_Dict_精魄解锁状态 = new MyDictionary<int, bool>();
		_解锁_出租云 = false;
		_解锁_AK = false;
		_解锁_猪八戒 = false;
		_解锁_四姐 = false;
		_List_对话类型_ID_已完成 = new List<int>();
		_List_章节提示_ID_已经触发 = new List<int>();
		_List_打坐点_ID_已经打坐 = new List<int>();
		_List_所有上香的地图ID = new List<int>();
		_List_已开启箱子ID_旧 = new List<int>();
		_List_已开启箱子ID_新 = new List<int>();
		_List_Boss已击败 = new List<int>();
		_Dict_宝箱位置 = new MyDictionary<int, Vector2>();
		_Player_BuffInfo_永久 = new Player_BuffInfo();
		_Player_LevelInfo = new Player_LevelInfo();
		_猪员外_阶段 = 1;
		_获得醒酒石 = false;
		_获得金片 = false;
		_获得石之厉 = false;
		_地窖入口已开启 = false;
		_道具_102 = 0;
		_道具_103 = 0;
		_道具_104 = 0;
		_道具_105 = 0;
		_List_已解锁_精魄 = new List<int>();
		_List_已解锁_变身 = new List<int>();
		_List_已解锁_同伴 = new List<int>();
		_List_已解锁_套装 = new List<int>();
	}

	public void F_重置数据_再入轮回()
	{
		_周目++;
		_上香地图ID = -1;
		_List_Boss已击败 = new List<int>();
		_Dict_宝箱位置 = new MyDictionary<int, Vector2>();
		_List_已开启箱子ID_新 = new List<int>();
		_猪员外_阶段 = 1;
		_获得醒酒石 = false;
		_获得金片 = false;
		_获得石之厉 = false;
		_地窖入口已开启 = false;
	}

	public void F_重置修行点()
	{
		_Player_LevelInfo = new Player_LevelInfo();
	}
}
