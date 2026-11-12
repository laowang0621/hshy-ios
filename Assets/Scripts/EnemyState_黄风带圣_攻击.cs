using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_黄风带圣_攻击 : EnemyState_Boss
{
	private EnemyAI_黄风带圣 _Mono_黄风带圣;

	private DataInfo_Enemy招式数值 转身扫击_1;

	private DataInfo_Enemy招式数值 转身扫击_2;

	private DataInfo_Enemy招式数值 下砸_1;

	private DataInfo_Enemy招式数值 下砸_2;

	private DataInfo_Enemy招式数值 回首掏_1;

	private DataInfo_Enemy招式数值 回首掏_2;

	private DataInfo_Enemy招式数值 前踏;

	private DataInfo_Enemy招式数值 召唤蝜蝂;

	private DataInfo_Enemy招式数值 双持三连;

	private DataInfo_Enemy招式数值 转身剑气;

	private DataInfo_Enemy招式数值 头槌;

	private DataInfo_Enemy招式数值 地刺;

	private DataInfo_Enemy招式数值 龙卷风;

	private DataInfo_Enemy招式数值 横扫二连;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private float _召唤蝜蝂CD = 10f;

	private bool _Can召唤蝜蝂 = true;

	private float _召唤龙卷风CD = 20f;

	private bool _Can召唤龙卷风 = true;

	private bool _已经首次召唤蝜蝂;

	private bool _已经首次召唤龙卷风;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_黄风带圣 = (EnemyAI_黄风带圣)data._Mono;
		转身扫击_1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6001);
		转身扫击_2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6002);
		下砸_1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6003);
		下砸_2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6004);
		回首掏_1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6005);
		回首掏_2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6006);
		前踏 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6007);
		召唤蝜蝂 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6008);
		双持三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6009);
		转身剑气 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6010);
		头槌 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6011);
		地刺 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6013);
		龙卷风 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6014);
		横扫二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6015);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		base._Mono组件._C_攻击判断._伤害 = 5f;
		_Cor_攻击 = _Data._Mono组件.StartCoroutine(F_攻击());
	}

	public override void OnExit()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private void F_发射弓箭(C_弹道 prefab)
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		float num = Mathf.Abs(_攻击方向.x);
		float num2 = Mathf.Abs(_攻击方向.y);
		Vector2 dir = ((num2 < 0.33f * num) ? _攻击方向 : new Vector2(_攻击方向.x, _攻击方向.y / num2 * 0.33f * num).normalized);
		Object.Instantiate(prefab, base._Mono.transform.position, Quaternion.identity).F_Init(dir, base._Mono组件._C_攻击判断._伤害);
	}

	private void F_生成地刺(float 延时, int count)
	{
		Object.Instantiate(_Mono_黄风带圣._地刺生成器, base._Mono.transform.position, Quaternion.identity).F_Init(延时, _攻击方向, count, _Data._Atk * 地刺._伤害系数);
	}

	public void F_召唤蝜蝂CD()
	{
		_Can召唤蝜蝂 = true;
	}

	public void F_召唤龙卷风CD()
	{
		_Can召唤龙卷风 = true;
	}

	private IEnumerator F_攻击()
	{
		base._Rb2D.velocity = Vector2.zero;
		int num;
		if (_Mono_黄风带圣._三阶段已触发)
		{
			if (!_已经首次召唤龙卷风)
			{
				_已经首次召唤龙卷风 = true;
				num = 16;
			}
			else
			{
				num = Random.Range(1, 17);
			}
		}
		else if (_Mono_黄风带圣._二阶段已触发)
		{
			if (!_已经首次召唤蝜蝂)
			{
				_已经首次召唤蝜蝂 = true;
				num = 7;
			}
			else
			{
				num = Random.Range(1, 15);
			}
		}
		else
		{
			num = Random.Range(1, 7);
		}
		switch (num)
		{
		case 1:
			yield return _Data._Mono组件.StartCoroutine(Cor_前踏());
			break;
		case 2:
			yield return _Data._Mono组件.StartCoroutine(Cor_横扫二连());
			yield return _Data._Mono组件.StartCoroutine(Cor_转身剑气());
			break;
		case 3:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_头槌());
			break;
		case 4:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_横扫二连());
			break;
		case 5:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_横扫二连());
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			break;
		case 6:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_横扫二连());
			yield return _Data._Mono组件.StartCoroutine(Cor_前踏());
			break;
		case 7:
			MyLog.LogError($"召唤蝜蝂>>{_Can召唤蝜蝂}");
			if (_Can召唤蝜蝂)
			{
				_Can召唤蝜蝂 = false;
				DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(_Data._Mono组件.gameObject, _召唤蝜蝂CD, F_召唤蝜蝂CD);
				yield return _Data._Mono组件.StartCoroutine(Cor_召唤蝜蝂());
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_1());
				yield return _Data._Mono组件.StartCoroutine(Cor_下砸_2());
			}
			break;
		case 8:
			yield return _Data._Mono组件.StartCoroutine(Cor_双持三连());
			break;
		case 9:
			yield return _Data._Mono组件.StartCoroutine(Cor_下砸_1());
			break;
		case 10:
			yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_1());
			break;
		case 11:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_横扫二连());
			yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_1());
			break;
		case 12:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_横扫二连());
			yield return _Data._Mono组件.StartCoroutine(Cor_下砸_2());
			break;
		case 13:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_下砸_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_前踏());
			break;
		case 14:
			yield return _Data._Mono组件.StartCoroutine(Cor_转身扫击_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_2());
			break;
		case 15:
			yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_下砸_2());
			break;
		case 16:
			if (_Can召唤龙卷风)
			{
				_Can召唤龙卷风 = false;
				DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(_Data._Mono组件.gameObject, _召唤龙卷风CD, F_召唤龙卷风CD);
				yield return _Data._Mono组件.StartCoroutine(Cor_召唤龙卷风());
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_1());
				yield return _Data._Mono组件.StartCoroutine(Cor_下砸_2());
			}
			break;
		default:
			yield return _Data._Mono组件.StartCoroutine(Cor_回首掏_1());
			break;
		}
		yield return null;
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_下砸_1()
	{
		base._Ani.Play("下砸_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下砸_1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下砸_1._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.24f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 10f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_生成地刺(0f, 3);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.36f));
	}

	private IEnumerator Cor_下砸_2()
	{
		base._Ani.Play("下砸_2", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下砸_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下砸_2._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 10f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_生成地刺(0f, 3);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
	}

	private IEnumerator Cor_前踏()
	{
		base._Ani.Play("前踏", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 前踏._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 前踏._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_生成地刺(0f, 3);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.43f));
	}

	private IEnumerator Cor_回首掏_1()
	{
		base._Ani.Play("回首掏_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 回首掏_1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 回首掏_1._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_生成地刺(0f, 3);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_回首掏_2()
	{
		base._Ani.Play("回首掏_2", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 回首掏_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 回首掏_2._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_生成地刺(0f, 3);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.36f));
	}

	private IEnumerator Cor_转身扫击_1()
	{
		base._Ani.Play("转身扫击_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 转身扫击_1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 转身扫击_1._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
	}

	private IEnumerator Cor_转身扫击_2()
	{
		base._Ani.Play("转身扫击_2", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 转身扫击_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 转身扫击_2._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.37f));
	}

	private IEnumerator Cor_双持三连()
	{
		base._Ani.Play("双持三连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 双持三连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 双持三连._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}

	private IEnumerator Cor_头槌()
	{
		base._Ani.Play("头槌", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 头槌._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 头槌._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.63f));
	}

	private IEnumerator Cor_横扫二连()
	{
		base._Ani.Play("横扫二连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横扫二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横扫二连._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.47f));
	}

	private IEnumerator Cor_转身剑气()
	{
		base._Ani.Play("转身剑气", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 转身剑气._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 转身剑气._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		F_发射弓箭(_Data._Mono组件._C_弹道_剑气Prefab);
		base._Rb2D.velocity = _攻击方向 * -75f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
	}

	private IEnumerator Cor_召唤蝜蝂()
	{
		base._Ani.Play("召唤蝜蝂", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 召唤蝜蝂._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 召唤蝜蝂._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		Object.Instantiate(_Mono_黄风带圣._C_黄风带圣_召唤蝜蝂, _目标位置, Quaternion.identity).F_Init(_攻击方向, base._Mono组件._C_攻击判断._伤害);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.45f));
	}

	private IEnumerator Cor_召唤龙卷风()
	{
		base._Ani.Play("召唤蝜蝂", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龙卷风._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龙卷风._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		for (int i = 0; i < 3; i++)
		{
			Object.Instantiate(_Mono_黄风带圣._EnemyAI_黄风带圣_龙卷风, _Mono_黄风带圣.transform.position, Quaternion.identity).F_Init(base._Mono组件._C_攻击判断._伤害);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.45f));
	}
}
