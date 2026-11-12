using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class C_场景_玩家在下面就_多个半透明 : C_场景_玩家在下面就半透明
{
	[Header("不需要父类的_SR")]
	[Header("多个SpriteRenderer列表")]
	public List<SpriteRenderer> _List_SR;

	private void OnEnable()
	{
		foreach (SpriteRenderer item in _List_SR)
		{
			item.SetAlpha(1f);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!(other.tag == "Player"))
		{
			return;
		}
		foreach (SpriteRenderer item in _List_SR)
		{
			item.DOFade(0.6f, 0.25f);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (!(other.tag == "Player"))
		{
			return;
		}
		foreach (SpriteRenderer item in _List_SR)
		{
			item.DOFade(1f, 0.25f);
		}
	}
}
