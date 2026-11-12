using DSB;
using UnityEngine;

public class UIState_默认主页 : UIState_Base
{
	private bool _CanOk;

	public override void OnEnter()
	{
		_Data._UI_MainMenu._CG.interactable = true;
		_Data._UI_InGame._CG.interactable = false;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		GameManager._暂停游戏 = true;
		GameManager._CanPlay = false;
		Time.timeScale = 1f;
		AudioListener.pause = true;
		EventArgs_回到主页.Trigger();
		EventArgs_清空同伴.Trigger(有烟雾生成: false);
		_Data._UI_MainMenu._可见性 = true;
		_Data._UI_InGame._可见性 = false;
		_Data._UI_遮罩._可见性 = false;
		_Data._UI_MainMenu._Ani_Btn.Play("默认");
		_Data._UI_MainMenu._GO_biligame.SetActive(value: false);
		_Data._UI_MainMenu._Ani.Play("进入主场景");
		GameManager._开启时间积累 = false;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_云宫迅音();
		_CanOk = true;
		_Data._UI_MainMenu.F_刷新UI();
		DSB_MonoSingleton<MapManager>.Instance.F_清空地图缓存();
	}

	public override void OnExit()
	{
		_Data._UI_MainMenu._CG.interactable = false;
		_Data._UI_InGame._CG.interactable = true;
		_Data._UI_MainMenu.F_保存调息休整相关数据();
		_CanOk = false;
		GameManager._开启时间积累 = true;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_BGM_黑神话_悟空();
		_Data._UI_MainMenu._Ani.Play("默认");
	}

	public override void F_InGame_暂停游戏_继续游戏()
	{
		if (_CanOk)
		{
			_Data._Mono.开始游戏();
		}
	}
}
