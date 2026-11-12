using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class My加点ItemInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	public int _BtnID;

	private CanvasGroup _CG;

	private Text _Text_点数提示;

	private Button _Btn_点数;

	private Image _Img;

	private UI_InGame_Main_修行_加点 _UI_加点;

	[HideInInspector]
	public bool _CanOnClick = true;

	private bool _已选中;

	public void OnAwake()
	{
		_CG = GetComponent<CanvasGroup>();
		_Img = GetComponentInChildren<Image>();
		_Btn_点数 = GetComponent<Button>();
		_Text_点数提示 = GetComponentInChildren<Text>();
		_UI_加点 = GetComponentInParent<UI_InGame_Main_修行_加点>();
	}

	public void OnShow()
	{
		_已选中 = false;
		F_判断是否满级();
	}

	private void Start()
	{
		_Btn_点数.onClick.AddListener(delegate
		{
			if (_CanOnClick && _已选中)
			{
				if (DSB_MonoSingleton<MyDataManager>.Instance.F_添加BtnID点数(_BtnID, out var is修行点足够))
				{
					DataInfo_PlayerLevel_Buff dataInfo_PlayerLevel_Buff = DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_Buff(_BtnID);
					if (_BtnID == 1001 || _BtnID == 1002 || _BtnID == 1003 || _BtnID == 1004)
					{
						_UI_加点.F_测试更新数值显示();
					}
					if (dataInfo_PlayerLevel_Buff._升级提示类型 == 0)
					{
						_UI_加点.F_文本提示(_BtnID);
					}
					else
					{
						_UI_加点.F_弹窗提示(dataInfo_PlayerLevel_Buff._升级描述, _BtnID);
					}
					if (F_判断是否满级())
					{
						DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_修行_升级();
					}
					else
					{
						DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_修行_满级();
					}
				}
				else if (!is修行点足够)
				{
					_UI_加点.F_文字提示("修行点不足!");
				}
			}
			_已选中 = true;
		});
	}

	private bool F_判断是否满级()
	{
		int num = DSB_MonoSingleton<MyDataManager>.Instance.F_Get_BtnID_加点点数(_BtnID);
		int num2 = DSB_MonoSingleton<MyDataManager>.Instance.F_Get_BtnID_最大点数(_BtnID);
		F_Set_点数提示(num, num2);
		if (num < num2)
		{
			_CG.interactable = true;
			_CG.blocksRaycasts = true;
			return false;
		}
		_CG.interactable = false;
		_CG.blocksRaycasts = false;
		return true;
	}

	public void F_Set_点数提示(int curr点数, int max点数)
	{
		if (_CanOnClick)
		{
			_Text_点数提示.text = $"{curr点数}/{max点数}";
		}
		else
		{
			_Text_点数提示.text = "未解锁";
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		_Img.DOColor(Color.green, 0.2f).SetUpdate(isIndependentUpdate: true);
		if (_CanOnClick)
		{
			_UI_加点.F_文本提示(_BtnID);
		}
		else
		{
			_UI_加点.F_文字提示("暂未解锁该法术", _会消失: false);
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		_已选中 = false;
		_Img.DOColor(Color.white, 0.2f).SetUpdate(isIndependentUpdate: true);
	}
}
