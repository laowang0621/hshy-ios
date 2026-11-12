using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_广智_攻击 : EnemyState_Boss
{
	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private DataInfo_Enemy招式数值 民工三连;

	private DataInfo_Enemy招式数值 投掷回旋镖;

	private DataInfo_Enemy招式数值 突刺二连;

	private DataInfo_Enemy招式数值 大风车;

	private DataInfo_Enemy招式数值 撑杆跳;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		民工三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(2001);
		投掷回旋镖 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(2002);
		突刺二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(2003);
		大风车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(2004);
		撑杆跳 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(2005);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		F_攻击();
	}

	public override void OnExit()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
		base._Rb2D.drag = _Data._StartDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private void F_攻击()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		Vector2 a = base._Mono.transform.position;
		_目标位置 = _Data._Pos_追击目标位置;
		if (Vector2.Distance(a, _目标位置) < 5f)
		{
			if (Random.value < 0.7f)
			{
				_Cor_攻击 = _Data._Mono组件.StartCoroutine(Cor_民工三连());
			}
			else
			{
				_Cor_攻击 = _Data._Mono组件.StartCoroutine(Cor_撑杆四连());
			}
		}
		else if (Mathf.Abs(a.y - _目标位置.y) < 2f && Random.value < 0.666f)
		{
			_Cor_攻击 = _Data._Mono组件.StartCoroutine(Cor_丢烧火棍());
		}
		else if (Random.value < 0.5f)
		{
			_Cor_攻击 = _Data._Mono组件.StartCoroutine(Cor_突刺二连());
		}
		else
		{
			_Cor_攻击 = _Data._Mono组件.StartCoroutine(Cor_大风车());
		}
	}

	private protected void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_民工三连()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 民工三连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 民工三连._击退力度;
		base._Ani.Play("横扫");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		base._Ani.Play("上挑");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Ani.Play("下劈");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_撑杆四连()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 撑杆跳._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 撑杆跳._击退力度;
		base._Ani.Play("Boss_撑杆跳_起手");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Ani.Play("Boss_撑杆跳");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Ani.Play("Boss_撑杆跳", 0, 0f);
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Ani.Play("Boss_撑杆跳", 0, 0f);
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		Object.Instantiate(base._Mono组件._拍地板特效Prefab, base._Mono组件.transform.position + new Vector3((float)((base._Mono组件.transform.localScale.x > 0f) ? 1 : (-1)) * 4.25f, 0f, 0f), Quaternion.identity).transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_突刺二连()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 突刺二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 突刺二连._击退力度;
		base._Ani.Play("突刺_0");
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Ani.Play("突刺_1");
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 50f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.275f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Ani.Play("突刺_2");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.drag = _Data._StartDrag;
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("突刺_0");
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		base._Ani.Play("突刺_1");
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 50f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.325f));
		base._Rb2D.drag = _Data._StartDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Ani.Play("突刺_2");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_丢烧火棍()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 投掷回旋镖._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 投掷回旋镖._击退力度;
		base._Ani.Play("投掷武器_0");
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		F_更新攻击方向();
		base._Ani.Play("投掷武器_1");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.6f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_大风车()
	{
		base._Ani.Play("Boss_大风车");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 大风车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 大风车._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		base._Rb2D.velocity = _攻击方向 * 22.5f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.54f));
		base._Rb2D.drag = _Data._StartDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.134f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
