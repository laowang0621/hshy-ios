using System;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_歪瓜 : MonoBehaviour
{
	private CanvasGroup _CG;

	private bool _;

	public Toggle _Toggle_10倍攻击;

	public Toggle _Toggle_3豆状态;

	public Toggle _Toggle_免疫伤害;

	public Toggle _Toggle_10倍移速;

	public Button _Button_喝假酒;

	public Button _Button_经验加66;

	public Button _Button_初始状态;

	public Button _Btn_神秘按钮;

	public Button _Btn_原地上香;

	public GameObject _龙卷风_Prefab;

	public Button _Btn_转送;

	public Transform _Tsf_传送子按钮;

	public Button _Button_重置技能;

	public Button _Button_解锁四技;

	public Button _Button_解锁精魄;

	public List<Button> _List_Btn_章节;

	public List<Transform> _List_Tsf_章节;

	public UI_InGame_歪瓜_传送按钮 _Item_传送按钮;

	public Button _Btn_减号;

	public Button _Btn_加号;

	public InputField _Text_等级;

	public Button _Btn_确定等级;

	public Slider _Slider_透明度;

	public Button _Button_恢复所有对话;

	public Button _Button_删除所有对话;

	public Button _Button_恢复所有宝箱;

	public Button _Button_解锁同伴;

	public Button _Button_加时间;

	public Button _Button_减时间;

	private int currID;

	private void Start()
	{
		_CG = GetComponent<CanvasGroup>();
		if (!Lunch._开启面板)
		{
			_CG.alpha = 0f;
			_CG.interactable = false;
			_CG.blocksRaycasts = false;
			return;
		}
		_ = true;
		_CG.alpha = (_ ? 1 : 0);
		_CG.blocksRaycasts = _;
		DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.面板.started += delegate
		{
			_ = !_;
			_CG.alpha = (_ ? 1 : 0);
			_CG.blocksRaycasts = _;
		};
		_Btn_神秘按钮.onClick.AddListener(delegate
		{
			UnityEngine.Object.Instantiate(_龙卷风_Prefab, DSB_MonoSingleton<GameManager>.Instance._PC.transform.position, Quaternion.identity);
		});
		_Toggle_10倍攻击.isOn = GameManager._10倍攻击;
		Text text_10倍攻击 = _Toggle_10倍攻击.transform.Find("Label").GetComponent<Text>();
		text_10倍攻击.color = (_Toggle_10倍攻击.isOn ? Color.green : Color.red);
		_Toggle_10倍攻击.onValueChanged.AddListener(delegate(bool b)
		{
			GameManager._10倍攻击 = b;
			text_10倍攻击.color = (b ? Color.green : Color.red);
		});
		_Toggle_3豆状态.isOn = GameManager._3豆状态;
		Text text_3豆状态 = _Toggle_3豆状态.transform.Find("Label").GetComponent<Text>();
		text_3豆状态.color = (_Toggle_3豆状态.isOn ? Color.green : Color.red);
		_Toggle_3豆状态.onValueChanged.AddListener(delegate(bool b)
		{
			GameManager._3豆状态 = b;
			text_3豆状态.color = (b ? Color.green : Color.red);
		});
		_Toggle_免疫伤害.isOn = GameManager._免疫伤害;
		Text text_免疫攻击 = _Toggle_免疫伤害.transform.Find("Label").GetComponent<Text>();
		text_免疫攻击.color = (_Toggle_免疫伤害.isOn ? Color.green : Color.red);
		_Toggle_免疫伤害.onValueChanged.AddListener(delegate(bool b)
		{
			GameManager._免疫伤害 = b;
			text_免疫攻击.color = (b ? Color.green : Color.red);
		});
		_Toggle_10倍移速.isOn = GameManager._10倍移速;
		Text text_固定摇杆 = _Toggle_10倍移速.transform.Find("Label").GetComponent<Text>();
		text_固定摇杆.color = (_Toggle_10倍移速.isOn ? Color.green : Color.red);
		_Toggle_10倍移速.onValueChanged.AddListener(delegate(bool b)
		{
			GameManager._10倍移速 = b;
			text_固定摇杆.color = (b ? Color.green : Color.red);
		});
		_Button_喝假酒.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<GameManager>.Instance.F_喝假酒();
		});
		_Button_经验加66.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(328f);
		});
		_Button_重置技能.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_定身术 = false;
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_聚形散气 = false;
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_化形_广智 = false;
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_身外身法 = false;
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._解锁_精魄_幽魂 = false;
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Dict_精魄解锁状态.Clear();
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_精魄.Clear();
		});
		_Button_解锁四技.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<GameManager>.Instance.F_解锁_定身术();
			DSB_MonoSingleton<GameManager>.Instance.F_解锁_聚形散气();
			DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_化形(2001);
			DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_化形(2002);
			DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_化形(2003);
			DSB_MonoSingleton<GameManager>.Instance.F_解锁_身外身法();
		});
		_Button_解锁精魄.onClick.AddListener(delegate
		{
			for (int j = 1005; j <= 1012; j++)
			{
				int 技能ID = j;
				DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_精魄(技能ID);
			}
		});
		_Button_初始状态.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_Boss已击败 = new List<int>();
		});
		_Btn_原地上香.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<GameManager>.Instance.F_上香();
		});
		_Tsf_传送子按钮.transform.localScale = Vector3.zero;
		bool canLook = false;
		_Btn_转送.onClick.AddListener(delegate
		{
			canLook = !canLook;
			_Tsf_传送子按钮.transform.DOScale(canLook ? 1 : 0, 0.2f).SetUpdate(isIndependentUpdate: true);
		});
		_Button_恢复所有对话.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_对话类型_ID_已完成.Clear();
		});
		_Button_删除所有对话.onClick.AddListener(delegate
		{
			Enum_对话[] array = (Enum_对话[])Enum.GetValues(typeof(Enum_对话));
			foreach (Enum_对话 对话ID in array)
			{
				DSB_MonoSingleton<MyDataManager>.Instance.F_完成对话((int)对话ID);
			}
		});
		_Button_恢复所有宝箱.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已开启箱子ID_新.Clear();
		});
		F_传送按钮部分();
		_Button_加时间.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._该存档游玩时间 += 720f;
		});
		_Button_减时间.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._该存档游玩时间 -= 720f;
		});
		_Button_解锁同伴.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<GameManager>.Instance.F_解锁并生成同伴(3001);
			DSB_MonoSingleton<GameManager>.Instance.F_解锁并生成同伴(3002);
		});
	}

	private void OnEnable()
	{
	}

	public void F_传送按钮部分()
	{
		for (int i = 0; i < _List_Btn_章节.Count; i++)
		{
			int currindex = i;
			_List_Btn_章节[currindex].onClick.AddListener(delegate
			{
				F_点击章节按钮(currindex);
			});
		}
		foreach (DataInfo_Map item in DSB_MonoSingleton<MyDataManager>.Instance._SOData_Map._List_Map_Infp)
		{
			int num = item._MapID / 100;
			if (num >= 0 && num < _List_Tsf_章节.Count)
			{
				UnityEngine.Object.Instantiate(_Item_传送按钮, _List_Tsf_章节[num]).F_Init(item._MapID, item._MapName);
			}
		}
		F_点击章节按钮(666);
		List<DataInfo_Player等级> list_Player等级 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player._List_Player等级;
		int curtrLevel = 100;
		int minLevel = 0;
		int maxLevel = list_Player等级.Count - 1;
		_Text_等级.text = curtrLevel.ToString();
		_Text_等级.onSubmit.AddListener(delegate(string str)
		{
			if (int.TryParse(str, out curtrLevel))
			{
				curtrLevel = Mathf.Clamp(curtrLevel, minLevel, maxLevel);
			}
		});
		_Text_等级.onValueChanged.AddListener(delegate(string str)
		{
			if (int.TryParse(str, out curtrLevel))
			{
				curtrLevel = Mathf.Clamp(curtrLevel, minLevel, maxLevel);
			}
		});
		_Btn_减号.onClick.AddListener(delegate
		{
			curtrLevel--;
			curtrLevel = Mathf.Clamp(curtrLevel, minLevel, maxLevel);
			_Text_等级.text = curtrLevel.ToString();
		});
		_Btn_加号.onClick.AddListener(delegate
		{
			curtrLevel++;
			curtrLevel = Mathf.Clamp(curtrLevel, minLevel, maxLevel);
			_Text_等级.text = curtrLevel.ToString();
		});
		_Btn_确定等级.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player等级 = curtrLevel;
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._Player经验 = 0f;
			DSB_MonoSingleton<GameManager>.Instance._PC._Data.F_初始化属性(游戏中升级: true);
			DSB_MonoSingleton<GameManager>.Instance._PC._Data.F_触发所有事件();
		});
		_Slider_透明度.value = 0.7f;
		_CG.alpha = 0.7f;
		_Slider_透明度.onValueChanged.AddListener(delegate(float val)
		{
			_CG.alpha = val;
		});
	}

	public void F_点击章节按钮(int index)
	{
		if (index == currID)
		{
			currID = 666;
			for (int i = 0; i < _List_Tsf_章节.Count; i++)
			{
				_List_Tsf_章节[i].transform.DOScale(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
			}
			return;
		}
		currID = index;
		for (int j = 0; j < _List_Tsf_章节.Count; j++)
		{
			int num = j;
			_List_Tsf_章节[num].transform.DOScale((index == num) ? 1 : 0, 0.2f).SetUpdate(isIndependentUpdate: true);
		}
	}
}
