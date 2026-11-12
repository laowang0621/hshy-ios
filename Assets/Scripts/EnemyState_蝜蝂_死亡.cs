using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_蝜蝂_死亡 : EnemyState_Boss_死亡
{
	protected override IEnumerator Cor_死亡()
	{
		base._Rb2D.drag = 5f;
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Mono.F_删除血条();
		base._Ani.Play("受击");
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		yield return new WaitForSeconds(0.5f);
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("倒地");
		yield return new WaitForSeconds(2f);
		_Mono_Boss.F_Action_Boss死亡_消失后();
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
		yield return null;
		Object.Destroy(base._Mono.gameObject);
	}
}
