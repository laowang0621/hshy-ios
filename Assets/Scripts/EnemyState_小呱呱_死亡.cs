using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_小呱呱_死亡 : EnemyState_小呱呱
{
	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		_Data.F_设置UI血量(1f, 1f, _可见性: false);
		base._Mono.StartCoroutine(Cor_死亡());
		base._Mono组件._C_攻击判断._伤害 = _Data._Atk * 5f;
	}

	private IEnumerator Cor_死亡()
	{
		base._Ani.Play("受击");
		base._Rb2D.velocity = 3f * new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? num : (0f - num), num, num);
		yield return new WaitForSeconds(0.5f);
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Mono组件._Rb2D.velocity = Vector2.zero;
		base._Mono组件._Ani.Play("死亡");
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
		base._Mono.gameObject.SetActive(value: false);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
