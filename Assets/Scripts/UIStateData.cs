using System;
using DSB;
using UnityEngine;

[Serializable]
public class UIStateData : IStateData
{
	private UIManager _mMono;

	public Enum_UIState _上一个状态;

	public bool _Exit结束;

	private UI_MainMenu _mUI_MainMenu;

	private UI_InGame _mUI_InGame;

	private UI_遮罩 _mUI_遮罩;

	private UI_地图提示 _mUI_地图提示;

	public bool _Is重生;

	public bool _Next_Is_上香点;

	public int _Next_MapID = -1;

	public int _Next_传送点ID = -1;

	public int _下一个对话ID;

	public Action _下一个对话回调;

	public UIManager _Mono
	{
		get
		{
			if (_mMono == null)
			{
				throw new Exception("_mMono为空！");
			}
			return _mMono;
		}
	}

	public UI_MainMenu _UI_MainMenu
	{
		get
		{
			if (_mUI_MainMenu == null)
			{
				_mUI_MainMenu = _Mono.transform.GetComponentInChildren<UI_MainMenu>();
				if (_mUI_MainMenu == null)
				{
					_mUI_MainMenu = UnityEngine.Object.Instantiate(Resources.Load<UI_MainMenu>("UI/UI_MainMenu"), _Mono.transform);
				}
			}
			return _mUI_MainMenu;
		}
	}

	public UI_InGame _UI_InGame
	{
		get
		{
			if (_mUI_InGame == null)
			{
				_mUI_InGame = _Mono.transform.GetComponentInChildren<UI_InGame>();
				if (_mUI_InGame == null)
				{
					if (Lunch._PCUI)
					{
						_mUI_InGame = UnityEngine.Object.Instantiate(Resources.Load<UI_InGame>("UI/UI_InGame_PC"), _Mono.transform);
					}
					else
					{
						_mUI_InGame = UnityEngine.Object.Instantiate(Resources.Load<UI_InGame>("UI/UI_InGame"), _Mono.transform);
					}
				}
			}
			return _mUI_InGame;
		}
	}

	public UI_遮罩 _UI_遮罩
	{
		get
		{
			if (_mUI_遮罩 == null)
			{
				_mUI_遮罩 = _Mono.transform.GetComponentInChildren<UI_遮罩>();
				if (_mUI_遮罩 == null)
				{
					_mUI_遮罩 = UnityEngine.Object.Instantiate(Resources.Load<UI_遮罩>("UI/UI_遮罩"), _Mono.transform);
				}
			}
			return _mUI_遮罩;
		}
	}

	public UI_地图提示 _UI_地图提示
	{
		get
		{
			if (_mUI_地图提示 == null)
			{
				_mUI_地图提示 = _Mono.transform.GetComponentInChildren<UI_地图提示>();
				if (_mUI_地图提示 == null)
				{
					_mUI_地图提示 = UnityEngine.Object.Instantiate(Resources.Load<UI_地图提示>("UI/UI_地图提示"), _Mono.transform);
				}
			}
			return _mUI_地图提示;
		}
	}

	public UIStateData(UIManager mono)
	{
		_mMono = mono;
		_ = _UI_地图提示 == null;
	}

	public void F_Set_Next_MapInfo(bool next_Is_上香点, int next_MapID, int next_传送点ID)
	{
		_Next_Is_上香点 = next_Is_上香点;
		if (!next_Is_上香点)
		{
			_Next_MapID = next_MapID;
			_Next_传送点ID = next_传送点ID;
		}
	}
}
