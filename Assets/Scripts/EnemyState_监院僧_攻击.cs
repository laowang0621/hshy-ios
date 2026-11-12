using System.Collections;
using UnityEngine;

public class EnemyState_监院僧_攻击 : EnemyState_狼剑客
{
	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_攻击());
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

	private protected void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? num : (0f - num), num, num);
	}

	public override void OnUpdate()
	{
	}

	private IEnumerator Cor_攻击()
	{
		float value = Random.value;
		_ = (Vector2)base._Mono.transform.position;
		if (value < 0.3f)
		{
			yield return base._Mono组件.StartCoroutine(Cor_一拳());
		}
		else if (value < 0.5f)
		{
			yield return base._Mono组件.StartCoroutine(Cor_一拳());
			yield return base._Mono组件.StartCoroutine(Cor_二拳());
			yield return base._Mono组件.StartCoroutine(Cor_一拳());
			yield return base._Mono组件.StartCoroutine(Cor_二拳());
		}
		else if (value < 0.5f)
		{
			yield return base._Mono组件.StartCoroutine(Cor_一拳());
			yield return base._Mono组件.StartCoroutine(Cor_二拳());
		}
		else if (value < 0.5f)
		{
			yield return base._Mono组件.StartCoroutine(Cor_一拳());
			yield return base._Mono组件.StartCoroutine(Cor_二拳());
			yield return base._Mono组件.StartCoroutine(Cor_一拳_快());
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_一拳()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_1");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2((_攻击方向.x > 0f) ? 1 : (-1), 0f) * 20f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}

	private IEnumerator Cor_一拳_快()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_1_快");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2((_攻击方向.x > 0f) ? 1 : (-1), 0f) * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.9f));
	}

	private IEnumerator Cor_二拳()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_2");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
	}
}
