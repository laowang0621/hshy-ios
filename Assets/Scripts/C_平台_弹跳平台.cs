using UnityEngine;

public class C_平台_弹跳平台 : C_平台
{
	public Animator _Ani_弹跳平台;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent<PlayerController>(out var component))
		{
			component.F_外部施加跳跃高度(2f);
			_Ani_弹跳平台.Play("弹跳平台_弹");
		}
	}
}
