using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_猴子猴孙_死亡 : EnemyState_猴子猴孙
{
	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		base._Mono.StartCoroutine(Cor_死亡());
		MyLog.LogError(base._Mono.name + ">>死亡");
	}

	private IEnumerator Cor_死亡()
	{
		base._Ani.Play("死亡前跳跃");
		base._Rb2D.velocity = 30f * new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? num : (0f - num), num, num);
		yield return new WaitForSeconds(0.5f);
		base._Rb2D.bodyType = RigidbodyType2D.Kinematic;
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("倒地");
		yield return new WaitForSeconds(0.75f);
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
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._SR_影子.SetAlpha(0f);
		yield return t1;
		Object.Destroy(base._Mono.gameObject);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
