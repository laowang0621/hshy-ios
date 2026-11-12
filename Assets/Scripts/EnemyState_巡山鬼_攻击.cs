using System.Collections;
using UnityEngine;

public class EnemyState_巡山鬼_攻击 : EnemyState_狼剑客
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
		float value = Random.value;
		if (Mathf.Abs(((Vector2)base._Mono.transform.position).y - _目标位置.y) < 2f && value < 0.35f)
		{
			_Cor_攻击 = _Data._Mono组件.StartCoroutine(Cor_回旋镖());
		}
		else if (_Data._攻击范围有敌人 && value < 0.7f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_跳劈());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_跳跃突进3连());
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

	private IEnumerator Cor_跳劈()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_1");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.71f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_跳跃突进3连()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		F_更新攻击方向();
		base._Ani.Play("攻击_2");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.72f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_回旋镖()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_3");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Data._Dict_Anis_name_time["攻击_3"]));
		base._Mono.ChangeState(EnemyStateName_Type.追击);
	}
}
