using System.Collections;
using UnityEngine;

public class EnemyState_御剑道士_攻击 : EnemyState_狼剑客
{
	private EnemyAI_御剑道士 _Mono_御剑道士;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono_御剑道士 = data._Mono as EnemyAI_御剑道士;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		_Mono_御剑道士._开启音效 = true;
		base._Rb2D.velocity = Vector2.zero;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		_ = (Vector2)base._Mono.transform.position;
		if (Random.value < 0.66f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_踢砍刺());
		}
		else if (Random.value < 0.5f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_砍刺());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_踢砍());
		}
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

	private IEnumerator Cor_踢砍刺()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_踢砍刺");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.51f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 21f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_砍刺()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_砍刺");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_踢砍()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_踢砍");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.51f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
