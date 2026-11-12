using System;
using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_得胜 : UIBase
{
	public Text _Text_Name;

	public Text _Text_用时;

	public Text _Text_游戏周目;

	private void Start()
	{
		base._CG.alpha = 0f;
		base._CG.blocksRaycasts = false;
	}

	public void F_显示得胜(Enum_Enemy name, Action action)
	{
		_Text_Name.text = name.ToString().Split('_')[0];
		_Text_用时.gameObject.SetActive(value: true);
		float 该存档游玩时间 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._该存档游玩时间;
		float num = (int)(该存档游玩时间 / 60f);
		_Text_用时.text = string.Format("用时:{0}分{1}秒", num, (该存档游玩时间 - num * 60f).ToString("00.00"));
		if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目 <= 1)
		{
			_Text_游戏周目.text = "";
		}
		else
		{
			_Text_游戏周目.text = $"{DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目}周目";
		}
		StartCoroutine(Cor_显示得胜(action));
	}

	private IEnumerator Cor_显示得胜(Action action)
	{
		GameManager._CanPlay = false;
		base._CG.blocksRaycasts = true;
		Time.timeScale = 0.15f;
		base._CG.DOFade(1f, 0.25f).SetUpdate(isIndependentUpdate: true);
		yield return new WaitForSecondsRealtime(1.75f);
		Time.timeScale = 0f;
		while (!DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.AnyKeyDown.WasPressedThisFrame())
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
		while (!DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.AnyKeyDown.WasPressedThisFrame())
		{
			yield return new WaitForEndOfFrame();
		}
		GameManager._CanPlay = true;
		Time.timeScale = 1f;
		base._CG.DOFade(0f, 0.25f).SetUpdate(isIndependentUpdate: true);
		base._CG.blocksRaycasts = false;
		action?.Invoke();
	}
}
