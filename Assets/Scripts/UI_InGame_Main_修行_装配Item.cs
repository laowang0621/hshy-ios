using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InGame_Main_修行_装配Item : MonoBehaviour
{
	public Sprite _Sprite_空白;

	public Image _Img_图标;

	public Transform _Tsf_选项;

	private CanvasGroup _CG_选项;

	public Transform _Tsf_当前选中;

	private Text _Text_当前选中;

	private UI_InGame_Main_修行_装配 _UI_装配;

	public Button _Btn_主要装配;

	public List<UI_InGame_Main_修行_装配Item_选项Item> _List_选项;

	public bool _当前是展开状态;

	public int _当前选中ID = -1;

	private void Awake()
	{
		_CG_选项 = _Tsf_选项.GetComponent<CanvasGroup>();
		_Text_当前选中 = _Tsf_当前选中.GetComponent<Text>();
		_UI_装配 = base.transform.GetComponentInParent<UI_InGame_Main_修行_装配>();
	}

	private void Start()
	{
		foreach (Transform item in _Tsf_选项)
		{
			item.gameObject.SetActive(value: false);
		}
		_Btn_主要装配.onClick.AddListener(F_点击主要装配按钮);
	}

	public void F_初始化数据(int 当前使用ID, List<int> 所有已解锁的ID)
	{
		foreach (Transform item in _Tsf_选项)
		{
			item.gameObject.SetActive(value: false);
		}
		if (所有已解锁的ID.Count <= 0)
		{
			_Img_图标.SetAlpha(0f);
			_Text_当前选中.text = "无";
			_Btn_主要装配.interactable = false;
			return;
		}
		_Btn_主要装配.interactable = true;
		F_设置当前选中信息(当前使用ID);
		foreach (UI_InGame_Main_修行_装配Item_选项Item item2 in _List_选项)
		{
			item2._当前ID = -1;
		}
		_CG_选项.blocksRaycasts = false;
		_CG_选项.interactable = false;
		for (int i = 0; i < 所有已解锁的ID.Count && i < _List_选项.Count; i++)
		{
			int index = i;
			_List_选项[index].gameObject.SetActive(value: true);
			_List_选项[index].F_初始化数据(所有已解锁的ID[index]);
		}
	}

	private void F_设置当前选中信息(int id)
	{
		_当前选中ID = id;
		if (DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_TryGet_Player_技能(id, out var dataInfo_Player技能))
		{
			_Img_图标.sprite = dataInfo_Player技能._装配图标;
			_Img_图标.SetAlpha(1f);
			_Text_当前选中.text = dataInfo_Player技能._技能Name;
		}
		else
		{
			_Img_图标.SetAlpha(0f);
			_Text_当前选中.text = "无";
		}
	}

	private void F_点击主要装配按钮()
	{
		if (_当前是展开状态)
		{
			F_收起();
		}
		else
		{
			F_展开();
		}
	}

	public void F_收起()
	{
		_当前是展开状态 = false;
		_CG_选项.blocksRaycasts = false;
		_CG_选项.interactable = false;
		_Tsf_选项.DOScale(Vector3.one, 0.15f).SetUpdate(isIndependentUpdate: true);
		_CG_选项.DOFade(0f, 0.15f).SetUpdate(isIndependentUpdate: true);
		_Text_当前选中.DOFade(1f, 0.15f).SetUpdate(isIndependentUpdate: true);
	}

	public void F_展开()
	{
		_当前是展开状态 = true;
		_UI_装配.F_收起装配Item(this);
		_CG_选项.blocksRaycasts = true;
		_CG_选项.interactable = true;
		_Text_当前选中.DOFade(0f, 0.15f).SetUpdate(isIndependentUpdate: true);
		_Tsf_选项.DOScale(Vector3.one, 0.15f).SetUpdate(isIndependentUpdate: true);
		_CG_选项.DOFade(1f, 0.15f).SetUpdate(isIndependentUpdate: true);
		foreach (UI_InGame_Main_修行_装配Item_选项Item item in _List_选项)
		{
			item.F_被展开时();
		}
	}

	public void F_强制收起()
	{
		_当前是展开状态 = false;
		_CG_选项.alpha = 0f;
		_Text_当前选中.SetAlpha(1f);
	}

	public void F_确认_选项Item(int id)
	{
		F_设置当前选中信息(id);
		F_收起();
		EventSystem.current.SetSelectedGameObject(_Btn_主要装配.gameObject);
		_UI_装配.F_清空Tip();
	}

	public void F_文本提示(int id)
	{
		F_文本提示(DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(id)._技能Name);
	}

	public void F_文本提示(string tips)
	{
		_UI_装配.F_文本提示(tips, _会消失: false);
	}
}
