using System;
using System.Collections;
using DSB;
using UnityEngine;

public class UIManager : DSB_MonoSingleton<UIManager>
{
	public DSB_FSM<Enum_UIState, UIState_Base> _FSM = new DSB_FSM<Enum_UIState, UIState_Base>();

	private bool _首次更新Data = true;

	public UIStateData _mData;

	[HideInInspector]
	public bool _是固定摇杆;

	private bool _已经在切换中;

	public Enum_UIState _当前状态_只读;

	public UIStateData _Data
	{
		get
		{
			if (_首次更新Data || _mData == null)
			{
				_首次更新Data = false;
				_mData = new UIStateData(this);
			}
			return _mData;
		}
	}

	private void Start()
	{
		_ = _Data;
		_FSM.AddState(Enum_UIState.默认主页, new UIState_默认主页().Init(_Data));
		_FSM.AddState(Enum_UIState.切换地图, new UIState_切换地图().Init(_Data));
		_FSM.AddState(Enum_UIState.游戏主界面, new UIState_游戏InGame界面().Init(_Data));
		_FSM.AddState(Enum_UIState.游戏暂停界面, new UIState_游戏暂停界面().Init(_Data));
		_FSM.AddState(Enum_UIState.对话界面, new UIState_对话界面().Init(_Data));
		ChangeState(Enum_UIState.默认主页, 需要等待: false);
	}

	public void F_触发对话(Enum_对话 对话类型, Action 对话回调 = null)
	{
		_Data._下一个对话ID = (int)对话类型;
		_Data._下一个对话回调 = 对话回调;
		ChangeState(Enum_UIState.对话界面, 需要等待: false);
	}

	public void F_设置摇杆类型(bool 是固定摇杆)
	{
	}

	public void ChangeState(Enum_UIState stateName, bool 需要等待)
	{
		StartCoroutine(Cor_ChangeState(stateName, 需要等待));
	}

	private IEnumerator Cor_ChangeState(Enum_UIState stateName, bool 需要等待)
	{
		while (_已经在切换中)
		{
			yield return null;
		}
		_已经在切换中 = true;
		_Data._Exit结束 = !需要等待;
		if (_FSM.states.TryGetValue(stateName, out var state))
		{
			_FSM.currState?.OnExit();
			while (需要等待 && !_Data._Exit结束)
			{
				yield return null;
			}
			_当前状态_只读 = stateName;
			_Data._上一个状态 = _FSM.currStateName;
			_FSM.currStateName = stateName;
			_FSM.currState = state;
			_FSM.currState.OnEnter();
		}
		else
		{
			MyLog.LogError($"不存在状态--{stateName}--无法切换。");
		}
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		_已经在切换中 = false;
	}

	public ItemUI_EnemyHP F_Get_ItemUI_EnemyHP()
	{
		return _Data._UI_InGame.F_Get_ItemUI_EnemyHP();
	}

	public ItemUI_EnemyHP_带盾 F_Get_ItemUI_EnemyHP_带盾()
	{
		return _Data._UI_InGame.F_Get_ItemUI_EnemyHP_带盾();
	}

	public ItemUI_BossHP F_Get_ItemUI_BossHP()
	{
		return _Data._UI_InGame.F_Get_ItemUI_BossHP();
	}

	public ItemUI_交互 F_Get_ItemUI_交互()
	{
		return _Data._UI_InGame.F_Get_ItemUI_交互();
	}

	public ItemUI_文本 F_Get_ItemUI_文本()
	{
		return _Data._UI_InGame.F_Get_ItemUI_文本();
	}

	public ItemUI_出租云 F_Get_ItemUI_出租云()
	{
		return _Data._UI_InGame.F_Get_ItemUI_出租云();
	}

	public void 开始游戏()
	{
		F_重生();
		EventArgs_开始游戏.Trigger();
	}

	public void F_再入轮回()
	{
		DSB_MonoSingleton<MyDataManager>.Instance.F_重置数据_再入轮回();
		开始游戏();
	}

	public void F_新的轮回()
	{
		DSB_MonoSingleton<MyDataManager>.Instance.F_重置数据();
		开始游戏();
	}

	public void F_InGame_暂停游戏()
	{
		if (!GameManager._暂停游戏 && GameManager._CanPlay)
		{
			ChangeState(Enum_UIState.游戏暂停界面, 需要等待: false);
		}
	}

	public void F_InGame_继续游戏()
	{
		ChangeState(Enum_UIState.游戏主界面, 需要等待: false);
	}

	public void F_InGame_返回主页()
	{
		_FSM.currState.F_InGame_返回主页();
	}

	public void F_通过传送区域切换场景(int next_MapID, int next_传送点ID)
	{
		_Data.F_Set_Next_MapInfo(next_Is_上香点: false, next_MapID, next_传送点ID);
		ChangeState(Enum_UIState.切换地图, 需要等待: false);
	}

	public void F_UI_定身术CD(float time)
	{
		_Data._UI_InGame._InGame_Main.F_UI_定身术CD(time);
	}

	public void F_UI_聚形散气CD(float time)
	{
		_Data._UI_InGame._InGame_Main.F_UI_聚形散气CD(time);
	}

	public void F_UI_身外身法CD(float time)
	{
		_Data._UI_InGame._InGame_Main.F_UI_身外身法CD(time);
	}

	public void F_UI_化形CD(float time)
	{
		_Data._UI_InGame._InGame_Main.F_UI_化形CD(time);
	}

	public void F_UI_精魄CD(float time)
	{
		_Data._UI_InGame._InGame_Main.F_UI_精魄CD(time);
	}

	public void F_重生()
	{
		_Data._Is重生 = true;
		DSB_MonoSingleton<MapManager>.Instance.F_重置();
		_Data.F_Set_Next_MapInfo(next_Is_上香点: true, 114, 514);
		ChangeState(Enum_UIState.切换地图, 需要等待: false);
	}

	public void F_显示Tips(string str)
	{
		_Data._UI_InGame._UI_InGame_Tips.F_显示Tips_前路不通(str);
	}

	public void F_隐藏Tips()
	{
		_Data._UI_InGame._UI_InGame_Tips.F_隐藏Tips_前路不通();
	}

	public void F_显示得胜(Enum_Enemy name, Action action)
	{
		_Data._UI_InGame._UI_InGame_得胜.F_显示得胜(name, action);
	}

	public static void F_CG_设置可见(CanvasGroup cg, bool 可见)
	{
		cg.alpha = (可见 ? 1 : 0);
		cg.interactable = 可见;
		cg.blocksRaycasts = 可见;
	}
}
