using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;
using UnityEngine.Events;

public class C_Boss生成器 : MonoBehaviour, IBoss生成器
{
	public Enum_Enemy _BossName;

	public bool _锁门 = true;

	public bool _更新二进墙边界无视是否锁门 = true;

	public GameObject _BossPrefab;

	public C_传送区域_筋斗云_Boss _传送区域_筋斗云_Boss;

	public C_宝箱 _C_宝箱_Boss;

	protected List<C_传送区域> _List_传送区域;

	public UnityEvent _Event_Boss已死亡;

	public UnityEvent _Event_Boss生成;

	public UnityEvent _Event_Boss死亡;

	public UnityEvent<Vector2> _Event_Boss死亡_带位置;

	private bool _未生成Boss;

	[Header("目前用于Boss未死亡,但又要更新边界的场景中使用")]
	public List<C_二进墙> _List_二进墙;

	private bool _已经关闭;

	private GameObject _BossGO;

	public void F_初始化二进墙()
	{
		if (_List_二进墙 == null || _List_二进墙.Count <= 0)
		{
			return;
		}
		foreach (C_二进墙 item in _List_二进墙)
		{
			item.F_接收_Boss生成器(this);
		}
	}

	public virtual void F_Init_数据()
	{
		_未生成Boss = false;
		_已经关闭 = false;
		F_初始化二进墙();
	}

	public virtual void Init()
	{
		F_Init_数据();
		if (_BossPrefab == null)
		{
			_未生成Boss = true;
			MyLog.LogError($"<<{_BossName}>>预制体不存在");
			return;
		}
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			MyLog.LogError($"<<{_BossName}>>已击败或不存在");
			_Event_Boss已死亡?.Invoke();
			_传送区域_筋斗云_Boss?.F_直接显示();
			_C_宝箱_Boss?.F_显示();
			_未生成Boss = true;
			return;
		}
		if (_BossGO != null)
		{
			UnityEngine.Object.Destroy(_BossGO);
		}
		_BossGO = UnityEngine.Object.Instantiate(_BossPrefab, base.transform.position, Quaternion.identity);
		IBoss component = _BossGO.GetComponent<IBoss>();
		if (base.transform.localScale.x < 0f)
		{
			Vector3 localScale = _BossGO.transform.localScale;
			localScale.x *= -1f;
			_BossGO.transform.localScale = localScale;
		}
		_Event_Boss生成?.Invoke();
		_传送区域_筋斗云_Boss?.F_隐藏();
		component.F_监听Boss死亡((Action)delegate
		{
			_Event_Boss死亡?.Invoke();
			_传送区域_筋斗云_Boss?.F_显示();
		});
		_C_宝箱_Boss?.F_隐藏();
		component.F_监听Boss死亡(delegate(Vector2 pos)
		{
			_Event_Boss死亡_带位置?.Invoke(pos);
			_C_宝箱_Boss?.F_空降显示(pos);
		});
		_List_传送区域 = DSB_MonoSingleton<MapManager>.Instance.F_得到当前地图()._List_传送区域;
		if (!_锁门 || _List_传送区域 == null || _List_传送区域.Count <= 0)
		{
			return;
		}
		foreach (C_传送区域 item in _List_传送区域)
		{
			item._已上锁 = true;
			item._已上锁_解锁 = false;
		}
		component.F_监听Boss死亡((Action)delegate
		{
			foreach (C_传送区域 item2 in _List_传送区域)
			{
				item2._已上锁_解锁 = true;
			}
		});
	}

	public void F_关闭二进墙()
	{
		MyLog.Log("进入方法>>F_关闭二进墙");
		MyLog.Log($"_未生成Boss>>{_未生成Boss}");
		MyLog.Log($"_锁门>>{_锁门}");
		MyLog.Log($"_已经关闭>>{_已经关闭}");
		if (_未生成Boss || (!_更新二进墙边界无视是否锁门 && !_锁门) || _已经关闭)
		{
			return;
		}
		_已经关闭 = true;
		if (_List_二进墙 == null || _List_二进墙.Count <= 0)
		{
			return;
		}
		foreach (C_二进墙 item in _List_二进墙)
		{
			item.F_关闭二进墙();
		}
	}

	private void OnDisable()
	{
		F_清空Boss();
	}

	public void F_清空Boss()
	{
		if (_BossGO != null)
		{
			UnityEngine.Object.Destroy(_BossGO);
			_BossGO = null;
		}
	}
}
