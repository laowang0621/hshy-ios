using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_雪僵尸_死亡 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		_Data.F_设置UI血量(1f, 1f, _可见性: false);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		if (base._Mono组件._Cor_死亡 != null)
		{
			base._Mono组件.StopCoroutine(base._Mono组件._Cor_死亡);
		}
		base._Mono组件._Cor_死亡 = base._Mono组件.StartCoroutine(Cor_死亡(base._Mono._BeAttack_Pos));
	}

	protected IEnumerator Cor_死亡(Vector2 beAttack_Pos)
	{
		base._Ani.Play("受击");
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小妖死亡();
		base._Rb2D.velocity = 9f * new Vector2(base._Mono组件.transform.position.x - beAttack_Pos.x, 0f).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono组件.transform.localScale = new Vector3((beAttack_Pos.x > base._Mono组件.transform.position.x) ? num : (0f - num), num, num);
		yield return new WaitForSeconds(0.3f);
		EnemyState_雪僵尸_攻击 雪僵尸攻击状态 = (EnemyState_雪僵尸_攻击)base._Mono._FSM.states[EnemyStateName_Type.攻击];
		yield return _Data._Mono组件.StartCoroutine(雪僵尸攻击状态.Cor_攻击_1());
		yield return _Data._Mono组件.StartCoroutine(雪僵尸攻击状态.Cor_攻击_2());
		yield return new WaitForSeconds(0.2f);
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Rb2D.velocity = Vector2.zero;
		yield return new WaitForSeconds(0.8f);
		WaitForSeconds t1 = new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++)
		{
			base._Mono组件._SR_Model.SetAlpha(0f);
			base._Mono组件._SR_影子.SetAlpha(0f);
			yield return t1;
			base._Mono组件._SR_Model.SetAlpha(1f);
			base._Mono组件._SR_影子.SetAlpha(1f);
			yield return t1;
		}
		base._Mono组件._Cor_死亡 = null;
		yield return null;
		base._Mono组件.gameObject.SetActive(value: false);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
