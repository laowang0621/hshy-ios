using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_章节提示 : UIBase
{
	public List<Sprite> _List_章节图片;

	public Image _Img_章节图片;

	private void Start()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_章节提示._EventID, F_显示章节提示);
	}

	public void F_显示章节提示(DSB_EventArgs args)
	{
		int num = ((EventArgs_章节提示)args)._章节ID - 1;
		MyLog.LogError($"UI_InGame_章节提示>>EventArgs_章节提示>>{num}");
		if (num >= 0 && num < _List_章节图片.Count && DSB_MonoSingleton<MyDataManager>.Instance.F_判断并保存该章节提示(num))
		{
			StartCoroutine(Cor_章节提示(num));
		}
	}

	private void OnDisable()
	{
		base._CG.DOKill();
		base._CG.alpha = 0f;
	}

	private IEnumerator Cor_章节提示(int index)
	{
		base._CG.DOKill();
		yield return null;
		_Img_章节图片.sprite = _List_章节图片[index];
		base._CG.DOFade(1f, 1f).SetUpdate(isIndependentUpdate: true);
		yield return new WaitForSecondsRealtime(2f);
		base._CG.DOFade(0f, 2f).SetUpdate(isIndependentUpdate: true);
	}
}
