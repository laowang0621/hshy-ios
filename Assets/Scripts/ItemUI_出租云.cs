using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI_出租云 : ItemUIBase<ItemUI_出租云>
{
	public int _普通字体大小 = 35;

	public int _放大字体大小 = 50;

	public Button _Btn_上云;

	public Button _Btn_退出大地图;

	public Button _Btn_退出小地图;

	public CanvasGroup _CG_大地图;

	public CanvasGroup _CG_小地图;

	public List<Button> _List_大地图;

	public List<Button> _List_小地图;

	private List<Text> _ListText_大地图;

	private List<Text> _ListText_小地图;

	private List<int> _List_所有上香的地图ID;

	private int _点击的大地图序号;

	private int _点击的小地图序号;

	private float _渐变补间速度 = 0.2f;

	private bool _打开云;

	public override bool _可见性
	{
		get
		{
			return _m可见性;
		}
		set
		{
			if (_Btn_上云.gameObject.activeSelf)
			{
				if (value)
				{
					base._CG.alpha = 1f;
				}
				else
				{
					base._CG.alpha = 0f;
				}
				base._CG.interactable = value;
				base._CG.blocksRaycasts = value;
				_m可见性 = value;
			}
		}
	}

	public override ItemUI_出租云 F_Init()
	{
		base.F_Init();
		_打开云 = false;
		F_初始化云();
		return this;
	}

	public void F_上云()
	{
		_打开云 = true;
		GameManager._CanPlay = false;
		Time.timeScale = 0f;
		F_SetPos(Camera.main.transform.position);
		_Btn_上云.gameObject.SetActive(value: false);
		_CG_大地图.interactable = true;
		_CG_大地图.blocksRaycasts = true;
		_CG_大地图.DOFade(1f, _渐变补间速度).SetUpdate(isIndependentUpdate: true);
		if (_List_大地图[0].gameObject.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(_List_大地图[0].gameObject);
		}
		else
		{
			EventSystem.current.SetSelectedGameObject(_Btn_退出大地图.gameObject);
		}
	}

	private void OnDestroy()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_翻滚 -= F_退出大地图;
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC -= F_退出大地图;
	}

	private void F_退出大地图()
	{
		if (_打开云)
		{
			_打开云 = false;
			GameManager._CanPlay = true;
			Time.timeScale = 1f;
			_ListText_大地图[_点击的大地图序号].fontSize = _普通字体大小;
			_ListText_小地图[_点击的小地图序号].fontSize = _普通字体大小;
			_Btn_上云.gameObject.SetActive(value: true);
			_CG_大地图.interactable = false;
			_CG_大地图.blocksRaycasts = false;
			_CG_大地图.DOFade(0f, _渐变补间速度).SetUpdate(isIndependentUpdate: true);
			_CG_小地图.interactable = false;
			_CG_小地图.blocksRaycasts = false;
			_CG_小地图.DOFade(0f, _渐变补间速度).SetUpdate(isIndependentUpdate: true);
		}
	}

	private void F_初始化云()
	{
		_Btn_上云.gameObject.SetActive(value: true);
		_CG_大地图.interactable = false;
		_CG_大地图.blocksRaycasts = false;
		_CG_大地图.alpha = 0f;
		_CG_小地图.interactable = false;
		_CG_小地图.blocksRaycasts = false;
		_CG_小地图.alpha = 0f;
		_ListText_大地图 = new List<Text>();
		_ListText_小地图 = new List<Text>();
		foreach (Button item2 in _List_大地图)
		{
			Text componentInChildren = item2.GetComponentInChildren<Text>();
			_ListText_大地图.Add(componentInChildren);
			componentInChildren.fontSize = _普通字体大小;
		}
		foreach (Button item3 in _List_小地图)
		{
			Text componentInChildren2 = item3.GetComponentInChildren<Text>();
			_ListText_小地图.Add(componentInChildren2);
			componentInChildren2.fontSize = _普通字体大小;
		}
		_Btn_上云.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_交互();
		});
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_翻滚 += F_退出大地图;
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_ESC += F_退出大地图;
		_Btn_退出大地图.onClick.AddListener(delegate
		{
			F_退出大地图();
		});
		_Btn_退出小地图.onClick.AddListener(delegate
		{
			EventSystem.current.SetSelectedGameObject(_Btn_退出大地图.gameObject);
			_ListText_大地图[_点击的大地图序号].fontSize = _普通字体大小;
			_ListText_小地图[_点击的小地图序号].fontSize = _普通字体大小;
			_CG_小地图.interactable = false;
			_CG_小地图.blocksRaycasts = false;
			_CG_小地图.DOFade(0f, _渐变补间速度).SetUpdate(isIndependentUpdate: true);
		});
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		_List_所有上香的地图ID = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_所有上香的地图ID;
		foreach (int item4 in _List_所有上香的地图ID)
		{
			int key = item4 / 100;
			if (dictionary.TryGetValue(key, out var value))
			{
				value.TryAdd(item4);
				continue;
			}
			dictionary.Add(key, new List<int> { item4 });
		}
		List<string> list_章节地图Name = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Map._List_章节地图Name;
		int i = 0;
		foreach (KeyValuePair<int, List<int>> item in dictionary)
		{
			int index = i++;
			_List_大地图[index].gameObject.SetActive(value: true);
			_ListText_大地图[index].text = list_章节地图Name[item.Key];
			_List_大地图[index].onClick.RemoveAllListeners();
			_List_大地图[index].onClick.AddListener(delegate
			{
				_ListText_大地图[_点击的大地图序号].fontSize = _普通字体大小;
				_ListText_小地图[_点击的小地图序号].fontSize = _普通字体大小;
				_点击的大地图序号 = index;
				_ListText_大地图[index].fontSize = _放大字体大小;
				_CG_小地图.interactable = true;
				_CG_小地图.blocksRaycasts = true;
				_CG_小地图.DOFade(1f, _渐变补间速度).SetUpdate(isIndependentUpdate: true);
				F_绑定小地图(item.Value);
			});
		}
		for (; i < _List_大地图.Count; i++)
		{
			_List_大地图[i].gameObject.SetActive(value: false);
		}
	}

	public void F_绑定小地图(List<int> miniMap)
	{
		SOData_Map sOData_Map = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Map;
		int i = 0;
		foreach (int item in miniMap)
		{
			int index = i++;
			_List_小地图[index].gameObject.SetActive(value: true);
			_ListText_小地图[index].text = sOData_Map.F_Map_ID_Get_Name(item);
			_List_小地图[index].onClick.RemoveAllListeners();
			_List_小地图[index].onClick.AddListener(delegate
			{
				if (_ListText_小地图[index].fontSize == _放大字体大小)
				{
					DSB_MonoSingleton<GameManager>.Instance.F_上香(item);
				}
				else
				{
					_ListText_小地图[_点击的小地图序号].fontSize = _普通字体大小;
					_点击的小地图序号 = index;
					_ListText_小地图[index].fontSize = _放大字体大小;
				}
			});
		}
		for (; i < _List_小地图.Count; i++)
		{
			_List_小地图[i].gameObject.SetActive(value: false);
		}
	}
}
