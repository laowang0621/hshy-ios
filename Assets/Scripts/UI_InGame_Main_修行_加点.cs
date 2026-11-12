using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InGame_Main_修行_加点 : MonoBehaviour
{
	public CanvasGroup _CG;

	public UI_InGame_Main_修行 _UI_修行;

	public InGame_Pause_物品栏 _InGame_Pause_物品栏;

	[Header("修行")]
	public Text _Text_修行点数;

	public GameObject _GO_Btns;

	[Header("关闭修行按钮")]
	public Button _Btn_关闭修行;

	public Button _Btn_关闭修行_2;

	public Button _Btn_重置;

	public Button _Btn_装配;

	[Header("文字提示")]
	public Text _Text_文字提示;

	public Text _Text_文字提示_修行点数;

	public RectTransform RTsf_文字提示;

	[Header("弹窗提示")]
	public CanvasGroup _CG_弹窗;

	public Text _Text_弹窗提示;

	public Button _Btn_关闭弹窗;

	[Header("玩家数值")]
	public Text _Text_攻击;

	public Text _Text_生命;

	public Text _Text_法力;

	public Text _Text_气力;

	[Header("加点按钮_游戏物体")]
	public GameObject _GO_定身术;

	public GameObject _GO_聚形散气;

	public GameObject _GO_化形;

	public GameObject _GO_身外身法;

	[Header("加点按钮_按钮")]
	public Button _Btn_定身术;

	public Button _Btn_聚形散气;

	public Button _Btn_化形;

	public Button _Btn_身外身法;

	private My加点ItemInfo _My加点ItemInfo_定身术;

	private My加点ItemInfo _My加点ItemInfo_聚形散气;

	private My加点ItemInfo _My加点ItemInfo_化形;

	private My加点ItemInfo _My加点ItemInfo_身外身法;

	[Header("其他")]
	public GameObject _GO_底图;

	private List<My加点ItemInfo> _List_Btn_加点;

	private GameObject _打开弹窗前的GO;

	private int _CurrBtnID = -1;

	private Sequence sequence;

	private void Awake()
	{
		_GO_底图.SetActive(Lunch._PCUI);
		_My加点ItemInfo_定身术 = _Btn_定身术.GetComponent<My加点ItemInfo>();
		_My加点ItemInfo_聚形散气 = _Btn_聚形散气.GetComponent<My加点ItemInfo>();
		_My加点ItemInfo_化形 = _Btn_化形.GetComponent<My加点ItemInfo>();
		_My加点ItemInfo_身外身法 = _Btn_身外身法.GetComponent<My加点ItemInfo>();
		_List_Btn_加点 = new List<My加点ItemInfo>();
		foreach (Transform item in _GO_Btns.transform)
		{
			if (item.TryGetComponent<My加点ItemInfo>(out var component))
			{
				_List_Btn_加点.Add(component);
				component.OnAwake();
				component.OnShow();
			}
		}
	}

	private void Start()
	{
		_Btn_关闭修行.onClick.AddListener(_UI_修行.F_关闭修行);
		_Btn_关闭修行_2.onClick.AddListener(_UI_修行.F_关闭修行);
		_Btn_重置.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance.F_重置修行点();
			foreach (My加点ItemInfo item in _List_Btn_加点)
			{
				item.OnShow();
			}
		});
		_Btn_装配.onClick.AddListener(delegate
		{
			F_关闭当前面板();
			_UI_修行._UI_装配.F_打开当前面板();
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_修行点_Change._EventID, F_修行点_Change);
		_Btn_关闭弹窗.onClick.AddListener(delegate
		{
			UIManager.F_CG_设置可见(_CG_弹窗, 可见: false);
			if (_CurrBtnID != -1)
			{
				F_文本提示(_CurrBtnID);
				_CurrBtnID = -1;
			}
			EventSystem.current.SetSelectedGameObject(_打开弹窗前的GO);
		});
		RTsf_文字提示.localScale = Vector3.zero;
		UIManager.F_CG_设置可见(_CG_弹窗, 可见: false);
	}

	public void F_打开修行面板()
	{
		_InGame_Pause_物品栏.F_更新物品栏UI();
		PlayerItemDataInfo data_SL = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL;
		bool flag = DSB_MonoSingleton<MyDataManager>.Instance.F_判断当前化形是否已经解锁();
		_GO_定身术.gameObject.SetActive(data_SL._解锁_定身术);
		_GO_聚形散气.gameObject.SetActive(data_SL._解锁_聚形散气);
		_GO_化形.gameObject.SetActive(flag);
		_GO_身外身法.gameObject.SetActive(data_SL._解锁_身外身法);
		_Btn_定身术.interactable = data_SL._解锁_定身术;
		_Btn_聚形散气.interactable = data_SL._解锁_聚形散气;
		_Btn_化形.interactable = flag;
		_Btn_身外身法.interactable = data_SL._解锁_身外身法;
		_My加点ItemInfo_定身术._CanOnClick = data_SL._解锁_定身术;
		_My加点ItemInfo_聚形散气._CanOnClick = data_SL._解锁_聚形散气;
		_My加点ItemInfo_化形._CanOnClick = flag;
		_My加点ItemInfo_身外身法._CanOnClick = data_SL._解锁_身外身法;
		F_更新玩家数值UI();
		foreach (My加点ItemInfo item in _List_Btn_加点)
		{
			item.OnShow();
		}
	}

	public void F_关闭修行()
	{
		DSB_MonoSingleton<MyDataManager>.Instance.F_开始游戏_计算玩家加点(null);
		DSB_MonoSingleton<GameManager>.Instance._PC.F_计算加点加成();
		DSB_MonoSingleton<GameManager>.Instance._PC.F_加点后调用();
		UIManager.F_CG_设置可见(_CG_弹窗, 可见: false);
		_CurrBtnID = -1;
	}

	public void F_打开当前面板()
	{
		EventSystem.current.SetSelectedGameObject(_Btn_关闭修行.gameObject);
		_CG.DOFade(1f, 0.2f).SetUpdate(isIndependentUpdate: true);
		_CG.interactable = true;
		_CG.blocksRaycasts = true;
	}

	public void F_关闭当前面板()
	{
		_CG.DOFade(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
		_CG.interactable = false;
		_CG.blocksRaycasts = false;
	}

	private void F_更新玩家数值UI()
	{
		Player_StateData data = DSB_MonoSingleton<GameManager>.Instance._PC._Data;
		_Text_攻击.text = ((int)data._攻击力).ToString();
		_Text_生命.text = ((int)data._Max生命).ToString();
		_Text_法力.text = ((int)data._Max法力).ToString();
		_Text_气力.text = ((int)data._Max气力).ToString();
	}

	public void F_测试更新数值显示()
	{
		DSB_MonoSingleton<MyDataManager>.Instance.F_开始游戏_计算玩家加点(null);
		DSB_MonoSingleton<GameManager>.Instance._PC.F_计算加点加成();
		F_更新玩家数值UI();
	}

	public void F_修行点_Change(DSB_EventArgs args)
	{
		EventArgs_Player_修行点_Change eventArgs_Player_修行点_Change = (EventArgs_Player_修行点_Change)args;
		_Text_修行点数.text = eventArgs_Player_修行点_Change._当前Val.ToString();
	}

	public void F_弹窗提示(string text, int _BtnID)
	{
		_CurrBtnID = _BtnID;
		F_清空完本Tip();
		UIManager.F_CG_设置可见(_CG_弹窗, 可见: true);
		_Text_弹窗提示.text = text;
		_打开弹窗前的GO = EventSystem.current.currentSelectedGameObject;
		EventSystem.current.SetSelectedGameObject(_Btn_关闭弹窗.gameObject);
	}

	private void F_清空完本Tip()
	{
		sequence?.Kill();
		RTsf_文字提示.DOKill();
		RTsf_文字提示.localScale = Vector3.zero;
		RTsf_文字提示.anchoredPosition = new Vector2(0f, 0f);
	}

	public void F_文本提示(int _BtnID)
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断该BtnID是否满级(_BtnID))
		{
			DataInfo_PlayerLevel_Buff dataInfo_PlayerLevel_Buff = DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_Buff_预选(_BtnID);
			F_文字提示(dataInfo_PlayerLevel_Buff._升级描述, _会消失: false, dataInfo_PlayerLevel_Buff._每一级消耗点数.ToString());
		}
		else
		{
			F_文字提示("已经达到满级");
		}
	}

	public void F_文字提示(string tips, bool _会消失 = true, string tip_修行点数 = null)
	{
		F_清空完本Tip();
		_Text_文字提示.text = tips;
		if (tip_修行点数 == null)
		{
			_Text_文字提示_修行点数.enabled = false;
		}
		else
		{
			_Text_文字提示_修行点数.enabled = true;
			_Text_文字提示_修行点数.text = $"需要<color=#6DFF53><b>{tip_修行点数}</b></color>修行点";
		}
		sequence = DOTween.Sequence().SetUpdate(isIndependentUpdate: true);
		sequence.Append(RTsf_文字提示.DOScale(1f, 0.2f).SetUpdate(isIndependentUpdate: true));
		sequence.Join(RTsf_文字提示.DOAnchorPosY(80f, 0.2f).SetUpdate(isIndependentUpdate: true));
		if (_会消失)
		{
			sequence.AppendInterval(1.15f).SetUpdate(isIndependentUpdate: true);
			sequence.Append(RTsf_文字提示.DOScale(0f, 0.2f).SetUpdate(isIndependentUpdate: true));
			sequence.Join(RTsf_文字提示.DOAnchorPosY(150f, 0.2f).SetUpdate(isIndependentUpdate: true));
		}
	}
}
