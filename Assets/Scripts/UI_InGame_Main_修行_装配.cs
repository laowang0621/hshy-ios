using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InGame_Main_修行_装配 : MonoBehaviour
{
	public CanvasGroup _CG;

	public UI_InGame_Main_修行 _UI_修行;

	public Text _Text_文字提示;

	public RectTransform RTsf_文字提示;

	public Button _Btn_返回加点;

	public Button _Btn_返回加点_2;

	public Button _Btn_关闭修行;

	public UI_InGame_Main_修行_装配Item _UI_装配Item_变身;

	public UI_InGame_Main_修行_装配Item _UI_装配Item_精魄;

	public UI_InGame_Main_修行_装配Item _UI_装配Item_套装;

	public UI_InGame_Main_修行_装配Item _UI_装配Item_同伴;

	private Sequence sequence;

	private void Awake()
	{
	}

	private void Start()
	{
		_Btn_关闭修行.onClick.AddListener(_UI_修行.F_关闭修行);
		_Btn_返回加点.onClick.AddListener(F_返回加点);
		_Btn_返回加点_2.onClick.AddListener(F_返回加点);
		F_强制收起全部子装配();
	}

	public void F_收起装配Item(UI_InGame_Main_修行_装配Item 忽略项 = null)
	{
		if (_UI_装配Item_变身 != 忽略项)
		{
			_UI_装配Item_变身.F_收起();
		}
		if (_UI_装配Item_精魄 != 忽略项)
		{
			_UI_装配Item_精魄.F_收起();
		}
		if (_UI_装配Item_套装 != 忽略项)
		{
			_UI_装配Item_套装.F_收起();
		}
		if (_UI_装配Item_同伴 != 忽略项)
		{
			_UI_装配Item_同伴.F_收起();
		}
	}

	public void F_打开修行面板()
	{
		PlayerItemDataInfo data_SL = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL;
		_UI_装配Item_精魄.F_初始化数据(data_SL._当前使用精魄, data_SL._List_已解锁_精魄);
		_UI_装配Item_变身.F_初始化数据(data_SL._当前使用变身, data_SL._List_已解锁_变身);
		_UI_装配Item_同伴.F_初始化数据(data_SL._当前使用同伴, data_SL._List_已解锁_同伴);
		_UI_装配Item_套装.F_初始化数据(data_SL._当前使用套装, data_SL._List_已解锁_套装);
	}

	public void F_关闭修行()
	{
		PlayerItemDataInfo data_SL = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL;
		if (data_SL._当前使用精魄 != _UI_装配Item_精魄._当前选中ID)
		{
			data_SL._当前使用精魄 = _UI_装配Item_精魄._当前选中ID;
			float 冷却时间 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(data_SL._当前使用精魄)._冷却时间;
			DSB_MonoSingleton<GameManager>.Instance._PC._Data.F_设置技能CD(DSB_MonoSingleton<GameManager>.Instance._PC._Data._精魄TimerID, 冷却时间);
		}
		if (data_SL._当前使用变身 != _UI_装配Item_变身._当前选中ID)
		{
			data_SL._当前使用变身 = _UI_装配Item_变身._当前选中ID;
			float 冷却时间2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(data_SL._当前使用变身)._冷却时间;
			DSB_MonoSingleton<GameManager>.Instance._PC._Data.F_设置技能CD(DSB_MonoSingleton<GameManager>.Instance._PC._Data._化形TimerID, 冷却时间2);
		}
		if (data_SL._当前使用同伴 != _UI_装配Item_同伴._当前选中ID)
		{
			data_SL._当前使用同伴 = _UI_装配Item_同伴._当前选中ID;
			EventArgs_清空同伴.Trigger(有烟雾生成: true);
			DSB_MonoSingleton<GameManager>.Instance.F_生成同伴(null, 生成烟雾: true);
		}
		data_SL._当前使用套装 = _UI_装配Item_套装._当前选中ID;
	}

	public void F_打开当前面板()
	{
		EventSystem.current.SetSelectedGameObject(_Btn_返回加点_2.gameObject);
		_CG.DOFade(1f, 0.2f).SetUpdate(isIndependentUpdate: true);
		_CG.interactable = true;
		_CG.blocksRaycasts = true;
		F_强制收起全部子装配();
		F_强制清空Tip();
	}

	public void F_关闭当前面板()
	{
		_CG.DOFade(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
		_CG.interactable = false;
		_CG.blocksRaycasts = false;
	}

	private void F_返回加点()
	{
		F_关闭当前面板();
		_UI_修行._UI_加点.F_打开当前面板();
	}

	private void F_强制收起全部子装配()
	{
		_UI_装配Item_变身.F_强制收起();
		_UI_装配Item_精魄.F_强制收起();
		_UI_装配Item_套装.F_强制收起();
		_UI_装配Item_同伴.F_强制收起();
	}

	private void F_强制清空Tip()
	{
		sequence?.Kill();
		RTsf_文字提示.DOKill();
		RTsf_文字提示.localScale = Vector3.zero;
	}

	public void F_清空Tip()
	{
		sequence?.Kill();
		RTsf_文字提示.DOKill();
		RTsf_文字提示.DOScale(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
	}

	public void F_文本提示(string tips, bool _会消失)
	{
		F_强制清空Tip();
		_Text_文字提示.text = tips;
		sequence = DOTween.Sequence().SetUpdate(isIndependentUpdate: true);
		sequence.Append(RTsf_文字提示.DOScale(1f, 0.2f).SetUpdate(isIndependentUpdate: true));
		if (_会消失)
		{
			sequence.AppendInterval(1.45f).SetUpdate(isIndependentUpdate: true);
			sequence.Append(RTsf_文字提示.DOScale(0f, 0.2f).SetUpdate(isIndependentUpdate: true));
		}
	}
}
