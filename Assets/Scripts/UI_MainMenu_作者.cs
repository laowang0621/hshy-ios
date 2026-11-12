using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_MainMenu_作者 : UIBase
{
	public Button _Btn_火山哥哥;

	public Button _Btn_林学学;

	public Button _Btn_Link_火山哥哥;

	public Button _Btn_Link_林学学;

	public CanvasGroup _CG_文字Btn;

	public CanvasGroup _CG_跳转Btn;

	public Transform _Tsf_文字Btn;

	public Transform _Tsf_跳转Btn;

	private bool _IsTween;

	private bool _IsCanSee;

	private float _Speed = 0.2f;

	private void Start()
	{
		_CG_文字Btn.transform.localScale = new Vector3(1f, 1f, 1f);
		_CG_文字Btn.alpha = 1f;
		_CG_文字Btn.blocksRaycasts = true;
		_CG_跳转Btn.transform.localScale = new Vector3(0f, 1f, 1f);
		_CG_跳转Btn.alpha = 1f;
		_CG_跳转Btn.blocksRaycasts = false;
		_Btn_火山哥哥.onClick.AddListener(delegate
		{
			F_显示_Link();
			MyLog.LogError("火山哥哥");
			_Btn_Link_火山哥哥.gameObject.SetActive(value: true);
			_Btn_Link_林学学.gameObject.SetActive(value: false);
		});
		_Btn_林学学.onClick.AddListener(delegate
		{
			F_显示_Link();
			MyLog.LogError("林学学");
			_Btn_Link_火山哥哥.gameObject.SetActive(value: false);
			_Btn_Link_林学学.gameObject.SetActive(value: true);
		});
		_Btn_Link_火山哥哥.onClick.AddListener(delegate
		{
			Application.OpenURL("https://space.bilibili.com/137429365");
			MyLog.LogError("_Btn_Link_火山哥哥");
		});
		_Btn_Link_林学学.onClick.AddListener(delegate
		{
			Application.OpenURL("https://space.bilibili.com/19792237");
			MyLog.LogError("_Btn_Link_林学学");
		});
	}

	private void Update()
	{
		if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
		{
			F_隐藏_Link();
		}
	}

	private void F_显示_Link()
	{
		if (_IsTween || _IsCanSee)
		{
			return;
		}
		_IsTween = true;
		_CG_文字Btn.blocksRaycasts = false;
		_Tsf_文字Btn.DOScaleX(0f, _Speed).SetUpdate(isIndependentUpdate: true).OnComplete(delegate
		{
			_Tsf_跳转Btn.DOScaleX(1f, _Speed).SetUpdate(isIndependentUpdate: true).OnComplete(delegate
			{
				_CG_跳转Btn.blocksRaycasts = true;
				_IsCanSee = true;
				_IsTween = false;
			});
		});
	}

	private void F_隐藏_Link()
	{
		if (_IsTween || !_IsCanSee)
		{
			return;
		}
		_IsTween = true;
		_CG_跳转Btn.blocksRaycasts = false;
		_Tsf_跳转Btn.DOScaleX(0f, _Speed).SetUpdate(isIndependentUpdate: true).OnComplete(delegate
		{
			_Tsf_文字Btn.DOScaleX(1f, _Speed).SetUpdate(isIndependentUpdate: true).OnComplete(delegate
			{
				_CG_文字Btn.blocksRaycasts = true;
				_IsCanSee = false;
				_IsTween = false;
			});
		});
	}

	private void OnDestroy()
	{
		_CG_文字Btn.DOKill();
		_CG_跳转Btn.DOKill();
	}
}
