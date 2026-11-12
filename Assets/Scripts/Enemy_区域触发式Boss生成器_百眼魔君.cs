using System.Collections;
using DSB;
using UnityEngine;

public class Enemy_区域触发式Boss生成器_百眼魔君 : Enemy_区域触发式Boss生成器
{
	[Header("主要用于进入场景中调用Init初始化,并不使用其他逻辑(不直接生成Boss)")]
	public int _666 = 666;

	public Enum_对话 _Enum_对话类型_战前;

	public Enum_对话 _Enum_对话类型_战后;

	private bool _已触发对话;

	public GameObject _GO_人形_百眼魔君;

	public Animator _Ani_人形_百眼魔君;

	public GameObject _GO_人形_紫蛛儿;

	public Animator _Ani_人形_紫蛛儿;

	public GameObject _GO_烟雾爆炸特效;

	public C_传送区域_筋斗云_Boss _传送区域_筋斗云_Boss;

	public override void Init()
	{
		MyLog.LogWarning("Init>>Enemy_区域触发式Boss生成器_百眼魔君");
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			MyLog.LogWarning($"<<{_BossName}>>已击败或不存在");
			_需要触发生成 = false;
			_已触发对话 = false;
			_Boss姿势.SetActive(value: false);
			_GO_人形_百眼魔君.SetActive(value: false);
			_GO_人形_紫蛛儿.SetActive(value: false);
			_Boss生成器.Init();
			return;
		}
		_需要触发生成 = true;
		MyLog.LogWarning($"<<{_BossName}>>未击败");
		_传送区域_筋斗云_Boss?.F_隐藏();
		_已触发对话 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Enum_对话类型_战前);
		if (_已触发对话)
		{
			_GO_人形_百眼魔君.SetActive(value: false);
			_GO_人形_紫蛛儿.SetActive(value: false);
			_Boss姿势.SetActive(value: true);
		}
		else
		{
			_Ani_人形_百眼魔君.Play("站立", 0, 0f);
			_Ani_人形_紫蛛儿.Play("站立", 0, 0f);
			_GO_人形_百眼魔君.SetActive(value: true);
			_GO_人形_紫蛛儿.SetActive(value: true);
			_Boss姿势.SetActive(value: false);
		}
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
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
		_已触发对话 = true;
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.6f, delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_Enum_对话类型_战前, delegate
			{
				_Ani_人形_百眼魔君.Play("施法", 0, 0f);
				_Ani_人形_紫蛛儿.Play("施法", 0, 0f);
				DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.7114514f, delegate
				{
					_GO_人形_百眼魔君.SetActive(value: false);
					_GO_人形_紫蛛儿.SetActive(value: false);
					Object.Instantiate(_GO_烟雾爆炸特效, _GO_人形_百眼魔君.transform.position + new Vector3(0f, 1f), Quaternion.identity);
					Object.Instantiate(_GO_烟雾爆炸特效, _GO_人形_紫蛛儿.transform.position + new Vector3(0f, 1f), Quaternion.identity);
					_Boss生成器.Init();
				});
			});
		});
	}

	public void F_Boss_死亡(Vector2 pos_死亡)
	{
		DSB_MonoSingleton<GameManager>.Instance._PC.ChangeToState(Player_StateName_Type.正常移动);
		GameManager._CanPlay = false;
		StartCoroutine(Cor_Boss死亡(pos_死亡));
	}

	private IEnumerator Cor_Boss死亡(Vector2 pos_死亡)
	{
		_GO_人形_百眼魔君.transform.position = pos_死亡;
		if (pos_死亡.x < base.transform.position.x)
		{
			_GO_人形_百眼魔君.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			_GO_人形_百眼魔君.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		yield return new WaitForSeconds(1.75f);
		_Boss生成器.F_清空Boss();
		_GO_人形_百眼魔君.SetActive(value: true);
		_GO_人形_紫蛛儿.SetActive(value: true);
		_Boss姿势.SetActive(value: false);
		Object.Instantiate(_GO_烟雾爆炸特效, _GO_人形_百眼魔君.transform.position + new Vector3(0f, 1f), Quaternion.identity);
		Object.Instantiate(_GO_烟雾爆炸特效, _GO_人形_紫蛛儿.transform.position + new Vector3(0f, 1f), Quaternion.identity);
		DSB_MonoSingleton<GameManager>.Instance._PC._Data._Mono组件._Ani.Play("站立");
		yield return new WaitForSeconds(0.75f);
		DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_Enum_对话类型_战后);
		yield return new WaitForSeconds(0.75f);
		_GO_人形_百眼魔君.SetActive(value: false);
		_GO_人形_紫蛛儿.SetActive(value: false);
		Object.Instantiate(_GO_烟雾爆炸特效, _GO_人形_百眼魔君.transform.position + new Vector3(0f, 1f), Quaternion.identity);
		Object.Instantiate(_GO_烟雾爆炸特效, _GO_人形_紫蛛儿.transform.position + new Vector3(0f, 1f), Quaternion.identity);
		yield return new WaitForSeconds(1f);
		_传送区域_筋斗云_Boss?.F_显示();
	}
}
