using System;
using System.Collections.Generic;
using UnityEngine;

public class SOData_Map : ScriptableObject
{
	public List<DataInfo_Map> _List_Map_Infp;

	public List<string> _List_章节地图Name;

	public string F_Map_ID_Get_Name(int mapID)
	{
		foreach (DataInfo_Map item in _List_Map_Infp)
		{
			if (mapID == item._MapID)
			{
				return item._MapName;
			}
		}
		throw new Exception($"没找到该ID对应的地图<<{mapID}");
	}

	public MapController F_Get地图(int mapID)
	{
		foreach (DataInfo_Map item in _List_Map_Infp)
		{
			_ = item._MapID;
		}
		throw new Exception($"没找到该ID对应的地图<<{mapID}");
	}

	public string F_Get地图Path(int mapID)
	{
		foreach (DataInfo_Map item in _List_Map_Infp)
		{
			if (mapID == item._MapID)
			{
				return item._Path_MapC_Prefab;
			}
		}
		throw new Exception($"没找到该ID对应的地图<<{mapID}");
	}
}
