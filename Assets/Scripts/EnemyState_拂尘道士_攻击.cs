using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_拂尘道士_攻击 : EnemyState_狼剑客
{
	private EnemyAI_拂尘道士 _Mono拂尘道士;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono拂尘道士 = (EnemyAI_拂尘道士)data._Mono;
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
		float value = Random.value;
		_ = (Vector2)base._Mono.transform.position;
		if (value < 0.35f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_扫戳());
		}
		else if (value < 0.7f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_戳());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_气功());
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

	private void F_发射弓箭()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		float num = Mathf.Abs(_攻击方向.x);
		float num2 = Mathf.Abs(_攻击方向.y);
		Vector2 dir = ((num2 < 0.33f * num) ? _攻击方向 : new Vector2(_攻击方向.x, _攻击方向.y / num2 * 0.33f * num).normalized);
		Object.Instantiate(base._Mono组件._弓箭Prefab, base._Mono.transform.position, Quaternion.identity).F_Init(dir, base._Mono组件._C_攻击判断._伤害);
	}

	private IEnumerator Cor_扫戳()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_扫");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Ani.Play("攻击_戳");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(((_攻击方向.x > 0f) ? 1 : (-1)) * 21, 0f);
		F_发射弓箭();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_戳()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_戳");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(((_攻击方向.x > 0f) ? 1 : (-1)) * 21, 0f);
		F_发射弓箭();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_气功()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_气功");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.83f));
		F_更新攻击方向();
		base._Rb2D.velocity = -_攻击方向 * 15f;
		F_发射弓箭();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.67f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
