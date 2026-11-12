using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_地图提示 : UIBase
{
	public CanvasGroup _CG_地图名字提示;

	public Text _Text_大地图;

	public Text _Text_小地图;

	private Sequence Tween_地图提示;

	private void Start()
	{
		_CG_地图名字提示.blocksRaycasts = false;
		_CG_地图名字提示.alpha = 0f;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_上香._EventID, F_提示上香点);
	}

	public void F_提示上香点(DSB_EventArgs args)
	{
		EventArgs_上香 eventArgs_上香 = (EventArgs_上香)args;
		if (eventArgs_上香.需要提示)
		{
			_可见性 = true;
			Tween_地图提示?.Kill();
			_CG_地图名字提示.DOKill();
			_Text_大地图.text = eventArgs_上香.大地图名字;
			_Text_小地图.text = eventArgs_上香.小地图名字;
			_CG_地图名字提示.alpha = 0f;
			Tween_地图提示 = DOTween.Sequence().SetUpdate(isIndependentUpdate: true);
			Tween_地图提示.Append(_CG_地图名字提示.DOFade(1f, 0.33f).SetUpdate(isIndependentUpdate: true));
			Tween_地图提示.AppendInterval(2.5f).SetUpdate(isIndependentUpdate: true);
			Tween_地图提示.Append(_CG_地图名字提示.DOFade(0f, 0.33f).SetUpdate(isIndependentUpdate: true));
			MyLog.LogError("提示地图名字");
		}
	}
}
