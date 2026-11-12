using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIState_游戏暂停界面 : UIState_Base
{
	private bool _CanOk;

	public override void OnEnter()
	{
		_Data._UI_InGame._InGame_Pause._InGame_Pause_物品栏.F_更新物品栏UI();
		Time.timeScale = 0f;
		AudioListener.pause = true;
		GameManager._暂停游戏 = true;
		GameManager._CanPlay = false;
		_Data._UI_InGame._InGame_Pause._CG.interactable = true;
		_Data._UI_InGame._InGame_Pause._CG.blocksRaycasts = true;
		_Data._UI_InGame._InGame_Pause._CG.DOFade(1f, 0.15f).SetUpdate(isIndependentUpdate: true);
		_Data._UI_InGame._InGame_Pause.F_显示当前存档时间(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._该存档游玩时间);
		_Data._UI_InGame._InGame_Main._CG_等级.alpha = 1f;
		_Data._UI_InGame._InGame_Main._CG_经验.alpha = 1f;
		_CanOk = true;
		_Data._UI_InGame._InGame_Pause.F_刷新UI();
		EventSystem.current.SetSelectedGameObject(_Data._UI_InGame._InGame_Pause._Btn_继续游戏.gameObject);
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC += F_InGame_暂停游戏_继续游戏;
	}

	public override void OnExit()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC -= F_InGame_暂停游戏_继续游戏;
		_Data._UI_InGame._InGame_Pause.F_保存调息休整相关数据();
		_CanOk = false;
		Time.timeScale = 1f;
		_Data._UI_InGame._InGame_Main._CG_等级.alpha = 0f;
		_Data._UI_InGame._InGame_Main._CG_经验.alpha = 0f;
		MyLog.LogError($"_Data._Exit结束>>{_Data._Exit结束}");
		if (_Data._Exit结束)
		{
			_Data._UI_InGame._InGame_Pause._CG.interactable = false;
			_Data._UI_InGame._InGame_Pause._CG.blocksRaycasts = false;
			_Data._UI_InGame._InGame_Pause._CG.DOFade(0f, 0.15f).SetUpdate(isIndependentUpdate: true);
			return;
		}
		_Data._UI_遮罩._CG.interactable = true;
		_Data._UI_遮罩._CG.blocksRaycasts = true;
		_Data._UI_遮罩._CG.DOFade(1f, 0.5f).SetUpdate(isIndependentUpdate: true).SetEase(Ease.OutCubic)
			.OnComplete(delegate
			{
				DSB_MonoSingleton<MapManager>.Instance.F_隐藏上一个地图();
				_Data._Exit结束 = true;
			});
	}

	public override void F_InGame_暂停游戏_继续游戏()
	{
		_Data._Mono.F_InGame_继续游戏();
	}

	public override void F_InGame_返回主页()
	{
		if (_CanOk)
		{
			_Data._Mono.ChangeState(Enum_UIState.默认主页, 需要等待: true);
		}
	}
}
