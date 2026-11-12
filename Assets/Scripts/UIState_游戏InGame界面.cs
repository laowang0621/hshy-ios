using DSB;
using UnityEngine;

public class UIState_游戏InGame界面 : UIState_Base
{
	public override void OnEnter()
	{
		GameManager._暂停游戏 = false;
		GameManager._CanPlay = true;
		GameManager._Can上香 = true;
		Time.timeScale = 1f;
		AudioListener.pause = false;
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC += F_InGame_暂停游戏_继续游戏;
	}

	public override void OnExit()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC -= F_InGame_暂停游戏_继续游戏;
	}

	public override void F_InGame_暂停游戏_继续游戏()
	{
		_Data._Mono.F_InGame_暂停游戏();
	}
}
