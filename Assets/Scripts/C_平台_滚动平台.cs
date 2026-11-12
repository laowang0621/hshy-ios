using UnityEngine;

public class C_平台_滚动平台 : C_平台
{
	public float _速度 = 10f;

	private bool _玩家进入;

	private Rigidbody2D _PCRb;

	private void FixedUpdate()
	{
		if (_玩家进入)
		{
			_PCRb.AddForce(new Vector2(_速度, 0f) * _PCRb.mass, ForceMode2D.Impulse);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent<PlayerController>(out var component))
		{
			_玩家进入 = true;
			_PCRb = component._Data._Mono组件._Rb2D;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent<PlayerController>(out var _))
		{
			_玩家进入 = false;
		}
	}
}
