using DSB;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InGame_Main_修行 : MonoBehaviour
{
	[Header("Player法术动画,用于手机UI打开修行时展开,与加点按钮对应")]
	public Animator _Ani_Player法术;

	[Header("技能加点")]
	public UI_InGame_Main_修行_加点 _UI_加点;

	[Header("技能装配")]
	public UI_InGame_Main_修行_装配 _UI_装配;

	[Header("修行页面")]
	public CanvasGroup _CG_修行页面;

	private void Start()
	{
		UIManager.F_CG_设置可见(_CG_修行页面, 可见: false);
		_UI_加点.gameObject.SetActive(value: true);
		_UI_装配.gameObject.SetActive(value: true);
	}

	public void F_打开修行面板()
	{
		if (GameManager._CanPlay && !(DSB_MonoSingleton<GameManager>.Instance._PC == null) && DSB_MonoSingleton<GameManager>.Instance._PC._FSM.currStateName != Player_StateName_Type.化形 && DSB_MonoSingleton<GameManager>.Instance._PC._FSM.currStateName != Player_StateName_Type.精魄)
		{
			DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main._CG_等级.alpha = 1f;
			DSB_MonoSingleton<UIManager>.Instance._Data._UI_InGame._InGame_Main._CG_经验.alpha = 1f;
			UIManager.F_CG_设置可见(_CG_修行页面, 可见: true);
			if (!Lunch._PCUI)
			{
				_Ani_Player法术.Play("默认_展开", 0, 0f);
			}
			Time.timeScale = 0f;
			GameManager._CanPlay = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC += F_关闭修行;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_翻滚 += F_关闭修行;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_修行 += F_关闭修行;
			UIManager.F_CG_设置可见(_UI_加点._CG, 可见: true);
			UIManager.F_CG_设置可见(_UI_装配._CG, 可见: false);
			_UI_加点.F_打开修行面板();
			_UI_装配.F_打开修行面板();
			_UI_加点.F_打开当前面板();
		}
	}

	public void F_关闭修行()
	{
		_UI_加点.F_关闭修行();
		_UI_装配.F_关闭修行();
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_翻滚 -= F_关闭修行;
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC -= F_关闭修行;
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_修行 -= F_关闭修行;
		Time.timeScale = 1f;
		GameManager._CanPlay = true;
		DSB_MonoSingleton<MyDataManager>.Instance.F_保存数据();
		if (!Lunch._PCUI)
		{
			_Ani_Player法术.Play("默认", 0, 0f);
		}
		UIManager.F_CG_设置可见(_CG_修行页面, 可见: false);
		EventSystem.current.SetSelectedGameObject(null);
	}
}
