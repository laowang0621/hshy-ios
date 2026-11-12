using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class UIState_切换地图 : UIState_Base
{
	public override void OnEnter()
	{
		GameManager._暂停游戏 = true;
		GameManager._CanPlay = false;
		Time.timeScale = 0f;
		AudioListener.pause = true;
		MyLog.Log("UIState_切换地图<<OnEnter");
		_Data._UI_遮罩._CG.blocksRaycasts = true;
		_Data._UI_遮罩._CG.alpha = 0f;
		_Data._UI_遮罩._CG.DOFade(1f, 0.5f).SetUpdate(isIndependentUpdate: true).SetEase(Ease.OutCubic)
			.OnComplete(delegate
			{
				MyLog.Log("ChangeState(Enum_UIState.游戏主界面, true)");
				_Data._Mono.ChangeState(Enum_UIState.游戏主界面, 需要等待: true);
			});
	}

	public override void OnExit()
	{
		_Data._Mono.StartCoroutine(Cor_OnExit());
	}

	private IEnumerator Cor_OnExit()
	{
		MyLog.Log("UIState_切换地图<<OnExit");
		EventArgs_切换地图_开始.Trigger();
		_Data._UI_遮罩.DOKill();
		_Data._UI_遮罩._CG.blocksRaycasts = false;
		_Data._UI_遮罩._CG.DOKill();
		_Data._UI_遮罩._CG.alpha = 1f;
		Vector2 pos;
		if (_Data._Next_Is_上香点)
		{
			if (_Data._上一个状态 == Enum_UIState.默认主页)
			{
				if ((DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._上香地图ID >= 200 || DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_章节提示_ID_已经触发.Contains(1)) && !DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_出租云)
				{
					DSB_MonoSingleton<GameManager>.Instance.F_解锁_出租云();
				}
				if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话(3005) && !DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_猪八戒)
				{
					DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_猪八戒 = true;
				}
				if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话(4003) && !DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_四姐)
				{
					DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_四姐 = true;
				}
				_Data._UI_MainMenu._可见性 = false;
				_Data._UI_InGame._可见性 = true;
				_Data._UI_InGame.F_隐藏所有子面板();
				_Data._UI_InGame._InGame_Main._可见性 = true;
				_Data._UI_InGame._InGame_Main.F_设置技能的显示();
			}
			int mapID = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._上香地图ID;
			if (mapID == -1)
			{
				while (!DSB_MonoSingleton<MapManager>.Instance.F_加载地图_To_传送点(101, 0, Is重置: true, out pos))
				{
					yield return DSB_MonoSingleton<MapManager>.Instance.F_实例化地图(101);
				}
			}
			else
			{
				while (!DSB_MonoSingleton<MapManager>.Instance.F_加载地图_To_上香点(mapID, out pos))
				{
					yield return DSB_MonoSingleton<MapManager>.Instance.F_实例化地图(mapID);
				}
			}
		}
		else
		{
			while (!DSB_MonoSingleton<MapManager>.Instance.F_加载地图_To_传送点(_Data._Next_MapID, _Data._Next_传送点ID, Is重置: false, out pos))
			{
				yield return DSB_MonoSingleton<MapManager>.Instance.F_实例化地图(_Data._Next_MapID);
			}
		}
		yield return new WaitForEndOfFrame();
		_Data._UI_遮罩._CG.DOFade(0f, 0.5f).SetEase(Ease.InCubic).SetUpdate(isIndependentUpdate: true);
		PlayerController playerController = DSB_MonoSingleton<GameManager>.Instance.F_显示玩家();
		playerController.F_SetPos(pos);
		if (_Data._Is重生)
		{
			_Data._Is重生 = false;
			playerController.F_重置状态();
		}
		playerController.F_注册交互事件();
		yield return new WaitForEndOfFrame();
		EventArgs_切换地图_结束.Trigger();
		_Data._Exit结束 = true;
	}
}
