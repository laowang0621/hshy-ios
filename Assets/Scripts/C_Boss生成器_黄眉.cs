using System;
using DSB;
using UnityEngine;

public class C_Boss生成器_黄眉 : C_Boss生成器
{
	[Header("黄眉相关")]
	public Enum_Enemy _BossName_1;

	public Enum_Enemy _BossName_2;

	public Enum_Enemy _BossName_3;

	public GameObject _BossPrefab_1;

	public GameObject _BossPrefab_2;

	public GameObject _BossPrefab_3;

	private GameObject _BossGO_1;

	private GameObject _BossGO_2;

	private GameObject _BossGO_3;

	public ParticleSystem _PS_血雾;

	public override void Init()
	{
		_PS_血雾.Stop();
		if (_BossPrefab_1 == null || _BossPrefab_2 == null || _BossPrefab_3 == null)
		{
			MyLog.LogError($"<<{_BossName}>>预制体不存在");
			return;
		}
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName_3))
		{
			MyLog.LogError($"<<{_BossName}>>已击败或不存在");
			_Event_Boss已死亡?.Invoke();
			_传送区域_筋斗云_Boss?.F_直接显示();
			_C_宝箱_Boss?.F_显示();
			return;
		}
		_List_传送区域 = DSB_MonoSingleton<MapManager>.Instance.F_得到当前地图()._List_传送区域;
		if (_锁门 && _List_传送区域 != null && _List_传送区域.Count > 0)
		{
			foreach (C_传送区域 item in _List_传送区域)
			{
				item._已上锁 = true;
				item._已上锁_解锁 = false;
			}
		}
		F_清空所有Boss实体();
		F_生成第1个Boss();
	}

	private void F_生成第1个Boss()
	{
		_BossGO_1 = UnityEngine.Object.Instantiate(_BossPrefab_1, base.transform.position, Quaternion.identity);
		IBoss component = _BossGO_1.GetComponent<IBoss>();
		component._开启Boss死亡UI = false;
		component.F_特殊调用1();
		_Event_Boss生成?.Invoke();
		_传送区域_筋斗云_Boss?.F_隐藏();
		_C_宝箱_Boss?.F_隐藏();
		component.F_监听Boss死亡((Action)delegate
		{
			_PS_血雾.Play();
		});
		component.F_监听Boss死亡_消失后(delegate(Vector2 pos)
		{
			F_生成第2个Boss(pos);
		});
	}

	private void F_生成第2个Boss(Vector2 pos)
	{
		_BossGO_2 = UnityEngine.Object.Instantiate(_BossPrefab_2, pos, Quaternion.identity);
		IBoss component = _BossGO_2.GetComponent<IBoss>();
		component._开启Boss死亡UI = false;
		component.F_监听Boss死亡_消失后((Action)delegate
		{
			F_生成第3个Boss();
		});
	}

	private void F_生成第3个Boss()
	{
		_PS_血雾.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
		_BossGO_3 = UnityEngine.Object.Instantiate(_BossPrefab_3, base.transform.position, Quaternion.identity);
		IBoss component = _BossGO_3.GetComponent<IBoss>();
		component._开启Boss死亡UI = true;
		component.F_监听Boss死亡((Action)delegate
		{
			_Event_Boss死亡?.Invoke();
			_传送区域_筋斗云_Boss?.F_显示();
			if (_List_传送区域 != null)
			{
				foreach (C_传送区域 item in _List_传送区域)
				{
					item._已上锁_解锁 = true;
				}
			}
		});
		component.F_监听Boss死亡(delegate(Vector2 pos)
		{
			_Event_Boss死亡_带位置?.Invoke(pos);
			_C_宝箱_Boss?.F_空降显示(pos);
		});
	}

	private void OnDisable()
	{
		F_清空所有Boss实体();
	}

	private void F_清空所有Boss实体()
	{
		if (_BossGO_1 != null)
		{
			UnityEngine.Object.Destroy(_BossGO_1);
			_BossGO_1 = null;
		}
		if (_BossGO_2 != null)
		{
			UnityEngine.Object.Destroy(_BossGO_2);
			_BossGO_2 = null;
		}
		if (_BossGO_3 != null)
		{
			UnityEngine.Object.Destroy(_BossGO_3);
			_BossGO_3 = null;
		}
	}
}
