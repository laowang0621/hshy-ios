using System.Collections;
using DSB;
using UnityEngine;

public class C_地刺生成器 : MonoBehaviour
{
	public C_攻击判断 _地刺1;

	public C_攻击判断 _地刺2;

	public void F_Init(float 延时, Vector2 dir, int count, float 伤害)
	{
		StartCoroutine(Cor_Init(延时, dir, count, 伤害));
	}

	private IEnumerator Cor_Init(float 延时, Vector2 dir, int count, float 伤害)
	{
		if (延时 > 0.01f)
		{
			yield return new WaitForSeconds(延时);
		}
		else
		{
			yield return null;
		}
		for (int i = 0; i <= count; i++)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
			Vector2 vector = (Vector2)base.transform.position + (2f + (float)i * 1f) * dir;
			C_攻击判断 c_攻击判断 = Object.Instantiate(_地刺1, vector, Quaternion.identity);
			c_攻击判断._伤害 = 伤害;
			Object.Destroy(c_攻击判断.gameObject, 0.33f);
			yield return new WaitForSeconds(0.175f);
		}
		Object.Destroy(base.gameObject, 0.1f);
	}
}
