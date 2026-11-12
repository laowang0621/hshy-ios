using DSB;
using UnityEngine;

public class UI_InGame_Main_化形 : MonoBehaviour
{
	public CanvasGroup _CG;

	public ItemUI_能量条 _能量条_生命;

	public ItemUI_能量条 _能量条_神力;

	public ItemUI_能量条 _能量条_蓄力_广智;

	public GameObject _蓄力豆_集合_黄龙;

	public GameObject[] _List_蓄力豆_集合_黄龙;

	public CanvasGroup _CG_广智;

	public CanvasGroup _CG_寅虎;

	public CanvasGroup _CG_黄龙;

	private int _当期化形ID = 2001;

	private void Start()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_化形_生命_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_化形_生命_Change eventArgs_Player_化形_生命_Change = (EventArgs_Player_化形_生命_Change)args;
			_能量条_生命.F_SetVal(eventArgs_Player_化形_生命_Change._当前Val, eventArgs_Player_化形_生命_Change._最大Val);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_化形_神力_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_化形_神力_Change eventArgs_Player_化形_神力_Change = (EventArgs_Player_化形_神力_Change)args;
			_能量条_神力.F_SetVal(eventArgs_Player_化形_神力_Change._当前Val, eventArgs_Player_化形_神力_Change._最大Val);
		});
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_化形_蓄力_Change._EventID, delegate(DSB_EventArgs args)
		{
			EventArgs_Player_化形_蓄力_Change eventArgs_Player_化形_蓄力_Change = (EventArgs_Player_化形_蓄力_Change)args;
			if (_当期化形ID == 2003)
			{
				int num = Mathf.RoundToInt(eventArgs_Player_化形_蓄力_Change._当前Val);
				for (int i = 0; i < _List_蓄力豆_集合_黄龙.Length; i++)
				{
					_List_蓄力豆_集合_黄龙[i].SetActive(i < num);
				}
			}
			else
			{
				_能量条_蓄力_广智.F_SetVal(eventArgs_Player_化形_蓄力_Change._当前Val, eventArgs_Player_化形_蓄力_Change._最大Val);
			}
		});
	}

	private void F_隐藏所有头像()
	{
		_CG_广智.alpha = 0f;
		_CG_寅虎.alpha = 0f;
		_CG_黄龙.alpha = 0f;
	}

	private void F_隐藏所有蓄力条()
	{
		_能量条_蓄力_广智.gameObject.SetActive(value: false);
		_蓄力豆_集合_黄龙.SetActive(value: false);
	}

	public void F_开启化形UI(int 化形id)
	{
		F_隐藏所有头像();
		F_隐藏所有蓄力条();
		_当期化形ID = 化形id;
		switch (化形id)
		{
		case 2001:
			_CG_广智.alpha = 1f;
			_能量条_蓄力_广智.gameObject.SetActive(value: true);
			break;
		case 2002:
			_CG_寅虎.alpha = 1f;
			_能量条_蓄力_广智.gameObject.SetActive(value: true);
			break;
		case 2003:
			_CG_黄龙.alpha = 1f;
			_蓄力豆_集合_黄龙.SetActive(value: true);
			break;
		default:
			_CG_广智.alpha = 1f;
			_能量条_蓄力_广智.gameObject.SetActive(value: true);
			break;
		}
	}
}
