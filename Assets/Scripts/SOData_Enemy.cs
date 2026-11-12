using System.Collections.Generic;
using UnityEngine;

public class SOData_Enemy : ScriptableObject
{
	public List<DataInfo_Enemy基础数值> _List_Enemy基础数值;

	public List<DataInfo_Enemy招式数值> _List_Enemy招式数值;

	public DataInfo_Enemy基础数值 F_基础数值(int EnemyID)
	{
		foreach (DataInfo_Enemy基础数值 item in _List_Enemy基础数值)
		{
			if (item._EnemyID == EnemyID)
			{
				return item;
			}
		}
		MyLog.LogError($"未找到<<{EnemyID}>>");
		return _List_Enemy基础数值[0];
	}

	public DataInfo_Enemy招式数值 F_招式数值(int Enemy招式ID)
	{
		foreach (DataInfo_Enemy招式数值 item in _List_Enemy招式数值)
		{
			if (item._Enemy招式ID == Enemy招式ID)
			{
				return item;
			}
		}
		MyLog.LogError($"未找到<<{Enemy招式ID}>>");
		return _List_Enemy招式数值[0];
	}
}
