using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
	private int _mMapID = -1;

	private GameSceneController _GSC;

	private Transform _List_Tsf_上香点;

	private Transform _List_Tsf_传送点;

	private Transform _List_Tsf_Enemys;

	private C_土地庙 _C_土地庙;

	[HideInInspector]
	public List<C_传送区域> _List_传送区域;

	private List<IEnemy> _List_Enemy;

	private IBoss生成器 _C_Boss生成器;

	public int _章节ID = -1;

	private bool _已经进入初始化;

	public int _MapID => _mMapID;

	public MapController Init(int mapID)
	{
		if (_已经进入初始化)
		{
			MyLog.Log("MapController>>已经初始化！");
			return null;
		}
		_已经进入初始化 = true;
		_mMapID = mapID;
		_GSC = base.transform.Find("GameSceneController").GetComponent<GameSceneController>();
		_List_Tsf_上香点 = base.transform.Find("上香点");
		_List_Tsf_传送点 = base.transform.Find("传送点");
		_List_Tsf_Enemys = base.transform.Find("EnemyList");
		_List_传送区域 = new List<C_传送区域>();
		_List_Enemy = new List<IEnemy>();
		_C_土地庙 = _List_Tsf_上香点.GetComponentInChildren<C_土地庙>();
		_C_Boss生成器 = _List_Tsf_上香点.GetComponentInChildren<IBoss生成器>();
		foreach (Transform item in _List_Tsf_传送点)
		{
			if (item.TryGetComponent<C_传送区域>(out var component))
			{
				_List_传送区域.Add(component);
			}
		}
		foreach (Transform list_Tsf_Enemy in _List_Tsf_Enemys)
		{
			if (list_Tsf_Enemy.TryGetComponent<IEnemy>(out var component2))
			{
				_List_Enemy.Add(component2);
			}
		}
		return this;
	}

	public MapController F_隐藏()
	{
		foreach (IEnemy item in _List_Enemy)
		{
			item.F_隐藏();
		}
		base.gameObject.SetActive(value: false);
		return this;
	}

	public MapController F_重置()
	{
		foreach (IEnemy item in _List_Enemy)
		{
			item.F_重置();
		}
		return this;
	}

	public void F_更新边界()
	{
		_GSC.F_更新边界();
	}

	public MapController F_显示(bool _Is重置)
	{
		base.gameObject.SetActive(value: true);
		_GSC.F_初始化();
		if (_章节ID > -1)
		{
			MyLog.LogError("Trigger>>EventArgs_章节提示");
			EventArgs_章节提示.Trigger(_章节ID);
		}
		foreach (IEnemy item in _List_Enemy)
		{
			item.F_显示();
		}
		foreach (C_传送区域 item2 in _List_传送区域)
		{
			item2.F_显示();
		}
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
		_C_Boss生成器?.Init();
		return this;
	}

	public Vector2 F_Get_上香点()
	{
		if (_C_土地庙 != null)
		{
			return _C_土地庙.F_得到范围内随机点();
		}
		return F_Get_传送点(0);
	}

	public bool F_是否存在传送点(int 传送点ID)
	{
		if (_List_传送区域 != null && 传送点ID >= 0 && _List_传送区域.Count > 传送点ID)
		{
			return true;
		}
		return false;
	}

	public Vector2 F_Get_传送点(int 传送点ID)
	{
		if (_List_传送区域 != null && _List_传送区域.Count > 传送点ID)
		{
			return _List_传送区域[传送点ID].F_GetPlayerPos();
		}
		throw new Exception($"没有传送点<<{传送点ID}>>的传送区域");
	}

	public (int, int) F_Get传送点ID连接的_Map_传送点ID(int 传送点ID)
	{
		if (_List_传送区域 != null && _List_传送区域.Count > 传送点ID)
		{
			return (_List_传送区域[传送点ID]._Next_MapID, _List_传送区域[传送点ID]._Next_传送点ID);
		}
		throw new Exception($"没有传送点<<{传送点ID}>>的传送区域");
	}
}
