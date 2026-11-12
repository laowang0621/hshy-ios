using System;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_Main : UIBase
{
	[Header("位置")]
	public RectTransform _RectBasePos_ItemUI_EnemyHP_Boss;

	public RectTransform _RectBasePos_ItemUI_EnemyHP;

	public RectTransform _RectBasePos_Tips;

	[Header("进度条--表现相关")]
	public ItemUI_能量条 _能量条_生命;

	public ItemUI_能量条 _能量条_法力;

	public ItemUI_Player_气力 _能量条_气力;

	public C_UI_蓄力 _C_蓄力;

	public ItemUI_能量条 _能量条_酒;

	public CanvasGroup _CG_Player;

	public UI_InGame_Main_化形 _UI_化形;

	[Header("经验--表现相关")]
	public ItemUI_能量条 _能量条_经验;

	public CanvasGroup _CG_等级;

	public Text _Text_等级;

	public Text _Text_经验;

	public CanvasGroup _CG_经验;

	[Header("按钮")]
	public Joystick _自由摇杆;

	public Joystick _固定摇杆;

	public Button _Btn_暂停游戏;

	public MyButton _Btn_棍花;

	public MyButton _Btn_重攻击;

	public MyButton _MyBtn_喝酒;

	public MyButton _MyBtn_跳跃;

	public MyButton _MyBtn_轻攻击;

	public MyButton _MyBtn_闪避;

	public MyButton _MyBtn_精魄;

	public MyButton _MyBtn_电话;

	public MyButton _MyBtn_AK;

	[Header("修行按钮--不在修行页面中")]
	public Button _Btn_修行按钮;

	public Animator _Ani_修行按钮;

	public UI_InGame_Main_修行 _UI_InGame_Main_修行;

	[Header("技能--定身术")]
	public GameObject _GO_定身术;

	private GameObject _GO_定身术_法力不足;

	private Image _Img_定身术_CD;

	[Header("技能--聚形散气")]
	public GameObject _GO_聚形散气;

	private GameObject _GO_聚形散气_法力不足;

	private Image _Img_聚形散气_CD;

	[Header("技能--化形")]
	public GameObject _GO_化形;

	private GameObject _GO_化形_法力不足;

	private Image _Img_化形_CD;

	[Header("技能--身外身法")]
	public GameObject _GO_身外身法;

	private GameObject _GO_身外身法_法力不足;

	private Image _Img_身外身法_CD;

	[Header("技能--精魄")]
	public GameObject _GO_精魄;

	public Image _Img_精魄;

	public Image _Img_精魄_头像;

	public Image _Img_化形_头像;

	public Image _Img_化形底图_头像;

	public Tween _tw_定身术CD_CD;

	public Tween _tw_聚形散气CD_CD;

	public Tween _tw_化形CD_CD;

	public Tween _tw_身外身法CD_CD;

	public Tween _tw_精魄CD_白;

	private Color co_Text_经验;

	private void Awake()
	{
		_GO_定身术_法力不足 = _GO_定身术.transform.Find("底").gameObject;
		_Img_定身术_CD = _GO_定身术.transform.Find("CD").GetComponent<Image>();
		_GO_聚形散气_法力不足 = _GO_聚形散气.transform.Find("底").gameObject;
		_Img_聚形散气_CD = _GO_聚形散气.transform.Find("CD").GetComponent<Image>();
		_GO_化形_法力不足 = _GO_化形.transform.Find("底").gameObject;
		_Img_化形_CD = _GO_化形.transform.Find("CD").GetComponent<Image>();
		_GO_身外身法_法力不足 = _GO_身外身法.transform.Find("底").gameObject;
		_Img_身外身法_CD = _GO_身外身法.transform.Find("CD").GetComponent<Image>();
	}

	public void F_设置摇杆类型()
	{
		if (!Lunch._PCUI)
		{
			if (DSB_MonoSingleton<UIManager>.Instance._是固定摇杆)
			{
				_自由摇杆.gameObject.SetActive(value: false);
				_固定摇杆.gameObject.SetActive(value: true);
				DSB_MonoSingleton<MyInputManager>.Instance._摇杆 = _固定摇杆;
			}
			else
			{
				_自由摇杆.gameObject.SetActive(value: true);
				_固定摇杆.gameObject.SetActive(value: false);
				DSB_MonoSingleton<MyInputManager>.Instance._摇杆 = _自由摇杆;
			}
		}
	}

	private void Start()
	{
		_CG_经验 = _能量条_经验.GetComponent<CanvasGroup>();
		_CG_经验.alpha = 0f;
		_CG_等级.alpha = 0f;
		F_设置摇杆类型();
		_Btn_暂停游戏.onClick.AddListener(DSB_MonoSingleton<UIManager>.Instance.F_InGame_暂停游戏);
		_MyBtn_喝酒._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_饮酒;
		_MyBtn_跳跃._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_跳跃;
		_MyBtn_跳跃._Up += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Up_跳跃;
		_MyBtn_轻攻击._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_轻攻击;
		_MyBtn_轻攻击._Up += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Up_轻攻击;
		_MyBtn_闪避._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_翻滚;
		_MyBtn_精魄._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_精魄;
		_MyBtn_电话._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_出租云;
		_MyBtn_AK._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_AK;
		_Btn_棍花._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_棍花;
		_Btn_棍花._Up += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Up_棍花;
		_Btn_重攻击._Down += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_重攻击;
		_Btn_重攻击._Up += DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Up_重攻击;
		F_绑定事件();
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_修行 += _UI_InGame_Main_修行.F_打开修行面板;
		_Btn_修行按钮.onClick.AddListener(DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_修行);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_修行点_Change._EventID, F_修行点_Change);
		if (Lunch._关闭多余UI && _GO_精魄.TryGetComponent<CanvasGroup>(out var component))
		{
			component.alpha = 0f;
		}
	}

	public void F_修行点_Change(DSB_EventArgs args)
	{
		EventArgs_Player_修行点_Change eventArgs_Player_修行点_Change = (EventArgs_Player_修行点_Change)args;
		if (Lunch._在没有修行点的时候隐藏按钮)
		{
			_Btn_修行按钮.gameObject.SetActive(eventArgs_Player_修行点_Change._当前Val > 0);
		}
		if (eventArgs_Player_修行点_Change._当前Val > 0)
		{
			_Ani_修行按钮.Play("修行按钮_浮动");
		}
		else
		{
			_Ani_修行按钮.Play("修行按钮_静止");
		}
	}

	public void F_绑定事件()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_生命_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_生命_Change eventArgs_Player_生命_Change = (EventArgs_Player_生命_Change)args;
			_能量条_生命.F_SetVal(eventArgs_Player_生命_Change._当前Val, eventArgs_Player_生命_Change._最大Val);
		});
		float 法力消耗_定身术 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(101)._法力消耗;
		float 法力消耗_聚形散气 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(102)._法力消耗;
		float 法力消耗_身外身法 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(104)._法力消耗;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_法力_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_法力_Change eventArgs_Player_法力_Change = (EventArgs_Player_法力_Change)args;
			_能量条_法力.F_SetVal(eventArgs_Player_法力_Change._当前Val, eventArgs_Player_法力_Change._最大Val);
			_GO_定身术_法力不足.SetActive(eventArgs_Player_法力_Change._当前Val < 法力消耗_定身术);
			_GO_聚形散气_法力不足.SetActive(eventArgs_Player_法力_Change._当前Val < 法力消耗_聚形散气);
			_GO_身外身法_法力不足.SetActive(eventArgs_Player_法力_Change._当前Val < 法力消耗_身外身法);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_气力_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_气力_Change eventArgs_Player_气力_Change = (EventArgs_Player_气力_Change)args;
			_能量条_气力.F_SetVal(eventArgs_Player_气力_Change._当前Val, eventArgs_Player_气力_Change._最大Val);
			_能量条_气力.F_SetVal_上限(eventArgs_Player_气力_Change._当前最大Val, eventArgs_Player_气力_Change._最大Val);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_蓄力_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_蓄力_Change eventArgs_Player_蓄力_Change = (EventArgs_Player_蓄力_Change)args;
			_C_蓄力.F_SetVal(eventArgs_Player_蓄力_Change._当前Val, eventArgs_Player_蓄力_Change._最大Val);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_豆子_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_豆子_Change eventArgs_Player_豆子_Change = (EventArgs_Player_豆子_Change)args;
			_C_蓄力.F_Set豆子(eventArgs_Player_豆子_Change._当前Val);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_酒_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_酒_Change eventArgs_Player_酒_Change = (EventArgs_Player_酒_Change)args;
			_能量条_酒.F_SetVal(eventArgs_Player_酒_Change._当前Val, eventArgs_Player_酒_Change._最大Val);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_等级_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_等级_Change eventArgs_Player_等级_Change = (EventArgs_Player_等级_Change)args;
			_Text_等级.text = eventArgs_Player_等级_Change._当前Val.ToString();
			_C_蓄力._豆子锁1.SetActive(eventArgs_Player_等级_Change._当前Val < DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.第二个豆等级);
			_C_蓄力._豆子锁2.SetActive(eventArgs_Player_等级_Change._当前Val < DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.第三个豆等级);
			_CG_等级.DOKill();
			Sequence s2 = DOTween.Sequence();
			s2.Append(_CG_等级.DOFade(1f, 0.3f));
			s2.AppendInterval(3f);
			s2.Append(_CG_等级.DOFade(0f, 0.3f));
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_经验_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_经验_Change eventArgs_Player_经验_Change = (EventArgs_Player_经验_Change)args;
			if (eventArgs_Player_经验_Change._变动Val == -1f)
			{
				_Text_经验.text = "";
				_能量条_经验.F_SetVal(eventArgs_Player_经验_Change._当前Val, eventArgs_Player_经验_Change._最大Val);
			}
			else
			{
				_Text_经验.text = "+" + eventArgs_Player_经验_Change._变动Val;
				_能量条_经验.F_SetVal(eventArgs_Player_经验_Change._当前Val, eventArgs_Player_经验_Change._最大Val);
				_CG_经验.DOKill();
				Sequence s = DOTween.Sequence();
				s.Append(_CG_经验.DOFade(1f, 0.3f));
				s.AppendInterval(3f);
				s.Append(_CG_经验.DOFade(0f, 0.3f));
			}
		});
	}

	public void F_开启化形UI(int 化形id)
	{
		_MyBtn_跳跃.gameObject.SetActive(value: false);
		_CG_Player.alpha = 0f;
		_CG_Player.blocksRaycasts = false;
		_GO_定身术.gameObject.SetActive(value: false);
		_GO_聚形散气.gameObject.SetActive(value: false);
		_GO_化形.gameObject.SetActive(value: true);
		_GO_身外身法.gameObject.SetActive(value: false);
		_UI_化形._CG.alpha = 1f;
		_UI_化形._CG.blocksRaycasts = true;
		_UI_化形.F_开启化形UI(化形id);
	}

	public void F_关闭化形UI()
	{
		_MyBtn_跳跃.gameObject.SetActive(value: true);
		_CG_Player.alpha = 1f;
		_CG_Player.blocksRaycasts = true;
		_UI_化形._CG.alpha = 0f;
		_UI_化形._CG.blocksRaycasts = false;
		F_设置技能的显示();
	}

	[Obsolete]
	public void F_化形_广智(bool 是开启)
	{
	}

	public void F_设置技能的显示()
	{
		PlayerItemDataInfo data_SL = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL;
		_GO_定身术.gameObject.SetActive(data_SL._解锁_定身术);
		_GO_聚形散气.gameObject.SetActive(data_SL._解锁_聚形散气);
		int 当前使用变身 = data_SL._当前使用变身;
		bool flag = data_SL._List_已解锁_变身.Contains(当前使用变身);
		_GO_化形.gameObject.SetActive(flag);
		if (flag)
		{
			F_设置化形图片(当前使用变身);
		}
		_GO_身外身法.gameObject.SetActive(data_SL._解锁_身外身法);
		int 当前使用精魄 = data_SL._当前使用精魄;
		bool flag2 = data_SL._List_已解锁_精魄.Contains(当前使用精魄);
		_GO_精魄.gameObject.SetActive(flag2);
		if (flag2)
		{
			F_设置精魄图片(当前使用精魄);
		}
		_MyBtn_电话.gameObject.SetActive(data_SL._解锁_出租云);
		_MyBtn_AK.gameObject.SetActive(data_SL._解锁_AK);
	}

	public void F_UI_定身术CD(float time)
	{
		bool 解锁_定身术 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_定身术;
		_GO_定身术.gameObject.SetActive(解锁_定身术);
		if (解锁_定身术)
		{
			_tw_定身术CD_CD?.Kill();
			_Img_定身术_CD.fillAmount = 1f;
			_tw_定身术CD_CD = _Img_定身术_CD.DOFillAmount(0f, time).SetEase(Ease.Linear);
		}
	}

	public void F_UI_聚形散气CD(float time)
	{
		bool 解锁_聚形散气 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_聚形散气;
		_GO_聚形散气.gameObject.SetActive(解锁_聚形散气);
		if (解锁_聚形散气)
		{
			_tw_聚形散气CD_CD?.Kill();
			_Img_聚形散气_CD.fillAmount = 1f;
			_tw_聚形散气CD_CD = _Img_聚形散气_CD.DOFillAmount(0f, time).SetEase(Ease.Linear);
		}
	}

	public void F_UI_化形CD(float time)
	{
		bool flag = DSB_MonoSingleton<MyDataManager>.Instance.F_判断当前化形是否已经解锁();
		_GO_化形.gameObject.SetActive(flag);
		if (flag)
		{
			F_设置化形图片(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用变身);
			_tw_化形CD_CD?.Kill();
			_Img_化形_CD.fillAmount = 1f;
			_tw_化形CD_CD = _Img_化形_CD.DOFillAmount(0f, time).SetEase(Ease.Linear);
		}
	}

	public void F_UI_身外身法CD(float time)
	{
		bool 解锁_身外身法 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_身外身法;
		_GO_身外身法.gameObject.SetActive(解锁_身外身法);
		if (解锁_身外身法)
		{
			_tw_身外身法CD_CD?.Kill();
			_Img_身外身法_CD.fillAmount = 1f;
			_tw_身外身法CD_CD = _Img_身外身法_CD.DOFillAmount(0f, time).SetEase(Ease.Linear);
		}
	}

	public void F_UI_精魄CD(float time)
	{
		PlayerItemDataInfo data_SL = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL;
		int 当前使用精魄 = data_SL._当前使用精魄;
		bool flag = data_SL._List_已解锁_精魄.Contains(当前使用精魄);
		_GO_精魄.gameObject.SetActive(flag);
		if (flag)
		{
			F_设置精魄图片(当前使用精魄);
			_tw_精魄CD_白?.Kill();
			_Img_精魄.fillAmount = 0f;
			_tw_精魄CD_白 = _Img_精魄.DOFillAmount(1f, time).SetEase(Ease.Linear);
		}
	}

	private void F_设置精魄图片(int 当前精魄ID)
	{
		_Img_精魄_头像.sprite = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(当前精魄ID)._技能图标;
	}

	private void F_设置化形图片(int 当前化形ID)
	{
		_Img_化形_头像.sprite = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(当前化形ID)._技能图标;
		_Img_化形底图_头像.sprite = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(当前化形ID)._技能图标;
	}

	public void F_暂停游戏()
	{
		_CG_等级.alpha = 1f;
		_CG_经验.alpha = 1f;
		co_Text_经验 = _Text_经验.color;
		co_Text_经验.a = 0f;
		_Text_经验.color = co_Text_经验;
	}

	public void F_继续游戏()
	{
		_CG_等级.alpha = 0f;
		_CG_经验.alpha = 0f;
		co_Text_经验.a = 1f;
		_Text_经验.color = co_Text_经验;
	}
}
