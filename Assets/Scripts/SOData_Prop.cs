using System;
using System.Collections.Generic;
using UnityEngine;

public class SOData_Prop : ScriptableObject
{
	public List<DataInfo_Porp> _List_Porp_Info;

	public DataInfo_Porp F_Porp_ID_Get_Info(int id)
	{
		foreach (DataInfo_Porp item in _List_Porp_Info)
		{
			if (id == item._PorpID)
			{
				return item;
			}
		}
		throw new Exception($"没找到该ID对应的道具<<{id}");
	}
}
