using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_猴子猴孙_聚形散气 : EnemyAI_猴子猴孙
{
	public override Enemy_StateData_猴子猴孙 _Data => null;

	public override void BeAttack(AttackInfo attackInfo)
	{
	}

	public void F_销毁()
	{
		EnemyMono组件 component = GetComponent<EnemyMono组件>();
		component._Co2D_自身碰撞器.enabled = false;
		component._SR_Model.DOFade(0f, 0.5f);
		component._SR_影子.DOFade(0f, 0.51f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	private new void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
	}

	private void F_On_Event_切换地图(DSB_EventArgs args)
	{
		Object.Destroy(base.gameObject);
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.LogError(base.name + ">>已经初始化！");
			return;
		}
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		MyLog.LogError("初始化<<" + base.name + ">>结束");
	}

	protected override void Update()
	{
	}

	protected override void LateUpdate()
	{
	}
}
