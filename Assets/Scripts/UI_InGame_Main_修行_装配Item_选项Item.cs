using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InGame_Main_修行_装配Item_选项Item : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	public Image _Img_图标;

	public int _当前ID = -1;

	private UI_InGame_Main_修行_装配Item _UI_装配Item;

	public bool _CanOnClick = true;

	private bool _已选中;

	private void Awake()
	{
		_UI_装配Item = GetComponentInParent<UI_InGame_Main_修行_装配Item>();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			if (_CanOnClick && _已选中)
			{
				_UI_装配Item.F_确认_选项Item(_当前ID);
			}
			_已选中 = true;
		});
	}

	public void F_初始化数据(int 当前使用ID)
	{
		_已选中 = false;
		_当前ID = 当前使用ID;
		if (_当前ID == -1)
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			_Img_图标.sprite = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(当前使用ID)._装配图标;
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		_Img_图标.DOColor(Color.green, 0.2f).SetUpdate(isIndependentUpdate: true);
		if (_CanOnClick)
		{
			_UI_装配Item.F_文本提示(_当前ID);
		}
		else
		{
			_UI_装配Item.F_文本提示("暂未解锁");
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		_已选中 = false;
		_Img_图标.DOColor(Color.white, 0.2f).SetUpdate(isIndependentUpdate: true);
	}

	public void F_被展开时()
	{
		_已选中 = false;
	}
}
