using System.Collections;
using UnityEngine;

public class EnemyState_青蝠_攻击 : EnemyState_狼剑客
{
	private EnemyAI_青蝠 _Mono青蝠;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono青蝠 = (EnemyAI_青蝠)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		_Cor_攻击 = _Data._Mono组件.StartCoroutine(F_攻击());
	}

	private IEnumerator F_攻击()
	{
		_ = Random.value;
		if (_Mono青蝠._飞_高)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_2());
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_1());
		}
		base._Ani.Play("飞_低");
		base._Mono.ChangeState(EnemyStateName_Type.站立);
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

	private IEnumerator Cor_攻击_1()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_1");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 20f;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.62f));
		base._Rb2D.velocity = _攻击方向 * 20f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.42f));
	}

	private IEnumerator Cor_攻击_2()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_2");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		for (int i = 0; i < 5; i++)
		{
			base._Rb2D.velocity = _攻击方向 * 15f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		_Mono青蝠.F_强制改变飞行状态(is高飞: false);
	}
}
