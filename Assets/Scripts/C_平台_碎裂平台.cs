using DG.Tweening;
using UnityEngine;

public class C_平台_碎裂平台 : C_平台
{
	public SpriteRenderer _SR;

	public Collider2D _Co;

	public float _触发后几秒碎裂 = 1.5f;

	private bool _已经碎裂;

	public void F_碎裂()
	{
		_已经碎裂 = true;
		Invoke("F_隐藏平台", _触发后几秒碎裂);
	}

	private void F_隐藏平台()
	{
		_Co.enabled = false;
		_SR.DOFade(0f, 0.5f).OnComplete(delegate
		{
		});
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!_已经碎裂 && collision.gameObject.CompareTag("Player"))
		{
			F_碎裂();
		}
	}
}
