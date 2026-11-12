using DSB;
using UnityEngine;

public class Enemy_蜘蛛二姐_生成器 : MonoBehaviour, IBoss生成器
{
	[Header("主要用于进入场景中调用Init初始化,并不使用其他逻辑(不直接生成Boss)")]
	public int _666 = 666;

	public C_Boss生成器 _Boss生成器;

	public Enum_Enemy _BossName;

	public Enum_对话 _Enum_对话类型;

	private bool _需要触发生成;

	private bool _已触发对话;

	public GameObject _人形二姐;

	public Animator _Ani_人形二姐;

	public GameObject _蜘蛛二姐;

	public GameObject _GO_烟雾爆炸特效;

	public void Init()
	{
		MyLog.LogWarning("Init>>Enemy_蜘蛛二姐_生成器");
		_Boss生成器.F_初始化二进墙();
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			MyLog.LogWarning($"<<{_BossName}>>已击败或不存在");
			_需要触发生成 = false;
			_已触发对话 = false;
			_人形二姐.SetActive(value: false);
			_蜘蛛二姐.SetActive(value: false);
			_Boss生成器.Init();
			return;
		}
		MyLog.LogWarning($"<<{_BossName}>>未击败");
		_需要触发生成 = true;
		_已触发对话 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Enum_对话类型);
		if (_已触发对话)
		{
			_人形二姐.SetActive(value: false);
			_蜘蛛二姐.SetActive(value: true);
		}
		else
		{
			_Ani_人形二姐.Play("站立", 0, 0f);
			_人形二姐.SetActive(value: true);
			_蜘蛛二姐.SetActive(value: false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_需要触发生成 || collision.gameObject.layer != 3)
		{
			return;
		}
		_需要触发生成 = false;
		if (_已触发对话)
		{
			F_生成Boss();
			return;
		}
		DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_Enum_对话类型, delegate
		{
			_Ani_人形二姐.Play("施法", 0, 0f);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.7114514f, F_生成Boss);
		});
	}

	private void F_生成Boss()
	{
		_人形二姐.SetActive(value: false);
		_蜘蛛二姐.SetActive(value: false);
		if (!_已触发对话)
		{
			Object.Instantiate(_GO_烟雾爆炸特效, base.transform.position + new Vector3(0f, 1f), Quaternion.identity);
		}
		_Boss生成器.Init();
	}
}
