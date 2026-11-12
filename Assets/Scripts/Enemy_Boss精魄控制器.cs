using DSB;
using UnityEngine;

public class Enemy_Boss精魄控制器 : MonoBehaviour, IEnemy
{
	public Enum_Enemy _BossName;

	[Header("这个好像没有用,都是要的")]
	public bool _需要同步位置;

	public GameObject _Model;

	private EnemyMono组件 _Mono组件;

	private void Awake()
	{
		_Mono组件 = GetComponent<EnemyMono组件>();
		_Model.SetActive(value: false);
	}

	private void Start()
	{
	}

	public void F_隐藏()
	{
		_Mono组件._Event_Evemy隐藏();
	}

	public void F_显示()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			_Mono组件._Event_Evemy显示(F_重置);
		}
		else
		{
			_Mono组件._Event_Evemy隐藏();
		}
	}

	public void F_空降显示(Vector2 pos)
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			base.transform.position = pos;
			_Mono组件._Event_Evemy死亡();
		}
	}

	public void F_重置()
	{
	}
}
