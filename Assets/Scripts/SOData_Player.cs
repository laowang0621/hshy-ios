using System.Collections.Generic;
using UnityEngine;

public class SOData_Player : ScriptableObject
{
	public int 初始等级;

	public int 葫芦_数量;

	public float 葫芦_恢复;

	public float 棍势_一段;

	public float 棍势_二段;

	public float 棍势_三段;

	public float 气力_恢复速度;

	public float 气力_回转时间;

	public int 第二个豆等级;

	public int 第三个豆等级;

	public float 正常移动速度;

	public float 蓄力移动速度;

	public float 棍花移动速度;

	public float 翻滚速度;

	public float 蓄力状态承受伤害;

	public int 身外身法数量;

	public float 八戒攻击力;

	public float 飞机大战时长;

	public float 影身状态承受伤害;

	public float 每周目敌人加成;

	public List<DataInfo_Player等级> _List_Player等级;

	public List<DataInfo_Player招式> _List_Player招式;

	public List<DataInfo_Player技能> _List_Player技能;

	public DataInfo_Player等级 F_Get_Player_等级数值(int level)
	{
		level = Mathf.Clamp(level, 0, _List_Player等级.Count - 1);
		return _List_Player等级[level];
	}

	public DataInfo_Player招式 F_Get_Player_招式系数(int 招式ID)
	{
		foreach (DataInfo_Player招式 item in _List_Player招式)
		{
			if (item._招式ID == 招式ID)
			{
				return item;
			}
		}
		MyLog.LogError($"为找到类型为为<<{招式ID}>>的招式系数");
		return _List_Player招式[0];
	}

	public DataInfo_Player技能 F_Get_Player_技能(int 技能ID)
	{
		foreach (DataInfo_Player技能 item in _List_Player技能)
		{
			if (item._技能ID == 技能ID)
			{
				return item;
			}
		}
		MyLog.LogError($"为找到类型为为<<{技能ID}>>的技能");
		return _List_Player技能[0];
	}

	public bool F_存在该ID技能(int 技能ID)
	{
		foreach (DataInfo_Player技能 item in _List_Player技能)
		{
			if (item._技能ID == 技能ID)
			{
				return true;
			}
		}
		return false;
	}

	public bool F_TryGet_Player_技能(int 技能ID, out DataInfo_Player技能 dataInfo_Player技能)
	{
		dataInfo_Player技能 = _List_Player技能[0];
		foreach (DataInfo_Player技能 item in _List_Player技能)
		{
			if (item._技能ID == 技能ID)
			{
				dataInfo_Player技能 = item;
				return true;
			}
		}
		return false;
	}
}
