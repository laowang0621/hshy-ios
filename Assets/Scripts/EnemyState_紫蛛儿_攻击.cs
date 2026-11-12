using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyState_紫蛛儿_攻击 : EnemyState_蜘蛛二姐_攻击
{
	protected DataInfo_Enemy招式数值 转身尾击;

	protected DataInfo_Enemy招式数值 埋头倒立毒爆;

	protected DataInfo_Enemy招式数值 致盲;

	protected List<int> _出招序号表_紫蛛儿 = new List<int> { 101, 102, 103, 104 };

	private bool _转阶段_2 = true;

	private float _血量百分比_2 = 0.3f;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		转身尾击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13101);
		埋头倒立毒爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13102);
		致盲 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13103);
		吐毒 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13100);
		毒液其他参数 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13104);
		毒液其他参数2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13105);
		return base.Init(data);
	}

	protected override IEnumerator Cor_攻击()
	{
		if (Random.value < 0.6f)
		{
			_攻击序号 = _出招序号表.GetRandom();
		}
		else
		{
			_攻击序号 = _出招序号表_紫蛛儿.GetRandom();
		}
		if (_转阶段_2 && _Data._Curr生命 < _Data._MaxHP * _血量百分比_2)
		{
			_转阶段_2 = false;
			_攻击序号 = 105;
		}
		if (_Mono_蜘蛛二姐._开启固定出招)
		{
			_攻击序号 = _Mono_蜘蛛二姐._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 1:
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
		case 101:
			yield return base._Mono组件.StartCoroutine(Cor_转身尾击());
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			break;
		case 102:
			yield return base._Mono组件.StartCoroutine(Cor_转身尾击());
			yield return base._Mono组件.StartCoroutine(Cor_转身尾击_吐丝());
			break;
		case 103:
			yield return base._Mono组件.StartCoroutine(Cor_埋头倒立毒爆());
			break;
		case 104:
			yield return base._Mono组件.StartCoroutine(Cor_六连击_1_2());
			yield return base._Mono组件.StartCoroutine(Cor_六连击_3_4());
			yield return base._Mono组件.StartCoroutine(Cor_六连击_3_4());
			yield return base._Mono组件.StartCoroutine(Cor_大风车());
			break;
		case 105:
			yield return base._Mono组件.StartCoroutine(Cor_致盲());
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_转身尾击()
	{
		base._Ani.Play("攻击_转身尾击", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 转身尾击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 转身尾击._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
	}

	protected IEnumerator Cor_转身尾击_吐丝()
	{
		base._Ani.Play("攻击_转身尾击_吐丝", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 转身尾击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 转身尾击._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		int count = 1 + Mathf.CeilToInt(Random.Range(毒液其他参数2._伤害系数, 毒液其他参数2._击退力度));
		for (int i = 0; i < count; i++)
		{
			F_生成一个毒液();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.22f));
	}

	protected IEnumerator Cor_埋头倒立毒爆()
	{
		base._Ani.Play("攻击_埋头倒立毒爆", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 埋头倒立毒爆._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 埋头倒立毒爆._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		for (int i = 0; i < 4; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * (24 - 3 * i);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		Object.Instantiate(_Mono_蜘蛛二姐._GO_落地扬尘_Prefab, base._Mono.transform.position, Quaternion.identity);
		for (int i = 0; i < 8; i++)
		{
			Vector2 vector = Random.insideUnitCircle * 5f;
			vector.y *= 0.33f;
			_目标位置 = _Data._Pos_追击目标位置;
			F_生成一个毒液(_目标位置 + vector);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.01f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	protected void F_生成一个毒液(Vector2 target_Pos)
	{
		Object.Instantiate(_Mono_蜘蛛二姐._蛇司药毒瓶, _Mono_蜘蛛二姐._Tsf_手.position, Quaternion.identity).F_Init(target_Pos);
	}

	protected IEnumerator Cor_致盲()
	{
		base._Ani.Play("攻击_致盲", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 致盲._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 致盲._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(3f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.44f));
	}
}
