using System.Collections;
using UnityEngine;

public class EnemyState_骨耸然_攻击 : EnemyState_狼剑客
{
	private Enemy_StateData_鼠校尉 _Data_骨悚然;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Data_骨悚然 = data as Enemy_StateData_鼠校尉;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		if (_Data_骨悚然._有盾 && _Data_骨悚然._Curr生命_盾 > 0f && Random.value < 0.6f)
		{
			if (Random.value < 0.5f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_盾击());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_开车());
			}
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_普通攻击());
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

	private IEnumerator Cor_盾击()
	{
		F_更新攻击方向();
		base._Ani.Play("盾击");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Data._Dict_Anis_name_time["盾击"]));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_开车()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		F_更新攻击方向();
		base._Ani.Play("开车");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.48f));
		for (int i = 0; i < 5; i++)
		{
			base._Rb2D.velocity = _攻击方向 * 25f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.13f));
		}
		base._Mono.ChangeState(EnemyStateName_Type.追击);
	}

	private IEnumerator Cor_普通攻击()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		if (Random.value < 0.33f)
		{
			F_更新攻击方向();
			base._Ani.Play("攻击_1");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
			base._Rb2D.velocity = _攻击方向 * 25f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
