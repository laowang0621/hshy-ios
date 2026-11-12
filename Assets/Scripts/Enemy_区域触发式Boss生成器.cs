using DSB;
using UnityEngine;

public class Enemy_区域触发式Boss生成器 : MonoBehaviour, IBoss生成器
{
	[Header("主要用于进入场景中调用Init初始化,并不使用其他逻辑(不直接生成Boss)")]
	public C_Boss生成器 _Boss生成器;

	public Enum_Enemy _BossName;

	protected bool _需要触发生成;

	public GameObject _Boss姿势;

	public virtual void Init()
	{
		MyLog.LogWarning("Init>>Enemy_区域触发式Boss生成器");
		_Boss生成器.F_Init_数据();
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
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (_需要触发生成 && collision.gameObject.layer == 3)
		{
			_需要触发生成 = false;
			F_生成Boss();
		}
	}

	protected virtual void F_生成Boss()
	{
		_Boss姿势.SetActive(value: false);
		_Boss生成器.Init();
	}
}
