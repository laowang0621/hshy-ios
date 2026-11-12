using System;
using DSB;
using UnityEngine;

public class SOData_PlayerLevel : ScriptableObject
{
	public MyDictionary<int, DataInfo_PlayerLevel_Buff> _Dict_PlayerLevel_Info = new MyDictionary<int, DataInfo_PlayerLevel_Buff>();

	public MyDictionary<int, DataInfo_PlayerLevel_Btn> _Dict_PlayerLevel_Info_Btn = new MyDictionary<int, DataInfo_PlayerLevel_Btn>();

	public int F_Get_BtnID_最大点数(int btnID)
	{
		if (_Dict_PlayerLevel_Info_Btn.ContainsKey(btnID))
		{
			return _Dict_PlayerLevel_Info_Btn[btnID]._Btn_等级上限;
		}
		throw new Exception("没有找到对应的BtnID");
	}

	public DataInfo_PlayerLevel_Buff F_Geto_PlayerLevel_Buff(int buffID)
	{
		if (_Dict_PlayerLevel_Info.ContainsKey(buffID))
		{
			return _Dict_PlayerLevel_Info[buffID];
		}
		throw new Exception("没有找到对应的buffID");
	}

	public DataInfo_PlayerLevel_Btn F_Geto_PlayerLevel_Btn(int btnID)
	{
		if (_Dict_PlayerLevel_Info_Btn.ContainsKey(btnID))
		{
			return _Dict_PlayerLevel_Info_Btn[btnID];
		}
		throw new Exception("没有找到对应的BtnID");
	}
}
