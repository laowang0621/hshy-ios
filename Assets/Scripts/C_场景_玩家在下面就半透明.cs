using DG.Tweening;
using DSB;
using UnityEngine;

public class C_场景_玩家在下面就半透明 : MonoBehaviour
{
	public SpriteRenderer _SR;

	private void OnEnable()
	{
		_SR.SetAlpha(1f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			_SR.DOFade(0.6f, 0.25f);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			_SR.DOFade(1f, 0.25f);
		}
	}
}
