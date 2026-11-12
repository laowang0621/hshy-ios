using System;
using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MapManager : DSB_MonoSingleton<MapManager>
{
	private SOData_Map _mData_Map;

	private Queue<MapController> _Queue_Map = new Queue<MapController>();

	private MapController _Last_MapC;

	public SOData_Map _Data_Map
	{
		get
		{
			if (_mData_Map == null)
			{
				_mData_Map = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Map;
			}
			return _mData_Map;
		}
	}

	public MapController F_Get地图(int mapID)
	{
		foreach (MapController item in _Queue_Map)
		{
			if (item._MapID == mapID)
			{
				return item;
			}
		}
		return null;
	}

	public void F_清空地图缓存()
	{
		while (_Queue_Map.Count > 0)
		{
			UnityEngine.Object.Destroy(_Queue_Map.Dequeue().gameObject);
		}
	}

	public Coroutine F_实例化地图(int mapID)
	{
		return StartCoroutine(Cor_实例化地图(mapID));
	}

	private IEnumerator Cor_实例化地图(int mapID)
	{
		AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(_Data_Map.F_Get地图Path(mapID), base.transform);
		yield return handle;
		MapController component = handle.Result.GetComponent<MapController>();
		component.Init(mapID);
		component.F_隐藏();
		_Queue_Map.Enqueue(component);
		yield return new WaitForEndOfFrame();
		while (_Queue_Map.Count > 5)
		{
			UnityEngine.Object.Destroy(_Queue_Map.Dequeue().gameObject);
		}
		yield return new WaitForEndOfFrame();
	}

	public void F_重置()
	{
		foreach (MapController item in _Queue_Map)
		{
			item.F_重置();
		}
	}

	public void F_传入地图ID获得大小地图名字(int mapID, out string 大地图名字, out string 小地图名字)
	{
		大地图名字 = _Data_Map._List_章节地图Name[mapID / 100];
		小地图名字 = _Data_Map.F_Map_ID_Get_Name(mapID);
	}

	public bool F_加载地图_To_传送点(int mapID, int 传送点ID, bool Is重置, out Vector2 pos)
	{
		MapController mapController = F_Get地图(mapID);
		if (mapController == null)
		{
			pos = Vector2.zero;
			return false;
		}
		F_显示地图(mapController, Is重置);
		pos = mapController.F_Get_传送点(传送点ID);
		return true;
	}

	public bool F_加载地图_To_上香点(int mapID, out Vector2 pos)
	{
		MapController mapController = F_Get地图(mapID);
		if (mapController == null)
		{
			pos = Vector2.zero;
			return false;
		}
		F_显示地图(mapController, Is重置: true);
		pos = mapController.F_Get_上香点();
		return true;
	}

	public void F_隐藏上一个地图()
	{
		if (_Last_MapC != null)
		{
			_Last_MapC.F_隐藏();
			_Last_MapC = null;
		}
	}

	public MapController F_得到当前地图()
	{
		if (_Last_MapC == null)
		{
			throw new Exception("当前没有地图");
		}
		return _Last_MapC;
	}

	public void F_显示地图(MapController mapC, bool Is重置)
	{
		F_隐藏上一个地图();
		_Last_MapC = mapC;
		mapC.F_显示(Is重置);
	}
}
