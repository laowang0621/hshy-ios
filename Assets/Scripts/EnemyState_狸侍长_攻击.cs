using System.Collections;
using UnityEngine;

public class EnemyState_狸侍长_攻击 : EnemyState_狼剑客
{
	private EnemyAI_狸侍长 _Mono_狸侍长;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono_狸侍长 = (EnemyAI_狸侍长)base._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		float value = Random.value;
		if (value < 0.4f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_二连击());
		}
		else if (value < 0.8f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_突刺());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_三连击());
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
		_目标位置 = _Mono_狸侍长._目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? num : (0f - num), num, num);
	}

	public override void OnUpdate()
	{
	}

	protected virtual IEnumerator Cor_突刺()
	{
		F_更新攻击方向();
		base._Ani.Play("突刺");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.325f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Rb2D.velocity = _攻击方向 * 50f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.333f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Rb2D.drag = _Data._StartDrag;
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected virtual IEnumerator Cor_二连击()
	{
		F_更新攻击方向();
		base._Ani.Play("二连击");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.11f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.31f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.31f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected virtual IEnumerator Cor_三连击()
	{
		F_更新攻击方向();
		base._Ani.Play("三连击");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.4f));
		F_更新攻击方向();
		base._Mono组件.transform.position = _目标位置;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		if (Random.value < 0.6f)
		{
			base._Ani.Play("三连击_3");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
