using DSB;
using UnityEngine;

public class UIState_对话界面 : UIState_Base
{
	public override void OnEnter()
	{
		Time.timeScale = 0f;
		AudioListener.pause = true;
		GameManager._暂停游戏 = true;
		GameManager._CanPlay = false;
		GameManager._Can上香 = false;
		DataInfo_一条对话 对话_一条对话 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_对话.F_得到一条对话(_Data._下一个对话ID);
		MyLog.LogError($"触发对话>>{(Enum_对话)对话_一条对话._对话ID}");
		_Data._UI_InGame._UI_InGame_对话.F_开始对话(对话_一条对话, delegate
		{
			_Data._Mono.ChangeState(Enum_UIState.游戏主界面, 需要等待: false);
			_Data._下一个对话回调?.Invoke();
			DSB_MonoSingleton<GameManager>.Instance.F_对话结束回调(对话_一条对话._对话ID, 对话_一条对话._回调事件ID);
		});
	}

	public override void OnExit()
	{
		_Data._UI_InGame._UI_InGame_对话.F_关闭对话();
	}
}
