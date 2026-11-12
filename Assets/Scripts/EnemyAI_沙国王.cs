using System.Collections.Generic;
using System.Linq;
using DSB;
using UnityEngine;

public class EnemyAI_沙国王 : EnemyAI_Boss
{
	public List<Enemy_不济谷柱子> _List_不济谷柱子;

	public Enemy_不济谷柱子 _当前柱子;

	public GameObject _GO_烟雾特效Prefab;

	public C_弹道_石头 _石头Prefab;

	private bool _m在地面;

	public bool _在地面
	{
		get
		{
			return _m在地面;
		}
		set
		{
			_m在地面 = value;
			MyLog.LogError($"_Data={_Data}");
			MyLog.LogError($"_Data._Mono组件={_Data._Mono组件}");
			MyLog.LogError($"_Data._Mono组件._Co2D_自身碰撞器={_Data._Mono组件._Co2D_自身碰撞器}");
			_Data._Mono组件._Co2D_自身碰撞器.enabled = value;
			_Data._Mono组件._SR_Model.transform.localPosition = new Vector3(0f, 0f, _m在地面 ? 0f : (-4.5f));
		}
	}

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_沙国王_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_沙国王_上柱子().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_沙国王_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_沙国王_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_沙国王_受击().Init(_Data));
		_List_不济谷柱子 = DSB_MonoSingleton<MapManager>.Instance.F_得到当前地图().transform.Find("EnemyList").GetComponentsInChildren<Enemy_不济谷柱子>().ToList();
		MyLog.LogError($"_List_不济谷柱子>>{_List_不济谷柱子.Count}");
		_在地面 = true;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_沙二郎死亡._EventID, F_沙二郎死亡);
	}

	public void F_注销_沙二郎死亡()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_沙二郎死亡._EventID, F_沙二郎死亡);
	}

	public void F_柱子碎裂(Enemy_不济谷柱子 不济谷柱子)
	{
		_在地面 = false;
		base.transform.position = 不济谷柱子.transform.position;
		ChangeState(EnemyStateName_Type.受击);
	}

	public void F_沙二郎死亡(DSB_EventArgs args)
	{
		F_注销_沙二郎死亡();
		Object.Instantiate(_GO_烟雾特效Prefab, base.transform.position + new Vector3(0f, 0.85f), Quaternion.identity);
		Object.Destroy(base.gameObject);
	}

	protected override void OnDestroy()
	{
		if (_当前柱子 != null)
		{
			_当前柱子._On_碎裂 -= F_柱子碎裂;
		}
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_沙二郎死亡._EventID, F_沙二郎死亡);
		base.OnDestroy();
	}
}
