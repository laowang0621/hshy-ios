using System;
using System.Collections.Generic;
using DSB;

[Serializable]
public class Player_LevelInfo
{
	public MyDictionary<int, int> _Dict_BtnID_点数 = new MyDictionary<int, int>();

	public int _已用技能点数
	{
		get
		{
			int num = 0;
			SOData_PlayerLevel sOData_PlayerLevel = DSB_MonoSingleton<MyDataManager>.Instance._SOData_PlayerLevel;
			foreach (KeyValuePair<int, int> item in _Dict_BtnID_点数)
			{
				if (!sOData_PlayerLevel._Dict_PlayerLevel_Info_Btn.TryGetValue(item.Key, out var value))
				{
					continue;
				}
				for (int i = 0; i < item.Value && i < value._List_BuffID.Length; i++)
				{
					if (sOData_PlayerLevel._Dict_PlayerLevel_Info.TryGetValue(value._List_BuffID[i], out var value2))
					{
						num += value2._每一级消耗点数;
					}
				}
			}
			return num;
		}
	}

	public Player_LevelInfo()
	{
		_Dict_BtnID_点数 = new MyDictionary<int, int>();
	}

	public int F_Get_BtnID_加点点数(int btnID)
	{
		if (!_Dict_BtnID_点数.ContainsKey(btnID))
		{
			_Dict_BtnID_点数.Add(btnID, 0);
		}
		return _Dict_BtnID_点数[btnID];
	}

	public void F_添加点数(int btnID)
	{
		if (_Dict_BtnID_点数.ContainsKey(btnID))
		{
			_Dict_BtnID_点数[btnID]++;
		}
		else
		{
			_Dict_BtnID_点数.Add(btnID, 1);
		}
	}
}
