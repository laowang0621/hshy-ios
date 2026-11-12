using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_年兽_攻击 : EnemyState_Boss
{
	private EnemyAI_年兽 _Mono_年兽;

	private DataInfo_Enemy招式数值 咆哮;

	private DataInfo_Enemy招式数值 踩踏;

	private DataInfo_Enemy招式数值 飞扑;

	private DataInfo_Enemy招式数值 泥头车;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_年兽 = (EnemyAI_年兽)data._Mono;
		咆哮 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(8001);
		踩踏 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(8002);
		飞扑 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(8003);
		泥头车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(8004);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		F_攻击();
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

	private void F_攻击()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		Vector2 a = base._Mono.transform.position;
		_目标位置 = _Data._Pos_追击目标位置;
		if (Vector2.Distance(a, _目标位置) > 8f)
		{
			if (Random.value < 0.5f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_泥头车());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_飞扑());
			}
		}
		else if (Random.value < 0.5f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_咆哮());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_踩踏());
		}
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_咆哮()
	{
		base._Ani.Play("咆哮");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 咆哮._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 咆哮._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(2f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_踩踏()
	{
		base._Ani.Play("踩踏");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 踩踏._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 踩踏._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_泥头车()
	{
		base._Ani.Play("龙车");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 泥头车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 泥头车._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		for (int i = 0; i < 8; i++)
		{
			base._Rb2D.velocity = _攻击方向 * 30f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_飞扑()
	{
		base._Ani.Play("飞扑");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 飞扑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 飞扑._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.9f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
