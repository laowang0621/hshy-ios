using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_获得道具提示 : UIBase
{
	public Image _丹药图片;

	public Text _道具名字;

	public Text _文字介绍;

	private void Start()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_道具提示._EventID, F_显示获得道具提示);
	}

	public void F_显示获得道具提示(DSB_EventArgs args)
	{
		EventArgs_道具提示 eventArgs_道具提示 = (EventArgs_道具提示)args;
		_丹药图片.sprite = eventArgs_道具提示.info._Sprite;
		_丹药图片.rectTransform.sizeDelta = _丹药图片.sprite.rect.size * 2f;
		_道具名字.text = eventArgs_道具提示.info._PorpName;
		_文字介绍.text = eventArgs_道具提示.info._Porp介绍;
		StartCoroutine(Cor_获得丹药提示());
	}

	private void OnDisable()
	{
		base._CG.DOKill();
		base._CG.alpha = 0f;
	}

	private IEnumerator Cor_获得丹药提示()
	{
		yield return null;
		yield return null;
		GameManager._CanPlay = false;
		Time.timeScale = 0f;
		base._CG.DOFade(1f, 0.33f).SetUpdate(isIndependentUpdate: true);
		yield return new WaitForSecondsRealtime(1f);
		while (!DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.AnyKeyDown.WasPressedThisFrame())
		{
			yield return null;
		}
		GameManager._CanPlay = true;
		Time.timeScale = 1f;
		base._CG.DOFade(0f, 0.25f).SetUpdate(isIndependentUpdate: true);
		base._CG.blocksRaycasts = false;
		base._CG.DOFade(0f, 0.33f).SetUpdate(isIndependentUpdate: true);
	}
}
