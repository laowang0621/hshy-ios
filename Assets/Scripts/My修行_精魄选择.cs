using System.Collections.Generic;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class My修行_精魄选择 : MonoBehaviour
{
	private Animator _Ani_精魄选择;

	private bool _CanClickBtn;

	private UI_InGame_Main_修行_加点 _UI_加点;

	public Image _Img_1;

	public Image _Img_2;

	public Image _Img_3;

	public Image _Img_4;

	public int _打开时精魄 = 1005;

	public List<DataInfo_Player技能> list_所有已解锁技能;

	public int _在List中索引;

	public int _当前选中精魄 = 1005;

	private void Awake()
	{
		_Ani_精魄选择 = GetComponent<Animator>();
		_UI_加点 = GetComponentInParent<UI_InGame_Main_修行_加点>();
		_CanClickBtn = true;
	}

	public void F_打开修行面板()
	{
		GameManager._CanPlay = false;
		_打开时精魄 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用精魄;
		_当前选中精魄 = _打开时精魄;
		MyLog.LogError("_打开时精魄=" + _打开时精魄);
		List<int> list_已解锁_精魄 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_精魄;
		list_所有已解锁技能 = new List<DataInfo_Player技能>();
		_在List中索引 = 0;
		bool flag = false;
		foreach (int item in list_已解锁_精魄)
		{
			list_所有已解锁技能.Add(DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(item));
			if (!flag)
			{
				if (item == _打开时精魄)
				{
					flag = true;
				}
				else
				{
					_在List中索引++;
				}
			}
		}
		if (list_所有已解锁技能.Count <= 0)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		F_设置技能图片();
		_在List中索引 = F_得到范围内循环的索引(_在List中索引, list_所有已解锁技能.Count);
	}

	public void F_关闭修行()
	{
		GameManager._CanPlay = true;
		if (base.gameObject.activeSelf)
		{
			_当前选中精魄 = list_所有已解锁技能[F_得到范围内循环的索引(_在List中索引, list_所有已解锁技能.Count)]._技能ID;
			if (_打开时精魄 != _当前选中精魄)
			{
				DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用精魄 = _当前选中精魄;
				float 冷却时间 = list_所有已解锁技能[_在List中索引]._冷却时间;
				DSB_MonoSingleton<GameManager>.Instance._PC._Data.F_设置技能CD(DSB_MonoSingleton<GameManager>.Instance._PC._Data._精魄TimerID, 冷却时间);
			}
		}
	}

	public void F_选中按钮()
	{
		_UI_加点.F_文字提示(list_所有已解锁技能[_在List中索引]._技能Name);
	}

	public void F_Ani_提示当前选中的精魄()
	{
		F_选中按钮();
	}

	public void F_Ani_可以点击按钮()
	{
		if (!_CanClickBtn)
		{
			_CanClickBtn = true;
			F_设置技能图片();
		}
	}

	public void F_Btn_点击按钮_is左箭头(bool is左箭头)
	{
		if (_CanClickBtn && base.gameObject.activeSelf)
		{
			_CanClickBtn = false;
			if (is左箭头)
			{
				_Img_1.sprite = list_所有已解锁技能[F_得到范围内循环的索引(_在List中索引 + 2, list_所有已解锁技能.Count)]._技能图标;
				_Ani_精魄选择.Play("精魄选择_1", 0, 0f);
				_在List中索引++;
			}
			else
			{
				_Img_1.sprite = list_所有已解锁技能[F_得到范围内循环的索引(_在List中索引 - 2, list_所有已解锁技能.Count)]._技能图标;
				_Ani_精魄选择.Play("精魄选择_2", 0, 0f);
				_在List中索引--;
			}
			_在List中索引 = F_得到范围内循环的索引(_在List中索引, list_所有已解锁技能.Count);
		}
	}

	private void F_设置技能图片()
	{
		_Img_2.sprite = list_所有已解锁技能[F_得到范围内循环的索引(_在List中索引 - 1, list_所有已解锁技能.Count)]._技能图标;
		_Img_3.sprite = list_所有已解锁技能[F_得到范围内循环的索引(_在List中索引, list_所有已解锁技能.Count)]._技能图标;
		_Img_4.sprite = list_所有已解锁技能[F_得到范围内循环的索引(_在List中索引 + 1, list_所有已解锁技能.Count)]._技能图标;
	}

	private int F_得到范围内循环的索引(int currI, int count)
	{
		return (currI % count + count) % count;
	}
}
