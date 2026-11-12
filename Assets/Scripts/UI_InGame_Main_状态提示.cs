using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_Main_状态提示 : MonoBehaviour
{
	private Camera _MainCamera;

	public CanvasGroup _CG;

	public RectTransform _Rect_Tips;

	public Text _Text_Tips;

	private Transform _Tsf_PlayerUI;

	private bool _第一次 = true;

	public bool _mIs过低;

	private float time_1 = 0.5f;

	private float timer;

	private float val_1;

	private Sequence sequence;

	public bool _Is过低
	{
		set
		{
			if (_第一次 || _mIs过低 != value)
			{
				_第一次 = false;
				_mIs过低 = value;
				if (_mIs过低)
				{
					timer = -1f;
				}
				else
				{
					_CG.DOFade(0f, 0.7f);
				}
			}
		}
	}

	private void Awake()
	{
		_MainCamera = Camera.main;
	}

	private void Start()
	{
		_Rect_Tips.localScale = Vector3.zero;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player状态提示._EventID, F_显示玩家状态提示);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player状态提示_生命过低._EventID, F_显示玩家状态提示_生命过低);
	}

	private void LateUpdate()
	{
		if ((bool)_Tsf_PlayerUI)
		{
			_Rect_Tips.position = _MainCamera.WorldToScreenPoint(_Tsf_PlayerUI.position);
		}
		if (_mIs过低)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				timer = time_1;
				val_1 = ((_CG.alpha < 0.5f) ? Random.Range(0.7f, 1f) : Random.Range(0f, 0.3f));
			}
			_CG.alpha = Mathf.Lerp(_CG.alpha, val_1, 5f * Time.deltaTime);
		}
	}

	public void F_显示玩家状态提示(DSB_EventArgs args)
	{
		EventArgs_Player状态提示 eventArgs_Player状态提示 = (EventArgs_Player状态提示)args;
		string tips = eventArgs_Player状态提示._Tips;
		_Tsf_PlayerUI = eventArgs_Player状态提示._Tsf_PlayerUI;
		sequence?.Kill();
		_Rect_Tips.DOKill();
		_Rect_Tips.localScale = Vector3.one * 0.3f;
		_Text_Tips.text = tips;
		sequence = DOTween.Sequence();
		sequence.Append(_Rect_Tips.DOScale(1f, 0.2f));
		sequence.AppendInterval(0.75f);
		sequence.Append(_Rect_Tips.DOScale(0f, 0.2f));
	}

	public void F_显示玩家状态提示_生命过低(DSB_EventArgs args)
	{
		_Is过低 = ((EventArgs_Player状态提示_生命过低)args)._Is过低;
	}
}
