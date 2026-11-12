using System.Collections.Generic;
using DSB;
using UnityEngine;

public class Enemy_区域触发式Boss生成器_毒敌大王 : Enemy_区域触发式Boss生成器
{
	public List<Enemy_罐子1> _List_Enemy_罐子1;

	public int _首次击破几个罐子激活Boss = 5;

	private int _当前被击破罐子数量;

	private bool _需要统计罐子击破数量;

	[Header("和宝箱ID一致,首次需要击破五个罐子用于激活毒敌大王")]
	public int _宝箱ID = 42150;

	private void Awake()
	{
		_当前被击破罐子数量 = 0;
		foreach (Enemy_罐子1 item in _List_Enemy_罐子1)
		{
			item._Event_死亡 += F_检测到罐子被击破;
		}
	}

	private void F_检测到罐子被击破()
	{
		if (!_需要统计罐子击破数量)
		{
			return;
		}
		_当前被击破罐子数量++;
		if (_当前被击破罐子数量 < _首次击破几个罐子激活Boss)
		{
			return;
		}
		DSB_MonoSingleton<MyDataManager>.Instance.F_打开宝箱_新(_宝箱ID);
		_需要统计罐子击破数量 = false;
		F_生成Boss();
		foreach (C_二进墙 item in _Boss生成器._List_二进墙)
		{
			item.gameObject.SetActive(value: true);
		}
		_Boss生成器.F_关闭二进墙();
	}

	public override void Init()
	{
		MyLog.LogWarning("Init>>Enemy_区域触发式Boss生成器");
		_Boss生成器.F_Init_数据();
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断宝箱已开启_新(_宝箱ID))
		{
			_需要统计罐子击破数量 = false;
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
			{
				MyLog.LogWarning($"<<{_BossName}>>已击败或不存在");
				_需要触发生成 = false;
				_Boss姿势.SetActive(value: false);
				_Boss生成器.Init();
			}
			else
			{
				MyLog.LogWarning($"<<{_BossName}>>未击败");
				_需要触发生成 = true;
				_Boss姿势.SetActive(value: true);
			}
			return;
		}
		_需要统计罐子击破数量 = true;
		_当前被击破罐子数量 = 0;
		_需要触发生成 = false;
		_Boss姿势.SetActive(value: true);
		foreach (C_二进墙 item in _Boss生成器._List_二进墙)
		{
			item.gameObject.SetActive(value: false);
		}
	}
}
