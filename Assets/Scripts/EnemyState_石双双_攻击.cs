using System;
using System.Collections;
using UnityEngine;

public class EnemyState_石双双_攻击 : EnemyState_狼剑客
{
	private EnemyAI_石双双 _Mono_石双双;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono_石双双 = (EnemyAI_石双双)data._Mono;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		if (_Mono_石双双._喷射中)
		{
			_Mono_石双双._喷射中 = false;
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_喷射());
		}
		else if (UnityEngine.Random.value < 0.6f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_左正蹬());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_磕头());
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

	protected virtual IEnumerator Cor_左正蹬()
	{
		F_更新攻击方向();
		base._Ani.Play("左正蹬");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected virtual IEnumerator Cor_磕头()
	{
		F_更新攻击方向();
		base._Ani.Play("磕头");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.22f));
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected virtual IEnumerator Cor_喷射()
	{
		F_更新攻击方向();
		base._Ani.Play("喷射");
		int count = UnityEngine.Random.Range(9, 15);
		for (int i = 0; i < count; i++)
		{
			C_弹道_弓箭 c_弹道_弓箭 = UnityEngine.Object.Instantiate(_Mono_石双双._喷射Prefab, base._Mono组件.transform.position, Quaternion.identity);
			float f = (float)UnityEngine.Random.Range(0, 360) * (MathF.PI / 180f);
			Vector2 dir = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
			c_弹道_弓箭.F_Init(dir, base._Mono组件._C_攻击判断._伤害);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.11f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
