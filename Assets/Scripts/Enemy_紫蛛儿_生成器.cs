using DSB;
using UnityEngine;

public class Enemy_紫蛛儿_生成器 : MonoBehaviour, IBoss生成器
{
	[Header("主要用于进入场景中调用Init初始化,并不使用其他逻辑(不直接生成Boss)")]
	public int _666 = 666;

	public C_Boss生成器 _Boss生成器;

	public Enum_Enemy _BossName;

	public Enum_对话 _Enum_对话类型;

	private bool _已触发对话;

	public GameObject _人形;

	public Animator _Ani_人形;

	public GameObject _蜘蛛;

	public GameObject _GO_烟雾爆炸特效;

	public C_传送区域_筋斗云_Boss _传送区域_筋斗云_Boss;

	public void Init()
	{
		MyLog.LogWarning("Init>>Enemy_蜘蛛二姐_生成器");
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			MyLog.LogWarning($"<<{_BossName}>>已击败或不存在");
			_已触发对话 = true;
			_人形.SetActive(value: false);
			_蜘蛛.SetActive(value: false);
			_Boss生成器.Init();
			return;
		}
		MyLog.LogWarning($"<<{_BossName}>>未击败");
		_传送区域_筋斗云_Boss?.F_隐藏();
		_已触发对话 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Enum_对话类型);
		if (_已触发对话)
		{
			_人形.SetActive(value: false);
			_蜘蛛.SetActive(value: false);
			_Boss生成器.transform.localScale = new Vector3(-1f, 1f, 1f);
			_Boss生成器.Init();
		}
		else
		{
			_Ani_人形.Play("站立", 0, 0f);
			_人形.SetActive(value: false);
			_蜘蛛.SetActive(value: false);
			_Boss生成器.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != 3 || _已触发对话)
		{
			return;
		}
		_已触发对话 = true;
		_人形.SetActive(value: true);
		_Ani_人形.Play("从天而降", 0, 0f);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.9f, delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_Enum_对话类型, delegate
			{
				_Ani_人形.Play("施法", 0, 0f);
				DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.7114514f, delegate
				{
					_人形.SetActive(value: false);
					_蜘蛛.SetActive(value: false);
					Object.Instantiate(_GO_烟雾爆炸特效, base.transform.position + new Vector3(0f, 1f), Quaternion.identity);
					_Boss生成器.Init();
				});
			});
		});
	}
}
