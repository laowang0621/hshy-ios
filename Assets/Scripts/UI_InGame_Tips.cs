using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_Tips : UIBase
{
	[Header("Tip_前路不通")]
	public CanvasGroup _CG_前路不通;

	public Text _Text_前路不通;

	[Header("Tip_获得精魄")]
	public CanvasGroup _CG_获得精魄;

	public Image _Img_获得精魄;

	public Text _Text_获得精魄;

	private void Start()
	{
		_可见性 = true;
		_CG_前路不通.blocksRaycasts = false;
		_CG_前路不通.alpha = 0f;
		_CG_获得精魄.alpha = 0f;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Tips_获得精魄._EventID, F_显示Tips_获得精魄);
	}

	public void F_显示Tips_前路不通(string str)
	{
		_可见性 = true;
		_Text_前路不通.text = str;
		_CG_前路不通.DOFade(1f, 0.5f).SetUpdate(isIndependentUpdate: true);
	}

	public void F_隐藏Tips_前路不通()
	{
		_CG_前路不通.DOFade(0f, 0.5f).SetUpdate(isIndependentUpdate: true);
	}

	public void F_显示Tips_获得精魄(DSB_EventArgs args)
	{
		EventArgs_Tips_获得精魄 eventArgs_Tips_获得精魄 = (EventArgs_Tips_获得精魄)args;
		_可见性 = true;
		_Img_获得精魄.sprite = eventArgs_Tips_获得精魄._Sprite;
		_Text_获得精魄.text = eventArgs_Tips_获得精魄._Str;
		_CG_获得精魄.alpha = 0f;
		Sequence s = DOTween.Sequence().SetUpdate(isIndependentUpdate: true);
		s.Append(_CG_获得精魄.DOFade(1f, 0.5f));
		s.AppendInterval(2f);
		s.Append(_CG_获得精魄.DOFade(0f, 0.5f));
	}
}
