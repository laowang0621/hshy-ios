using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu_教学 : UIBase
{
	public Button _Btn_教学;

	public CanvasGroup _CG_教学内容;

	private bool _IsLook;

	public Image _Img_键盘;

	public Image _Img_手柄;

	public Transform _Tsf_问号;

	public static int _没有点击过教学 = 1;

	private void OnEnable()
	{
		_CG_教学内容.alpha = 0f;
		_CG_教学内容.blocksRaycasts = false;
		_CG_教学内容.transform.localScale = Vector3.zero;
		_IsLook = false;
	}

	private void Awake()
	{
		_没有点击过教学 = PlayerPrefs.GetInt("没有点击过教学", 1);
		if (_没有点击过教学 == 1)
		{
			_Tsf_问号.DOScale(1.5f, 0.7f).SetUpdate(isIndependentUpdate: true).SetLoops(-1, LoopType.Yoyo);
		}
	}

	private void Start()
	{
		_Img_键盘.gameObject.SetActive(value: true);
		_Img_手柄.gameObject.SetActive(value: false);
		_Btn_教学.onClick.AddListener(delegate
		{
			if (_没有点击过教学 == 1)
			{
				_没有点击过教学 = 0;
				_Tsf_问号.DOKill();
				_Tsf_问号.localScale = Vector3.one;
				PlayerPrefs.SetInt("没有点击过教学", 0);
				PlayerPrefs.Save();
			}
			if (_IsLook)
			{
				_CG_教学内容.DOFade(0f, 0.25f).SetUpdate(isIndependentUpdate: true);
				_CG_教学内容.transform.DOScale(0f, 0.25f).SetUpdate(isIndependentUpdate: true);
			}
			else
			{
				_CG_教学内容.DOFade(1f, 0.25f).SetUpdate(isIndependentUpdate: true);
				_CG_教学内容.transform.DOScale(1f, 0.25f).SetUpdate(isIndependentUpdate: true);
			}
			_IsLook = !_IsLook;
		});
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_Any键盘 += delegate
		{
			_Img_键盘.gameObject.SetActive(value: true);
			_Img_手柄.gameObject.SetActive(value: false);
			_Img_键盘.SetAlpha(0f);
			_Img_手柄.SetAlpha(1f);
			_Img_键盘.DOFade(1f, 0.2f).SetUpdate(isIndependentUpdate: true);
			_Img_手柄.DOFade(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
		};
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_Any手柄 += delegate
		{
			_Img_键盘.gameObject.SetActive(value: false);
			_Img_手柄.gameObject.SetActive(value: true);
			_Img_键盘.SetAlpha(1f);
			_Img_手柄.SetAlpha(0f);
			_Img_键盘.DOFade(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
			_Img_手柄.DOFade(1f, 0.2f).SetUpdate(isIndependentUpdate: true);
		};
	}
}
