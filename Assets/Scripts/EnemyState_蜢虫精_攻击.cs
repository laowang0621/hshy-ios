using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_蜢虫精_攻击 : EnemyState_狼剑客
{
	private EnemyAI_蜢虫精 _Mono蜢虫精;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono蜢虫精 = (EnemyAI_蜢虫精)data._Mono;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		_ = (Vector2)base._Mono.transform.position;
		_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_攻击());
	}

	private IEnumerator Cor_攻击()
	{
		if (_Mono蜢虫精._下次攻击_破茧攻击)
		{
			_Mono蜢虫精._下次攻击_破茧攻击 = false;
			yield return _Data._Mono组件.StartCoroutine(Cor_破茧二连_0());
			yield return _Data._Mono组件.StartCoroutine(Cor_破茧二连_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_破茧二连_2());
			if (Random.value < 0.5f)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_手里剑());
			}
		}
		else if (Random.value < 0.5f)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_二连_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_二连_2());
			if (Random.value < 0.5f)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_手里剑());
			}
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_突刺());
			yield return _Data._Mono组件.StartCoroutine(Cor_跳砸());
			if (Random.value < 0.7f)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_手里剑());
			}
		}
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

	private IEnumerator Cor_二连_1()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_二连");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 8f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 16f;
	}

	private IEnumerator Cor_二连_2()
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 21f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.29f));
	}

	private IEnumerator Cor_破茧二连_0()
	{
		base._Ani.Play("攻击_破茧二连");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
	}

	private IEnumerator Cor_破茧二连_1()
	{
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 8f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 16f;
	}

	private IEnumerator Cor_破茧二连_2()
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 21f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.29f));
	}

	private IEnumerator Cor_突刺()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_突刺");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_跳砸()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_跳砸");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.12f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_手里剑()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_手里剑");
		base._Rb2D.velocity = -_攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = -_攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_发射弓箭();
		base._Rb2D.velocity = -_攻击方向 * 9f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
	}

	private void F_发射弓箭()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		Object.Instantiate(base._Mono组件._弓箭Prefab, base._Mono.transform.position, Quaternion.identity).F_Init((_Data._Pos_追击目标位置 - base._Mono.transform.position).normalized, base._Mono._Data._Atk);
	}
}
