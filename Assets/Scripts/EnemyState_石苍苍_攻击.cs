using System.Collections;
using UnityEngine;

public class EnemyState_石苍苍_攻击 : EnemyState_狼剑客
{
	private EnemyAI_石苍苍 _Mono_石苍苍;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono_石苍苍 = (EnemyAI_石苍苍)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		if (_Mono_石苍苍._解除伪装后直接攻击)
		{
			_Mono_石苍苍._解除伪装后直接攻击 = false;
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_跳跃磕头());
		}
		else if (_Data._攻击范围有敌人)
		{
			if (Random.value < 0.65f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_磕头());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_开车());
			}
		}
		else if (Random.value < 0.5f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_跳跃磕头());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_开车());
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

	protected virtual IEnumerator Cor_开车()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		F_更新攻击方向();
		base._Ani.Play("开车");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		int 氮气Count = Random.Range(5, 7);
		for (int i = 0; i < 氮气Count; i++)
		{
			base._Rb2D.velocity = _攻击方向 * 20f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.13f));
		}
		base._Mono.ChangeState(EnemyStateName_Type.追击);
	}

	protected virtual IEnumerator Cor_磕头()
	{
		F_更新攻击方向();
		base._Ani.Play("磕头");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected virtual IEnumerator Cor_跳跃磕头()
	{
		F_更新攻击方向();
		base._Ani.Play("跳跃磕头");
		for (int i = 0; i < 3; i++)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.17f));
			base._Rb2D.velocity = _攻击方向 * 23f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.22f));
		base._Mono.ChangeState(EnemyStateName_Type.追击);
	}
}
