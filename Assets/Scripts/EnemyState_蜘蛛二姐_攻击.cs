using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyState_蜘蛛二姐_攻击 : EnemyState_Boss
{
	protected EnemyAI_蜘蛛二姐 _Mono_蜘蛛二姐;

	protected DataInfo_Enemy招式数值 毒液其他参数;

	protected DataInfo_Enemy招式数值 毒液其他参数2;

	protected DataInfo_Enemy招式数值 三连击;

	protected DataInfo_Enemy招式数值 前刺;

	protected DataInfo_Enemy招式数值 下砸;

	protected DataInfo_Enemy招式数值 吐毒;

	protected DataInfo_Enemy招式数值 跳砸;

	protected DataInfo_Enemy招式数值 六连击;

	protected DataInfo_Enemy招式数值 大风车;

	protected Vector2 _目标位置;

	protected Vector2 _攻击方向;

	protected float _EnterDrag;

	protected int _攻击序号 = 10;

	protected List<int> _出招序号表 = new List<int>
	{
		10, 11, 12, 20, 21, 30, 31, 40, 41, 50,
		51
	};

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_蜘蛛二姐 = (EnemyAI_蜘蛛二姐)data._Mono;
		三连击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13001);
		前刺 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13002);
		下砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13003);
		吐毒 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13004);
		毒液其他参数 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13008);
		毒液其他参数2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13009);
		跳砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13005);
		六连击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13006);
		大风车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13007);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Data._Mono组件.StopAllCoroutines();
		base._Mono组件.StartCoroutine(Cor_攻击());
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		_Mono_蜘蛛二姐._GO_蜘蛛旋转特效.SetActive(value: false);
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	protected virtual IEnumerator Cor_攻击()
	{
		_攻击序号 = _出招序号表.GetRandom();
		if (_Mono_蜘蛛二姐._开启固定出招)
		{
			_攻击序号 = _Mono_蜘蛛二姐._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 10:
			yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_3());
			if (Random.value < 0.6f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_前刺_1());
				yield return base._Mono组件.StartCoroutine(Cor_前刺_2());
			}
			break;
		case 11:
			yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
			yield return base._Mono组件.StartCoroutine(Cor_大风车());
			break;
		case 12:
			yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			if (Random.value < 0.4f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
				yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
				yield return base._Mono组件.StartCoroutine(Cor_三连击_3());
			}
			break;
		case 20:
			yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			if (Random.value < 0.6f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
				yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			}
			yield return base._Mono组件.StartCoroutine(Cor_前刺_1());
			yield return base._Mono组件.StartCoroutine(Cor_前刺_2());
			break;
		case 21:
			yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
			if (Random.value < 0.4f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_三连击_3());
				break;
			}
			yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			break;
		case 30:
			yield return base._Mono组件.StartCoroutine(Cor_跳砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_跳砸_2());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
			if (Random.value < 0.5f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_三连击_3());
				break;
			}
			yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			break;
		case 31:
			yield return base._Mono组件.StartCoroutine(Cor_跳砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_跳砸_2());
			yield return base._Mono组件.StartCoroutine(Cor_前刺_1());
			yield return base._Mono组件.StartCoroutine(Cor_前刺_2());
			if (Random.value < 0.5f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_跳砸_1());
				yield return base._Mono组件.StartCoroutine(Cor_跳砸_2());
			}
			break;
		case 40:
			yield return base._Mono组件.StartCoroutine(Cor_吐毒_1());
			yield return base._Mono组件.StartCoroutine(Cor_吐毒_2());
			break;
		case 41:
			yield return base._Mono组件.StartCoroutine(Cor_吐毒_1());
			yield return base._Mono组件.StartCoroutine(Cor_吐毒_2());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_1());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_2());
			yield return base._Mono组件.StartCoroutine(Cor_三连击_3());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_1());
			yield return base._Mono组件.StartCoroutine(Cor_下砸_2());
			break;
		case 50:
			yield return base._Mono组件.StartCoroutine(Cor_六连击_1_2());
			yield return base._Mono组件.StartCoroutine(Cor_六连击_3_4());
			yield return base._Mono组件.StartCoroutine(Cor_六连击_3_4());
			break;
		case 51:
			yield return base._Mono组件.StartCoroutine(Cor_六连击_1_2());
			yield return base._Mono组件.StartCoroutine(Cor_六连击_3_4());
			yield return base._Mono组件.StartCoroutine(Cor_六连击_3_4());
			yield return base._Mono组件.StartCoroutine(Cor_大风车());
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	protected void F_生成一个毒液()
	{
		C_蛇司药毒瓶 c_蛇司药毒瓶 = Object.Instantiate(_Mono_蜘蛛二姐._蛇司药毒瓶, _Mono_蜘蛛二姐._Tsf_手.position, Quaternion.identity);
		Vector2 vector = Random.insideUnitCircle * 4f;
		vector.y *= 0.5f;
		c_蛇司药毒瓶.F_Init(_目标位置 + vector, base._Mono._Data._Atk * 吐毒._伤害系数, 吐毒._击退力度, Random.Range(毒液其他参数._伤害系数, 毒液其他参数._击退力度), 2);
	}

	protected IEnumerator Cor_三连击_1()
	{
		base._Ani.Play("攻击_三连击", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连击._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	protected IEnumerator Cor_三连击_2()
	{
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	protected IEnumerator Cor_三连击_3()
	{
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
	}

	protected IEnumerator Cor_前刺_1()
	{
		base._Ani.Play("攻击_前刺", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 前刺._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 前刺._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	protected IEnumerator Cor_前刺_2()
	{
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.42f));
	}

	protected IEnumerator Cor_下砸_1()
	{
		base._Ani.Play("攻击_下砸", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	protected IEnumerator Cor_下砸_2()
	{
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.42f));
	}

	protected IEnumerator Cor_吐毒_1()
	{
		base._Ani.Play("攻击_吐毒", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 吐毒._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 吐毒._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.625f));
	}

	protected IEnumerator Cor_吐毒_2()
	{
		int count = 1 + Mathf.CeilToInt(Random.Range(毒液其他参数2._伤害系数, 毒液其他参数2._击退力度));
		for (int i = 0; i < count; i++)
		{
			F_生成一个毒液();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	protected IEnumerator Cor_跳砸_1()
	{
		base._Ani.Play("攻击_跳砸_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跳砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		int conut = Random.Range(3, 6);
		for (int i = 0; i < conut; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 25f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
	}

	protected IEnumerator Cor_跳砸_2()
	{
		base._Ani.Play("攻击_跳砸_2");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		Object.Instantiate(_Mono_蜘蛛二姐._GO_落地扬尘_Prefab, base._Mono.transform.position, Quaternion.identity);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.175f));
	}

	protected IEnumerator Cor_六连击_1_2()
	{
		base._Ani.Play("攻击_六连击", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 六连击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 六连击._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
	}

	protected IEnumerator Cor_六连击_3_4()
	{
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
	}

	protected IEnumerator Cor_大风车()
	{
		base._Ani.Play("攻击_大风车", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 大风车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 大风车._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		_Mono_蜘蛛二姐._GO_蜘蛛旋转特效.SetActive(value: true);
		for (int i = 0; i < 3; i++)
		{
			base._Rb2D.velocity = _攻击方向 * (24 - i * 2);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}
}
